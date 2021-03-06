﻿using System;
using System.Collections.Generic;

namespace TribalSvcPortal.Data.Models
{
    public partial class T_OD_ASSESSMENT_CONTENT
    {
        public Guid ASSESSMENT_CONTENT_IDX { get; set; }
        public Guid ASSESSMENT_IDX { get; set; }
        public Guid REF_WASTE_TYPE_IDX { get; set; }
        public decimal? WASTE_AMT { get; set; }
        public Guid? UNIT_MSR_IDX { get; set; }
        public Guid? WASTE_DISPOSAL_METHOD { get; set; }
        public string WASTE_DISPOSAL_DIST { get; set; }
        public decimal? WASTE_WEIGHT_LBS { get; set; }

        public T_OD_ASSESSMENTS ASSESSMENT_IDXNavigation { get; set; }
        public T_OD_REF_WASTE_TYPE REF_WASTE_TYPE_IDXNavigation { get; set; }
    }
}
