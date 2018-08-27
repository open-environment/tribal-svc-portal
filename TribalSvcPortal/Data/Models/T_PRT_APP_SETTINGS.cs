using System;
using System.Collections.Generic;

namespace TribalSvcPortal.Data.Models
{
    public partial class T_PRT_APP_SETTINGS
    {
        public int SettingIdx { get; set; }
        public string SettingName { get; set; }
        public string SettingDesc { get; set; }
        public string SettingValue { get; set; }
        public bool? EncryptInd { get; set; }
        public string ModifyUserId { get; set; }
        public DateTime? ModifyDt { get; set; }
    }
}
