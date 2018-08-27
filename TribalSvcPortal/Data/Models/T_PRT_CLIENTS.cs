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

        public string ClientId { get; set; }
        public string ClientName { get; set; }
        public string ClientGrantType { get; set; }
        public string ClientRedirectUri { get; set; }
        public string ClientPostLogoutUri { get; set; }
        public string ClientUrl { get; set; }
        public byte[] ClientImage { get; set; }
        public bool ClientLocalInd { get; set; }

        public ICollection<T_PRT_CLIENT_ROLES> T_PRT_CLIENT_ROLES { get; set; }
        public ICollection<T_PRT_ORG_USER_CLIENT> T_PRT_ORG_USER_CLIENT { get; set; }
    }
}
