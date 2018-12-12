using System;
using System.Collections.Generic;

namespace TribalSvcPortal.Data.Models
{
    public partial class T_PRT_SYS_LOG
    {
        public int SYS_LOG_ID { get; set; }
        public DateTime LOG_DT { get; set; }
        public string LOG_USER_ID { get; set; }
        public string LOG_TYPE { get; set; }
        public string LOG_MSG { get; set; }
    }
}
