using System;
using System.Collections.Generic;

namespace TribalSvcPortal.Data.Models
{
    public partial class T_PRT_REF_UNITS
    {
        public T_PRT_REF_UNITS()
        {
            T_OD_ASSESSMENT_CONTENT = new HashSet<T_OD_ASSESSMENT_CONTENT>();
            T_OD_REF_WASTE_TYPE = new HashSet<T_OD_REF_WASTE_TYPE>();
            T_OD_REF_WASTE_TYPE_UNITS = new HashSet<T_OD_REF_WASTE_TYPE_UNITS>();
        }

        public Guid UNIT_MSR_IDX { get; set; }
        public string UNIT_MSR_CD { get; set; }
        public string UNIT_MSR_CAT { get; set; }
        public bool? STD_UNIT_IND { get; set; }
        public decimal? UNIT_CONVERSION { get; set; }
        public string CREATE_USER_ID { get; set; }
        public DateTime? CREATE_DT { get; set; }
        public string MODIFY_USER_ID { get; set; }
        public DateTime? MODIFY_DT { get; set; }

        public virtual ICollection<T_OD_ASSESSMENT_CONTENT> T_OD_ASSESSMENT_CONTENT { get; set; }
        public virtual ICollection<T_OD_REF_WASTE_TYPE> T_OD_REF_WASTE_TYPE { get; set; }
        public virtual ICollection<T_OD_REF_WASTE_TYPE_UNITS> T_OD_REF_WASTE_TYPE_UNITS { get; set; }
    }
}
