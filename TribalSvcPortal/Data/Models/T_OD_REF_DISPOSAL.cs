using System;
using System.Collections.Generic;

namespace TribalSvcPortal.Data.Models
{
    public partial class T_OD_REF_DISPOSAL
    {
        public Guid REF_DISPOSAL_IDX { get; set; }
        public string DISPOSAL_NAME { get; set; }
        public decimal? PRICE_PER_TON { get; set; }
        public string ORG_ID { get; set; }
    }
}
