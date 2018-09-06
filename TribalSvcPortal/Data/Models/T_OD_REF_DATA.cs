using System;
using System.Collections.Generic;

namespace TribalSvcPortal.Data.Models
{
    public partial class T_OD_REF_DATA
    {
        public T_OD_REF_DATA()
        {
            T_OD_DUMP_ASSESSMENTS = new HashSet<T_OD_DUMP_ASSESSMENTS>();
            T_OD_SITESCommunityIdxNavigation = new HashSet<T_OD_SITES>();
            T_OD_SITESSiteSettingIdxNavigation = new HashSet<T_OD_SITES>();
        }

        public Guid RefDataIdx { get; set; }
        public string RefDataCatName { get; set; }
        public string RefDataName { get; set; }
        public string RefDataDesc { get; set; }
        public string OrgId { get; set; }
        public bool UserCreateInd { get; set; }
        public string CreateUserId { get; set; }
        public DateTime? CreateDt { get; set; }
        public string ModifyUserId { get; set; }
        public DateTime? ModifyDt { get; set; }

        public T_OD_REF_DATA_CATEGORIES RefDataCatNameNavigation { get; set; }
        public ICollection<T_OD_DUMP_ASSESSMENTS> T_OD_DUMP_ASSESSMENTS { get; set; }
        public ICollection<T_OD_SITES> T_OD_SITESCommunityIdxNavigation { get; set; }
        public ICollection<T_OD_SITES> T_OD_SITESSiteSettingIdxNavigation { get; set; }
    }
}
