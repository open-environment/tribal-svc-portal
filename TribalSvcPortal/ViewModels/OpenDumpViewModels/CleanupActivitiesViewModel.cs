using System;
using System.Collections.Generic;
using TribalSvcPortal.Data.Models;

namespace TribalSvcPortal.ViewModels.OpenDumpViewModels
{
    public class CleanupActivitiesViewModel
    {
        public string CleanupCategory { get; set; }
        public T_OD_CLEANUP_PROJECT CleanupProject { get; set; }
        //public T_OD_ASSESSMENTS Assessment { get; set; }
        public List<T_OD_CLEANUP_ACTIVITIES> CleanupActivities { get; set; }
        public string returnURL { get; set; }
        //edit
        public Guid? edit_cleanupActivityIdx { get; set; }
        public string newCleanupActivityName { get; set; }
        public decimal? newCleanupActivityAmt { get; set; }
        public decimal? newCleanupActivityUnitCost { get; set; }
        public string newCleanupActivityQuantity { get; set; }
        public string newCleanupActivityQuantityUnit { get; set; }

    }
}
