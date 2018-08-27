using System;
using System.Collections.Generic;

namespace TribalSvcPortal.Data.Models
{
    public partial class T_OD_DUMP_ASSESSMENTS
    {
        public Guid DumpAssessmentsIdx { get; set; }
        public Guid SiteIdx { get; set; }
        public DateTime AssessmentDt { get; set; }
        public string InspectedBy { get; set; }
        public Guid? InspectionTypeIdx { get; set; }
        public string SiteDescription { get; set; }
        public string SiteGeography { get; set; }
        public string NoOfStructures { get; set; }
        public string SiteAccess { get; set; }
        public string Signage { get; set; }
        public bool ActiveSiteInd { get; set; }
        public bool MaintainedInd { get; set; }
        public bool BurningTakenPlaceInd { get; set; }
        public string Notes { get; set; }
        public string CreateUserId { get; set; }
        public DateTime? CreateDt { get; set; }
        public string ModifyUserId { get; set; }
        public DateTime? ModifyDt { get; set; }

        public T_OD_REF_DATA InspectionTypeIdxNavigation { get; set; }
        public T_OD_SITES SiteIdxNavigation { get; set; }
    }
}
