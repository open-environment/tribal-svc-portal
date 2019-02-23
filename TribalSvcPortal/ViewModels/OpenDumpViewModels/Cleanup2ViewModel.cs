using System;
using System.Collections.Generic;
using TribalSvcPortal.AppLogic.DataAccessLayer;
using TribalSvcPortal.Data.Models;

namespace TribalSvcPortal.ViewModels.OpenDumpViewModels
{
    public class Cleanup2ViewModel
    {
        public T_OD_CLEANUP_PROJECT CleanupProject { get; set; }
        public List<AssessmentCleanupDisplayType> AssessmentCleanups { get; set; }
        public Guid? edit_cleanupActivityIdx { get; set; }
        public string edit_CleanupActivityName { get; set; }
        public decimal? edit_CleanupActivityAmt { get; set; }
    }
}
