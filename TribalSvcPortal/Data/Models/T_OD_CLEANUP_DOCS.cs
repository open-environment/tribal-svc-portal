using System;
using System.Collections.Generic;

namespace TribalSvcPortal.Data.Models
{
    public partial class T_OD_CLEANUP_DOCS
    {
        public Guid DOC_IDX { get; set; }
        public Guid CLEANUP_PROJECT_IDX { get; set; }

        public T_OD_CLEANUP_PROJECT CLEANUP_PROJECT_IDXNavigation { get; set; }
    }
}
