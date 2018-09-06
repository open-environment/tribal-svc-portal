using System;
using System.Collections.Generic;

namespace TribalSvcPortal.Data.Models
{
    public partial class T_OD_DUMP_ASSESSMENT_DOCS
    {
        public Guid DumpAssessmentsIdx { get; set; }
        public Guid DocIdx { get; set; }

        public T_OD_DUMP_ASSESSMENTS DumpAssessmentsIdxNavigation { get; set; }
    }
}
