using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using TribalSvcPortal.Data.Models;

namespace TribalSvcPortal.ViewModels.InspectionViewModels
{
    public class IndexViewModel
    {
        public List<V_INSP_ASSESSMENTS> Assessments { get; set; }
        public IEnumerable<SelectListItem> ddl_AssessType { get; set; }
        public string selAssessType { get; set; }
        public string str { get; set; }
        public string bldg { get; set; }

    }
}
