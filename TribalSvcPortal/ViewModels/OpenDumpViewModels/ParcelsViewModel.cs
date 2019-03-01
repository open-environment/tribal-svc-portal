using System;
using System.Collections.Generic;
using System.Linq;
using TribalSvcPortal.Data.Models;

namespace TribalSvcPortal.ViewModels.OpenDumpViewModels
{
    public class ParcelsViewModel
    {
        public T_PRT_SITES T_PRT_SITES { get; set; }
        public List<T_OD_SITE_PARCELS> T_OD_SITE_PARCELS { get; set; }
    }
}
