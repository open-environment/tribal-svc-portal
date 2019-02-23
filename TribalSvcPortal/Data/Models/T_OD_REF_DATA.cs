using System;
using System.Collections.Generic;

namespace TribalSvcPortal.Data.Models
{
    public partial class T_OD_REF_DATA
    {
        public T_OD_REF_DATA()
        {
            T_OD_ASSESSMENTS = new HashSet<T_OD_ASSESSMENTS>();
            T_OD_SITESCOMMUNITY_IDXNavigation = new HashSet<T_OD_SITES>();
            T_OD_SITESSITE_SETTING_IDXNavigation = new HashSet<T_OD_SITES>();
        }

        public Guid REF_DATA_IDX { get; set; }
        public string REF_DATA_CAT_NAME { get; set; }
        public string REF_DATA_VAL { get; set; }
        public string REF_DATA_DESC { get; set; }
        public string ORG_ID { get; set; }
        public string CREATE_USER_ID { get; set; }
        public DateTime? CREATE_DT { get; set; }
        public string MODIFY_USER_ID { get; set; }
        public DateTime? MODIFY_DT { get; set; }

        public T_OD_REF_DATA_CATEGORIES REF_DATA_CAT_NAMENavigation { get; set; }
        public ICollection<T_OD_ASSESSMENTS> T_OD_ASSESSMENTS { get; set; }
        public ICollection<T_OD_SITES> T_OD_SITESCOMMUNITY_IDXNavigation { get; set; }
        public ICollection<T_OD_SITES> T_OD_SITESSITE_SETTING_IDXNavigation { get; set; }
    }
}
