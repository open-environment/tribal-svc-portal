using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TribalSvcPortal.Data.Models;

namespace TribalSvcPortal.ViewModels.OpenDumpViewModels
{
    public class FieldAssessmentViewModel
    {
        public T_PRT_SITES TPrtSites { get; set; }
        public IEnumerable<SelectListItem> AssessmentDropDownList { get; set; }
        public IEnumerable<SelectListItem> AssessmentTypeList { get; set; }
        public IEnumerable<SelectListItem> AssessedByList { get; set; }
        public T_OD_DUMP_ASSESSMENTS TOdDumpAssessments { get; set; }
        public List<T_OD_DUMP_ASSESSMENTS> TOdDumpAssessmentsGridList { get; set; }
        public Guid selDumpAssessmentIdx { get; set; }
    }
}
