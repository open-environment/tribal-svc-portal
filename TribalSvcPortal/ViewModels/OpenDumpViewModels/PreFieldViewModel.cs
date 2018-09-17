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
        public T_OD_SITES TOdSites { get; set; }

    }
}
