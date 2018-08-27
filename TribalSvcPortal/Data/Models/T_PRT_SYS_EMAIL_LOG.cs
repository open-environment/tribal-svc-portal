using System;
using System.Collections.Generic;

namespace TribalSvcPortal.Data.Models
{
    public partial class T_PRT_SYS_EMAIL_LOG
    {
        public int EmailLogId { get; set; }
        public DateTime? LogDt { get; set; }
        public string LogFrom { get; set; }
        public string LogTo { get; set; }
        public string LogCc { get; set; }
        public string LogSubj { get; set; }
        public string LogMsg { get; set; }
        public string EmailType { get; set; }
    }
}
