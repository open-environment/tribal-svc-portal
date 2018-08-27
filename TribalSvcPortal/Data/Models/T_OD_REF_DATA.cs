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

        public Guid DataIdx { get; set; }
        public string OrgId { get; set; }
        public string DataName { get; set; }
        public string DataCatName { get; set; }
        public bool UserCreateInd { get; set; }
        public string CreateUserId { get; set; }
        public DateTime? CreateDt { get; set; }
        public string ModifyUserId { get; set; }
        public DateTime? ModifyDt { get; set; }

        public T_OD_REF_DATA_CATEGORIES DataCatNameNavigation { get; set; }
        public ICollection<T_OD_DUMP_ASSESSMENTS> T_OD_DUMP_ASSESSMENTS { get; set; }
        public ICollection<T_OD_SITES> T_OD_SITESCommunityIdxNavigation { get; set; }
        public ICollection<T_OD_SITES> T_OD_SITESSiteSettingIdxNavigation { get; set; }
    }
}
