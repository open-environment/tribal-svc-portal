using System;
using System.Collections.Generic;

namespace TribalSvcPortal.Data.Models
{
    public partial class T_PRT_REF_DOC_STATUS_TYPE
    {
        public T_PRT_REF_DOC_STATUS_TYPE()
        {
            T_PRT_DOCUMENTS = new HashSet<T_PRT_DOCUMENTS>();
        }

        public string DocStatusType { get; set; }
        public bool ActInd { get; set; }
        public string CreateUserId { get; set; }
        public DateTime? CreateDt { get; set; }
        public string ModifyUserId { get; set; }
        public DateTime? ModifyDt { get; set; }

        public ICollection<T_PRT_DOCUMENTS> T_PRT_DOCUMENTS { get; set; }
    }
}
