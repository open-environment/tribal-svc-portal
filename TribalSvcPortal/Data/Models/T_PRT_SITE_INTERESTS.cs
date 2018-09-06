using System;
using System.Collections.Generic;

namespace TribalSvcPortal.Data.Models
{
    public partial class T_PRT_SITE_INTERESTS
    {
        public Guid SiteInterestIdx { get; set; }
        public Guid SiteIdx { get; set; }
        public string InterestName { get; set; }
        public string CreateUserId { get; set; }
        public DateTime? CreateDt { get; set; }
        public string ModifyUserId { get; set; }
        public DateTime? ModifyDt { get; set; }

        public T_PRT_SITES SiteIdxNavigation { get; set; }
    }
}
