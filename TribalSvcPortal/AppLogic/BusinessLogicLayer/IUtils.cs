using System.Collections.Generic;

namespace TribalSvcPortal.AppLogic.BusinessLogicLayer {
    public interface IUtils {
        bool SendEmail(string from, string to, List<string> cc, List<string> bcc, string subj, string body, byte[] attach, string attachFileName, string bodyHTML = null);
    }
}