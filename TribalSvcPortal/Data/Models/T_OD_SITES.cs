using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TribalSvcPortal.Data.Models
{
    public partial class T_OD_SITES
    {
        public T_OD_SITES()
        {
            T_OD_DUMP_ASSESSMENTS = new HashSet<T_OD_DUMP_ASSESSMENTS>();
        }

        public Guid SiteIdx { get; set; }
        [Required(ErrorMessage = "please Enter Community")]
        public Guid CommunityIdx { get; set; }
        [Required(ErrorMessage = "please Enter SiteSetting")]
        public Guid SiteSettingIdx { get; set; }
        public string ReportedBy { get; set; }
        public DateTime? ReportedOn { get; set; }
        [Required(ErrorMessage = "please Enter ResponseAction")]
        public string ResponseAction { get; set; }

        public T_OD_REF_DATA CommunityIdxNavigation { get; set; }
        public T_OD_REF_DATA SiteSettingIdxNavigation { get; set; }
        public ICollection<T_OD_DUMP_ASSESSMENTS> T_OD_DUMP_ASSESSMENTS { get; set; }
    }
}
