using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using TribalSvcPortal.AppLogic.DataAccessLayer;

namespace TribalSvcPortal.ViewModels.AdminViewModels
{
    public class OrgUserEditViewModel
    {
        public string UserIDX { get; set; }
        public int OrgUserIDX { get; set; }
        public IEnumerable<OrgUserClientDisplayType> OrgUserClients { get; set; }
        public IEnumerable<SelectListItem> ddl_Clients { get; set; }
    }
}
