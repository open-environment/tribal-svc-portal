using System;
using System.Collections.Generic;
using TribalSvcPortal.AppLogic.DataAccessLayer;
using TribalSvcPortal.Data.Models;

namespace TribalSvcPortal.ViewModels.OpenDumpViewModels
{
    public class AssessmentsViewModel
    {
        public Guid? SiteIDX { get; set; }
        public string SiteName { get; set; }
        public string OrgName { get; set; }
        public IEnumerable<AssessmentSummaryDisplayType> T_OD_DUMP_ASSESSMENTS { get; set; }
    }
}
