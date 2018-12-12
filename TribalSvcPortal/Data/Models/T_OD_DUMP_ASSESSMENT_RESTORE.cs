using System;
using System.Collections.Generic;

namespace TribalSvcPortal.Data.Models
{
    public partial class T_OD_DUMP_ASSESSMENT_RESTORE
    {
        public Guid DUMP_ASSESSMENT_RESTORE_IDX { get; set; }
        public Guid DUMP_ASSESSMENTS_IDX { get; set; }
        public string RESTORE_CAT { get; set; }
        public string RESTORE_ACTIVITY { get; set; }
        public decimal RESTORE_COST { get; set; }
        public string MODIFY_USER_ID { get; set; }
        public DateTime? MODIFY_DT { get; set; }

        public T_OD_DUMP_ASSESSMENTS DUMP_ASSESSMENTS_IDXNavigation { get; set; }
    }
}
