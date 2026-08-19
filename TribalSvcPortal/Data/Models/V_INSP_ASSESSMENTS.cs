using System;
using System.Collections.Generic;

namespace TribalSvcPortal.Data.Models
{
    public partial class V_INSP_ASSESSMENTS
    {
        public int? YR { get; set; }
        public Guid SITE_IDX { get; set; }
        public string SITE_ID { get; set; }
        public string SITE_NAME { get; set; }
        public string INSP_TYP { get; set; }
        public Guid ASSESSMENT_IDX { get; set; }
        public int? OBJECTID { get; set; }
        public DateTime ASSESSMENT_DT { get; set; }
        public string ASSESSED_BY { get; set; }
        public string BUILDING_TYPE { get; set; }
    }
}
