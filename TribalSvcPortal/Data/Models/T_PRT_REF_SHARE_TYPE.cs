using System;
using System.Collections.Generic;

namespace TribalSvcPortal.Data.Models
{
    public partial class T_PRT_REF_SHARE_TYPE
    {
        public T_PRT_REF_SHARE_TYPE()
        {
            T_PRT_DOCUMENTS = new HashSet<T_PRT_DOCUMENTS>();
        }

        public string SHARE_TYPE { get; set; }
        public string SHARE_DESC { get; set; }
        public bool? ACT_IND { get; set; }

        public ICollection<T_PRT_DOCUMENTS> T_PRT_DOCUMENTS { get; set; }
    }
}
