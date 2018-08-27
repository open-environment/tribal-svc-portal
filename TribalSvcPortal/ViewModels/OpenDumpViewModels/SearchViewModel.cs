using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TribalSvcPortal.Data.Models;

namespace TribalSvcPortal.ViewModels.OpenDumpViewModels
{

    public class SearchViewModel
    {
        public string selStr { get; set; }
        public string selStatus { get; set; }
        public string selOrg { get; set; }
        public IEnumerable<SelectListItem> ddl_Status { get; set; }
        public IEnumerable<SelectListItem> ddl_Org { get; set; }

        public SearchViewModel()
        {
            ddl_Status = new List<SelectListItem>();
            ddl_Org = new List<SelectListItem>();
        }
    }
}
