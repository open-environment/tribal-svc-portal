
using System.Collections.Generic;
using TribalSvcPortal.Data.Models;

namespace TribalSvcPortal.ViewModels.HomeViewModels
{
    public class HomeViewModel
    {
        public string selOrg { get; set; }
        public bool WarnNoClientInd { get; set; }
        public string Announcement { get; set; }
        public IEnumerable<T_PRT_CLIENTS> _clients { get; set; }
        public List<T_PRT_ORGANIZATIONS> _WordPressOrgs { get; set; }

    }
}
