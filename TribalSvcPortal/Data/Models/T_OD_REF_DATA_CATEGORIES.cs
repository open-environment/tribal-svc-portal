using System;
using System.Collections.Generic;

namespace TribalSvcPortal.Data.Models
{
    public partial class T_OD_REF_DATA_CATEGORIES
    {
        public T_OD_REF_DATA_CATEGORIES()
        {
            T_OD_REF_DATA = new HashSet<T_OD_REF_DATA>();
        }

        public string REF_DATA_CAT_NAME { get; set; }
        public string REF_DATA_CAT_DESC { get; set; }

        public ICollection<T_OD_REF_DATA> T_OD_REF_DATA { get; set; }
    }
}
