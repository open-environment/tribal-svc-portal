using System.Collections.Generic;
using TribalSvcPortal.Data.Models;

namespace TribalSvcPortal.ViewModels
{
    public class LeftMenuViewModel
    {
        public IEnumerable<T_PRT_CLIENTS> _clients { get; set; }
        public List<T_PRT_ORGANIZATIONS> _myOrgAdmins { get; set; }
        public bool IsOrgClientAdmin { get; set; }
    }
}
