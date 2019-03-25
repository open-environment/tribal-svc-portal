using System;
using System.Collections.Generic;

namespace TribalSvcPortal.Data.Models
{
    public partial class T_OD_CLEANUP_DISPOSAL_DTL
    {
        public Guid CLEANUP_DISPOSAL_DTL_IDX { get; set; }
        public Guid CLEANUP_PROJECT_IDX { get; set; }
        public Guid REF_DISPOSAL_IDX { get; set; }
        public decimal? DISPOSAL_WEIGHT_LBS { get; set; }
        public decimal? PRICE_PER_TON { get; set; }
        public decimal? DISPOSAL_COST { get; set; }

        public T_OD_CLEANUP_PROJECT CLEANUP_PROJECT_IDXNavigation { get; set; }
    }
}
