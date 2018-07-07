using System;
using System.Collections.Generic;

namespace TribalSvcPortal.Data.Models
{
    public partial class TPrtTenantUsers
    {
        public TPrtTenantUsers()
        {
            TPrtTenantUserClient = new HashSet<TPrtTenantUserClient>();
        }

        public int TenantUserIdx { get; set; }
        public string TenantId { get; set; }
        public string Id { get; set; }
        public bool TenantAdminInd { get; set; }
        public string StatusInd { get; set; }
        public string CreateUserId { get; set; }
        public DateTime? CreateDt { get; set; }
        public string ModifyUserId { get; set; }
        public DateTime? ModifyDt { get; set; }

        public TPrtTenants Tenant { get; set; }
        public ICollection<TPrtTenantUserClient> TPrtTenantUserClient { get; set; }
    }
}
