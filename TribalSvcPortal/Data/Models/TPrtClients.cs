using System;
using System.Collections.Generic;

namespace TribalSvcPortal.Data.Models
{
    public partial class TPrtClients
    {
        public TPrtClients()
        {
            TPrtClientRoles = new HashSet<TPrtClientRoles>();
            TPrtTenantUserClient = new HashSet<TPrtTenantUserClient>();
        }

        public string ClientId { get; set; }
        public string ClientName { get; set; }
        public string ClientGrantType { get; set; }
        public string ClientRedirectUri { get; set; }
        public string ClientPostLogoutUri { get; set; }
        public string ClientUrl { get; set; }
        public byte[] ClientImage { get; set; }

        public ICollection<TPrtClientRoles> TPrtClientRoles { get; set; }
        public ICollection<TPrtTenantUserClient> TPrtTenantUserClient { get; set; }
    }
}
