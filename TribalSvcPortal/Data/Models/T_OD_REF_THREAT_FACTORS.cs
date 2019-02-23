using System;
using System.Collections.Generic;

namespace TribalSvcPortal.Data.Models
{
    public partial class T_OD_REF_THREAT_FACTORS
    {
        public T_OD_REF_THREAT_FACTORS()
        {
            T_OD_ASSESSMENTSHF_ACCESS_CONTROLNavigation = new HashSet<T_OD_ASSESSMENTS>();
            T_OD_ASSESSMENTSHF_BURNINGNavigation = new HashSet<T_OD_ASSESSMENTS>();
            T_OD_ASSESSMENTSHF_DRAINAGENavigation = new HashSet<T_OD_ASSESSMENTS>();
            T_OD_ASSESSMENTSHF_FENCINGNavigation = new HashSet<T_OD_ASSESSMENTS>();
            T_OD_ASSESSMENTSHF_FLOODINGNavigation = new HashSet<T_OD_ASSESSMENTS>();
            T_OD_ASSESSMENTSHF_PUBLIC_CONCERNNavigation = new HashSet<T_OD_ASSESSMENTS>();
            T_OD_ASSESSMENTSHF_RAINFALLNavigation = new HashSet<T_OD_ASSESSMENTS>();
            T_OD_SITESPF_AQUIFER_VERT_DISTNavigation = new HashSet<T_OD_SITES>();
            T_OD_SITESPF_HOMES_DISTNavigation = new HashSet<T_OD_SITES>();
            T_OD_SITESPF_SURF_WATER_HORIZ_DISTNavigation = new HashSet<T_OD_SITES>();
        }

        public Guid THREAT_FACTOR_IDX { get; set; }
        public string THREAT_FACTOR_TYPE { get; set; }
        public string THREAT_FACTOR_NAME { get; set; }
        public int? THREAT_FACTOR_SCORE { get; set; }

        public ICollection<T_OD_ASSESSMENTS> T_OD_ASSESSMENTSHF_ACCESS_CONTROLNavigation { get; set; }
        public ICollection<T_OD_ASSESSMENTS> T_OD_ASSESSMENTSHF_BURNINGNavigation { get; set; }
        public ICollection<T_OD_ASSESSMENTS> T_OD_ASSESSMENTSHF_DRAINAGENavigation { get; set; }
        public ICollection<T_OD_ASSESSMENTS> T_OD_ASSESSMENTSHF_FENCINGNavigation { get; set; }
        public ICollection<T_OD_ASSESSMENTS> T_OD_ASSESSMENTSHF_FLOODINGNavigation { get; set; }
        public ICollection<T_OD_ASSESSMENTS> T_OD_ASSESSMENTSHF_PUBLIC_CONCERNNavigation { get; set; }
        public ICollection<T_OD_ASSESSMENTS> T_OD_ASSESSMENTSHF_RAINFALLNavigation { get; set; }
        public ICollection<T_OD_SITES> T_OD_SITESPF_AQUIFER_VERT_DISTNavigation { get; set; }
        public ICollection<T_OD_SITES> T_OD_SITESPF_HOMES_DISTNavigation { get; set; }
        public ICollection<T_OD_SITES> T_OD_SITESPF_SURF_WATER_HORIZ_DISTNavigation { get; set; }
    }
}
