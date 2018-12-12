using System;
using System.Collections.Generic;

namespace TribalSvcPortal.Data.Models
{
    public partial class T_PRT_REF_DOC_TYPE
    {
        public T_PRT_REF_DOC_TYPE()
        {
            T_PRT_DOCUMENTS = new HashSet<T_PRT_DOCUMENTS>();
        }

        public string DOC_TYPE { get; set; }
        public string DOC_TYPE_DESC { get; set; }
        public bool ACT_IND { get; set; }
        public string CREATE_USER_ID { get; set; }
        public DateTime? CREATE_DT { get; set; }
        public string MODIFY_USER_ID { get; set; }
        public DateTime? MODIFY_DT { get; set; }

        public ICollection<T_PRT_DOCUMENTS> T_PRT_DOCUMENTS { get; set; }
    }
}
