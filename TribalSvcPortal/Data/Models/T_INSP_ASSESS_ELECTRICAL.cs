using System;
using System.Collections.Generic;

namespace TribalSvcPortal.Data.Models
{
    public partial class T_INSP_ASSESS_ELECTRICAL
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
        public string Gen_Site { get; set; }
        public string Gen_Hazard { get; set; }
        public string Gen_Address { get; set; }
        public string Elec_Rust { get; set; }
        public string Elec_Label { get; set; }
        public string Elec_Space { get; set; }
        public string Wiring_Secured { get; set; }
        public string Wiring_Damage { get; set; }
        public string Wiring_Separate { get; set; }
        public string Outlet_Func { get; set; }
        public string Outlet_GFCI { get; set; }
        public string Outlet_Tamper { get; set; }
        public string Light_Fixtures { get; set; }
        public string Light_Exit { get; set; }
        public string Light_Ill { get; set; }
        public string Ground_Connections { get; set; }
        public string Ground_Corrosion { get; set; }
        public string Ground_Electrode { get; set; }
        public string Surge_Protectors { get; set; }
        public string Surge_Func { get; set; }
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
