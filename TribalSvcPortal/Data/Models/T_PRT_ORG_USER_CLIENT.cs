using System;
using System.Collections.Generic;

namespace TribalSvcPortal.Data.Models
{
    public partial class T_PRT_ORG_USER_CLIENT
    {
        public int ORG_USER_CLIENT_IDX { get; set; }
        public int ORG_USER_IDX { get; set; }
        public string CLIENT_ID { get; set; }
        public bool ADMIN_IND { get; set; }
        public string STATUS_IND { get; set; }
        public string CREATE_USER_ID { get; set; }
        public DateTime? CREATE_DT { get; set; }
        public string MODIFY_USER_ID { get; set; }
        public DateTime? MODIFY_DT { get; set; }

        public T_PRT_CLIENTS CLIENT_ { get; set; }
        public T_PRT_ORG_USERS ORG_USER_IDXNavigation { get; set; }
    }
}
