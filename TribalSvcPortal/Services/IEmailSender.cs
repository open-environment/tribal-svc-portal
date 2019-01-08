using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TribalSvcPortal.Services
{
    public class emailParam
    {
        public string PARAM_NAME { get; set; }
        public string PARAM_VAL { get; set; }
    }

    public interface IEmailSender
    {
        bool SendEmail(string from, string to, List<string> cc, List<string> bcc, byte[] attach, string attachFileName, string emailTemplateName, List<emailParam> emailParams);
        bool SendEmail(string from, string to, List<string> cc, List<string> bcc, byte[] attach, string attachFileName, string emailTemplateName, string param1Name, string param1Val);
    }
}
