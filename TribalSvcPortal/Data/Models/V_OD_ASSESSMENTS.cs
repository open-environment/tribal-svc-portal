using System;
using System.Collections.Generic;

namespace TribalSvcPortal.Data.Models
{
    public partial class V_OD_ASSESSMENTS
    {
        public V_OD_ASSESSMENTS()
        {
        }
        public Guid SITE_IDX { get; set; }
        public Guid ASSESSMENT_IDX { get; set; }
        public DateTime? SiteAssessmentDate { get; set; }
        public string SiteAssessedBy { get; set; }
        public string AssessmentTypeText { get; set; }
        public string SiteStatusText { get; set; }
        public DateTime? SiteClosedDate { get; set; }
        public string AssessmentDescription { get; set; }
        public string AssessmentSiteObservations { get; set; }
        public decimal? SiteSurfaceAreaValue { get; set; }
        public decimal? SiteVolumeValue { get; set; }
        public string SiteHealthThreatScoreSummaryText { get; set; }
        public int? SiteHealthThreatScoreValue { get; set; }
        public string HFRainfall { get; set; }
        public string HFDrainage { get; set; }
        public string HFFlooding { get; set; }
        public string HFBurning { get; set; }
        public string HFFencing { get; set; }
        public string HFAccess { get; set; }
        public string HFPublic { get; set; }
    }
}
