﻿using System;
using System.Collections.Generic;

namespace TribalSvcPortal.Data.Models
{
    public partial class T_OD_REF_WASTE_TYPE
    {
        public T_OD_REF_WASTE_TYPE()
        {
            T_OD_ASSESSMENT_CONTENT = new HashSet<T_OD_ASSESSMENT_CONTENT>();
            T_OD_REF_WASTE_TYPE_UNITS = new HashSet<T_OD_REF_WASTE_TYPE_UNITS>();
        }

        public Guid REF_WASTE_TYPE_IDX { get; set; }
        public string REF_WASTE_TYPE_NAME { get; set; }
        public string REF_WASTE_TYPE_CAT { get; set; }
        public int? REF_WASTE_HAZFACT_SUBSCORE { get; set; }
        public Guid? DEFAULT_UNIT_MSR_IDX { get; set; }
        public decimal? DENSITY_LBS_CUYD { get; set; }
        public decimal? DENSITY_LBS_UNIT { get; set; }
        public int? TRANSPORT_AMT_PER_LOAD { get; set; }
        public string MODIFY_USER_ID { get; set; }
        public DateTime? MODIFY_DT { get; set; }

        public ICollection<T_OD_ASSESSMENT_CONTENT> T_OD_ASSESSMENT_CONTENT { get; set; }
        public ICollection<T_OD_REF_WASTE_TYPE_UNITS> T_OD_REF_WASTE_TYPE_UNITS { get; set; }
    }
}
