using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TribalSvcPortal.ViewModels.OpenDumpViewModels
{
    public class OpenDumpViewModel
    {
        public PreFieldViewModel oPreFieldViewModel { get; set; }
        public FieldAssessmentViewModel oFieldAssessmentViewModel { get; set; }
        public OpenDumpTab ActiveTab { get; set; }
    }
    public enum OpenDumpTab
    {
        Prefield, FieldAssessment, HealthThreat, WasteProfile, SiteCleanUp
    }
}
