using System;
using System.Collections.Generic;
using System.Linq;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using TribalSvcPortal.AppLogic.DataAccessLayer;
using System.Net;

namespace TribalSvcPortal.AppLogic.BusinessLogicLayer
{
    public class SendGridHelper
    {
        /// <summary>
        /// Sends out an email using SendGrid. 
        /// Note: Updated to work with SendGrid version 9.8
        /// </summary>
        /// <returns>true if successful</returns>
        public static async Task<bool> SendGridEmail(string from, string to, List<string> cc, List<string> bcc, string subj, string body, string apiKey, string bodyHTML = null)
        {
            try
            {
                var client = new SendGridClient(apiKey);

                //******************** CONSTRUCT EMAIL ********************************************               
                var msg = new SendGridMessage()
                {
                    From = new EmailAddress(from),
                    Subject = subj,
                    PlainTextContent = body,
                    HtmlContent = body
                };

                msg.AddTo(new EmailAddress(to));

                // Disable click tracking.
                // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
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
                 return false;
            }
            return true;
        }

    }
}
