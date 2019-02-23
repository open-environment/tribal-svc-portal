using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel;
using TribalSvcPortal.Data.Models;
using Microsoft.AspNetCore.Http;

namespace TribalSvcPortal.ViewModels.OpenDumpViewModels
{
    public class AssessmentDetailViewModel
    {
        public string SiteName { get; set; }
        public string OrgName { get; set; }
        public IEnumerable<SelectListItem> ddl_Assessments { get; set; }  //used for changing assessments only
        public T_OD_ASSESSMENTS Assessment { get; set; }
        public IEnumerable<SelectListItem> ddl_AssessmentTypeList { get; set; }
        public IEnumerable<SelectListItem> ddl_SiteStatus  { get; set; }


        public List<T_PRT_DOCUMENTS> filesPhoto_existing { get; set; }
        public List<T_PRT_DOCUMENTS> files_existing { get; set; }
        public IFormFile filesPhoto { get; set; }  //new photo
        public string FilePhotoDescription { get; set; }

    }
}
