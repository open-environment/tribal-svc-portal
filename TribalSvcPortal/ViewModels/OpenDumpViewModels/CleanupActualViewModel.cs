using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using TribalSvcPortal.Data.Models;

namespace TribalSvcPortal.ViewModels.OpenDumpViewModels
{
    public class CleanupActualViewModel
    {
        public string SiteName { get; set; }
        public T_OD_ASSESSMENTS Assessment { get; set; }
        public T_OD_CLEANUP_PROJECT CleanupProject { get; set; }
        public List<T_OD_CLEANUP_ACTIVITIES> CleanupActivities { get; set; }

        //edits
        public Guid? edit_cleanupActivityIdx { get; set; }
        public string newCleanupActivityName { get; set; }
        public decimal? newCleanupActivityAmt { get; set; }
        public decimal? newCleanupActivityUnitCost { get; set; }
        public string newCleanupActivityQuantity { get; set; }
        public string newCleanupActivityQuantityUnit { get; set; }

        //photos
        public List<T_PRT_DOCUMENTS> picsBefore_existing { get; set; }
        public List<T_PRT_DOCUMENTS> picsAfter_existing { get; set; }
        public IFormFile filesPhoto { get; set; }  //new photo
        public string FilePhotoDescription { get; set; }
        public string FilePhotoType { get; set; }

    }
}
