using System;
using System.Collections.Generic;

namespace TribalSvcPortal.Data.Models
{
    public partial class T_PRT_SYS_EMAIL_LOG
    {
        public int EMAIL_LOG_ID { get; set; }
        public DateTime? LOG_DT { get; set; }
        public string LOG_FROM { get; set; }
        public string LOG_TO { get; set; }
        public string LOG_CC { get; set; }
        public string LOG_SUBJ { get; set; }
        public string LOG_MSG { get; set; }
        public string EMAIL_TYPE { get; set; }
    }
}
