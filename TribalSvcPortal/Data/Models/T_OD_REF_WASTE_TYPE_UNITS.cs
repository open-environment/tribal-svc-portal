using System;
using System.Collections.Generic;

namespace TribalSvcPortal.Data.Models
{
    public partial class T_OD_REF_WASTE_TYPE_UNITS
    {
        public Guid REF_WASTE_TYPE_IDX { get; set; }
        public Guid UNIT_MSR_IDX { get; set; }
        public string MODIFY_USER_ID { get; set; }
        public DateTime? MODIFY_DT { get; set; }

        public T_OD_REF_WASTE_TYPE REF_WASTE_TYPE_IDXNavigation { get; set; }
    }
}
