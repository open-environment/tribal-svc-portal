﻿using System;
using System.Collections.Generic;

namespace TribalSvcPortal.Data.Models
{
    public partial class T_PRT_SITES
    {
        public Guid SiteIdx { get; set; }
        public string OrgId { get; set; }
        public string SiteName { get; set; }
        public string EpaId { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string SiteAddress { get; set; }
        public string CreateUserId { get; set; }
        public DateTime? CreateDt { get; set; }
        public string ModifyUserId { get; set; }
        public DateTime? ModifyDt { get; set; }

        public T_PRT_ORGANIZATIONS Org { get; set; }
    }
}
