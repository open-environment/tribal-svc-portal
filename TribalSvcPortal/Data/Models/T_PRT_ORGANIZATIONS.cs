using System;
using System.Collections.Generic;

namespace TribalSvcPortal.Data.Models
{
    public partial class T_PRT_ORGANIZATIONS
    {
        public T_PRT_ORGANIZATIONS()
        {
            T_PRT_DOCUMENTS = new HashSet<T_PRT_DOCUMENTS>();
            T_PRT_ORG_EMAIL_RULE = new HashSet<T_PRT_ORG_EMAIL_RULE>();
            T_PRT_ORG_USERS = new HashSet<T_PRT_ORG_USERS>();
            T_PRT_SITES = new HashSet<T_PRT_SITES>();
        }

        public string ORG_ID { get; set; }
        public string ORG_NAME { get; set; }
        public string WORDPRESS_URL { get; set; }
        public string ORG_SEAL { get; set; }

        public ICollection<T_PRT_DOCUMENTS> T_PRT_DOCUMENTS { get; set; }
        public ICollection<T_PRT_ORG_EMAIL_RULE> T_PRT_ORG_EMAIL_RULE { get; set; }
        public ICollection<T_PRT_ORG_USERS> T_PRT_ORG_USERS { get; set; }
        public ICollection<T_PRT_SITES> T_PRT_SITES { get; set; }
    }
}
