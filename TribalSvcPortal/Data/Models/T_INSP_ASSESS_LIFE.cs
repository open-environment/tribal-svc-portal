using System;
using System.Collections.Generic;

namespace TribalSvcPortal.Data.Models
{
    public partial class T_INSP_ASSESS_LIFE
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
        public string AddressVisible { get; set; }
        public string Floor { get; set; }
        public string Walls { get; set; }
        public string Ceiling { get; set; }
        public string Stairs { get; set; }
        public string Panic { get; set; }
        public string Closures { get; set; }
        public string Notes { get; set; }
        public string BuildingType { get; set; }
        public string Fire_FireAlarm { get; set; }
        public string Fire_Sprinklers1 { get; set; }
        public string Fire_Sprinklers5 { get; set; }
        public string Fire_Plate { get; set; }
        public string Fire_EmergLight { get; set; }
        public string Fire_Exit { get; set; }
        public string Fire_KitchenHood { get; set; }
        public string Fire_HoodSup { get; set; }
        public string Fire_EvacDrill { get; set; }
        public string Fire_Evac { get; set; }
        public string Fire_Storage { get; set; }
        public string Fire_Piled { get; set; }
        public string Fire_Food { get; set; }
        public string Fire_Hazard { get; set; }
        public string Fire_SDS { get; set; }
        public string Fire_Extinguisher { get; set; }
        public string Fire_ExtinAnnual { get; set; }
        public string Fire_Notes { get; set; }
        public string Plum_Water { get; set; }
        public string Plum_Drain { get; set; }
        public string Plum_Sink { get; set; }
        public string Plum_Toilet { get; set; }
        public string Plum_Tub { get; set; }
        public string Plum_Dish { get; set; }
        public string Plum_Heater { get; set; }
        public string Plum_Backflow { get; set; }
        public string Plum_Notes { get; set; }
        public string Elec_Hazard { get; set; }
        public string Elec_Panel { get; set; }
        public string Elec_Label { get; set; }
        public string Elec_Obstruct { get; set; }
        public string Elec_Outlet { get; set; }
        public string Elec_Lighting { get; set; }
        public string Elec_Surge { get; set; }
        public string Elec_Ground { get; set; }
        public string Elec_Sign { get; set; }
        public string Elec_Notes { get; set; }
        public string Mech_Filters { get; set; }
        public string Mech_Register { get; set; }
        public string Mech_Equip { get; set; }
        public string Mech_VentAir { get; set; }
        public string Mech_VentSyst { get; set; }
        public string Mech_Pipe { get; set; }
        public string Mech_Therm { get; set; }
        public string Mech_Duct { get; set; }
        public string Mech_Notes { get; set; }
        public string MODIFY_USER_ID { get; set; }
        public DateTime? MODIFY_DT { get; set; }

        public virtual T_INSP_SITES SITE_IDXNavigation { get; set; }
    }
}
