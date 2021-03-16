using System.Collections.Generic;
using TribalSvcPortal.AppLogic.DataAccessLayer;
using TribalSvcPortal.Data.Models;

namespace TribalSvcPortal.ViewModels.OpenDumpViewModels
{
    public class CleanupViewModel
    {
        public string SiteName { get; set; }
        public T_OD_ASSESSMENTS Assessment { get; set; }
        public T_OD_CLEANUP_PROJECT CleanupProject { get; set; }
        public List<AssessmentContentTypeDisplay> DumpContents { get; set; }
    }
}
