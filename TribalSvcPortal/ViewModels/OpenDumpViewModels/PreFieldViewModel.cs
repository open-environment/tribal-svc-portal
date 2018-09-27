using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TribalSvcPortal.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TribalSvcPortal.ViewModels.OpenDumpViewModels
{
    public class PreFieldViewModel
    {
        public T_PRT_SITES TPrtSites { get; set; }
        public IEnumerable<SelectListItem> SiteSettingsList { get; set; }
        public IEnumerable<SelectListItem> CommunityList { get; set; }
        public IEnumerable<SelectListItem> AquiferList { get; set; }
        public IEnumerable<SelectListItem> SurfaceWaterList { get; set; }
        public IEnumerable<SelectListItem> HomesList { get; set; }        
        public T_OD_SITES TOdSites { get; set; }
        public string returnURL { get; set; }
        public string selOrg { get; set; }
        public IEnumerable<SelectListItem> OrgList { get; set; }
        public string activeTab { get; set; }
        public string Title { get; set; }

        public IEnumerable<SelectListItem> AssessmentDropDownList { get; set; }
        public IEnumerable<SelectListItem> AssessmentTypeList { get; set; }
        public T_OD_DUMP_ASSESSMENTS TOdDumpAssessments { get; set; }
        public List<T_OD_DUMP_ASSESSMENTS> TOdDumpAssessmentsGridList { get; set; }
        public Guid DUMP_ASSESSMENTS_IDX { get; set; }
    }
}
