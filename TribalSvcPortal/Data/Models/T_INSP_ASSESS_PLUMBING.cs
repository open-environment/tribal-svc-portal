using System;
using System.Collections.Generic;

namespace TribalSvcPortal.Data.Models
{
    public partial class T_INSP_ASSESS_PLUMBING
    {
        public Guid ASSESSMENT_IDX { get; set; }
        public Guid SITE_IDX { get; set; }
        public DateTime ASSESSMENT_DT { get; set; }
        public string ASSESSED_BY { get; set; }
        public int? OBJECTID { get; set; }
        public Guid? GlobalID { get; set; }
        public string Title { get; set; }
        public string WaterPSI { get; set; }
        public string Meter_Location { get; set; }
        public string Supply_Lines { get; set; }
        public string Drains { get; set; }
        public string Sewer_Loc { get; set; }
        public string Sink_Faucet { get; set; }
        public string Toilet_Urinal { get; set; }
        public string Shower_Tub { get; set; }
        public string Dishwasher { get; set; }
        public string Water_Heater { get; set; }
        public string Heater_Type { get; set; }
        public string Backflow { get; set; }
        public string Repairs { get; set; }
        public string MODIFY_USER_ID { get; set; }
        public DateTime? MODIFY_DT { get; set; }

        public virtual T_INSP_SITES SITE_IDXNavigation { get; set; }
    }
}
