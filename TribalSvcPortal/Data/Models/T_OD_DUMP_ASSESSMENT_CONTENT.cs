using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TribalSvcPortal.Data.Models
{
    public partial class T_OD_DUMP_ASSESSMENT_CONTENT
    {
        public Guid DUMP_ASSESSMENTS_CONTENT_IDX { get; set; }
        public Guid DUMP_ASSESSMENTS_IDX { get; set; }
        public Guid REF_WASTE_TYPE_IDX { get; set; }
        public decimal? WASTE_AMT { get; set; }
        public Guid? WASTE_UNIT_MSR { get; set; }
        public Guid? WASTE_DISPOSAL_METHOD { get; set; }
        public string WASTE_DISPOSAL_DIST { get; set; }

        public T_OD_DUMP_ASSESSMENTS DUMP_ASSESSMENTS_IDXNavigation { get; set; }
        public T_OD_REF_WASTE_TYPE REF_WASTE_TYPE_IDXNavigation { get; set; }
        [NotMapped]
        public string REF_WASTE_TYPE_NAME { get; set; }
        [NotMapped]
        public string REF_WASTE_TYPE_CAT { get; set; }
    }
}
