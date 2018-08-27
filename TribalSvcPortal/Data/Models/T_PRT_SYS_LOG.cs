using System;
using System.Collections.Generic;

namespace TribalSvcPortal.Data.Models
{
    public partial class T_PRT_SYS_LOG
    {
        public int SysLogId { get; set; }
        public DateTime LogDt { get; set; }
        public string LogUserId { get; set; }
        public string LogType { get; set; }
        public string LogMsg { get; set; }
    }
}
