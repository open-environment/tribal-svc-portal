using System;
using System.Collections.Generic;

namespace TribalSvcPortal.Data.Models
{
    public partial class TPrtTenants
    {
        public TPrtTenants()
        {
            TPrtTenantUsers = new HashSet<TPrtTenantUsers>();
        }

        public string TenantId { get; set; }
        public string TenantName { get; set; }

        public ICollection<TPrtTenantUsers> TPrtTenantUsers { get; set; }
    }
}
