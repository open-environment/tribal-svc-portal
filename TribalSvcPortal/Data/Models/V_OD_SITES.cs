using System;
using System.Collections.Generic;

namespace TribalSvcPortal.Data.Models
{
    public partial class V_OD_SITES
    {
        public V_OD_SITES()
        {
        }
        public Guid SITE_IDX { get; set; }
        public string FacilitySiteIdentitifer { get; set; }
        public string FacilitySiteName { get; set; }
        public string OriginatingPartnerName { get; set; }
        public string InformationSystemAcronymName { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public string LocationAddressText { get; set; }
        public string StateName { get; set; }
        public string CountyName { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string CommunityName { get; set; }
        public string TribalLandStatusText { get; set; }
        public string SiteSettingName { get; set; }
        public string SiteDistanceToHomesText { get; set; }
        public string SiteVerticalDistanceToAquiferText { get; set; }
        public string SiteHorizontalDistanceToSurfaceWaterText { get; set; }
        public string SiteInitialReportedByText { get; set; }
        public DateTime? SiteInitialReportedDate { get; set; }
        public string SiteStatusText { get; set; }
        public int? SiteHealthThreatScoreValue { get; set; }
    }
}
