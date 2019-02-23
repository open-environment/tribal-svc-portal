using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using TribalSvcPortal.AppLogic.DataAccessLayer;

namespace TribalSvcPortal.ViewModels.OpenDumpViewModels
{
    public class CleanupProjectsViewModel
    {
        public IEnumerable<CleanupProjectsDisplayType> CleanupProjects { get; set; }
        public IEnumerable<SelectListItem> ddlAssessments { get; set; }
        public IEnumerable<SelectListItem> ddlCleanupType { get; set; }
        public Guid? selAssessID { get; set; }
        public string selCleanupType { get; set; }
    }
}
