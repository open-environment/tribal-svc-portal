using System;
using System.Collections.Generic;

namespace TribalSvcPortal.Data.Models
{
    public partial class T_PRT_SITE_INTERESTS
    {
        public Guid SITE_INTEREST_IDX { get; set; }
        public Guid SITE_IDX { get; set; }
        public string INTEREST_NAME { get; set; }
        public string CREATE_USER_ID { get; set; }
        public DateTime? CREATE_DT { get; set; }
        public string MODIFY_USER_ID { get; set; }
        public DateTime? MODIFY_DT { get; set; }

        public T_PRT_SITES SITE_IDXNavigation { get; set; }
    }
}
