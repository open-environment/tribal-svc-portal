using System;
using System.Collections.Generic;

namespace TribalSvcPortal.Data.Models
{
    public partial class TPrtTenantUserClient
    {
        public int TenantUserClientIdx { get; set; }
        public int TenantUserIdx { get; set; }
        public string ClientId { get; set; }
        public bool AdminInd { get; set; }
        public string StatusInd { get; set; }
        public string CreateUserId { get; set; }
        public DateTime? CreateDt { get; set; }
        public string ModifyUserId { get; set; }
        public DateTime? ModifyDt { get; set; }

        public TPrtClients Client { get; set; }
        public TPrtTenantUsers TenantUserIdxNavigation { get; set; }
    }
}
