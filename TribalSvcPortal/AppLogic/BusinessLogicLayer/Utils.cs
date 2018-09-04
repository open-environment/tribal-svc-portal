using System;
using System.Collections.Generic;
using System.Linq;
using TribalSvcPortal.Data.Models;
using TribalSvcPortal.AppLogic.DataAccessLayer;
using System.IO;
using System.Threading.Tasks;
using TribalSvcPortal.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace TribalSvcPortal.AppLogic.BusinessLogicLayer
{
    public static class Utils
    {       
       // private static readonly DbContextOptions<ApplicationDbContext> _contextOptions = new DbContextOptions<ApplicationDbContext>();
      //  private static IOptions<AuthMessageSenderOptions> _optionsAccessor = new IOptions<AuthMessageSenderOptions>();      
       // private static IEmailSender _emailSender = new EmailSender(_optionsAccessor);
     

        /// <summary>
        ///  Better than built-in SubString by handling cases where string is too short
        /// </summary>
        public static string SubStringPlus(this string str, int index, int length)
        {
            if (index >= str.Length)
                return String.Empty;

            if (index + length > str.Length)
                return str.Substring(index);

            return str.Substring(index, length);
        }
        #region EMAIL HELPERS

        /// <summary>
        /// Sends out an email from the application. Returns true if successful. Supports multiple CC, BCC
        /// </summary>
        /// <param name="from">Email address of sender. Leave null to use default.</param>
        /// <param name="to">Email address sending to</param>
        /// <param name="subj">Email subject</param>
        /// <param name="body">Email body</param>
        /// <param name="attach">Attachment as byte array</param>
        /// <param name="attachFileName">Attachment file name including extension e.g. test.doc</param>
        /// <returns></returns>
        //public static async Task<bool> SendEmail(string from, string to, List<string> cc, List<string> bcc, string subj, string body, byte[] attach, string attachFileName, string callbackUrl, string bodyHTML = null)
        //{
        //    try
        //    {
        //        //************* GET SMTP SERVER SETTINGS ****************************


        //        ApplicationDbContext _context = new ApplicationDbContext(_contextOptions);
        //        IDbPortal oDbPortal = new DbPortal(_context);

        //        string mailServer = oDbPortal.GetT_PRT_APP_SETTING("EMAIL_SERVER");
        //        string Port = oDbPortal.GetT_PRT_APP_SETTING("EMAIL_PORT");
        //        string smtpUser = oDbPortal.GetT_PRT_APP_SETTING("EMAIL_SECURE_USER");
        //        string smtpUserPwd = oDbPortal.GetT_PRT_APP_SETTING("EMAIL_SECURE_PWD");


        //        //*************SET MESSAGE SENDER *********************
        //        if (from == null)
        //            from = oDbPortal.GetT_PRT_APP_SETTING("EMAIL_FROM");

        //        //************** REROUTE TO SENDGRID HELPER IF SENDGRID ENABLED ******
        //        if (mailServer == "smtp.sendgrid.net")
        //        {
        //            //bool SendStatus = SendGridHelper.SendGridEmail(from, to, cc, bcc, subj, body, smtpUserPwd, bodyHTML).GetAwaiter().GetResult();
        //            //return SendStatus;     
                   

        //            await _emailSender.SendEmailConfirmationAsync(to, callbackUrl);
        //            return true;
        //        }

        //        System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
        //        message.From = new System.Net.Mail.MailAddress(from);
        //        message.To.Add(to);
        //        if (cc != null)
        //        {
        //            foreach (string cc1 in cc)
        //                message.CC.Add(cc1);
        //        }
        //        if (bcc != null)
        //        {
        //            foreach (string bcc1 in bcc)
        //                message.Bcc.Add(bcc1);
        //        }

        //        message.Subject = subj;
        //        message.Body = body;
        //        //*************ATTACHMENT START**************************
        //        if (attach != null)
        //        {
        //            System.Net.Mail.Attachment att = new System.Net.Mail.Attachment(new MemoryStream(attach), attachFileName);
        //            message.Attachments.Add(att);
        //        }
        //        //*************ATTACHMENT END****************************


        //        //***************SET SMTP SERVER *************************
        //        if (smtpUser.Length > 0)  //smtp server requires authentication
        //        {
        //            var smtp = new System.Net.Mail.SmtpClient(mailServer, Convert.ToInt32(Port))
        //            {
        //                Credentials = new System.Net.NetworkCredential(smtpUser, smtpUserPwd),
        //                EnableSsl = true
        //            };
        //            smtp.Send(message);
        //        }
        //        else
        //        {
        //            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(mailServer);
        //            smtp.Send(message);
        //        }

        //        return true;

        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex.InnerException != null)

        //            return false;
        //    }
        //    return false;
        //}

        #endregion

    }
}
