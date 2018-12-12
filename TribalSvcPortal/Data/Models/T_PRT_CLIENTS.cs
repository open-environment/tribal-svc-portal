using System;
using System.Collections.Generic;

namespace TribalSvcPortal.Data.Models
{
    public partial class T_PRT_CLIENTS
    {
        public T_PRT_CLIENTS()
        {
            T_PRT_CLIENT_ROLES = new HashSet<T_PRT_CLIENT_ROLES>();
            T_PRT_ORG_USER_CLIENT = new HashSet<T_PRT_ORG_USER_CLIENT>();
        }

        public string CLIENT_ID { get; set; }
        public string CLIENT_NAME { get; set; }
        public string CLIENT_GRANT_TYPE { get; set; }
        public string CLIENT_REDIRECT_URI { get; set; }
        public string CLIENT_POST_LOGOUT_URI { get; set; }
        public string CLIENT_URL { get; set; }
        public byte[] CLIENT_IMAGE { get; set; }
        public bool CLIENT_LOCAL_IND { get; set; }

        public ICollection<T_PRT_CLIENT_ROLES> T_PRT_CLIENT_ROLES { get; set; }
        public ICollection<T_PRT_ORG_USER_CLIENT> T_PRT_ORG_USER_CLIENT { get; set; }
    }
}
