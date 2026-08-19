using System;
using System.Collections.Generic;

namespace TribalSvcPortal.Data.Models
{
    public partial class T_INSP_ASSESS_MECHANICAL
    {
        public Guid ASSESSMENT_IDX { get; set; }
        public Guid SITE_IDX { get; set; }
        public DateTime ASSESSMENT_DT { get; set; }
        public string ASSESSED_BY { get; set; }
        public int? OBJECTID { get; set; }
        public Guid? GlobalID { get; set; }
        public string Title { get; set; }
        public string Contact { get; set; }
        public string Compliance { get; set; }
        public string HVAC_Clean { get; set; }
        public string HVAC_Filters { get; set; }
        public string HVAC_Therm { get; set; }
        public string Duct_Leak { get; set; }
        public string Duct_Insulation { get; set; }
        public string Duct_Obstruction { get; set; }
        public string Piping_Leaks { get; set; }
        public string Piping_Insulation { get; set; }
        public string Piping_Valves { get; set; }
        public string Equip_Operational { get; set; }
        public string Equip_Doc { get; set; }
        public string Equip_Guards { get; set; }
        public string Vent_Airflow { get; set; }
        public string Vent_Exhaust { get; set; }
        public string Vent_Air { get; set; }
        public string Comp_Codes { get; set; }
        public string Comp_Signage { get; set; }
        public string Comp_Records { get; set; }
        public string Corrective { get; set; }
        public string BuildingType { get; set; }
        public string MODIFY_USER_ID { get; set; }
        public DateTime? MODIFY_DT { get; set; }

        public virtual T_INSP_SITES SITE_IDXNavigation { get; set; }
    }
}
