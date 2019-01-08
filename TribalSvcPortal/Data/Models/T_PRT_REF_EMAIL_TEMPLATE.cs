using System;
using System.Collections.Generic;

namespace TribalSvcPortal.Data.Models
{
    public partial class T_PRT_REF_EMAIL_TEMPLATE
    {
        public int EMAIL_TEMPLATE_ID { get; set; }
        public string EMAIL_TEMPLATE_NAME { get; set; }
        public string EMAIL_TEMPLATE_DESC { get; set; }
        public string SUBJ { get; set; }
        public string MSG { get; set; }
        public string MODIFY_USER_ID { get; set; }
        public DateTime? MODIFY_DT { get; set; }
    }
}
