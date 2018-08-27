using System;
using System.Collections.Generic;

namespace TribalSvcPortal.Data.Models
{
    public partial class T_PRT_ORG_USERS
    {
        public T_PRT_ORG_USERS()
        {
            T_PRT_ORG_USER_CLIENT = new HashSet<T_PRT_ORG_USER_CLIENT>();
        }

        public int OrgUserIdx { get; set; }
        public string OrgId { get; set; }
        public string Id { get; set; }
        public bool OrgAdminInd { get; set; }
        public string StatusInd { get; set; }
        public string CreateUserId { get; set; }
        public DateTime? CreateDt { get; set; }
        public string ModifyUserId { get; set; }
        public DateTime? ModifyDt { get; set; }

        public T_PRT_ORGANIZATIONS Org { get; set; }
        public ICollection<T_PRT_ORG_USER_CLIENT> T_PRT_ORG_USER_CLIENT { get; set; }
    }
}
