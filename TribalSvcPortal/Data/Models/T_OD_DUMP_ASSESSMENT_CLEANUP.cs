using System;
using System.Collections.Generic;

namespace TribalSvcPortal.Data.Models
{
    public partial class T_OD_DUMP_ASSESSMENT_CLEANUP
    {
        public Guid DUMP_ASSESSMENT_CLEANUP_IDX { get; set; }
        public Guid DUMP_ASSESSMENTS_IDX { get; set; }
        public string REF_WASTE_TYPE_CAT { get; set; }
        public string REF_ASSET_NAME { get; set; }
        public decimal? CLEANUP_COST { get; set; }

        public T_OD_DUMP_ASSESSMENTS DUMP_ASSESSMENTS_IDXNavigation { get; set; }
    }
}
