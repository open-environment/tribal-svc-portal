using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TribalSvcPortal.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TribalSvcPortal.ViewModels.OpenDumpViewModels
{
    public class FieldAssessmentViewModel
    {
        public IEnumerable<SelectListItem> AssessmentDropDownList { get; set; }
        public IEnumerable<SelectListItem> AssessmentTypeList { get; set; }
        public T_OD_DUMP_ASSESSMENTS TOdDumpAssessments { get; set; }
        public List<T_OD_DUMP_ASSESSMENTS> TOdDumpAssessmentsGridList { get; set; }
        public Guid DUMP_ASSESSMENTS_IDX { get; set; }
    }
}
