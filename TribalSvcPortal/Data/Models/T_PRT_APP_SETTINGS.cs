using System;
using System.Collections.Generic;

namespace TribalSvcPortal.Data.Models
{
    public partial class T_PRT_APP_SETTINGS
    {
        public int SETTING_IDX { get; set; }
        public string SETTING_NAME { get; set; }
        public string SETTING_DESC { get; set; }
        public string SETTING_VALUE { get; set; }
        public bool? ENCRYPT_IND { get; set; }
        public string MODIFY_USER_ID { get; set; }
        public DateTime? MODIFY_DT { get; set; }
    }
}
