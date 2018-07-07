using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using TribalSvcPortal.AppLogic.DataAccessLayer;

namespace TribalSvcPortal.Models.AdminViewModels
{
    public class TenantUserEditViewModel
    {
        public string UserIDX { get; set; }
        public int TenantUserIDX { get; set; }
        public IEnumerable<TenantUserClientDisplayType> TenantUserClients { get; set; }
        public IEnumerable<SelectListItem> ddl_Clients { get; set; }
    }
}
