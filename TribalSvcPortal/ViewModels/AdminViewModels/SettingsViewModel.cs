﻿using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TribalSvcPortal.Data.Models;

namespace TribalSvcPortal.ViewModels.AdminViewModels
{
    public class SettingsViewModel
    {
        public List<T_PRT_APP_SETTINGS> app_settings { get; set; }
        public T_PRT_APP_SETTINGS edit_app_setting { get; set; }

        [DisplayName("Terms & Conditions")]
        [DataType(DataType.MultilineText)]       
        [UIHint("tinymce_full_compressed")]
        [StringLength(6000)]
        public string TermsAndConditions { get; set; }

        [DisplayName("Announcements")]
        [DataType(DataType.MultilineText)]
        [UIHint("forumeditor")]
        [StringLength(6000)]
        public string Announcements { get; set; }

    }
}
