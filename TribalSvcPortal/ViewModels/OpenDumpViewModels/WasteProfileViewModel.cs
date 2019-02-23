using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using TribalSvcPortal.AppLogic.DataAccessLayer;
using TribalSvcPortal.Data.Models;

namespace TribalSvcPortal.ViewModels.OpenDumpViewModels
{
    public class WasteProfileViewModel
    {
        public string SiteName { get; set; }
        public IEnumerable<SelectListItem> ddl_Assessments { get; set; }  //used for changing assessments only
        public T_OD_ASSESSMENTS Assessment { get; set; }


        //******* T2 **************
        public List<SelectedWasteTypeDisplay> ContentCheckBoxList { get; set; }
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

        public int SizeScore { get; set; }
        public int ContentScore { get; set; }
        public int HazardFactorScore { get; set; }
        public int ProximityScore { get; set; }
        public string FinalScore { get; set; }
    }
}
