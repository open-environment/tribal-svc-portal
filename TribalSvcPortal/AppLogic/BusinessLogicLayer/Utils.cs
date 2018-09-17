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
        private static readonly DbContextOptions<ApplicationDbContext> _contextOptions = new DbContextOptions<ApplicationDbContext>();       
        private static ApplicationDbContext _context = new ApplicationDbContext(_contextOptions);

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
        public static bool SendEmail(string from, string to, List<string> cc, List<string> bcc, string subj, string body, byte[] attach, string attachFileName, string bodyHTML = null)
        {
            try
            {

                // IDbPortal _DbPortal = new DbPortal(_context);
                IDbPortal _DbPortal = new DbPortal();
                //************* GET SMTP SERVER SETTINGS ****************************
                string mailServer = _DbPortal.GetT_PRT_APP_SETTING("EMAIL_SERVER");
                string Port = _DbPortal.GetT_PRT_APP_SETTING("EMAIL_PORT");
                string smtpUser = _DbPortal.GetT_PRT_APP_SETTING("EMAIL_SECURE_USER");
                string smtpUserPwd = _DbPortal.GetT_PRT_APP_SETTING("EMAIL_SECURE_PWD");
              
                //*************SET MESSAGE SENDER *********************                   
                if (from == null)
                {
                    from = _DbPortal.GetT_PRT_APP_SETTING("EMAIL_FROM");
                }

                //************** REROUTE TO SENDGRID HELPER IF SENDGRID ENABLED ******
                if (mailServer == "smtp.sendgrid.net")
                {                  
                    bool SendStatus = SendGridHelper.SendGridEmail(from, to, cc, bcc, subj, body, smtpUserPwd, bodyHTML).GetAwaiter().GetResult();
                    return SendStatus;
                }
                else
                {

                    System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
                    message.From = new System.Net.Mail.MailAddress(from);
                    message.To.Add(to);
                    if (cc != null)
                    {
                        foreach (string cc1 in cc)
                        {
                            message.CC.Add(cc1);
                        }
                    }
                    if (bcc != null)
                    {
                        foreach (string bcc1 in bcc)
                        {
                            message.Bcc.Add(bcc1);
                        }
                    }

                    message.Subject = subj;
                    message.Body = body;
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
                    
                }
                return true;
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)               

                return false;
            }
            return true;
        }
      
    }
}
