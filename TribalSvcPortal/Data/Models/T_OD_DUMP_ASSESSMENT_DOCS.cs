using System;
using System.Collections.Generic;

namespace TribalSvcPortal.Data.Models
{
    public partial class T_OD_DUMP_ASSESSMENT_DOCS
    {
        public Guid DUMP_ASSESSMENTS_IDX { get; set; }
        public Guid DOC_IDX { get; set; }

        public T_OD_DUMP_ASSESSMENTS DUMP_ASSESSMENTS_IDXNavigation { get; set; }
    }
}
