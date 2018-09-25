using System;
using System.Collections.Generic;

namespace TribalSvcPortal.Data.Models
{
    public partial class T_OD_SITES
    {
        public T_OD_SITES()
        {
            T_OD_DUMP_ASSESSMENTS = new HashSet<T_OD_DUMP_ASSESSMENTS>();
        }

        public Guid SITE_IDX { get; set; }
        public string REPORTED_BY { get; set; }
        public DateTime? REPORTED_ON { get; set; }
        public Guid? COMMUNITY_IDX { get; set; }
        public Guid? SITE_SETTING_IDX { get; set; }
        public Guid? PF_AQUIFER_VERT_DIST { get; set; }
        public Guid? PF_SURF_WATER_HORIZ_DIST { get; set; }
        public Guid? PF_HOMES_DIST { get; set; }

        public T_OD_REF_DATA COMMUNITY_IDXNavigation { get; set; }
        public T_OD_REF_THREAT_FACTORS PF_AQUIFER_VERT_DISTNavigation { get; set; }
        public T_OD_REF_THREAT_FACTORS PF_HOMES_DISTNavigation { get; set; }
        public T_OD_REF_THREAT_FACTORS PF_SURF_WATER_HORIZ_DISTNavigation { get; set; }
        public T_OD_REF_DATA SITE_SETTING_IDXNavigation { get; set; }
        public ICollection<T_OD_DUMP_ASSESSMENTS> T_OD_DUMP_ASSESSMENTS { get; set; }
    }
}
