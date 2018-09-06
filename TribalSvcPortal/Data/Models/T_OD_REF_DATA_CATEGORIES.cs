﻿using System;
using System.Collections.Generic;

namespace TribalSvcPortal.Data.Models
{
    public partial class T_OD_REF_DATA_CATEGORIES
    {
        public T_OD_REF_DATA_CATEGORIES()
        {
            T_OD_REF_DATA = new HashSet<T_OD_REF_DATA>();
        }

        public string RefDataCatName { get; set; }
        public string RefDataCatDesc { get; set; }

        public ICollection<T_OD_REF_DATA> T_OD_REF_DATA { get; set; }
    }
}
