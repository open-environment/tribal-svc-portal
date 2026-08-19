using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TribalSvcPortal.AppLogic.DataAccessLayer;
using TribalSvcPortal.Data.Models;
using TribalSvcPortal.AppLogic.BusinessLogicLayer;
using System;
using System.Linq;
using iTextSharp.text.pdf;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics.Metrics;
using TribalSvcPortal.ViewModels.InspectionViewModels;
//using DocumentFormat.OpenXml.Drawing;

namespace TribalSvcPortal.Controllers
{
    [Authorize]
    public class InspectionController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IDbPortal _DbPortal;
        private readonly IDbInspection _DbInspection;
        private readonly IHostingEnvironment _hostingEnvironment;
            
        public InspectionController(
            UserManager<ApplicationUser> userManager,
            IDbPortal DbPortal,
            IDbInspection DbInspection,
            IHostingEnvironment hostingEnvironment)
        {
            _userManager = userManager;
            _DbPortal = DbPortal;
            _DbInspection = DbInspection;
            _hostingEnvironment = hostingEnvironment;
        }


        public IActionResult Index(string selAssessType, string str, string bldg)
        {
            var model = new ViewModels.InspectionViewModels.IndexViewModel();
            model.ddl_AssessType = _DbInspection.get_ddl_AssessType();
            model.selAssessType = selAssessType;
            model.str = str;
            model.bldg = bldg;
            model.Assessments = _DbInspection.GetV_INSP_ASSESSMENTS(selAssessType, str, bldg);
            return View(model);
        }


        [HttpPost]
        public IActionResult ImportPlumbing() 
        {
            string _UserIDX = _userManager.GetUserId(User);

            var appPath = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            var _esriHelper = new esriHelper(_DbPortal);
            var _PlumbingResults = _esriHelper.queryPlumbing(appPath, null, new string[] { "*" });

            foreach (var _PlumbingResult in (_PlumbingResults ?? Enumerable.Empty<PlumbingInspections>()))
            {
                //get PRT_SITE_IDX if there is one by looking up from Facility ID
                var _existingPRT_SITE = _DbPortal.GetT_PRT_SITES_BySITEID(_PlumbingResult.FacilitlyID);

                //insert into T_PRT_SITES
                Guid? _PrtSite = _DbPortal.InsertUpdateT_PRT_SITES(_existingPRT_SITE?.SITE_IDX, "MCNCREEK", _PlumbingResult.FacilityName, null, null, null, null, _UserIDX, null, null, null, null, _PlumbingResult.County, _PlumbingResult.FacilitlyID);

                //insert into T_INSP_SITES
                if (_PrtSite != null)
                {
                    DateTime? editDate = _PlumbingResult.EditDate is long ms ? DateTimeOffset.FromUnixTimeMilliseconds(ms).UtcDateTime : null;
                    _DbInspection.InsertUpdateT_INSP_SITES(_PrtSite.Value, _PlumbingResult.Editor, editDate?? System.DateTime.Now);

                    //insert into T_INSP_ASSESS_PLUMBING
                    DateTime? assessDate = _PlumbingResult.Date.ConvertOrDefault<long>() is long ms2 ? DateTimeOffset.FromUnixTimeMilliseconds(ms2).UtcDateTime : null;
                    _DbInspection.InsertUpdateT_INSP_ASSESS_PLUMBING(null, _PrtSite, assessDate, _PlumbingResult.Inspector, _PlumbingResult.OBJECTID, _PlumbingResult.GlobalID,
                        _PlumbingResult.Title, _PlumbingResult.WaterPSI, _PlumbingResult.Meter_Location, _PlumbingResult.Supply_Lines, _PlumbingResult.Drains, _PlumbingResult.Sewer_Loc,
                        _PlumbingResult.Sink_Faucet, _PlumbingResult.Toilet_Urinal, _PlumbingResult.Shower_Tub, _PlumbingResult.Dishwasher, _PlumbingResult.Water_Heater,
                        _PlumbingResult.Heater_Type, _PlumbingResult.Backflow, _PlumbingResult.Repairs);

                }

            }
            return RedirectToAction("Index", new { selAssessType = "PLUMBING" }); 
        }


        [HttpPost]
        public IActionResult ImportElectrical()
        {
            string _UserIDX = _userManager.GetUserId(User);

            var appPath = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            var _esriHelper = new esriHelper(_DbPortal);


            //FIRST QUERY THE ELECTRICAL INSPECTIONS
            var _ElectResults = _esriHelper.queryElectrical(appPath, null, new string[] { "*" });

            foreach (var _ElectResult in (_ElectResults ?? Enumerable.Empty<ElectricalInspections>()))
            {
                //get PRT_SITE_IDX if there is one by looking up from Facility ID
                var _existingPRT_SITE = _DbPortal.GetT_PRT_SITES_BySITEID(_ElectResult.FacilityID);

                //insert into T_PRT_SITES
                Guid? _PrtSite = _DbPortal.InsertUpdateT_PRT_SITES(_existingPRT_SITE?.SITE_IDX, "MCNCREEK", _ElectResult.FacilityName, null, null, null, null, _UserIDX, null, null, null, null, _ElectResult.County, _ElectResult.FacilityID);

                //insert into T_INSP_SITES
                if (_PrtSite != null)
                {
                    DateTime? editDate = _ElectResult.EditDate is long ms ? DateTimeOffset.FromUnixTimeMilliseconds(ms).UtcDateTime : null;
                    _DbInspection.InsertUpdateT_INSP_SITES(_PrtSite.Value, _ElectResult.Editor, editDate ?? System.DateTime.Now);

                    //insert into T_INSP_ASSESS_ELECTRICAL
                    DateTime? assessDate = _ElectResult.Date.ConvertOrDefault<long>() is long ms2 ? DateTimeOffset.FromUnixTimeMilliseconds(ms2).UtcDateTime : null;
                    _DbInspection.InsertUpdateT_INSP_ASSESS_ELECTRICAL(null, _PrtSite, assessDate, _ElectResult.Inspector, _ElectResult.OBJECTID, _ElectResult.GlobalID,
                        _ElectResult.Title, _ElectResult.Contact, _ElectResult.Compliance, _ElectResult.Gen_Site, _ElectResult.Gen_Hazard, _ElectResult.Gen_Address, 
                        _ElectResult.Elec_Rust, _ElectResult.Elec_Label, _ElectResult.Elec_Space, _ElectResult.Wiring_Secured, _ElectResult.Wiring_Damage, _ElectResult.Wiring_Separate, 
                        _ElectResult.Outlet_Func, _ElectResult.Outlet_GFCI, _ElectResult.Outlet_Tamper, _ElectResult.Light_Fixtures, _ElectResult.Light_Exit, _ElectResult.Light_Ill, 
                        _ElectResult.Ground_Connections, _ElectResult.Ground_Corrosion, _ElectResult.Ground_Electrode, _ElectResult.Surge_Protectors, _ElectResult.Surge_Func, 
                        _ElectResult.Comp_Codes, _ElectResult.Comp_Signage, _ElectResult.Comp_Records, _ElectResult.Corrective, _ElectResult.BuildingUse);

                }
            }


            //THEN QUERY THE MECHANICAL INSPECTIONS
            var _MechResults = _esriHelper.QueryInspection<MechanicalInspections>(appPath, 12);
            foreach (var _MechResult in (_MechResults ?? Enumerable.Empty<MechanicalInspections>()))
            {
                //get PRT_SITE_IDX if there is one by looking up from Facility ID
                var _existingPRT_SITE = _DbPortal.GetT_PRT_SITES_BySITEID(_MechResult.FacilityID);

                //insert into T_PRT_SITES
                Guid? _PrtSite = _DbPortal.InsertUpdateT_PRT_SITES(_existingPRT_SITE?.SITE_IDX, "MCNCREEK", _MechResult.FacilityName, null, null, null, null, _UserIDX, null, null, null, null, _MechResult.County, _MechResult.FacilityID);

                //insert into T_INSP_SITES
                if (_PrtSite != null)
                {
                    DateTime? editDate = _MechResult.EditDate is long ms ? DateTimeOffset.FromUnixTimeMilliseconds(ms).UtcDateTime : null;
                    _DbInspection.InsertUpdateT_INSP_SITES(_PrtSite.Value, _MechResult.Editor, editDate ?? System.DateTime.Now);

                    //insert into T_INSP_ASSESS_MECHANICAL
                    DateTime? assessDate = _MechResult.Date.ConvertOrDefault<long>() is long ms2 ? DateTimeOffset.FromUnixTimeMilliseconds(ms2).UtcDateTime : null;

                    _DbInspection.InsertUpdateT_INSP_ASSESS_MECHANICAL(null, _PrtSite, assessDate, _MechResult.Inspector, _MechResult.OBJECTID, _MechResult.GlobalID,
                        _MechResult.Title, _MechResult.Contact, _MechResult.Compliance, _MechResult.HVAC_Clean, _MechResult.HVAC_Filters, _MechResult.HVAC_Therm, _MechResult.Duct_Leak,
                        _MechResult.Duct_Insulation, _MechResult.Duct_Obstruction, _MechResult.Piping_Leaks, _MechResult.Piping_Insulation, _MechResult.Piping_Valves, _MechResult.Equip_Operational,
                        _MechResult.Equip_Doc, _MechResult.Equip_Guards, _MechResult.Vent_Airflow, _MechResult.Vent_Exhaust, _MechResult.Vent_Air,
                        _MechResult.Comp_Codes, _MechResult.Comp_Signage, _MechResult.Comp_Logs, _MechResult.Corrective, _MechResult.BuildingUse);

                }
            }

            return RedirectToAction("Index", new { selAssessType = "ELECTRICAL" });

        }


        [HttpPost]
        public IActionResult ImportFire()
        {
            string _UserIDX = _userManager.GetUserId(User);

            var appPath = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            var _esriHelper = new esriHelper(_DbPortal);
            var _FireResults = _esriHelper.queryFire(appPath, null, new string[] { "*" });

            foreach (var _FireResult in (_FireResults ?? Enumerable.Empty<FireInspections>()))
            {
                //get PRT_SITE_IDX if there is one by looking up from Facility ID
                var _existingPRT_SITE = _DbPortal.GetT_PRT_SITES_BySITEID(_FireResult.Facility_ID);

                //insert into T_PRT_SITES
                Guid? _PrtSite = _DbPortal.InsertUpdateT_PRT_SITES(_existingPRT_SITE?.SITE_IDX, "MCNCREEK", _FireResult.FacilityName, null, null, null, null, _UserIDX, null, null, null, null, _FireResult.County, _FireResult.Facility_ID);

                //insert into T_INSP_SITES
                if (_PrtSite != null)
                {
                    DateTime? editDate = _FireResult.EditDate is long ms ? DateTimeOffset.FromUnixTimeMilliseconds(ms).UtcDateTime : null;
                    _DbInspection.InsertUpdateT_INSP_SITES(_PrtSite.Value, _FireResult.Editor, editDate ?? System.DateTime.Now);

                    //insert into T_INSP_ASSESS_ELECTRICAL
                    DateTime? assessDate = _FireResult.Date.ConvertOrDefault<long>() is long ms2 ? DateTimeOffset.FromUnixTimeMilliseconds(ms2).UtcDateTime : null;
                    DateTime? riserInspDate = _FireResult.Risers_Inspected.ConvertOrDefault<long>() is long ms3 ? DateTimeOffset.FromUnixTimeMilliseconds(ms3).UtcDateTime : null;
                    DateTime? ventServiceDate = _FireResult.Vent_Service.ConvertOrDefault<long>() is long ms4 ? DateTimeOffset.FromUnixTimeMilliseconds(ms4).UtcDateTime : null;
                    DateTime? aedDate = _FireResult.AEDExpiration.ConvertOrDefault<long>() is long ms5 ? DateTimeOffset.FromUnixTimeMilliseconds(ms5).UtcDateTime : null;

                    string _correctAct = string.IsNullOrEmpty(_FireResult.CorrectiveAction) ? _FireResult.Corrective : _FireResult.CorrectiveAction;

                    _DbInspection.InsertUpdateT_INSP_ASSESS_FIRE(null, _PrtSite, assessDate, _FireResult.InspectorName, _FireResult.OBJECTID, _FireResult.GlobalID, 
                        _FireResult.InspectorTitle, _FireResult.FacilityContact, _FireResult.Compliance, _correctAct, _FireResult.Doc_FireAlarm, _FireResult.Doc__FireSprinklers, 
                        _FireResult.Doc_SprinklerTest, _FireResult.Doc_EmergPwr, _FireResult.Doc_EmergLight, _FireResult.Doc_KitchenHoodSys, _FireResult.Doc_Generator, 
                        _FireResult.Doc_EvacDrill, _FireResult.FireDept_Address, _FireResult.FireDept_AccessKeys, _FireResult.FireDept_Access, _FireResult.FireDept_Valves,
                        _FireResult.Egress_Path, _FireResult.Egress_Signs, _FireResult.Egress_Latch, _FireResult.Egress_DoorsOpen, _FireResult.Egress_DoorsKey, _FireResult.Egress_Evac, 
                        _FireResult.Egress_Storage, _FireResult.Storage_Housekeeping, _FireResult.Storage_Cylinders, _FireResult.Storage_Sprinklers, _FireResult.Storage_Combustible,
                        _FireResult.Storage_OilRags, _FireResult.Storage_Food, _FireResult.Elec_Covers, _FireResult.Elec_Panels, _FireResult.Elec_Clearance, _FireResult.Elec_Cords,
                        _FireResult.Elec_NoCord, _FireResult.Elec_Appliances, _FireResult.Elec_Adapters, _FireResult.Elec_GFI, _FireResult.Elec_Heater, _FireResult.Hazmat_Quantities,
                        _FireResult.Hazmat_Liquids, _FireResult.Hazmat_Shelving, _FireResult.Hazmat_Tanks, _FireResult.Hazmat_EmptyTanks, _FireResult.Hazmat_SDS, _FireResult.Hazmat_704,
                        _FireResult.Fuel_Valves, _FireResult.Fuel_Extinguisher, _FireResult.Fuel_Signs, _FireResult.Fuel_Materials, _FireResult.Vent_Caps, _FireResult.Vent_PullStation,
                        _FireResult.Vent_Exhaust, _FireResult.Vent_GasValve, _FireResult.Vent_Gauge, _FireResult.Vent_Hydro, _FireResult.Vent_BrandDate, _FireResult.Vent_Cleanliness, 
                        _FireResult.Vent_Extinguishers, ventServiceDate, _FireResult.FireExt_Doc, _FireResult.FireExt_Mounted, _FireResult.FireExt_CollarTag,
                        _FireResult.FireExt_Sys, (riserInspDate != null ? riserInspDate.Value.ToShortDateString() : ""), _FireResult.Risers_Tagged, _FireResult.Risers_Sprinkler, _FireResult.Risers_Deficiencies,
                        _FireResult.Risers_Plate, _FireResult.Risers_Gauges, _FireResult.Sprinkler_Cond, _FireResult.CeilingType, _FireResult.CeilingCond, _FireResult.AED,
                        aedDate, _FireResult.Facility_Use);
                }

            }

            return RedirectToAction("Index", new { selAssessType = "FIRE" });
        }

        [HttpPost]
        public IActionResult ImportBuilding()
        {
            string _UserIDX = _userManager.GetUserId(User);

            var appPath = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            var _esriHelper = new esriHelper(_DbPortal);
            var _BuildingResults = _esriHelper.queryBuilding(appPath, null, new string[] { "*" });

            foreach (var _BuildingResult in (_BuildingResults ?? Enumerable.Empty<BuildingInspections>()))
            {
                //get PRT_SITE_IDX if there is one by looking up from Facility ID
                var _existingPRT_SITE = _DbPortal.GetT_PRT_SITES_BySITEID(_BuildingResult.FacilityID);

                //insert into T_PRT_SITES
                Guid? _PrtSite = _DbPortal.InsertUpdateT_PRT_SITES(_existingPRT_SITE?.SITE_IDX, "MCNCREEK", _BuildingResult.FacilityName, null, null, null, null, _UserIDX, null, null, null, null, null, _BuildingResult.FacilityID);

                //insert into T_INSP_SITES
                if (_PrtSite != null)
                {
                    DateTime? editDate = _BuildingResult.EditDate is long ms ? DateTimeOffset.FromUnixTimeMilliseconds(ms).UtcDateTime : null;
                    _DbInspection.InsertUpdateT_INSP_SITES(_PrtSite.Value, _BuildingResult.Editor, editDate ?? System.DateTime.Now);

                    //insert into T_INSP_ASSESS_ELECTRICAL
                    DateTime? assessDate = _BuildingResult.Date.ConvertOrDefault<long>() is long ms2 ? DateTimeOffset.FromUnixTimeMilliseconds(ms2).UtcDateTime : null;

                    _DbInspection.InsertUpdateT_INSP_ASSESS_BUILDING(null, _PrtSite, assessDate, _BuildingResult.Inspector, _BuildingResult.OBJECTID, _BuildingResult.GlobalID,
                        _BuildingResult.Title, _BuildingResult.Walkway, _BuildingResult.Windows, _BuildingResult.Doors, _BuildingResult.Roof, _BuildingResult.Foundation, _BuildingResult.Exterior, 
                        _BuildingResult.Floor, _BuildingResult.Walls, _BuildingResult.Ceiling, _BuildingResult.Stairs, _BuildingResult.Panic, _BuildingResult.Closures, _BuildingResult.Notes, _BuildingResult.BuildingUse);

                }

            }

            return RedirectToAction("Index", new { selAssessType = "BUILDING" });
        }


        [HttpPost]
        public IActionResult ImportLife()
        {
            string _UserIDX = _userManager.GetUserId(User);

            var appPath = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            var _esriHelper = new esriHelper(_DbPortal);
            var _LifeResults = _esriHelper.queryLife(appPath, null, new string[] { "*" });

            foreach (var _LifeResult in (_LifeResults ?? Enumerable.Empty<LifeInspections>()))
            {
                //get PRT_SITE_IDX if there is one by looking up from Facility ID
                var _existingPRT_SITE = _DbPortal.GetT_PRT_SITES_BySITEID(_LifeResult.FacilityID);

                //insert into T_PRT_SITES
                Guid? _PrtSite = _DbPortal.InsertUpdateT_PRT_SITES(_existingPRT_SITE?.SITE_IDX, "MCNCREEK", _LifeResult.FacilityName, null, null, null, null, _UserIDX, null, null, null, null, null, _LifeResult.FacilityID);

                //insert into T_INSP_SITES
                if (_PrtSite != null)
                {
                    DateTime? editDate = _LifeResult.EditDate is long ms ? DateTimeOffset.FromUnixTimeMilliseconds(ms).UtcDateTime : null;
                    _DbInspection.InsertUpdateT_INSP_SITES(_PrtSite.Value, _LifeResult.Editor, editDate ?? System.DateTime.Now);

                    //insert into T_INSP_ASSESS_LIFE
                    DateTime? assessDate = _LifeResult.Date.ConvertOrDefault<DateTime?>();
                    //DateTime ? assessDate = _LifeResult.Date.ConvertOrDefault<long>() is long ms2 ? DateTimeOffset.FromUnixTimeMilliseconds(ms2).UtcDateTime : null;

                    _DbInspection.InsertUpdateT_INSP_ASSESS_LIFE(null, _PrtSite, assessDate, _LifeResult.Inspector, _LifeResult.OBJECTID, _LifeResult.GlobalID,
                        _LifeResult.InspectorTitle, _LifeResult.bldg_sidewalks, _LifeResult.Bldg_Windows,_LifeResult.Bldg_Doors, _LifeResult.Bldg_Roof, _LifeResult.Bldg_Foundation, _LifeResult.Bldg_Exterior, _LifeResult.Bldg_Address, _LifeResult.Bldg_Floor,
                        _LifeResult.Bldg_Walls,_LifeResult.Bldg_Ceiling,_LifeResult.Bldg_Stairs,_LifeResult.Bldg_Panic,_LifeResult.Bldg_Closures,_LifeResult.Bldg_Notes,_LifeResult.BuildingUse,_LifeResult.Fire_FireAlarm,_LifeResult.Fire_Sprinklers1,
                        _LifeResult.Fire_Sprinklers5, _LifeResult.Fire_Plate,_LifeResult.Fire_EmergLight,_LifeResult.Fire_Exit,_LifeResult.Fire_KitchenHood,_LifeResult.Fire_HoodSup,_LifeResult.Fire_EvacDrill, 
                        _LifeResult.Fire_Evac, _LifeResult.Fire_Storage,_LifeResult.Fire_Piled,_LifeResult.Fire_Food,_LifeResult.Fire_Food, _LifeResult.Fire_SDS, _LifeResult.Fire_Extinguisher, _LifeResult.Fire_ExtinAnnual, 
                        _LifeResult.Fire_Notes, _LifeResult.Plum_Water, _LifeResult.Plum_Drain, _LifeResult.Plum_Sink, _LifeResult.Plum_Toilet, _LifeResult.Plum_Tub, _LifeResult.Plum_Dish, _LifeResult.Plum_Heater,
                        _LifeResult.Plum_Backflow,_LifeResult.Plum_Notes, _LifeResult.Elec_Hazard, _LifeResult.Elec_Panel, _LifeResult.Elec_Label, _LifeResult.Elec_Obstruct, _LifeResult.Elec_Outlet, _LifeResult.Elec_Lighting,
                        _LifeResult.Elec_Surge, _LifeResult.Elec_Ground, _LifeResult.Elec_Sign, _LifeResult.Elec_Notes, _LifeResult.Mech_Filters, _LifeResult.Mech_Register, _LifeResult.Mech_Equip, _LifeResult.Mech_VentAir, 
                        _LifeResult.Mech_VentSyst, _LifeResult.Mech_Pipe, _LifeResult.Mech_Therm, _LifeResult.Mech_Duct, _LifeResult.Mech_Notes);

                }

            }

            return RedirectToAction("Index", new { selAssessType = "LIFE" });
        }


        public IActionResult InspectionReport(Guid? id)
        {
            var _rpt = GenInspectionRpt(id.ConvertOrDefault<Guid>());
            byte[] rptData = _rpt.rptContent;

            if (rptData != null)
            {
                var content = new System.IO.MemoryStream(rptData);
                return File(rptData, "application/pdf", _rpt.rptName + ".pdf");
            }
            else
            {
                //if got this far, it failed
                TempData["Error"] = "Unable to generate document.";
                return RedirectToAction("Sites");
            }
        }


        internal RptDisplayType GenInspectionRpt(Guid AssessId)
        {
            V_INSP_ASSESSMENTS _assess = _DbInspection.GetV_INSP_ASSESSMENTS_ByAssessID(AssessId);
            T_PRT_SITES _site = _DbPortal.GetT_PRT_SITES_BySITEIDX(_assess.SITE_IDX);
            if (_assess == null || _site == null)
                return null;

            //plumbing
            if (_assess.INSP_TYP == "PLUMBING")
            {
                T_INSP_ASSESS_PLUMBING _plumbing = _DbInspection.GetT_INSP_ASSESS_PLUMBING_ByAssessID(AssessId);
                if (_plumbing == null)
                    return null;

                using (MemoryStream ms = new MemoryStream())
                {
                    // create a new PDF reader based on the PDF template document  
                    PdfReader pdfReader = new PdfReader(Path.Combine(_hostingEnvironment.WebRootPath, "files", "PlumbingInspectionForm.pdf"));

                    //stamper and memorystream
                    using (PdfStamper pdfStamper = new PdfStamper(pdfReader, ms))
                    {
                        //change file metadata
                        var info = pdfReader.Info;
                        info["Producer"] = "Open Environment Software";
                        info["Title"] = "Plumbing Inspection Form";
                        pdfStamper.MoreInfo = info;

                        //fill in form data
                        pdfStamper.AcroFields.SetField("Facility Name  Address", _site.SITE_NAME ?? "");
                        pdfStamper.AcroFields.SetField("County", _site.COUNTY ?? "");
                        pdfStamper.AcroFields.SetField("Inspection Date", _assess.ASSESSMENT_DT.ToShortDateString() ?? "");
                        pdfStamper.AcroFields.SetField("Water PSI", _plumbing.WaterPSI ?? "");


                        pdfStamper.AcroFields.SetField("Meter Location", _plumbing.Meter_Location ?? "");

                        pdfStamper.AcroFields.SetField("chkSupplyPass", _plumbing.Supply_Lines == "P" ? "Yes" : "");
                        pdfStamper.AcroFields.SetField("chkSupplyWork", _plumbing.Supply_Lines == "N" ? "Yes" : "");
                        pdfStamper.AcroFields.SetField("chkSupplyFail", _plumbing.Supply_Lines == "F" ? "Yes" : "");

                        pdfStamper.AcroFields.SetField("chkDrainsPass", _plumbing.Drains == "P" ? "Yes" : "");
                        pdfStamper.AcroFields.SetField("chkDrainsWork", _plumbing.Drains == "N" ? "Yes" : "");
                        pdfStamper.AcroFields.SetField("chkDrainsFail", _plumbing.Drains == "F" ? "Yes" : "");

                        pdfStamper.AcroFields.SetField("Sewer Location", _plumbing.Sewer_Loc ?? "");

                        pdfStamper.AcroFields.SetField("chkSinksPass", _plumbing.Sink_Faucet == "P" ? "Yes" : "");
                        pdfStamper.AcroFields.SetField("chkSinksWork", _plumbing.Sink_Faucet == "N" ? "Yes" : "");
                        pdfStamper.AcroFields.SetField("chkSinksFail", _plumbing.Sink_Faucet == "F" ? "Yes" : "");

                        pdfStamper.AcroFields.SetField("chkToiletsPass", _plumbing.Toilet_Urinal == "P" ? "Yes" : "");
                        pdfStamper.AcroFields.SetField("chkToiletsWork", _plumbing.Toilet_Urinal == "N" ? "Yes" : "");
                        pdfStamper.AcroFields.SetField("chkToiletsFail", _plumbing.Toilet_Urinal == "F" ? "Yes" : "");

                        pdfStamper.AcroFields.SetField("chkShowersPass", _plumbing.Shower_Tub == "P" ? "Yes" : "");
                        pdfStamper.AcroFields.SetField("chkShowersWork", _plumbing.Shower_Tub == "N" ? "Yes" : "");
                        pdfStamper.AcroFields.SetField("chkShowersFail", _plumbing.Shower_Tub == "F" ? "Yes" : "");

                        pdfStamper.AcroFields.SetField("chkDishwashPass", _plumbing.Dishwasher == "P" ? "Yes" : "");
                        pdfStamper.AcroFields.SetField("chkDishwashPass", _plumbing.Dishwasher == "N" ? "Yes" : "");
                        pdfStamper.AcroFields.SetField("chkDishwashPass", _plumbing.Dishwasher == "F" ? "Yes" : "");

                        pdfStamper.AcroFields.SetField("chkWaterHeaterPass", _plumbing.Water_Heater == "P" ? "Yes" : "");
                        pdfStamper.AcroFields.SetField("chkWaterHeaterWork", _plumbing.Water_Heater == "N" ? "Yes" : "");
                        pdfStamper.AcroFields.SetField("chkWaterHeaterFail", _plumbing.Water_Heater == "F" ? "Yes" : "");

                        pdfStamper.AcroFields.SetField("chkGas", _plumbing.Heater_Type == "G" ? "Yes" : "");
                        pdfStamper.AcroFields.SetField("chkElectric", _plumbing.Heater_Type == "E" ? "Yes" : "");

                        pdfStamper.AcroFields.SetField("Backflow", _plumbing.Backflow ?? "");
                        pdfStamper.AcroFields.SetField("Repairs", _plumbing.Repairs ?? "");


                        //add photos if there are any
                        AddPhotos(_plumbing.OBJECTID, pdfReader, pdfStamper,4);



                        // flatten the form to remove editting options, set it to false to leave the form open to subsequent manual edits  
                        pdfStamper.FormFlattening = false;
                    }

                    //return value
                    var _rpt = new RptDisplayType
                    {
                        rptContent = ms.ToArray(),
                        rptName = "PlumbingInspection_" + _site.SITE_NAME + "_" + _assess.ASSESSMENT_DT.ToShortDateString().Replace("/", ""),
                    };
                    return _rpt;
                }
            }
            else if (_assess.INSP_TYP == "ELECTRICAL")
            {
                T_INSP_ASSESS_ELECTRICAL _elect = _DbInspection.GetT_INSP_ASSESS_ELECTRICAL_ByAssessID(AssessId);
                if (_elect == null)
                    return null;

                using (MemoryStream ms = new MemoryStream())
                {
                    // create a new PDF reader based on the PDF template document  
                    PdfReader pdfReader = new PdfReader(Path.Combine(_hostingEnvironment.WebRootPath, "files", "ElectricalInspectionForm.pdf"));

                    //stamper and memorystream
                    using (PdfStamper pdfStamper = new PdfStamper(pdfReader, ms))
                    {
                        //change file metadata
                        var info = pdfReader.Info;
                        info["Producer"] = "Open Environment Software";
                        info["Title"] = "Electrical Inspection Form";
                        pdfStamper.MoreInfo = info;

                        //fill in form data
                        pdfStamper.AcroFields.SetField("Facility Name  Address", _site.SITE_NAME ?? "");
                        pdfStamper.AcroFields.SetField("County", _site.COUNTY ?? "");
                        pdfStamper.AcroFields.SetField("Inspector_Title", _elect.ASSESSED_BY + " " + _elect.Title);
                        pdfStamper.AcroFields.SetField("FacContact", _elect.Contact);
                        pdfStamper.AcroFields.SetField("BuildingUse", _elect.BuildingType);
                        pdfStamper.AcroFields.SetField("Inspection Date", _assess.ASSESSMENT_DT.ToShortDateString() ?? "");

                        pdfStamper.AcroFields.SetField("chkCompliancePass", _elect.Compliance == "P" ? "Yes" : "");
                        pdfStamper.AcroFields.SetField("chkComplianceWork", _elect.Compliance == "N" ? "Yes" : "");
                        pdfStamper.AcroFields.SetField("chkComplianceFail", _elect.Compliance == "F" ? "Yes" : "");

                        pdfStamper.AcroFields.SetField("chkGenAccessOK", _elect.Gen_Site == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkGenAccessWork", _elect.Gen_Site == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkGenAccessNA", _elect.Gen_Site == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("chkGenHazardOK", _elect.Gen_Hazard == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkGenHazardWork", _elect.Gen_Hazard == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkGenHazardNA", _elect.Gen_Hazard == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("chkGenAddressOK", _elect.Gen_Address == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkGenAddressWork", _elect.Gen_Address == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkGenAddressNA", _elect.Gen_Address == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("chkElectRustOK", _elect.Elec_Rust == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkElectRustWork", _elect.Elec_Rust == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkElectRustNA", _elect.Elec_Rust == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("chkElectLabelOK", _elect.Elec_Label == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkElectLabelWork", _elect.Elec_Label == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkElectLabelNA", _elect.Elec_Label == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("chkElectSpaceOK", _elect.Elec_Space == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkElectSpaceWork", _elect.Elec_Space == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkElectSpaceNA", _elect.Elec_Space == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("chkWiringSecureOK", _elect.Wiring_Secured == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkWiringSecureWork", _elect.Wiring_Secured == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkWiringSecureNA", _elect.Wiring_Secured == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("chkWiringDamageOK", _elect.Wiring_Damage == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkWiringDamageWork", _elect.Wiring_Damage == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkWiringDamageNA", _elect.Wiring_Damage == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("chkWiringSeparateOK", _elect.Wiring_Separate == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkWiringSeparateWork", _elect.Wiring_Separate == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkWiringSeparateNA", _elect.Wiring_Separate == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("chkOutletFuncOK", _elect.Outlet_Func == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkOutletFuncWork", _elect.Outlet_Func == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkOutletFuncNA", _elect.Outlet_Func == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("chkOutletGFCIOK", _elect.Outlet_GFCI == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkOutletGFCIWork", _elect.Outlet_GFCI == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkOutletGFCINA", _elect.Outlet_GFCI == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("chkOutletTamperOK", _elect.Outlet_Tamper == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkOutletTamperWork", _elect.Outlet_Tamper == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkOutletTamperNA", _elect.Outlet_Tamper == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("chkLightFixtureOK", _elect.Light_Fixtures == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkLightFixtureWork", _elect.Light_Fixtures == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkLightFixtureNA", _elect.Light_Fixtures == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("chkLightExitOK", _elect.Light_Exit == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkLightExitWork", _elect.Light_Exit == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkLightExitNA", _elect.Light_Exit == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("chkLightIllOK", _elect.Light_Ill == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkLightIllWork", _elect.Light_Ill == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkLightIllNA", _elect.Light_Ill == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("chkGroundConnectionOK", _elect.Ground_Connections == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkGroundConnectionWork", _elect.Ground_Connections == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkGroundConnectionNA", _elect.Ground_Connections == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("chkGroundCorrosionOK", _elect.Ground_Corrosion == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkGroundCorrosionWork", _elect.Ground_Corrosion == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkGroundCorrosionNA", _elect.Ground_Corrosion == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("chkGroundElectrodeOK", _elect.Ground_Electrode == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkGroundElectrodeWork", _elect.Ground_Electrode == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkGroundElectrodeNA", _elect.Ground_Electrode == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("chkSurgeProtOK", _elect.Surge_Protectors == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkSurgeProtWork", _elect.Surge_Protectors == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkSurgeProtNA", _elect.Surge_Protectors == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("chkSurgeFuncOK", _elect.Surge_Func == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkSurgeFuncWork", _elect.Surge_Func == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkSurgeFuncNA", _elect.Surge_Func == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("chkCompCodesOK", _elect.Comp_Codes == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkCompCodesWork", _elect.Comp_Codes == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkCompCodesNA", _elect.Comp_Codes == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("chkCompSignageOK", _elect.Comp_Signage == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkCompSignageWork", _elect.Comp_Signage == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkCompSignageNA", _elect.Comp_Signage == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("chkCompRecordsOK", _elect.Comp_Records == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkCompRecordsWork", _elect.Comp_Records == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkCompRecordsNA", _elect.Comp_Records == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Corrective", _elect.Corrective ?? "");


                        //add photos if there are any
                        AddPhotos(_elect.OBJECTID, pdfReader, pdfStamper, 5);



                        // flatten the form to remove editting options, set it to false to leave the form open to subsequent manual edits  
                        pdfStamper.FormFlattening = false;
                    }

                    //return value
                    var _rpt = new RptDisplayType
                    {
                        rptContent = ms.ToArray(),
                        rptName = "ElectricalInspection_" + _site.SITE_NAME + "_" + _assess.ASSESSMENT_DT.ToShortDateString().Replace("/", ""),
                    };
                    return _rpt;
                }
            }
            else if (_assess.INSP_TYP == "MECHANICAL")
            {
                T_INSP_ASSESS_MECHANICAL _elect = _DbInspection.GetT_INSP_ASSESS_MECHANICAL_ByAssessID(AssessId);
                if (_elect == null)
                    return null;

                using (MemoryStream ms = new MemoryStream())
                {
                    // create a new PDF reader based on the PDF template document  
                    PdfReader pdfReader = new PdfReader(Path.Combine(_hostingEnvironment.WebRootPath, "files", "MechanicalInspectionForm.pdf"));

                    //stamper and memorystream
                    using (PdfStamper pdfStamper = new PdfStamper(pdfReader, ms))
                    {
                        //change file metadata
                        var info = pdfReader.Info;
                        info["Producer"] = "Open Environment Software";
                        info["Title"] = "Mechanical Inspection Form";
                        pdfStamper.MoreInfo = info;

                        //fill in form data
                        pdfStamper.AcroFields.SetField("Facility Name  Address", _site.SITE_NAME ?? "");
                        pdfStamper.AcroFields.SetField("County", _site.COUNTY ?? "");
                        pdfStamper.AcroFields.SetField("Inspector_Title", _elect.ASSESSED_BY + " " + _elect.Title);
                        pdfStamper.AcroFields.SetField("FacContact", _elect.Contact);
                        pdfStamper.AcroFields.SetField("Inspection Date", _assess.ASSESSMENT_DT.ToShortDateString() ?? "");

                        pdfStamper.AcroFields.SetField("BuildingUse", _elect.BuildingType + " ");

                        pdfStamper.AcroFields.SetField("chkCompliancePass", _elect.Compliance == "P" ? "Yes" : "");
                        pdfStamper.AcroFields.SetField("chkComplianceWork", _elect.Compliance == "N" ? "Yes" : "");
                        pdfStamper.AcroFields.SetField("chkComplianceFail", _elect.Compliance == "F" ? "Yes" : "");

                        pdfStamper.AcroFields.SetField("HVAC_CleanOK", _elect.HVAC_Clean == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("HVAC_CleanWork", _elect.HVAC_Clean == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("HVAC_CleanNA", _elect.HVAC_Clean == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("HVAC_FiltersOK", _elect.HVAC_Filters == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("HVAC_FiltersWork", _elect.HVAC_Filters == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("HVAC_FiltersNA", _elect.HVAC_Filters == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("HVAC_ThermOK", _elect.HVAC_Therm == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("HVAC_ThermWork", _elect.HVAC_Therm == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("HVAC_ThermNA", _elect.HVAC_Therm == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Duct_LeakOK", _elect.Duct_Leak == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Duct_LeakWork", _elect.Duct_Leak == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Duct_LeakNA", _elect.Duct_Leak == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Duct_InsulationOK", _elect.Duct_Insulation == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Duct_InsulationWork", _elect.Duct_Insulation == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Duct_InsulationNA", _elect.Duct_Insulation == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Duct_ObstructionOK", _elect.Duct_Obstruction == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Duct_ObstructionWork", _elect.Duct_Obstruction == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Duct_ObstructionNA", _elect.Duct_Obstruction == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Piping_LeaksOK", _elect.Piping_Leaks == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Piping_LeaksWork", _elect.Piping_Leaks == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Piping_LeaksNA", _elect.Piping_Leaks == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Piping_InsulationOK", _elect.Piping_Insulation == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Piping_InsulationWork", _elect.Piping_Insulation == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Piping_InsulationNA", _elect.Piping_Insulation == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Piping_ValvesOK", _elect.Piping_Valves == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Piping_ValvesWork", _elect.Piping_Valves == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Piping_ValvesNA", _elect.Piping_Valves == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Equip_OperationalOK", _elect.Equip_Operational == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Equip_OperationalWork", _elect.Equip_Operational == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Equip_OperationalNA", _elect.Equip_Operational == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Equip_DocOK", _elect.Equip_Doc == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Equip_DocWork", _elect.Equip_Doc == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Equip_DocNA", _elect.Equip_Doc == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Equip_GuardsOK", _elect.Equip_Guards == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Equip_GuardsWork", _elect.Equip_Guards == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Equip_GuardsNA", _elect.Equip_Guards == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Vent_AirflowOK", _elect.Vent_Airflow == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Vent_AirflowWork", _elect.Vent_Airflow == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Vent_AirflowNA", _elect.Vent_Airflow == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Vent_ExhaustOK", _elect.Vent_Exhaust == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Vent_ExhaustWork", _elect.Vent_Exhaust == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Vent_ExhaustNA", _elect.Vent_Exhaust == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Vent_AirOK", _elect.Vent_Air == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Vent_AirWork", _elect.Vent_Air == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Vent_AirNA", _elect.Vent_Air == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("mechCompCodesOK", _elect.Comp_Codes == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("mechCompCodesWork", _elect.Comp_Codes == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("mechCompCodesNA", _elect.Comp_Codes == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("mechCompSignageOK", _elect.Comp_Signage == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("mechCompSignageWork", _elect.Comp_Signage == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("mechCompSignageNA", _elect.Comp_Signage == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("mechCompRecordsOK", _elect.Comp_Records == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("mechCompRecordsWork", _elect.Comp_Records == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("mechCompRecordsNA", _elect.Comp_Records == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Corrective", _elect.Corrective ?? "");


                        //add photos if there are any
                        AddPhotos(_elect.OBJECTID, pdfReader, pdfStamper, 6);



                        // flatten the form to remove editting options, set it to false to leave the form open to subsequent manual edits  
                        pdfStamper.FormFlattening = false;
                    }

                    //return value
                    var _rpt = new RptDisplayType
                    {
                        rptContent = ms.ToArray(),
                        rptName = "MechanicalInspection_" + _site.SITE_NAME + "_" + _assess.ASSESSMENT_DT.ToShortDateString().Replace("/", ""),
                    };
                    return _rpt;
                }
            }
            else if (_assess.INSP_TYP == "BUILDING")
            {
                T_INSP_ASSESS_BUILDING _elect = _DbInspection.GetT_INSP_ASSESS_BUILDING_ByAssessID(AssessId);
                if (_elect == null)
                    return null;

                using (MemoryStream ms = new MemoryStream())
                {
                    // create a new PDF reader based on the PDF template document  
                    PdfReader pdfReader = new PdfReader(Path.Combine(_hostingEnvironment.WebRootPath, "files", "BuildingInspectionForm.pdf"));

                    //stamper and memorystream
                    using (PdfStamper pdfStamper = new PdfStamper(pdfReader, ms))
                    {
                        //change file metadata
                        var info = pdfReader.Info;
                        info["Producer"] = "Open Environment Software";
                        info["Title"] = "Building Inspection Form";
                        pdfStamper.MoreInfo = info;

                        //fill in form data
                        pdfStamper.AcroFields.SetField("FacilityName", _site.SITE_NAME ?? "");
                        pdfStamper.AcroFields.SetField("BuildingUse", _elect.BuildingType ?? "");

                        pdfStamper.AcroFields.SetField("Inspector", _elect.ASSESSED_BY + ", " + _elect.Title);
                        pdfStamper.AcroFields.SetField("Date", _assess.ASSESSMENT_DT.ToShortDateString() ?? "");

                        pdfStamper.AcroFields.SetField("WalkwayGood", _elect.Walkway == "G" ? "Yes" : "");
                        pdfStamper.AcroFields.SetField("WalkwayFair", _elect.Walkway == "F" ? "Yes" : "");
                        pdfStamper.AcroFields.SetField("WalkwayPoor", _elect.Walkway == "P" ? "Yes" : "");

                        pdfStamper.AcroFields.SetField("WindowsGood", _elect.Windows == "G" ? "Yes" : "");
                        pdfStamper.AcroFields.SetField("WindowsFair", _elect.Windows == "F" ? "Yes" : "");
                        pdfStamper.AcroFields.SetField("WindowsPoor", _elect.Windows == "P" ? "Yes" : "");

                        pdfStamper.AcroFields.SetField("DoorsGood", _elect.Doors == "G" ? "Yes" : "");
                        pdfStamper.AcroFields.SetField("DoorsFair", _elect.Doors == "F" ? "Yes" : "");
                        pdfStamper.AcroFields.SetField("DoorsPoor", _elect.Doors == "P" ? "Yes" : "");

                        pdfStamper.AcroFields.SetField("RoofGood", _elect.Roof == "G" ? "Yes" : "");
                        pdfStamper.AcroFields.SetField("RoofFair", _elect.Roof == "F" ? "Yes" : "");
                        pdfStamper.AcroFields.SetField("RoofPoor", _elect.Roof == "P" ? "Yes" : "");

                        pdfStamper.AcroFields.SetField("FoundationGood", _elect.Foundation == "G" ? "Yes" : "");
                        pdfStamper.AcroFields.SetField("FoundationFair", _elect.Foundation == "F" ? "Yes" : "");
                        pdfStamper.AcroFields.SetField("FoundationPoor", _elect.Foundation == "P" ? "Yes" : "");

                        pdfStamper.AcroFields.SetField("ExteriorGood", _elect.Exterior == "G" ? "Yes" : "");
                        pdfStamper.AcroFields.SetField("ExteriorFair", _elect.Exterior == "F" ? "Yes" : "");
                        pdfStamper.AcroFields.SetField("ExteriorPoor", _elect.Exterior == "P" ? "Yes" : "");

                        pdfStamper.AcroFields.SetField("FloorGood", _elect.Floor == "G" ? "Yes" : "");
                        pdfStamper.AcroFields.SetField("FloorFair", _elect.Floor == "F" ? "Yes" : "");
                        pdfStamper.AcroFields.SetField("FloorPoor", _elect.Floor == "P" ? "Yes" : "");

                        pdfStamper.AcroFields.SetField("WallsGood", _elect.Walls == "G" ? "Yes" : "");
                        pdfStamper.AcroFields.SetField("WallsFair", _elect.Walls == "F" ? "Yes" : "");
                        pdfStamper.AcroFields.SetField("WallsPoor", _elect.Walls == "P" ? "Yes" : "");

                        pdfStamper.AcroFields.SetField("CeilingGood", _elect.Ceiling == "G" ? "Yes" : "");
                        pdfStamper.AcroFields.SetField("CeilingFair", _elect.Ceiling == "F" ? "Yes" : "");
                        pdfStamper.AcroFields.SetField("CeilingPoor", _elect.Ceiling == "P" ? "Yes" : "");

                        pdfStamper.AcroFields.SetField("StairsGood", _elect.Stairs == "G" ? "Yes" : "");
                        pdfStamper.AcroFields.SetField("StairsFair", _elect.Stairs == "F" ? "Yes" : "");
                        pdfStamper.AcroFields.SetField("StairsPoor", _elect.Stairs == "P" ? "Yes" : "");

                        pdfStamper.AcroFields.SetField("PanicGood", _elect.Panic == "G" ? "Yes" : "");
                        pdfStamper.AcroFields.SetField("PanicFair", _elect.Panic == "F" ? "Yes" : "");
                        pdfStamper.AcroFields.SetField("PanicPoor", _elect.Panic == "P" ? "Yes" : "");

                        pdfStamper.AcroFields.SetField("DoorGood", _elect.Doors == "G" ? "Yes" : "");
                        pdfStamper.AcroFields.SetField("DoorFair", _elect.Doors == "F" ? "Yes" : "");
                        pdfStamper.AcroFields.SetField("DoorPoor", _elect.Doors == "P" ? "Yes" : "");

                        pdfStamper.AcroFields.SetField("Notes", _elect.Notes ?? "");


                        //add photos if there are any
                        AddPhotos(_elect.OBJECTID, pdfReader, pdfStamper, 7);


                        // flatten the form to remove editting options, set it to false to leave the form open to subsequent manual edits  
                        pdfStamper.FormFlattening = false;



                    }

                    //return value
                    var _rpt = new RptDisplayType
                    {
                        rptContent = ms.ToArray(),
                        rptName = "BuildingInspection_" + _site.SITE_NAME + "_" + _assess.ASSESSMENT_DT.ToShortDateString().Replace("/", ""),
                    };
                    return _rpt;
                }
            }
            else if (_assess.INSP_TYP == "FIRE")
            {
                T_INSP_ASSESS_FIRE _elect = _DbInspection.GetT_INSP_ASSESS_FIRE_ByAssessID(AssessId);
                if (_elect == null)
                    return null;

                using (MemoryStream ms = new MemoryStream())
                {
                    // create a new PDF reader based on the PDF template document  
                    PdfReader pdfReader = new PdfReader(Path.Combine(_hostingEnvironment.WebRootPath, "files", "FireInspectionForm.pdf"));

                    //stamper and memorystream
                    using (PdfStamper pdfStamper = new PdfStamper(pdfReader, ms))
                    {
                        //change file metadata
                        var info = pdfReader.Info;
                        info["Producer"] = "Open Environment Software";
                        info["Title"] = "Fire Inspection Form";
                        pdfStamper.MoreInfo = info;

                        //fill in form data
                        pdfStamper.AcroFields.SetField("Facility Name  Address", _site.SITE_NAME ?? "");
                        pdfStamper.AcroFields.SetField("FacilityUse", _elect.Facility_Use ?? "");
                        pdfStamper.AcroFields.SetField("County", _site.COUNTY ?? "");
                        pdfStamper.AcroFields.SetField("Inspector_Title", _elect.ASSESSED_BY + ": " + _elect.Title);
                        pdfStamper.AcroFields.SetField("FacContact", _elect.FacContact);
                        pdfStamper.AcroFields.SetField("Inspection Date", _assess.ASSESSMENT_DT.ToShortDateString() ?? "");

                        pdfStamper.AcroFields.SetField("chkCompliancePass", _elect.Compliance == "P" ? "Yes" : "");
                        pdfStamper.AcroFields.SetField("chkComplianceWork", _elect.Compliance == "N" ? "Yes" : "");
                        pdfStamper.AcroFields.SetField("chkComplianceFail", _elect.Compliance == "F" ? "Yes" : "");

                        pdfStamper.AcroFields.SetField("chkDoc_FireAlarmOK", _elect.Doc_FireAlarm == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkDoc_FireAlarmWork", _elect.Doc_FireAlarm == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkDoc_FireAlarmNA", _elect.Doc_FireAlarm == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("chkDoc_FireSprinkler5OK", _elect.Doc_FireSprinkler5 == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkDoc_FireSprinkler5Work", _elect.Doc_FireSprinkler5 == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkDoc_FireSprinkler5NA", _elect.Doc_FireSprinkler5 == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("chkDoc_FireSprinklerAnnOK", _elect.Doc_FireSprinklerAnn == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkDoc_FireSprinklerAnnWork", _elect.Doc_FireSprinklerAnn == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkDoc_FireSprinklerAnnNA", _elect.Doc_FireSprinklerAnn == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("chkDoc_EmergPwrOK", _elect.Doc_EmergPwr == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkDoc_EmergPwrWork", _elect.Doc_EmergPwr == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkDoc_EmergPwrNA", _elect.Doc_EmergPwr == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("chkDoc_EmergLightOK", _elect.Doc_EmergLight == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkDoc_EmergLightWork", _elect.Doc_EmergLight == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkDoc_EmergLightNA", _elect.Doc_EmergLight == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("chkDoc_KitchenHoodSysOK", _elect.Doc_KitchenHoodSys == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkDoc_KitchenHoodSysWork", _elect.Doc_KitchenHoodSys == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkDoc_KitchenHoodSysNA", _elect.Doc_KitchenHoodSys == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("chkDoc_GeneratorOK", _elect.Doc_Generator == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkDoc_GeneratorWork", _elect.Doc_Generator == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkDoc_GeneratorNA", _elect.Doc_Generator == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("chkDoc_EvacDrillOK", _elect.Doc_EvacDrill == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkDoc_EvacDrillWork", _elect.Doc_EvacDrill == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkDoc_EvacDrillNA", _elect.Doc_EvacDrill == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("chkFireDept_AddressOK", _elect.FireDept_Address == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkFireDept_AddressWork", _elect.FireDept_Address == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkFireDept_AddressNA", _elect.FireDept_Address == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("chkFireDept_AccessOK", _elect.FireDept_Access == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkFireDept_AccessWork", _elect.FireDept_Access == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkFireDept_AccessNA", _elect.FireDept_Access == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("chkFireDept_AccessKeysOK", _elect.FireDept_AccessKeys == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkFireDept_AccessKeysWork", _elect.FireDept_AccessKeys == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkFireDept_AccessKeysNA", _elect.FireDept_AccessKeys == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("chkFireDept_ValvesOK", _elect.FireDept_Valves == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkFireDept_ValvesWork", _elect.FireDept_Valves == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkFireDept_ValvesNA", _elect.FireDept_Valves == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("chkEgress_PathOK", _elect.Egress_Path == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkEgress_PathWork", _elect.Egress_Path == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkEgress_PathNA", _elect.Egress_Path == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("chkEgress_SignsOK", _elect.Egress_Signs == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkEgress_SignsWork", _elect.Egress_Signs == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkEgress_SignsNA", _elect.Egress_Signs == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("chkEgress_LatchOK", _elect.Egress_Latch == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkEgress_LatchWork", _elect.Egress_Latch == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkEgress_LatchNA", _elect.Egress_Latch == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("chkEgress_DoorOpenOK", _elect.Egress_DoorsOpen == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkEgress_DoorOpenWork", _elect.Egress_DoorsOpen == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkEgress_DoorOpenNA", _elect.Egress_DoorsOpen == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("chkEgress_DoorKeyOK", _elect.Egress_DoorsKey == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkEgress_DoorKeyWork", _elect.Egress_DoorsKey == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkEgress_DoorKeyNA", _elect.Egress_DoorsKey == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("chkEgress_EvacOK", _elect.Egress_Evac == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkEgress_EvacWork", _elect.Egress_Evac == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkEgress_EvacNA", _elect.Egress_Evac == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("chkEgress_StorageOK", _elect.Egress_Storage == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkEgress_StorageWork", _elect.Egress_Storage == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkEgress_StorageNA", _elect.Egress_Storage == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("chkStorage_HousekeepingOK", _elect.Storage_Housekeeping == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkStorage_HousekeepingWork", _elect.Storage_Housekeeping == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkStorage_HousekeepingNA", _elect.Storage_Housekeeping == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("chkStorage_CylindersOK", _elect.Storage_Cylinders == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkStorage_CylindersWork", _elect.Storage_Cylinders == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkStorage_CylindersNA", _elect.Storage_Cylinders == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("chkStorage_SprinklersOK", _elect.Storage_Sprinklers == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkStorage_SprinklersWork", _elect.Storage_Sprinklers == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkStorage_SprinklersNA", _elect.Storage_Sprinklers == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("chkStorage_CombustOK", _elect.Storage_Combust == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkStorage_CombustWork", _elect.Storage_Combust == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkStorage_CombustNA", _elect.Storage_Combust == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("chkStorage_OilRagsOK", _elect.Storage_OilRags == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkStorage_OilRagsWork", _elect.Storage_OilRags == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkStorage_OilRagsNA", _elect.Storage_OilRags == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("chkStorage_FoodOK", _elect.Storage_Food == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkStorage_FoodWork", _elect.Storage_Food == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("chkStorage_FoodNA", _elect.Storage_Food == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("ELec_CoversOK", _elect.ELec_Covers == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("ELec_CoversWork", _elect.ELec_Covers == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("ELec_CoversNA", _elect.ELec_Covers == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Elec_PanelsOK", _elect.Elec_Panels == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Elec_PanelsWork", _elect.Elec_Panels == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Elec_PanelsNA", _elect.Elec_Panels == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Elec_ClearanceOK", _elect.Elec_Clearance == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Elec_ClearanceWork", _elect.Elec_Clearance == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Elec_ClearanceNA", _elect.Elec_Clearance == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Elec_CordsOK", _elect.Elec_Cords == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Elec_CordsWork", _elect.Elec_Cords == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Elec_CordsNA", _elect.Elec_Cords == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Elec_NoCordOK", _elect.Elec_NoCord == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Elec_NoCordWork", _elect.Elec_NoCord == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Elec_NoCordNA", _elect.Elec_NoCord == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Elec_AppliancesOK", _elect.Elec_Appliances == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Elec_AppliancesWork", _elect.Elec_Appliances == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Elec_AppliancesNA", _elect.Elec_Appliances == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Elec_AdaptersOK", _elect.Elec_Adapters == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Elec_AdaptersWork", _elect.Elec_Adapters == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Elec_AdaptersNA", _elect.Elec_Adapters == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Elec_GFIOK", _elect.Elec_GFI == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Elec_GFIWork", _elect.Elec_GFI == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Elec_GFINA", _elect.Elec_GFI == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Elec_HeaterOK", _elect.Elec_Heater == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Elec_HeaterWork", _elect.Elec_Heater == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Elec_HeaterNA", _elect.Elec_Heater == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Hazmat_QuantitiesOK", _elect.Hazmat_Quantities == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Hazmat_QuantitiesWork", _elect.Hazmat_Quantities == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Hazmat_QuantitiesNA", _elect.Hazmat_Quantities == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Hazmat_LiquidsOK", _elect.Hazmat_Liquids == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Hazmat_LiquidsWork", _elect.Hazmat_Liquids == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Hazmat_LiquidsNA", _elect.Hazmat_Liquids == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Hazmat_ShelvingOK", _elect.Hazmat_Shelving == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Hazmat_ShelvingWork", _elect.Hazmat_Shelving == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Hazmat_ShelvingNA", _elect.Hazmat_Shelving == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Hazmat_EmptyTanksOK", _elect.Hazmat_EmptyTanks == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Hazmat_EmptyTanksWork", _elect.Hazmat_EmptyTanks == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Hazmat_EmptyTanksNA", _elect.Hazmat_EmptyTanks == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Hazmat_TanksOK", _elect.Hazmat_Tanks == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Hazmat_TanksWork", _elect.Hazmat_Tanks == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Hazmat_TanksNA", _elect.Hazmat_Tanks == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Hazmat_SDSOK", _elect.Hazmat_SDS == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Hazmat_SDSWork", _elect.Hazmat_SDS == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Hazmat_SDSNA", _elect.Hazmat_SDS == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Hazmat_704OK", _elect.Hazmat_704 == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Hazmat_704Work", _elect.Hazmat_704 == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Hazmat_704NA", _elect.Hazmat_704 == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Fuel_ValvesOK", _elect.Fuel_Valves == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Fuel_ValvesWork", _elect.Fuel_Valves == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Fuel_ValvesNA", _elect.Fuel_Valves == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Fuel_ExtinguisherOK", _elect.Fuel_Extinguisher == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Fuel_ExtinguisherWork", _elect.Fuel_Extinguisher == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Fuel_ExtinguisherNA", _elect.Fuel_Extinguisher == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Fuel_SignsOK", _elect.Fuel_Signs == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Fuel_SignsWork", _elect.Fuel_Signs == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Fuel_SignsNA", _elect.Fuel_Signs == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Fuel_MaterialsOK", _elect.Fuel_Materials == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Fuel_MaterialsWork", _elect.Fuel_Materials == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Fuel_MaterialsNA", _elect.Fuel_Materials == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Vent_CapsOK", _elect.Vent_Caps == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Vent_CapsWork", _elect.Vent_Caps == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Vent_CapsNA", _elect.Vent_Caps == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Vent_PullStationOK", _elect.Vent_PullStation == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Vent_PullStationWork", _elect.Vent_PullStation == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Vent_PullStationNA", _elect.Vent_PullStation == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Vent_ExhaustOK", _elect.Vent_Exhaust == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Vent_ExhaustWork", _elect.Vent_Exhaust == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Vent_ExhaustNA", _elect.Vent_Exhaust == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Vent_GasValveOK", _elect.Vent_GasValve == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Vent_GasValveWork", _elect.Vent_GasValve == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Vent_GasValveNA", _elect.Vent_GasValve == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Vent_GaugeOK", _elect.Vent_Gauge == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Vent_GaugeWork", _elect.Vent_Gauge == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Vent_GaugeNA", _elect.Vent_Gauge == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Vent_Hydro", _elect.Vent_Hydro ?? "");
                        //pdfStamper.AcroFields.SetField("Vent_HydroOK", _elect.Vent_Hydro == "O" ? "X" : "");
                        //pdfStamper.AcroFields.SetField("Vent_HydroWork", _elect.Vent_Hydro == "N" ? "X" : "");
                        //pdfStamper.AcroFields.SetField("Vent_HydroNA", _elect.Vent_Hydro == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Vent_BrandDate", _elect.Vent_BrandDate ?? "");
                        //pdfStamper.AcroFields.SetField("Vent_BrandDateOK", _elect.Vent_BrandDate == "O" ? "X" : "");
                        //pdfStamper.AcroFields.SetField("Vent_BrandDateWork", _elect.Vent_BrandDate == "N" ? "X" : "");
                        //pdfStamper.AcroFields.SetField("Vent_BrandDateNA", _elect.Vent_BrandDate == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Vent_CleanlinessOK", _elect.Vent_Cleanliness == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Vent_CleanlinessWork", _elect.Vent_Cleanliness == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Vent_CleanlinessNA", _elect.Vent_Cleanliness == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Vent_ExtinguishersOK", _elect.Vent_Extinguishers == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Vent_ExtinguishersWork", _elect.Vent_Extinguishers == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Vent_ExtinguishersNA", _elect.Vent_Extinguishers == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("VentSvcDate", _elect.Vent_ServiceDate != null ? _elect.Vent_ServiceDate.Value.ToShortDateString() : "");

                        pdfStamper.AcroFields.SetField("FireExt_DocOK", _elect.FireExt_Doc == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("FireExt_DocWork", _elect.FireExt_Doc == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("FireExt_DocNA", _elect.FireExt_Doc == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("FireExt_MountedOK", _elect.FireExt_Mounted == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("FireExt_MountedWork", _elect.FireExt_Mounted == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("FireExt_MountedNA", _elect.FireExt_Mounted == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("FireExt_CollarOK", _elect.FireExt_Collar == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("FireExt_CollarWork", _elect.FireExt_Collar == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("FireExt_CollarNA", _elect.FireExt_Collar == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("FireExt_SysOK", _elect.FireExt_Sys == "O" ? "X" : "");
                        pdfStamper.AcroFields.SetField("FireExt_SysWork", _elect.FireExt_Sys == "N" ? "X" : "");
                        pdfStamper.AcroFields.SetField("FireExt_SysNA", _elect.FireExt_Sys == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Risers_Inspected", _elect.Risers_Inspected ?? "");
                        pdfStamper.AcroFields.SetField("Tagged", _elect.Risers_Tagged ?? "");
                        pdfStamper.AcroFields.SetField("Risers_Sprinkler", _elect.Risers_Sprinkler ?? "");
                        pdfStamper.AcroFields.SetField("Risers_Deficiencies", _elect.Risers_Deficiencies ?? "");
                        pdfStamper.AcroFields.SetField("Risers_Plate", _elect.Risers_Plate ?? "");
                        pdfStamper.AcroFields.SetField("Risers_Gauges", _elect.Risers_Gauges ?? "");


                        pdfStamper.AcroFields.SetField("chkSprinklerOK", _elect.Sprinkler_Cond == "O" ? "Yes" : "");
                        pdfStamper.AcroFields.SetField("chkSprinklerWrong", _elect.Sprinkler_Cond == "W" ? "Yes" : "");
                        pdfStamper.AcroFields.SetField("chkSprinklerReplace", _elect.Sprinkler_Cond == "R" ? "Yes" : "");
                        pdfStamper.AcroFields.SetField("chkSprinklerNA", _elect.Sprinkler_Cond == "NA" ? "Yes" : "");

                        pdfStamper.AcroFields.SetField("chkCeilingTileOK", (_elect.Ceiling_Type == "T" && _elect.Ceiling_Condition == "O") ? "Yes" : "");
                        pdfStamper.AcroFields.SetField("chkCeilingTileRepair", (_elect.Ceiling_Type == "T" && _elect.Ceiling_Condition == "R") ? "Yes" : "");
                        pdfStamper.AcroFields.SetField("chkCeilingHardOK", (_elect.Ceiling_Type == "H" && _elect.Ceiling_Condition == "O") ? "Yes" : "");
                        pdfStamper.AcroFields.SetField("chkCeilingHardRepair", (_elect.Ceiling_Type == "H" && _elect.Ceiling_Condition == "R") ? "Yes" : "");
                        pdfStamper.AcroFields.SetField("chkCeilingOpenOK", (_elect.Ceiling_Type == "O" && _elect.Ceiling_Condition == "O") ? "Yes" : "");
                        pdfStamper.AcroFields.SetField("chkCeilingOpenRepair", (_elect.Ceiling_Type == "O" && _elect.Ceiling_Condition == "R") ? "Yes" : "");


                        pdfStamper.AcroFields.SetField("chkAED_OK", _elect.AED == "O" ? "Yes" : "");
                        pdfStamper.AcroFields.SetField("chkAED_Repair", _elect.AED == "R" ? "Yes" : "");
                        pdfStamper.AcroFields.SetField("AED Expiration", _elect.AED_Expiration != null ? _elect.AED_Expiration.Value.ToShortDateString() : "");

                        pdfStamper.AcroFields.SetField("Corrective", _elect.Corrective ?? "");


                        //add photos if there are any
                        AddPhotos(_elect.OBJECTID, pdfReader, pdfStamper, 9);


                        // flatten the form to remove editting options, set it to false to leave the form open to subsequent manual edits  
                        pdfStamper.FormFlattening = false;
                    }

                    //return value
                    var _rpt = new RptDisplayType
                    {
                        rptContent = ms.ToArray(),
                        rptName = "FireInspection_" + _site.SITE_NAME + "_" + _assess.ASSESSMENT_DT.ToShortDateString().Replace("/", ""),
                    };
                    return _rpt;
                }
            }
            else if (_assess.INSP_TYP == "LIFE")
            {
                T_INSP_ASSESS_LIFE _elect = _DbInspection.GetT_INSP_ASSESS_LIFE_ByAssessID(AssessId);
                if (_elect == null)
                    return null;

                using (MemoryStream ms = new MemoryStream())
                {
                    // create a new PDF reader based on the PDF template document  
                    PdfReader pdfReader = new PdfReader(Path.Combine(_hostingEnvironment.WebRootPath, "files", "LifeInspectionForm.pdf"));

                    //stamper and memorystream
                    using (PdfStamper pdfStamper = new PdfStamper(pdfReader, ms))
                    {
                        //change file metadata
                        var info = pdfReader.Info;
                        info["Producer"] = "Open Environment Software";
                        info["Title"] = "Life Safety Inspection Form";
                        pdfStamper.MoreInfo = info;

                        //fill in form data
                        pdfStamper.AcroFields.SetField("FacilityName", _site.SITE_NAME ?? "");
                        pdfStamper.AcroFields.SetField("BuildingUse", _elect.BuildingType ?? "");

                        pdfStamper.AcroFields.SetField("Inspector", _elect.ASSESSED_BY);
                        pdfStamper.AcroFields.SetField("Title", _elect.Title);
                        pdfStamper.AcroFields.SetField("AssessmentDate", _assess.ASSESSMENT_DT.ToShortDateString() ?? "");

                        pdfStamper.AcroFields.SetField("WalkwayGood", _elect.Walkway == "G" ? "X" : "");
                        pdfStamper.AcroFields.SetField("WalkwayFair", _elect.Walkway == "F" ? "X" : "");
                        pdfStamper.AcroFields.SetField("WalkwayPoor", _elect.Walkway == "P" ? "X" : "");

                        pdfStamper.AcroFields.SetField("WindowsGood", _elect.Windows == "G" ? "X" : "");
                        pdfStamper.AcroFields.SetField("WindowsFair", _elect.Windows == "F" ? "X" : "");
                        pdfStamper.AcroFields.SetField("WindowsPoor", _elect.Windows == "P" ? "X" : "");

                        pdfStamper.AcroFields.SetField("DoorsGood", _elect.Doors == "G" ? "X" : "");
                        pdfStamper.AcroFields.SetField("DoorsFair", _elect.Doors == "F" ? "X" : "");
                        pdfStamper.AcroFields.SetField("DoorsPoor", _elect.Doors == "P" ? "X" : "");

                        pdfStamper.AcroFields.SetField("RoofGood", _elect.Roof == "G" ? "X" : "");
                        pdfStamper.AcroFields.SetField("RoofFair", _elect.Roof == "F" ? "X" : "");
                        pdfStamper.AcroFields.SetField("RoofPoor", _elect.Roof == "P" ? "X" : "");

                        pdfStamper.AcroFields.SetField("FoundationGood", _elect.Foundation == "G" ? "X" : "");
                        pdfStamper.AcroFields.SetField("FoundationFair", _elect.Foundation == "F" ? "X" : "");
                        pdfStamper.AcroFields.SetField("FoundationPoor", _elect.Foundation == "P" ? "X" : "");

                        pdfStamper.AcroFields.SetField("ExteriorGood", _elect.Exterior == "G" ? "X" : "");
                        pdfStamper.AcroFields.SetField("ExteriorFair", _elect.Exterior == "F" ? "X" : "");
                        pdfStamper.AcroFields.SetField("ExteriorPoor", _elect.Exterior == "P" ? "X" : "");

                        pdfStamper.AcroFields.SetField("AddressVisYes", _elect.AddressVisible == "Y" ? "X" : "");
                        pdfStamper.AcroFields.SetField("AddressVisNo", _elect.AddressVisible == "N" ? "X" : "");

                        pdfStamper.AcroFields.SetField("FloorGood", _elect.Floor == "G" ? "X" : "");
                        pdfStamper.AcroFields.SetField("FloorFair", _elect.Floor == "F" ? "X" : "");
                        pdfStamper.AcroFields.SetField("FloorPoor", _elect.Floor == "P" ? "X" : "");

                        pdfStamper.AcroFields.SetField("WallsGood", _elect.Walls == "G" ? "X" : "");
                        pdfStamper.AcroFields.SetField("WallsFair", _elect.Walls == "F" ? "X" : "");
                        pdfStamper.AcroFields.SetField("WallsPoor", _elect.Walls == "P" ? "X" : "");

                        pdfStamper.AcroFields.SetField("CeilingGood", _elect.Ceiling == "G" ? "X" : "");
                        pdfStamper.AcroFields.SetField("CeilingFair", _elect.Ceiling == "F" ? "X" : "");
                        pdfStamper.AcroFields.SetField("CeilingPoor", _elect.Ceiling == "P" ? "X" : "");

                        pdfStamper.AcroFields.SetField("StairsGood", _elect.Stairs == "G" ? "X" : "");
                        pdfStamper.AcroFields.SetField("StairsFair", _elect.Stairs == "F" ? "X" : "");
                        pdfStamper.AcroFields.SetField("StairsPoor", _elect.Stairs == "P" ? "X" : "");

                        pdfStamper.AcroFields.SetField("PanicGood", _elect.Panic == "G" ? "X" : "");
                        pdfStamper.AcroFields.SetField("PanicFair", _elect.Panic == "F" ? "X" : "");
                        pdfStamper.AcroFields.SetField("PanicPoor", _elect.Panic == "P" ? "X" : "");

                        pdfStamper.AcroFields.SetField("DoorGood", _elect.Doors == "G" ? "X" : "");
                        pdfStamper.AcroFields.SetField("DoorFair", _elect.Doors == "F" ? "X" : "");
                        pdfStamper.AcroFields.SetField("DoorPoor", _elect.Doors == "P" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Notes", _elect.Notes ?? "");

                        pdfStamper.AcroFields.SetField("Fire_FireAlarmGood", _elect.Fire_FireAlarm == "G" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Fire_FireAlarmFair", _elect.Fire_FireAlarm == "F" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Fire_FireAlarmPoor", _elect.Fire_FireAlarm == "P" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Fire_Sprinklers1Good", _elect.Fire_Sprinklers1 == "G" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Fire_Sprinklers1Fair", _elect.Fire_Sprinklers1 == "F" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Fire_Sprinklers1Poor", _elect.Fire_Sprinklers1 == "P" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Fire_Sprinklers5Good", _elect.Fire_Sprinklers5 == "G" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Fire_Sprinklers5Fair", _elect.Fire_Sprinklers5 == "F" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Fire_Sprinklers5Poor", _elect.Fire_Sprinklers5 == "P" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Fire_PlateGood", _elect.Fire_Plate == "G" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Fire_PlateFair", _elect.Fire_Plate == "F" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Fire_PlatePoor", _elect.Fire_Plate == "P" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Fire_EmergLightGood", _elect.Fire_EmergLight == "G" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Fire_EmergLightFair", _elect.Fire_EmergLight == "F" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Fire_EmergLightPoor", _elect.Fire_EmergLight == "P" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Fire_ExitGood", _elect.Fire_Exit == "G" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Fire_ExitFair", _elect.Fire_Exit == "F" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Fire_ExitPoor", _elect.Fire_Exit == "P" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Fire_KitchenHoodGood", _elect.Fire_KitchenHood == "G" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Fire_KitchenHoodFair", _elect.Fire_KitchenHood == "F" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Fire_KitchenHoodPoor", _elect.Fire_KitchenHood == "P" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Fire_HoodSupGood", _elect.Fire_HoodSup == "G" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Fire_HoodSupFair", _elect.Fire_HoodSup == "F" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Fire_HoodSupPoor", _elect.Fire_HoodSup == "P" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Fire_EvacDrillGood", _elect.Fire_EvacDrill == "G" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Fire_EvacDrillFair", _elect.Fire_EvacDrill == "F" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Fire_EvacDrillPoor", _elect.Fire_EvacDrill == "P" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Fire_EvacGood", _elect.Fire_Evac == "G" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Fire_EvacFair", _elect.Fire_Evac == "F" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Fire_EvacPoor", _elect.Fire_Evac == "P" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Fire_StorageGood", _elect.Fire_Storage == "G" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Fire_StorageFair", _elect.Fire_Storage == "F" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Fire_StoragePoor", _elect.Fire_Storage == "P" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Fire_PiledGood", _elect.Fire_Piled == "G" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Fire_PiledFair", _elect.Fire_Piled == "F" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Fire_PiledPoor", _elect.Fire_Piled == "P" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Fire_FoodGood", _elect.Fire_Food == "G" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Fire_FoodFair", _elect.Fire_Food == "F" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Fire_FoodPoor", _elect.Fire_Food == "P" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Fire_HazardGood", _elect.Fire_Hazard == "G" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Fire_HazardFair", _elect.Fire_Hazard == "F" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Fire_HazardPoor", _elect.Fire_Hazard == "P" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Fire_HazardNA", _elect.Fire_Hazard == "N" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Fire_SDSGood", _elect.Fire_SDS == "G" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Fire_SDSFair", _elect.Fire_SDS == "F" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Fire_SDSPoor", _elect.Fire_SDS == "P" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Fire_SDSNA", _elect.Fire_SDS == "N" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Fire_ExtinguisherGood", _elect.Fire_Extinguisher == "G" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Fire_ExtinguisherFair", _elect.Fire_Extinguisher == "F" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Fire_ExtinguisherPoor", _elect.Fire_Extinguisher == "P" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Fire_ExtinAnnualGood", _elect.Fire_ExtinAnnual == "G" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Fire_ExtinAnnualFair", _elect.Fire_ExtinAnnual == "F" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Fire_ExtinAnnualPoor", _elect.Fire_ExtinAnnual == "P" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Fire_Notes", _elect.Fire_Notes ?? "");

                        pdfStamper.AcroFields.SetField("Plum_WaterGood", _elect.Plum_Water == "G" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Plum_WaterFair", _elect.Plum_Water == "F" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Plum_WaterPoor", _elect.Plum_Water == "P" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Plum_DrainGood", _elect.Plum_Drain == "G" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Plum_DrainFair", _elect.Plum_Drain == "F" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Plum_DrainPoor", _elect.Plum_Drain == "P" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Plum_SinkGood", _elect.Plum_Sink == "G" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Plum_SinkFair", _elect.Plum_Sink == "F" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Plum_SinkPoor", _elect.Plum_Sink == "P" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Plum_ToiletGood", _elect.Plum_Toilet == "G" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Plum_ToiletFair", _elect.Plum_Toilet == "F" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Plum_ToiletPoor", _elect.Plum_Toilet == "P" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Plum_TubGood", _elect.Plum_Tub == "G" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Plum_TubFair", _elect.Plum_Tub == "F" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Plum_TubPoor", _elect.Plum_Tub == "P" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Plum_TubNA", _elect.Plum_Tub == "N" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Plum_DishGood", _elect.Plum_Dish == "G" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Plum_DishFair", _elect.Plum_Dish == "F" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Plum_DishPoor", _elect.Plum_Dish == "P" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Plum_HeaterGood", _elect.Plum_Heater == "G" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Plum_HeaterFair", _elect.Plum_Heater == "F" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Plum_HeaterPoor", _elect.Plum_Heater == "P" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Plum_BackflowYes", _elect.Plum_Backflow == "Yes" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Plum_BackflowNo", _elect.Plum_Backflow == "No" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Plum_BackflowNA", _elect.Plum_Backflow == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Plum_Notes", _elect.Plum_Notes ?? "");

                        pdfStamper.AcroFields.SetField("Elec_HazardGood", _elect.Elec_Hazard == "G" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Elec_HazardFair", _elect.Elec_Hazard == "F" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Elec_HazardPoor", _elect.Elec_Hazard == "P" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Elec_PanelGood", _elect.Elec_Panel == "G" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Elec_PanelFair", _elect.Elec_Panel == "F" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Elec_PanelPoor", _elect.Elec_Panel == "P" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Elec_LabelGood", _elect.Elec_Label == "G" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Elec_LabelFair", _elect.Elec_Label == "F" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Elec_LabelPoor", _elect.Elec_Label == "P" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Elec_ObstructGood", _elect.Elec_Obstruct == "G" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Elec_ObstructFair", _elect.Elec_Obstruct == "F" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Elec_ObstructPoor", _elect.Elec_Obstruct == "P" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Elec_OutletGood", _elect.Elec_Outlet == "G" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Elec_OutletFair", _elect.Elec_Outlet == "F" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Elec_OutletPoor", _elect.Elec_Outlet == "P" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Elec_LightingGood", _elect.Elec_Lighting == "G" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Elec_LightingFair", _elect.Elec_Lighting == "F" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Elec_LightingPoor", _elect.Elec_Lighting == "P" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Elec_SurgeGood", _elect.Elec_Surge == "G" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Elec_SurgeFair", _elect.Elec_Surge == "F" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Elec_SurgePoor", _elect.Elec_Surge == "P" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Elec_GroundGood", _elect.Elec_Ground == "G" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Elec_GroundFair", _elect.Elec_Ground == "F" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Elec_GroundPoor", _elect.Elec_Ground == "P" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Elec_SignYes", _elect.Elec_Sign == "Yes" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Elec_SignNo", _elect.Elec_Sign == "No" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Elec_SignNA", _elect.Elec_Sign == "NA" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Elec_Notes", _elect.Elec_Notes ?? "");

                        pdfStamper.AcroFields.SetField("Mech_FiltersGood", _elect.Mech_Filters == "G" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Mech_FiltersFair", _elect.Mech_Filters == "F" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Mech_FiltersPoor", _elect.Mech_Filters == "P" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Mech_RegisterGood", _elect.Mech_Register == "G" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Mech_RegisterFair", _elect.Mech_Register == "F" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Mech_RegisterPoor", _elect.Mech_Register == "P" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Mech_EquipGood", _elect.Mech_Equip == "G" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Mech_EquipFair", _elect.Mech_Equip == "F" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Mech_EquipPoor", _elect.Mech_Equip == "P" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Mech_VentAirGood", _elect.Mech_VentAir == "G" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Mech_VentAirFair", _elect.Mech_VentAir == "F" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Mech_VentAirPoor", _elect.Mech_VentAir == "P" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Mech_VentSystGood", _elect.Mech_VentSyst == "G" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Mech_VentSystFair", _elect.Mech_VentSyst == "F" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Mech_VentSystPoor", _elect.Mech_VentSyst == "P" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Mech_PipeGood", _elect.Mech_Pipe == "G" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Mech_PipeFair", _elect.Mech_Pipe == "F" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Mech_PipePoor", _elect.Mech_Pipe == "P" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Mech_ThermGood", _elect.Mech_Therm == "G" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Mech_ThermFair", _elect.Mech_Therm == "F" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Mech_ThermPoor", _elect.Mech_Therm == "P" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Mech_DuctGood", _elect.Mech_Duct == "G" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Mech_DuctFair", _elect.Mech_Duct == "F" ? "X" : "");
                        pdfStamper.AcroFields.SetField("Mech_DuctPoor", _elect.Mech_Duct == "P" ? "X" : "");

                        pdfStamper.AcroFields.SetField("Mech_Notes", _elect.Mech_Notes ?? "");

                        //add photos if there are any
                        AddPhotos(_elect.OBJECTID, pdfReader, pdfStamper, 8);


                        // flatten the form to remove editting options, set it to false to leave the form open to subsequent manual edits  
                        pdfStamper.FormFlattening = false;



                    }

                    //return value
                    var _rpt = new RptDisplayType
                    {
                        rptContent = ms.ToArray(),
                        rptName = "LifeSafetyInspection_" + _site.SITE_NAME + "_" + _assess.ASSESSMENT_DT.ToShortDateString().Replace("/", ""),
                    };
                    return _rpt;
                }
            }
            return null;
        }

        private void AddPhotos(int? OBJECTID, PdfReader pdfReader, PdfStamper pdfStamper, int layerNum)
        {
            //add attachments
            if (OBJECTID != null)
            {
                var appPath = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
                var _esriHelper = new esriHelper(_DbPortal);
                var _atts = _esriHelper.DownloadAttachments(appPath, layerNum, OBJECTID.Value);

                if (_atts != null && _atts.Count > 0)
                {
                    var pageSize = pdfReader.GetPageSize(1);

                    int imagesPerPage = 2;
                    //int imagesPerPage = 6;  //6 images per page (3 rows, 2 columns)
                    int totalPages = (int)Math.Ceiling(_atts.Count / (double)imagesPerPage);
                    int originalPageCount = pdfReader.NumberOfPages;

                    // Insert required pages
                    for (int p = 1; p <= totalPages; p++)
                        pdfStamper.InsertPage(originalPageCount + p, pageSize);

                    for (int i = 0; i < _atts.Count; i++)
                    {
                        int currentPage = originalPageCount + (i / imagesPerPage) + 1;
                        var content = pdfStamper.GetOverContent(currentPage);
                        try
                        {
                            iTextSharp.text.Image pic = iTextSharp.text.Image.GetInstance(_atts[i].Data, true);
                            if (pic.Height > pic.Width)
                                pic.RotationDegrees = 90f; // this will rotate the image to 90 degree clockwise

                            float scaleFactor = 465 / pic.Width;
                            pic.ScaleAbsoluteWidth(pic.Width * scaleFactor);
                            pic.ScaleAbsoluteHeight(pic.Height * scaleFactor);

                            //determine place on page
                            //int row = (i % imagesPerPage) / 2; // 0-2  (6 images per page option)
                            int row = (i % 2 == 0) ? 0 : 1; //(2 images per page option)

                            //int col = (i % 2); // 0 or 1 (6 images per page option)
                            int col = 0;  // (2 images per page option)

                            float x = 50 + 240 * col;
                            //float y = 50 + (2 - row) * 250;  // Adjust for PDF height (start from top)  6 images per page option
                            float y = 40f + (1 - row) * 365;
                            pic.SetAbsolutePosition(x, y);
                            content.AddImage(pic);

                            // draw filename text just below the image
                            var bf = iTextSharp.text.pdf.BaseFont.CreateFont(
                                iTextSharp.text.pdf.BaseFont.HELVETICA,
                                iTextSharp.text.pdf.BaseFont.CP1252,
                                iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);

                            content.BeginText();
                            content.SetFontAndSize(bf, 10);
                            content.ShowTextAligned(
                                iTextSharp.text.Element.ALIGN_LEFT,
                                _atts[i].FileName,          // from your tuple/class
                                x,                          // same X as image
                                y - 12,                     // a few points below image
                                0);
                            content.EndText();
                        }
                        catch { }
                    }
                }
            }
        }
    }
}
