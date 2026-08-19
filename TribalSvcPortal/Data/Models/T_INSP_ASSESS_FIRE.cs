using System;
using System.Collections.Generic;

namespace TribalSvcPortal.Data.Models
{
    public partial class T_INSP_ASSESS_FIRE
    {
        public Guid ASSESSMENT_IDX { get; set; }
        public Guid SITE_IDX { get; set; }
        public DateTime ASSESSMENT_DT { get; set; }
        public string ASSESSED_BY { get; set; }
        public int? OBJECTID { get; set; }
        public Guid? GlobalID { get; set; }
        public string Title { get; set; }
        public string FacContact { get; set; }
        public string Compliance { get; set; }
        public string Corrective { get; set; }
        public string Doc_FireAlarm { get; set; }
        public string Doc_FireSprinkler5 { get; set; }
        public string Doc_FireSprinklerAnn { get; set; }
        public string Doc_EmergPwr { get; set; }
        public string Doc_EmergLight { get; set; }
        public string Doc_KitchenHoodSys { get; set; }
        public string Doc_Generator { get; set; }
        public string Doc_EvacDrill { get; set; }
        public string FireDept_Address { get; set; }
        public string FireDept_AccessKeys { get; set; }
        public string FireDept_Access { get; set; }
        public string FireDept_Valves { get; set; }
        public string Egress_Path { get; set; }
        public string Egress_Signs { get; set; }
        public string Egress_Latch { get; set; }
        public string Egress_DoorsOpen { get; set; }
        public string Egress_DoorsKey { get; set; }
        public string Egress_Evac { get; set; }
        public string Egress_Storage { get; set; }
        public string Storage_Housekeeping { get; set; }
        public string Storage_Cylinders { get; set; }
        public string Storage_Sprinklers { get; set; }
        public string Storage_Combust { get; set; }
        public string Storage_OilRags { get; set; }
        public string Storage_Food { get; set; }
        public string ELec_Covers { get; set; }
        public string Elec_Panels { get; set; }
        public string Elec_Clearance { get; set; }
        public string Elec_Cords { get; set; }
        public string Elec_NoCord { get; set; }
        public string Elec_Appliances { get; set; }
        public string Elec_Adapters { get; set; }
        public string Elec_GFI { get; set; }
        public string Elec_Heater { get; set; }
        public string Hazmat_Quantities { get; set; }
        public string Hazmat_Liquids { get; set; }
        public string Hazmat_Shelving { get; set; }
        public string Hazmat_Tanks { get; set; }
        public string Hazmat_EmptyTanks { get; set; }
        public string Hazmat_SDS { get; set; }
        public string Hazmat_704 { get; set; }
        public string Fuel_Valves { get; set; }
        public string Fuel_Extinguisher { get; set; }
        public string Fuel_Signs { get; set; }
        public string Fuel_Materials { get; set; }
        public string Vent_Caps { get; set; }
        public string Vent_PullStation { get; set; }
        public string Vent_Exhaust { get; set; }
        public string Vent_GasValve { get; set; }
        public string Vent_Gauge { get; set; }
        public string Vent_Hydro { get; set; }
        public string Vent_BrandDate { get; set; }
        public string Vent_Cleanliness { get; set; }
        public string Vent_Extinguishers { get; set; }
        public DateTime? Vent_ServiceDate { get; set; }
        public string FireExt_Doc { get; set; }
        public string FireExt_Mounted { get; set; }
        public string FireExt_Collar { get; set; }
        public string FireExt_Sys { get; set; }
        public string Risers_Inspected { get; set; }
        public string Risers_Tagged { get; set; }
        public string Risers_Sprinkler { get; set; }
        public string Risers_Deficiencies { get; set; }
        public string Risers_Plate { get; set; }
        public string Risers_Gauges { get; set; }
        public string Sprinkler_Cond { get; set; }
        public string Ceiling_Type { get; set; }
        public string Ceiling_Condition { get; set; }
        public string AED { get; set; }
        public DateTime? AED_Expiration { get; set; }
        public string MODIFY_USER_ID { get; set; }
        public DateTime? MODIFY_DT { get; set; }
        public string Facility_Use { get; set; }

        public virtual T_INSP_SITES SITE_IDXNavigation { get; set; }
    }
}
