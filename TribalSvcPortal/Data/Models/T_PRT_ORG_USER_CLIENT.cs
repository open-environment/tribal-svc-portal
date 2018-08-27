using System;
using System.Collections.Generic;

namespace TribalSvcPortal.Data.Models
{
    public partial class T_PRT_ORG_USER_CLIENT
    {
        public int OrgUserClientIdx { get; set; }
        public int OrgUserIdx { get; set; }
        public string ClientId { get; set; }
        public bool AdminInd { get; set; }
        public string StatusInd { get; set; }
        public string CreateUserId { get; set; }
        public DateTime? CreateDt { get; set; }
        public string ModifyUserId { get; set; }
        public DateTime? ModifyDt { get; set; }

        public T_PRT_CLIENTS Client { get; set; }
        public T_PRT_ORG_USERS OrgUserIdxNavigation { get; set; }
    }
}
