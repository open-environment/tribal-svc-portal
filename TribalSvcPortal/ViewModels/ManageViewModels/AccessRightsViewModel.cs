using System.Collections.Generic;
using TribalSvcPortal.AppLogic.DataAccessLayer;
using TribalSvcPortal.Data.Models;

namespace TribalSvcPortal.ViewModels.ManageViewModels
{
    public class AccessRightsViewModel
    {
        public List<UserOrgDisplayType> AccessRights { get; set; }
        public List<T_PRT_CLIENTS> Clients { get; set; }
        public List<string> Roles { get; set; }
    }
}
