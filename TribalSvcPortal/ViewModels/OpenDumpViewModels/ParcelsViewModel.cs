using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using TribalSvcPortal.Data.Models;

namespace TribalSvcPortal.ViewModels.OpenDumpViewModels
{
    public class ParcelsViewModel
    {
        public IEnumerable<SelectListItem> ddlLayers { get; set; }
        public T_PRT_SITES T_PRT_SITES { get; set; }
        public string defaultPARCEL_LAYER { get; set; }
        public List<T_OD_SITE_PARCELS> T_OD_SITE_PARCELS { get; set; }

    }


}
