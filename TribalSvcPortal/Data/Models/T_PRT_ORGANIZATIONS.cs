using System;
using System.Collections.Generic;

namespace TribalSvcPortal.Data.Models
{
    public partial class T_PRT_ORGANIZATIONS
    {
        public T_PRT_ORGANIZATIONS()
        {
            T_PRT_DOCUMENTS = new HashSet<T_PRT_DOCUMENTS>();
            T_PRT_ORG_USERS = new HashSet<T_PRT_ORG_USERS>();
            T_PRT_SITES = new HashSet<T_PRT_SITES>();
        }

        public string OrgId { get; set; }
        public string OrgName { get; set; }

        public ICollection<T_PRT_DOCUMENTS> T_PRT_DOCUMENTS { get; set; }
        public ICollection<T_PRT_ORG_USERS> T_PRT_ORG_USERS { get; set; }
        public ICollection<T_PRT_SITES> T_PRT_SITES { get; set; }
    }
}
