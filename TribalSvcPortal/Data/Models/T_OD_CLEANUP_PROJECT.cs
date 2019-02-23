using System;
using System.Collections.Generic;

namespace TribalSvcPortal.Data.Models
{
    public partial class T_OD_CLEANUP_PROJECT
    {
        public T_OD_CLEANUP_PROJECT()
        {
            T_OD_CLEANUP_ACTIVITIES = new HashSet<T_OD_CLEANUP_ACTIVITIES>();
            T_OD_CLEANUP_CLEANUP_DTL = new HashSet<T_OD_CLEANUP_CLEANUP_DTL>();
            T_OD_CLEANUP_DOCS = new HashSet<T_OD_CLEANUP_DOCS>();
        }

        public Guid CLEANUP_PROJECT_IDX { get; set; }
        public Guid ASSESSMENT_IDX { get; set; }
        public string PROJECT_TYPE { get; set; }
        public string PROJECT_DESCRIPTION { get; set; }
        public DateTime? START_DATE { get; set; }
        public DateTime? COMPLETION_DATE { get; set; }
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
        public string CLEANUP_BY { get; set; }
        public string CLEANUP_BY_TITLE { get; set; }

        public T_OD_ASSESSMENTS ASSESSMENT_IDXNavigation { get; set; }
        public ICollection<T_OD_CLEANUP_ACTIVITIES> T_OD_CLEANUP_ACTIVITIES { get; set; }
        public ICollection<T_OD_CLEANUP_CLEANUP_DTL> T_OD_CLEANUP_CLEANUP_DTL { get; set; }
        public ICollection<T_OD_CLEANUP_DOCS> T_OD_CLEANUP_DOCS { get; set; }
    }
}
