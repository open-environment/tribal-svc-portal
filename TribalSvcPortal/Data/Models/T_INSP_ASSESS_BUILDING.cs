using System;
using System.Collections.Generic;

namespace TribalSvcPortal.Data.Models
{
    public partial class T_INSP_ASSESS_BUILDING
    {
        public Guid ASSESSMENT_IDX { get; set; }
        public Guid SITE_IDX { get; set; }
        public DateTime ASSESSMENT_DT { get; set; }
        public string ASSESSED_BY { get; set; }
        public int? OBJECTID { get; set; }
        public Guid? GlobalID { get; set; }
        public string Title { get; set; }
        public string Walkway { get; set; }
        public string Windows { get; set; }
        public string Doors { get; set; }
        public string Roof { get; set; }
        public string Foundation { get; set; }
        public string Exterior { get; set; }
        public string Floor { get; set; }
        public string Walls { get; set; }
        public string Ceiling { get; set; }
        public string Stairs { get; set; }
        public string Panic { get; set; }
        public string Closures { get; set; }
        public string Notes { get; set; }
        public string BuildingType { get; set; }

        public string MODIFY_USER_ID { get; set; }
        public DateTime? MODIFY_DT { get; set; }


        public virtual T_INSP_SITES SITE_IDXNavigation { get; set; }
    }
}
