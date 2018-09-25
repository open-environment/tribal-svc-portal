using System;
using System.Collections.Generic;

namespace TribalSvcPortal.Data.Models
{
    public partial class T_OD_REF_WASTE_TYPE_CAT_CLEANUP
    {
        public Guid REF_WASTE_TYPE_CAT_CLEANUP_IDX { get; set; }
        public string REF_WASTE_TYPE_CAT { get; set; }
        public string REF_ASSET_NAME { get; set; }
        public decimal? PROCESS_RATE_PER_HR { get; set; }
        public string PROCESS_RATE_UNIT { get; set; }
        public decimal? ASSET_HOURLY_RATE { get; set; }
        public int? ASSET_COUNT { get; set; }
        public bool? PER_UNIT_IND { get; set; }
        public string ORG_ID { get; set; }

        public T_OD_REF_CLEANUP_ASSETS REF_ASSET_NAMENavigation { get; set; }
        public T_OD_REF_WASTE_TYPE_CAT REF_WASTE_TYPE_CATNavigation { get; set; }
    }
}
