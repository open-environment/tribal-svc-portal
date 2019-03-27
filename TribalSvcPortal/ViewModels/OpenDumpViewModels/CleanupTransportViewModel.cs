using System;
using System.Collections.Generic;
using System.Linq;
using TribalSvcPortal.AppLogic.DataAccessLayer;
using TribalSvcPortal.Data.Models;

namespace TribalSvcPortal.ViewModels.OpenDumpViewModels
{
    public class CleanupTransportViewModel
    {
        public T_OD_CLEANUP_PROJECT CleanupProject { get; set; }

        public List<CleanupTransportDetailsType> TransportDetails { get; set; }
    }
}
