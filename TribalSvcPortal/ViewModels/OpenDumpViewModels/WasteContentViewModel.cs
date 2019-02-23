using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using TribalSvcPortal.AppLogic.DataAccessLayer;
using TribalSvcPortal.Data.Models;

namespace TribalSvcPortal.ViewModels.OpenDumpViewModels
{
    public class WasteContentViewModel
    {
        public string SiteName { get; set; }
        public IEnumerable<SelectListItem> ddl_Assessments { get; set; }  //used for changing assessments only
        public T_OD_ASSESSMENTS Assessment { get; set; }
        public List<AssessmentContentTypeDisplay> DumpContents { get; set; }
        public IEnumerable<SelectListItem> ddl_DisposalMethod { get; set; }
        public bool CleanupEstimatesInd { get; set; }
        public bool RecalcInd { get; set; }
    }
}
