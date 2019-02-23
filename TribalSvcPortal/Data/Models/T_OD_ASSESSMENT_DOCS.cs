using System;
using System.Collections.Generic;

namespace TribalSvcPortal.Data.Models
{
    public partial class T_OD_ASSESSMENT_DOCS
    {
        public Guid DOC_IDX { get; set; }
        public Guid ASSESSMENT_IDX { get; set; }

        public T_OD_ASSESSMENTS ASSESSMENT_IDXNavigation { get; set; }
    }
}
