using System;
using System.Collections.Generic;

namespace TribalSvcPortal.Data.Models
{
    public partial class T_OD_CLEANUP_CLEANUP_DTL
    {
        public Guid CLEANUP_CLEANUP_DTL_IDX { get; set; }
        public Guid CLEANUP_PROJECT_IDX { get; set; }
        public string REF_WASTE_TYPE_CAT { get; set; }
        public string REF_ASSET_NAME { get; set; }
        public decimal? CLEANUP_COST { get; set; }

        public T_OD_CLEANUP_PROJECT CLEANUP_PROJECT_IDXNavigation { get; set; }
    }
}
