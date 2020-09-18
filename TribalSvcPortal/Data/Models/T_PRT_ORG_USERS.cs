using System;
using System.Collections.Generic;

namespace TribalSvcPortal.Data.Models
{
    public partial class T_PRT_ORG_USERS
    {
        public T_PRT_ORG_USERS()
        {
            T_PRT_ORG_USER_CLIENT = new HashSet<T_PRT_ORG_USER_CLIENT>();
        }

        public int ORG_USER_IDX { get; set; }
        public string ORG_ID { get; set; }
        public string Id { get; set; }
        public string ACCESS_LEVEL { get; set; }
        public string STATUS_IND { get; set; }
        public string CREATE_USER_ID { get; set; }
        public DateTime? CREATE_DT { get; set; }
        public string MODIFY_USER_ID { get; set; }
        public DateTime? MODIFY_DT { get; set; }
        public int? WordPressUserId { get; set; }
        public T_PRT_ORGANIZATIONS ORG_ { get; set; }
        public ICollection<T_PRT_ORG_USER_CLIENT> T_PRT_ORG_USER_CLIENT { get; set; }
    }
}
