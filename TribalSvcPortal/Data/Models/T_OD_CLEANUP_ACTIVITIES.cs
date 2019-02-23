using System;
using System.Collections.Generic;

namespace TribalSvcPortal.Data.Models
{
    public partial class T_OD_CLEANUP_ACTIVITIES
    {
        public Guid CLEANUP_ACTIVITY_IDX { get; set; }
        public Guid CLEANUP_PROJECT_IDX { get; set; }
        public string CLEANUP_CAT { get; set; }
        public string CLEANUP_ACTIVITY { get; set; }
        public decimal CLEANUP_COST { get; set; }
        public string CREATE_USER_ID { get; set; }
        public DateTime? CREATE_DT { get; set; }
        public string MODIFY_USER_ID { get; set; }
        public DateTime? MODIFY_DT { get; set; }
        public decimal? CLEANUP_UNIT_COST { get; set; }
        public string QUANTITY { get; set; }
        public string QUANTITY_UNIT { get; set; }

        public T_OD_CLEANUP_PROJECT CLEANUP_PROJECT_IDXNavigation { get; set; }
    }
}
