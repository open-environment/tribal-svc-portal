using System;
using System.Collections.Generic;

namespace TribalSvcPortal.Data.Models
{
    public partial class TPrtClientRoles
    {
        public int ClientRolesIdx { get; set; }
        public string ClientRoleName { get; set; }
        public string ClientId { get; set; }

        public TPrtClients Client { get; set; }
    }
}
