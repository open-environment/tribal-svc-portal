using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TribalSvcPortal.AppLogic.DataAccessLayer;
using TribalSvcPortal.Data.Models;

namespace TribalSvcPortal.ViewModels.OpenDumpViewModels
{
    public class CleanupDisposalViewModel
    {
        public T_OD_CLEANUP_PROJECT CleanupProject { get; set; }
        public List<CleanupDisposalDetailsType> DisposalDetails { get; set; }
    }
}
