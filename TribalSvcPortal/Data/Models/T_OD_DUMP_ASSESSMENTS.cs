using System;
using System.Collections.Generic;

namespace TribalSvcPortal.Data.Models
{
    public partial class T_OD_DUMP_ASSESSMENTS
    {
        public T_OD_DUMP_ASSESSMENTS()
        {
            T_OD_DUMP_ASSESSMENT_CLEANUP = new HashSet<T_OD_DUMP_ASSESSMENT_CLEANUP>();
            T_OD_DUMP_ASSESSMENT_CONTENT = new HashSet<T_OD_DUMP_ASSESSMENT_CONTENT>();
            T_OD_DUMP_ASSESSMENT_DOCS = new HashSet<T_OD_DUMP_ASSESSMENT_DOCS>();
            T_OD_DUMP_ASSESSMENT_RESTORE = new HashSet<T_OD_DUMP_ASSESSMENT_RESTORE>();
        }

        public Guid DUMP_ASSESSMENTS_IDX { get; set; }
        public Guid SITE_IDX { get; set; }
        public DateTime ASSESSMENT_DT { get; set; }
        public string ASSESSED_BY { get; set; }
        public Guid? ASSESSMENT_TYPE_IDX { get; set; }
        public string CURRENT_SITE_STATUS { get; set; }
        public DateTime? CLEANED_CLOSED_DT { get; set; }
        public decimal? AREA_ACRES { get; set; }
        public decimal? VOLUME_CU_YD { get; set; }
        public Guid? HF_RAINFALL { get; set; }
        public Guid? HF_DRAINAGE { get; set; }
        public Guid? HF_FLOODING { get; set; }
        public Guid? HF_BURNING { get; set; }
        public Guid? HF_FENCING { get; set; }
        public Guid? HF_ACCESS_CONTROL { get; set; }
        public Guid? HF_PUBLIC_CONCERN { get; set; }
        public int? HEALTH_THREAT_SCORE { get; set; }
        public string SITE_DESCRIPTION { get; set; }
        public string ASSESSMENT_NOTES { get; set; }
        public decimal? COST_CLEANUP_AMT { get; set; }
        public decimal? COST_TRANSPORT_AMT { get; set; }
        public decimal? COST_DISPOSAL_AMT { get; set; }
        public decimal? COST_RESTORE_AMT { get; set; }
        public decimal? COST_SURVEIL_AMT { get; set; }
        public decimal? COST_TOTAL_AMT { get; set; }
        public string CREATE_USER_ID { get; set; }
        public DateTime? CREATE_DT { get; set; }
        public string MODIFY_USER_ID { get; set; }
        public DateTime? MODIFY_DT { get; set; }

        public T_OD_REF_DATA ASSESSMENT_TYPE_IDXNavigation { get; set; }
        public T_OD_REF_THREAT_FACTORS HF_ACCESS_CONTROLNavigation { get; set; }
        public T_OD_REF_THREAT_FACTORS HF_BURNINGNavigation { get; set; }
        public T_OD_REF_THREAT_FACTORS HF_DRAINAGENavigation { get; set; }
        public T_OD_REF_THREAT_FACTORS HF_FENCINGNavigation { get; set; }
        public T_OD_REF_THREAT_FACTORS HF_FLOODINGNavigation { get; set; }
        public T_OD_REF_THREAT_FACTORS HF_PUBLIC_CONCERNNavigation { get; set; }
        public T_OD_REF_THREAT_FACTORS HF_RAINFALLNavigation { get; set; }
        public T_OD_SITES SITE_IDXNavigation { get; set; }
        public ICollection<T_OD_DUMP_ASSESSMENT_CLEANUP> T_OD_DUMP_ASSESSMENT_CLEANUP { get; set; }
        public ICollection<T_OD_DUMP_ASSESSMENT_CONTENT> T_OD_DUMP_ASSESSMENT_CONTENT { get; set; }
        public ICollection<T_OD_DUMP_ASSESSMENT_DOCS> T_OD_DUMP_ASSESSMENT_DOCS { get; set; }
        public ICollection<T_OD_DUMP_ASSESSMENT_RESTORE> T_OD_DUMP_ASSESSMENT_RESTORE { get; set; }
    }
}
