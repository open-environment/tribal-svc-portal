using System;
using System.Collections.Generic;

namespace TribalSvcPortal.Data.Models
{
    public partial class T_PRT_CLIENT_ROLES
    {
        public int ClientRolesIdx { get; set; }
        public string ClientRoleName { get; set; }
        public string ClientId { get; set; }

        public T_PRT_CLIENTS Client { get; set; }
    }
}
