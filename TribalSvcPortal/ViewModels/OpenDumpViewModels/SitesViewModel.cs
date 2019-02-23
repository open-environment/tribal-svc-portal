using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using TribalSvcPortal.AppLogic.DataAccessLayer;

namespace TribalSvcPortal.ViewModels.OpenDumpViewModels
{
    public class SitesViewModel
    {
        public string selStr { get; set; }
        public string selStatus { get; set; }
        public string selOrg { get; set; }
        public string tab { get; set; }
        public IEnumerable<SelectListItem> ddl_Status { get; set; }
        public IEnumerable<SelectListItem> ddl_Org { get; set; }
        public List<OpenDumpSiteListDisplay> sites { get; set; }      
    }
}
