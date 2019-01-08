using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TribalSvcPortal.AppLogic.DataAccessLayer;
using TribalSvcPortal.Data.Models;

namespace TribalSvcPortal.Services
{
    /// <summary>
    /// This class sends out emails.
    /// </summary>
    public class EmailSender : IEmailSender
    {
        private readonly IDbPortal _DbPortal;
        private readonly Ilog _log;

        public EmailSender(IDbPortal DbPortal, Ilog log)
        {
            _DbPortal = DbPortal;
            _log = log;
        }


        public bool SendEmail(string from, string to, List<string> cc, List<string> bcc, byte[] attach, string attachFileName, string emailTemplateName, List<emailParam> emailParams)
        {
            try
            {
                //************* GET SMTP SERVER SETTINGS ****************************
                string mailServer = _DbPortal.GetT_PRT_APP_SETTING("EMAIL_SERVER");
                string Port = _DbPortal.GetT_PRT_APP_SETTING("EMAIL_PORT");
                string smtpUser = _DbPortal.GetT_PRT_APP_SETTING("EMAIL_SECURE_USER");
                string smtpUserPwd = _DbPortal.GetT_PRT_APP_SETTING("EMAIL_SECURE_PWD");

                //*************SET MESSAGE SENDER IF NOT SUPPLIED*********************                   
                if (from == null)
                    from = _DbPortal.GetT_PRT_APP_SETTING("EMAIL_FROM");

                //************GET EMAIL CONTENT FROM TEMPLATE******************************
                T_PRT_REF_EMAIL_TEMPLATE _temp = _DbPortal.GetT_PRT_REF_EMAIL_TEMPLATE_ByName(emailTemplateName);
                if (_temp != null)
                {
                    string subj = _temp.SUBJ;
                    string body = _temp.MSG;

                    foreach (emailParam _item in emailParams)
                        body = body.Replace("{"+ _item.PARAM_NAME + "}", _item.PARAM_VAL);


                    //************** REROUTE TO SENDGRID HELPER IF SENDGRID ENABLED ******
                    bool SuccID = false;
                    if (mailServer == "smtp.sendgrid.net")
                        SuccID = SendEmailUsingSendGrid(from, to, cc, bcc, subj, body, smtpUserPwd).GetAwaiter().GetResult();
                    else
                        SuccID = SendEmailUsingSMTP(from, to, cc, bcc, attach, attachFileName, mailServer, Port, smtpUser, smtpUserPwd, subj, body);


                    //*************** LOG EMAIL SENT ****************************************
                    _log.InsertT_PRT_SYS_EMAIL_LOG(from, to, null, subj, null, emailTemplateName);  //TODO record success/failure

                    return SuccID;
                }
                else
                    return false;

            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    _log.LogEFException(ex);

                return false;
            }

        }

        /// <summary>
        /// Shorthand version of SendEmail method to use in cases where only 1 variable is passed to the email body.
        /// </summary>
        public bool SendEmail(string from, string to, List<string> cc, List<string> bcc, byte[] attach, string attachFileName, string emailTemplateName, string param1Name, string param1Val)
        {
            List<emailParam> emailParams = new List<emailParam>()
            {
                new emailParam()
                {
                    PARAM_NAME = param1Name,
                    PARAM_VAL = param1Val
                }
            };

            return SendEmail(from, to, cc, bcc, attach, attachFileName, emailTemplateName, emailParams);
        }


        private static bool SendEmailUsingSMTP(string from, string to, List<string> cc, List<string> bcc, byte[] attach, string attachFileName, string mailServer, string Port, string smtpUser, string smtpUserPwd, string subj, string body)
        {
            //************** IF NOT SENDGRID, SEND USING LOCAL SMTP SERVER ******
            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
            message.Subject = subj;
            message.Body = body;
            message.IsBodyHtml = true;
            message.From = new System.Net.Mail.MailAddress(from);
            message.To.Add(to);
            if (cc != null)
            {
                foreach (string cc1 in cc)
                    message.CC.Add(cc1);
            }
            if (bcc != null)
            {
                foreach (string bcc1 in bcc)
                    message.Bcc.Add(bcc1);
            }


            //*************ATTACHMENT START**************************
            if (attach != null)
            {
                System.Net.Mail.Attachment att = new System.Net.Mail.Attachment(new MemoryStream(attach), attachFileName);
                message.Attachments.Add(att);
            }
            //*************ATTACHMENT END****************************


            //***************SET SMTP SERVER *************************
            if (smtpUser.Length > 0)  //smtp server requires authentication
            {
                var smtp = new System.Net.Mail.SmtpClient(mailServer, Convert.ToInt32(Port))
                {
                    Credentials = new System.Net.NetworkCredential(smtpUser, smtpUserPwd),
                    EnableSsl = true
                };
                smtp.Send(message);

            }
            else
            {
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(mailServer);
                smtp.Send(message);
            }

            return true;
        }


        /// <summary>
        /// Sends out an email using SendGrid. 
        /// Note: Updated to work with SendGrid version 9.8
        /// </summary>
        /// <returns>true if successful</returns>
        public async Task<bool> SendEmailUsingSendGrid(string from, string to, List<string> cc, List<string> bcc, string subj, string body, string apiKey)
        {
            try
            {
                var client = new SendGridClient(apiKey);

                //******************** CONSTRUCT EMAIL ********************************************               
                var msg = new SendGridMessage()
                {
                    From = new EmailAddress(from),
                    Subject = subj
                };

                msg.AddContent(MimeType.Html, body);
                msg.AddTo(new EmailAddress(to));

                foreach (string cc1 in cc ?? Enumerable.Empty<string>())
                    msg.AddCc(cc1);

                foreach (string bcc1 in bcc ?? Enumerable.Empty<string>())
                    msg.AddBcc(bcc1);


                // Disable click tracking. See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
                msg.TrackingSettings = new TrackingSettings
                {
                    ClickTracking = new ClickTracking { Enable = false }
                };

                //******************** SEND EMAIL ****************************************************
                var response = await client.SendEmailAsync(msg).ConfigureAwait(false);


                //******************** RETURN RESPONSE ***********************************************
                if (response.StatusCode == HttpStatusCode.Accepted)
                    return true;
                else
                    return false;
                //************************************************************************************

            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    _log.LogEFException(ex);

                return false;
            }
        }

    }
}
