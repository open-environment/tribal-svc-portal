using System;
using System.Collections.Generic;

namespace TribalSvcPortal.Data.Models
{
    public partial class T_PRT_CLIENT_ROLES
    {
        public int CLIENT_ROLES_IDX { get; set; }
        public string CLIENT_ROLE_NAME { get; set; }
        public string CLIENT_ID { get; set; }

        public virtual T_PRT_CLIENTS CLIENT { get; set; }
    }
}
