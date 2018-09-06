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

        public string ShareType { get; set; }
        public string ShareDesc { get; set; }
        public bool? ActInd { get; set; }

        public ICollection<T_PRT_DOCUMENTS> T_PRT_DOCUMENTS { get; set; }
    }
}
