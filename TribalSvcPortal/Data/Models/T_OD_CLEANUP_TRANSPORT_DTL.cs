using System;
using System.Collections.Generic;

namespace TribalSvcPortal.Data.Models
{
    public partial class T_OD_CLEANUP_TRANSPORT_DTL
    {
        public Guid CLEANUP_TRANSPORT_DTL_IDX { get; set; }
        public Guid CLEANUP_PROJECT_IDX { get; set; }
        public Guid REF_WASTE_TYPE_IDX { get; set; }
        public int? NUM_LOADS { get; set; }
        public decimal? HOURS_LOAD { get; set; }
        public decimal? HOURLY_RATE { get; set; }
        public decimal? TRANSPORT_COST { get; set; }

        public T_OD_CLEANUP_PROJECT CLEANUP_PROJECT_IDXNavigation { get; set; }
    }
}
