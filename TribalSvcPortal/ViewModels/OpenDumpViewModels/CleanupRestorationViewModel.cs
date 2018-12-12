using System;
using System.Collections.Generic;
using TribalSvcPortal.Data.Models;

namespace TribalSvcPortal.ViewModels.OpenDumpViewModels
{
    public class CleanupRestorationViewModel
    {
        public string rESTORE_CAT { get; set; }
        public T_OD_DUMP_ASSESSMENTS Assessment { get; set; }
        public List<T_OD_DUMP_ASSESSMENT_RESTORE> Restores { get; set; }
        public Guid? edit_restore_idx { get; set; }
        public string newRestoreActivity { get; set; }
        public decimal? newRestoreAmt { get; set; }
    }
}
