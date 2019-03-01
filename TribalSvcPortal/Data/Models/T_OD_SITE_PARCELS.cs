using System;
using System.Collections.Generic;

namespace TribalSvcPortal.Data.Models
{
    public partial class T_OD_SITE_PARCELS
    {
        public Guid SITE_PARCEL_IDX { get; set; }
        public Guid SITE_IDX { get; set; }
        public string PARCEL_NUM { get; set; }
        public string OWNER { get; set; }
        public string ACRES { get; set; }
    }
}
