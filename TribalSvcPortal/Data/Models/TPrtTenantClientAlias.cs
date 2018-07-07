using System;
using System.Collections.Generic;

namespace TribalSvcPortal.Data.Models
{
    public partial class TPrtTenantClientAlias
    {
        public string TenantId { get; set; }
        public string ClientId { get; set; }
        public string TenantClientAlias { get; set; }
    }
}
