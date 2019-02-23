using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using TribalSvcPortal.AppLogic.DataAccessLayer;
using TribalSvcPortal.Data.Models;

namespace TribalSvcPortal.ViewModels.AdminViewModels
{
    public class ManageUsersViewModel
    {
        public IEnumerable<SelectListItem> ddl_AdminOfOrgClients { get; set; }
        public int? selOrgUserClient { get; set; }  //the selected admin org/client/user record from top dropdown

        public IEnumerable<SelectListItem> ddl_Users { get; set; }
        public string org_user_idx { get; set; }   //sel org user
        public string selOrg { get; set; }
        public string client_id { get; set; }   //sel client id
        public List<OrgUserClientDisplayType> OrgUserClients { get; set; }
    }
}
