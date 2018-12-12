using System.Collections.Generic;
using TribalSvcPortal.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TribalSvcPortal.ViewModels.OpenDumpViewModels
{
    public class PreFieldViewModel
    {
        public T_PRT_SITES TPrtSite { get; set; }
        public T_OD_SITES TOdSite { get; set; }
        public IEnumerable<SelectListItem> SiteSettingsList { get; set; }
        public IEnumerable<SelectListItem> CommunityList { get; set; }
        public IEnumerable<SelectListItem> AquiferList { get; set; }
        public IEnumerable<SelectListItem> SurfaceWaterList { get; set; }
        public IEnumerable<SelectListItem> HomesList { get; set; }
        public IEnumerable<SelectListItem> OrgList { get; set; }
        public string returnURL { get; set; }  
    }
}
