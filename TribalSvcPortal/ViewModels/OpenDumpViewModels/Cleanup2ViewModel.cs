using System.Collections.Generic;
using TribalSvcPortal.AppLogic.DataAccessLayer;
using TribalSvcPortal.Data.Models;

namespace TribalSvcPortal.ViewModels.OpenDumpViewModels
{
    public class Cleanup2ViewModel
    {
        public T_OD_DUMP_ASSESSMENTS Assessment { get; set; }
        public List<AssessmentCleanupDisplayType> AssessmentCleanups { get; set; }
    }
}
