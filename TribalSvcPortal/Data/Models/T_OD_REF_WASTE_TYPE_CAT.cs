using System;
using System.Collections.Generic;

namespace TribalSvcPortal.Data.Models
{
    public partial class T_OD_REF_WASTE_TYPE_CAT
    {
        public T_OD_REF_WASTE_TYPE_CAT()
        {
            T_OD_REF_WASTE_TYPE_CAT_CLEANUP = new HashSet<T_OD_REF_WASTE_TYPE_CAT_CLEANUP>();
        }

        public string REF_WASTE_TYPE_CAT { get; set; }

        public ICollection<T_OD_REF_WASTE_TYPE_CAT_CLEANUP> T_OD_REF_WASTE_TYPE_CAT_CLEANUP { get; set; }
    }
}
