using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using TribalSvcPortal.Data.Models;
using Microsoft.AspNetCore.Http;
using TribalSvcPortal.AppLogic.DataAccessLayer;

namespace TribalSvcPortal.ViewModels.OpenDumpViewModels
{
    public class FieldAssessmentViewModel
    {
        public T_PRT_SITES TPrtSites { get; set; }
        public IEnumerable<SelectListItem> AssessmentDropDownList { get; set; }
        public IEnumerable<SelectListItem> AssessmentForHealthThreatDropDownList { get; set; }
        public IEnumerable<SelectListItem> AssessmentTypeList { get; set; }
        public IEnumerable<SelectListItem> AssessedByList { get; set; }
        public T_OD_DUMP_ASSESSMENTS TOdDumpAssessments { get; set; }
        public List<T_OD_DUMP_ASSESSMENTS> TOdDumpAssessmentsGridList { get; set; }
        public Guid selDumpAssessmentIdx { get; set; }

        public IEnumerable<SelectListItem> AverageRainfallList { get; set; }
        public int? RainfallSubScore { get; set; }
        public IEnumerable<SelectListItem> DrainageList { get; set; }
        public int? DrainageSubScore { get; set; }
        public IEnumerable<SelectListItem> FloodingList { get; set; }
        public int? FloodingSubScore { get; set; }
        public IEnumerable<SelectListItem> BurningList { get; set; }
        public int? BurningSubScore { get; set; }
        public IEnumerable<SelectListItem> FencedList { get; set; }
        public int? FencedSubScore { get; set; }
        public IEnumerable<SelectListItem> AccessList { get; set; }
        public int? AccessSubScore { get; set; }
        public IEnumerable<SelectListItem> ConcernList { get; set; }
        public int? ConcernSubScore { get; set; }
        public IEnumerable<SelectListItem> DisposalMethodList { get; set; }
        public List<T_OD_DUMP_ASSESSMENT_CONTENT> WasteAmountList { get; set; }

        public List<RefThreatFactor> TOdRefThreatFactorList { get; set; }

        public List<T_OD_REF_WASTE_TYPE> ContentCheckBoxList { get; set; }
        public int SizeScore { get; set; }
        public int ContentScore { get; set; }
        public int HazardFactorScore { get; set; }

        public int SizeTotalScore { get; set; }
        public int ContentTotalScore { get; set; }
        public int HazardTotalScore { get; set; }
        public string FinalScore { get; set; }

        [DisplayName("Upload Photo")]
        public IFormFile filesPhoto { get; set; }
        public string FilePhotoDescription { get; set; }
        public List<T_PRT_DOCUMENTS> filesPhoto_existing { get; set; }

        [DisplayName("Upload File")]
        public IFormFile files { get; set; }
        public string FileDescription { get; set; }
        public List<T_PRT_DOCUMENTS> files_existing { get; set; }
    }
}
