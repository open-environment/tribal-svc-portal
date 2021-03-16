using System;
using System.Collections.Generic;
using System.Linq;
using TribalSvcPortal.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TribalSvcPortal.ViewModels.OpenDumpViewModels
{
    public class ParcelLayerSelectViewModel
    {
        public T_PRT_SITES T_PRT_SITES { get; set; }
        public IEnumerable<SelectListItem> ddlLayers { get; set; }
    }


}
