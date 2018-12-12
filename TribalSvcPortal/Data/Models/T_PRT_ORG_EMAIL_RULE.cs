using System;
using System.Collections.Generic;

namespace TribalSvcPortal.Data.Models
{
    public partial class T_PRT_ORG_EMAIL_RULE
    {
        public string ORG_ID { get; set; }
        public string EMAIL_STRING { get; set; }
        public int? MODIFY_USERIDX { get; set; }
        public DateTime? MODIFY_DT { get; set; }

        public T_PRT_ORGANIZATIONS ORG_ { get; set; }
    }
}
