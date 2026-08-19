using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using TribalSvcPortal.AppLogic.BusinessLogicLayer;
using TribalSvcPortal.Data.Models;

namespace TribalSvcPortal.AppLogic.DataAccessLayer
{
    public interface IDbInspection
    {

        //************** T_INSP_SITES **********************************
        Guid? InsertUpdateT_INSP_SITES(Guid sITE_IDX, string lAST_UPDATE_BY, DateTime? lAST_UPDATE_DT);

        //************** T_INSP_ASSESS_PLUMBING **********************************
        Guid? InsertUpdateT_INSP_ASSESS_PLUMBING(Guid? aSSESSMENT_IDX, Guid? sITE_IDX, DateTime? aSSESSMENT_DT, string aSSESSED_BY, int? oBJECTID, Guid? globalID, string title, string waterPSI, string meter_location, string supply_lines, string drains, string sewer_loc, string sink_faucet, string toilet_urinal, string shower_hub, string dishwasher, string water_heater, string heater_type, string backflow, string repairs);
        T_INSP_ASSESS_PLUMBING GetT_INSP_ASSESS_PLUMBING_ByAssessID(Guid id);


        //************** T_INSP_ASSESS_ELECTRICAL **********************************
        Guid? InsertUpdateT_INSP_ASSESS_ELECTRICAL(Guid? aSSESSMENT_IDX, Guid? sITE_IDX, DateTime? aSSESSMENT_DT, string aSSESSED_BY, int? oBJECTID, Guid? globalID, string title, string contact, string compliance, string gen_site, string gen_hazard, string gen_address, string elec_rust, string elec_label, string elec_space, string wiring_secured, string wiring_damage, string wiring_separate, string Outlet_Func, string Outlet_GFCI, string Outlet_Tamper, string Light_Fixtures, string Light_Exit, string Light_Ill, string Ground_Connections, string Ground_Corrosion, string Ground_Electrode, string Surge_Protectors, string Surge_Func, string Comp_Codes, string Comp_Signage, string comp_records, string corrective, string BuildingType);
        T_INSP_ASSESS_ELECTRICAL GetT_INSP_ASSESS_ELECTRICAL_ByAssessID(Guid id);


        //************** T_INSP_ASSESS_MECHANICAL **********************************
        Guid? InsertUpdateT_INSP_ASSESS_MECHANICAL(Guid? aSSESSMENT_IDX, Guid? sITE_IDX, DateTime? aSSESSMENT_DT, string aSSESSED_BY, int? oBJECTID, Guid? globalID, string title, string contact, string compliance, string HVAC_Clean, string HVAC_Filters, string HVAC_Therm, string Duct_Leak, string Duct_Insulation, string Duct_Obstruction, string Piping_Leaks, string Piping_Insulation, string Piping_Valves, string Equip_Operational, string Equip_Doc, string Equip_Guards, string Vent_Airflow, string Vent_Exhaust, string Vent_Air, string Comp_Codes, string Comp_Signage, string Comp_Logs, string Corrective, string BuildingType);
        T_INSP_ASSESS_MECHANICAL GetT_INSP_ASSESS_MECHANICAL_ByAssessID(Guid id);


        //************** V_INSP_ASSESSMENTS **********************************
        List<V_INSP_ASSESSMENTS> GetV_INSP_ASSESSMENTS(string assessType, string str, string bldg);
        V_INSP_ASSESSMENTS GetV_INSP_ASSESSMENTS_ByAssessID(Guid id);


        //************** T_INSP_ASSESS_FIRE **********************************
        T_INSP_ASSESS_FIRE GetT_INSP_ASSESS_FIRE_ByAssessID(Guid id);
        Guid? InsertUpdateT_INSP_ASSESS_FIRE(Guid? aSSESSMENT_IDX, Guid? sITE_IDX, DateTime? aSSESSMENT_DT, string aSSESSED_BY, int? oBJECTID, Guid? globalID, string title, string contact, string compliance, string corrective, string Doc_FireAlarm, string Doc_FireSprinkler5, string Doc_FireSprinklerAnn, string Doc_EmergPwr, string Doc_EmergLight, string Doc_KitchenHoodSys, string Doc_Generator, string Doc_EvacDrill, string FireDept_Address, string FireDept_AccessKeys, string FireDept_Access, string FireDept_Valves, string Egress_Path, string Egress_Signs, string Egress_Latch, string Egress_DoorsOpen, string Egress_DoorsKey, string Egress_Evac, string Egress_Storage, string Storage_Housekeeping, string Storage_Cylinders, string Storage_Sprinklers, string Storage_Combust, string Storage_OilRags, string Storage_Food, string ELec_Covers, string Elec_Panels, string Elec_Clearance, string Elec_Cords, string Elec_NoCord, string Elec_Appliances, string Elec_Adapters, string Elec_GFI, string Elec_Heater, string Hazmat_Quantities, string Hazmat_Liquids, string Hazmat_Shelving, string Hazmat_Tanks, string Hazmat_EmptyTanks, string Hazmat_SDS, string Hazmat_704, string Fuel_Valves, string Fuel_Extinguisher, string Fuel_Signs, string Fuel_Materials, string Vent_Caps, string Vent_PullStation, string Vent_Exhaust, string Vent_GasValve, string Vent_Gauge, string Vent_Hydro, string Vent_BrandDate, string Vent_Cleanliness, string Vent_Extinguishers, DateTime? Vent_ServiceDate, string FireExt_Doc, string FireExt_Mounted, string FireExt_Collar, string FireExt_Sys, string Risers_Inspected, string Risers_Tagged, string Risers_Sprinkler, string Risers_Deficiencies, string Risers_Plate, string Risers_Gauges, string Sprinkler_Cond, string Ceiling_Type, string Ceiling_Condition, string AED, DateTime? AED_Expiration, string Facility_Use);


        //************** T_INSP_ASSESS_BUILDING **********************************
        T_INSP_ASSESS_BUILDING GetT_INSP_ASSESS_BUILDING_ByAssessID(Guid id);
        Guid? InsertUpdateT_INSP_ASSESS_BUILDING(Guid? aSSESSMENT_IDX, Guid? sITE_IDX, DateTime? aSSESSMENT_DT, string aSSESSED_BY, int? oBJECTID, Guid? globalID, string title, string Walkway, string Windows, string Doors, string Roof, string Foundation, string Exterior, string Floor, string Walls, string Ceiling, string Stairs, string Panic, string Closures, string Notes, string BuildingType);

        //************** T_INSP_ASSESS_LIFE **********************************
        T_INSP_ASSESS_LIFE GetT_INSP_ASSESS_LIFE_ByAssessID(Guid id);
        Guid? InsertUpdateT_INSP_ASSESS_LIFE(Guid? aSSESSMENT_IDX, Guid? sITE_IDX, DateTime? aSSESSMENT_DT, string aSSESSED_BY, int? oBJECTID, Guid? globalID, string title, string Walkway, string Windows, string Doors, string Roof, string Foundation, string Exterior, string AddressVisible, string Floor, string Walls, string Ceiling, string Stairs, string Panic, string Closures, string Notes, string BuildingType, string Fire_FireAlarm, string Fire_Sprinklers1, string Fire_Sprinklers5, string Fire_Plate, string Fire_EmergLight, string Fire_Exit, string Fire_KitchenHood, string Fire_HoodSup, string Fire_EvacDrill, string Fire_Evac, string Fire_Storage, string Fire_Piled, string Fire_Food, string Fire_Hazard, string Fire_SDS, string Fire_Extinguisher, string Fire_ExtinAnnual, string Fire_Notes, string Plum_Water, string Plum_Drain, string Plum_Sink, string Plum_Toilet, string Plum_Tub, string Plum_Dish, string Plum_Heater, string Plum_Backflow, string Plum_Notes, string Elec_Hazard, string Elec_Panel, string Elec_Label, string Elec_Obstruct, string Elec_Outlet, string Elec_Lighting, string Elec_Surge, string Elec_Ground, string Elec_Sign, string Elec_Notes, string Mech_Filters, string Mech_Register, string Mech_Equip, string Mech_VentAir, string Mech_VentSyst, string Mech_Pipe, string Mech_Therm, string Mech_Duct, string Mech_Notes);


        //*****other*********************
        IEnumerable<SelectListItem> get_ddl_AssessType();
    }


    public class DbInspection : IDbInspection
    {
        private readonly ApplicationDbContext ctx;
        private readonly Ilog _log;

        public DbInspection(ApplicationDbContext _context, Ilog log)
        {
            ctx = _context;
            _log = log ?? throw new ArgumentNullException(nameof(log));
        }

        //************** T_INSP_SITES **********************************

        public Guid? InsertUpdateT_INSP_SITES(Guid sITE_IDX, string lAST_UPDATE_BY, DateTime? lAST_UPDATE_DT)
        {
            try
            {
                Boolean insInd = false;

                T_INSP_SITES e = (from c in ctx.T_INSP_SITES
                                where c.SITE_IDX == sITE_IDX
                                select c).FirstOrDefault();

                //insert case
                if (e == null)
                {
                    insInd = true;
                    e = new T_INSP_SITES();
                    e.SITE_IDX = sITE_IDX;
                }

                if (lAST_UPDATE_BY != null) e.LAST_UPDATE_BY = lAST_UPDATE_BY;
                if (lAST_UPDATE_DT != null) e.LAST_UPDATE_DT = lAST_UPDATE_DT;

                if (insInd)
                    ctx.T_INSP_SITES.Add(e);
                ctx.SaveChanges();
                return e.SITE_IDX;
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }


        //************** T_INSP_ASSESS_PLUMBING **********************************

        public Guid? InsertUpdateT_INSP_ASSESS_PLUMBING(Guid? aSSESSMENT_IDX, Guid? sITE_IDX, DateTime? aSSESSMENT_DT, string aSSESSED_BY, int? oBJECTID, Guid? globalID, string title, 
            string waterPSI, string meter_location, string supply_lines, string drains, string sewer_loc, string sink_faucet, string toilet_urinal, string shower_hub, string dishwasher, 
            string water_heater, string heater_type, string backflow, string repairs)
        {
            try
            {
                bool insInd = false;

                //first get based on our unique key
                T_INSP_ASSESS_PLUMBING e = (from c in ctx.T_INSP_ASSESS_PLUMBING
                                            where c.ASSESSMENT_IDX == aSSESSMENT_IDX
                                            select c).FirstOrDefault();

                //then get based on ESRI unique key which is OBJECTID
                if (e == null)
                {
                    e = (from c in ctx.T_INSP_ASSESS_PLUMBING
                         where c.OBJECTID == oBJECTID
                         select c).FirstOrDefault();
                }

                //insert case
                if (e == null)
                {
                    insInd = true;
                    e = new T_INSP_ASSESS_PLUMBING();
                    e.ASSESSMENT_IDX = Guid.NewGuid();
                }

                if (sITE_IDX != null) e.SITE_IDX = sITE_IDX.Value;
                if (aSSESSMENT_DT != null) e.ASSESSMENT_DT = aSSESSMENT_DT.Value;
                if (aSSESSED_BY != null) e.ASSESSED_BY = aSSESSED_BY;
                if (oBJECTID != null) e.OBJECTID = oBJECTID.Value;
                if (globalID != null) e.GlobalID = globalID;
                if (title != null) e.Title = title;
                if (waterPSI != null) e.WaterPSI = waterPSI;
                if (meter_location != null) e.Meter_Location = meter_location.SubStringPlus(0,50);
                if (supply_lines != null) e.Supply_Lines = supply_lines;
                if (drains != null) e.Drains = drains;
                if (sewer_loc != null) e.Sewer_Loc = sewer_loc.SubStringPlus(0,50);
                if (sink_faucet != null) e.Sink_Faucet = sink_faucet;
                if (toilet_urinal != null) e.Toilet_Urinal = toilet_urinal;
                if (shower_hub != null) e.Shower_Tub = shower_hub;
                if (dishwasher != null) e.Dishwasher = dishwasher;
                if (water_heater != null) e.Water_Heater = water_heater;
                if (heater_type != null) e.Heater_Type = heater_type;
                if (backflow != null) e.Backflow = backflow;
                if (repairs != null) e.Repairs = repairs;

                if (insInd)
                    ctx.T_INSP_ASSESS_PLUMBING.Add(e);
                ctx.SaveChanges();
                return e.SITE_IDX;
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }

        public T_INSP_ASSESS_PLUMBING GetT_INSP_ASSESS_PLUMBING_ByAssessID(Guid id)
        {
            try
            {
                return ctx.T_INSP_ASSESS_PLUMBING.Where(x => x.ASSESSMENT_IDX == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }

        //************** T_INSP_ASSESS_ELECTRICAL **********************************

        public Guid? InsertUpdateT_INSP_ASSESS_ELECTRICAL(Guid? aSSESSMENT_IDX, Guid? sITE_IDX, DateTime? aSSESSMENT_DT, string aSSESSED_BY, int? oBJECTID, Guid? globalID, string title,
            string contact, string compliance, string gen_site, string gen_hazard, string gen_address, string elec_rust, string elec_label, string elec_space, string wiring_secured,
            string wiring_damage, string wiring_separate, string Outlet_Func, string Outlet_GFCI, string Outlet_Tamper, string Light_Fixtures, string Light_Exit, string Light_Ill, 
            string Ground_Connections, string Ground_Corrosion, string Ground_Electrode, string Surge_Protectors, string Surge_Func, string Comp_Codes, string Comp_Signage, 
            string comp_records, string corrective, string BuildingType)
        {
            try
            {
                bool insInd = false;

                //first get based on our unique key
                T_INSP_ASSESS_ELECTRICAL e = ctx.T_INSP_ASSESS_ELECTRICAL.Where(x=>x.ASSESSMENT_IDX == aSSESSMENT_IDX).FirstOrDefault();

                //then get based on ESRI unique key which is OBJECTID
                if (e == null)
                    e = ctx.T_INSP_ASSESS_ELECTRICAL.Where(x=>x.OBJECTID == oBJECTID).FirstOrDefault();

                //insert case
                if (e == null)
                {
                    insInd = true;
                    e = new T_INSP_ASSESS_ELECTRICAL();
                    e.ASSESSMENT_IDX = Guid.NewGuid();
                }

                if (sITE_IDX != null) e.SITE_IDX = sITE_IDX.Value;
                if (aSSESSMENT_DT != null) e.ASSESSMENT_DT = aSSESSMENT_DT.Value;
                if (aSSESSED_BY != null) e.ASSESSED_BY = aSSESSED_BY;
                if (oBJECTID != null) e.OBJECTID = oBJECTID.Value;
                if (globalID != null) e.GlobalID = globalID;
                if (title != null) e.Title = title;
                if (contact != null) e.Contact = contact;
                if (compliance != null) e.Compliance = compliance;
                if (gen_site != null) e.Gen_Site = gen_site;
                if (gen_hazard != null) e.Gen_Hazard = gen_hazard;
                if (gen_address != null) e.Gen_Address = gen_address;
                if (elec_rust != null) e.Elec_Rust = elec_rust;
                if (elec_label != null) e.Elec_Label = elec_label;
                if (elec_space != null) e.Elec_Space = elec_space;
                if (wiring_secured != null) e.Wiring_Secured = wiring_secured;
                if (wiring_damage != null) e.Wiring_Damage = wiring_damage;
                if (wiring_separate != null) e.Wiring_Separate = wiring_separate;
                if (Outlet_Func != null) e.Outlet_Func = Outlet_Func;
                if (Outlet_GFCI != null) e.Outlet_GFCI = Outlet_GFCI;
                if (Outlet_Tamper != null) e.Outlet_Tamper = Outlet_Tamper;
                if (Light_Fixtures != null) e.Light_Fixtures = Light_Fixtures;
                if (Light_Exit != null) e.Light_Exit = Light_Exit;
                if (Light_Ill != null) e.Light_Ill = Light_Ill;
                if (Ground_Connections != null) e.Ground_Connections = Ground_Connections;
                if (Ground_Corrosion != null) e.Ground_Corrosion = Ground_Corrosion;
                if (Ground_Electrode != null) e.Ground_Electrode = Ground_Electrode;
                if (Surge_Protectors != null) e.Surge_Protectors = Surge_Protectors;
                if (Surge_Func != null) e.Surge_Func = Surge_Func;
                if (Comp_Codes != null) e.Comp_Codes = Comp_Codes;
                if (Comp_Signage != null) e.Comp_Signage = Comp_Signage;
                if (comp_records != null) e.Comp_Records = comp_records;
                if (corrective != null) e.Corrective = corrective;
                if (BuildingType != null) e.BuildingType = BuildingType;

                if (insInd)
                    ctx.T_INSP_ASSESS_ELECTRICAL.Add(e);
                ctx.SaveChanges();
                return e.SITE_IDX;
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }

        public T_INSP_ASSESS_ELECTRICAL GetT_INSP_ASSESS_ELECTRICAL_ByAssessID(Guid id)
        {
            try
            {
                return ctx.T_INSP_ASSESS_ELECTRICAL.Where(x => x.ASSESSMENT_IDX == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }


        //************** T_INSP_ASSESS_MECHANICAL **********************************
        public Guid? InsertUpdateT_INSP_ASSESS_MECHANICAL(Guid? aSSESSMENT_IDX, Guid? sITE_IDX, DateTime? aSSESSMENT_DT, string aSSESSED_BY, int? oBJECTID, Guid? globalID, string title,
            string contact, string compliance, string HVAC_Clean, string HVAC_Filters, string HVAC_Therm, string Duct_Leak, string Duct_Insulation, string Duct_Obstruction, string Piping_Leaks, 
            string Piping_Insulation, string Piping_Valves, string Equip_Operational, string Equip_Doc, string Equip_Guards, string Vent_Airflow, string Vent_Exhaust, string Vent_Air, 
            string Comp_Codes, string Comp_Signage, string Comp_Logs, string Corrective, string BuildingType)
        {
            try
            {
                bool insInd = false;

                //first get based on our unique key
                T_INSP_ASSESS_MECHANICAL e = ctx.T_INSP_ASSESS_MECHANICAL.Where(x => x.ASSESSMENT_IDX == aSSESSMENT_IDX).FirstOrDefault();

                //then get based on ESRI unique key which is OBJECTID
                if (e == null)
                    e = ctx.T_INSP_ASSESS_MECHANICAL.Where(x => x.OBJECTID == oBJECTID).FirstOrDefault();

                //insert case
                if (e == null)
                {
                    insInd = true;
                    e = new T_INSP_ASSESS_MECHANICAL();
                    e.ASSESSMENT_IDX = Guid.NewGuid();
                }

                if (sITE_IDX != null) e.SITE_IDX = sITE_IDX.Value;
                if (aSSESSMENT_DT != null) e.ASSESSMENT_DT = aSSESSMENT_DT.Value;
                if (aSSESSED_BY != null) e.ASSESSED_BY = aSSESSED_BY;
                if (oBJECTID != null) e.OBJECTID = oBJECTID.Value;
                if (globalID != null) e.GlobalID = globalID;
                if (title != null) e.Title = title;
                if (contact != null) e.Contact = contact;
                if (compliance != null) e.Compliance = compliance;

                if (HVAC_Clean != null) e.HVAC_Clean = HVAC_Clean;
                if (HVAC_Filters != null) e.HVAC_Filters = HVAC_Filters;
                if (HVAC_Therm != null) e.HVAC_Therm = HVAC_Therm;
                if (Duct_Leak != null) e.Duct_Leak = Duct_Leak;
                if (Duct_Insulation != null) e.Duct_Insulation = Duct_Insulation;
                if (Duct_Obstruction != null) e.Duct_Obstruction = Duct_Obstruction;
                if (Piping_Leaks != null) e.Piping_Leaks = Piping_Leaks;
                if (Piping_Insulation != null) e.Piping_Insulation = Piping_Insulation;
                if (Piping_Valves != null) e.Piping_Valves = Piping_Valves;
                if (Equip_Operational != null) e.Equip_Operational = Equip_Operational;
                if (Equip_Doc != null) e.Equip_Doc = Equip_Doc;
                if (Equip_Guards != null) e.Equip_Guards = Equip_Guards;
                if (Vent_Airflow != null) e.Vent_Airflow = Vent_Airflow;
                if (Vent_Exhaust != null) e.Vent_Exhaust = Vent_Exhaust;
                if (Vent_Air != null) e.Vent_Air = Vent_Air;
                if (Comp_Codes != null) e.Comp_Codes = Comp_Codes;
                if (Comp_Signage != null) e.Comp_Signage = Comp_Signage;
                if (Comp_Logs != null) e.Comp_Records = Comp_Logs;
                if (Corrective != null) e.Corrective = Corrective;
                if (BuildingType != null) e.BuildingType = BuildingType;

                if (insInd)
                    ctx.T_INSP_ASSESS_MECHANICAL.Add(e);
                ctx.SaveChanges();
                return e.SITE_IDX;
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }

        public T_INSP_ASSESS_MECHANICAL GetT_INSP_ASSESS_MECHANICAL_ByAssessID(Guid id)
        {
            try
            {
                return ctx.T_INSP_ASSESS_MECHANICAL.Where(x => x.ASSESSMENT_IDX == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }

        //************** T_INSP_ASSESS_FIRE **********************************
        public Guid? InsertUpdateT_INSP_ASSESS_FIRE(Guid? aSSESSMENT_IDX, Guid? sITE_IDX, DateTime? aSSESSMENT_DT, string aSSESSED_BY, int? oBJECTID, Guid? globalID, string title,
            string contact, string compliance, string corrective, string Doc_FireAlarm, string Doc_FireSprinkler5, string Doc_FireSprinklerAnn, string Doc_EmergPwr, string Doc_EmergLight, 
            string Doc_KitchenHoodSys, string Doc_Generator, string Doc_EvacDrill, string FireDept_Address, string FireDept_AccessKeys, string FireDept_Access, string FireDept_Valves, 
            string Egress_Path, string Egress_Signs, string Egress_Latch, string Egress_DoorsOpen, string Egress_DoorsKey, string Egress_Evac, string Egress_Storage, string Storage_Housekeeping, 
            string Storage_Cylinders, string Storage_Sprinklers, string Storage_Combust, string Storage_OilRags, string Storage_Food, string ELec_Covers, string Elec_Panels, 
            string Elec_Clearance, string Elec_Cords, string Elec_NoCord, string Elec_Appliances, string Elec_Adapters, string Elec_GFI, string Elec_Heater, string Hazmat_Quantities, 
            string Hazmat_Liquids, string Hazmat_Shelving, string Hazmat_Tanks, string Hazmat_EmptyTanks, string Hazmat_SDS, string Hazmat_704, string Fuel_Valves, string Fuel_Extinguisher, 
            string Fuel_Signs, string Fuel_Materials, string Vent_Caps, string Vent_PullStation, string Vent_Exhaust, string Vent_GasValve, string Vent_Gauge, string Vent_Hydro, 
            string Vent_BrandDate, string Vent_Cleanliness, string Vent_Extinguishers, DateTime? Vent_ServiceDate, string FireExt_Doc, string FireExt_Mounted, string FireExt_Collar, 
            string FireExt_Sys, string Risers_Inspected, string Risers_Tagged, string Risers_Sprinkler, string Risers_Deficiencies, string Risers_Plate, string Risers_Gauges, 
            string Sprinkler_Cond, string Ceiling_Type, string Ceiling_Condition, string AED, DateTime? AED_Expiration, string Facility_Use)
        {
            try
            {
                bool insInd = false;

                //first get based on our unique key
                T_INSP_ASSESS_FIRE e = ctx.T_INSP_ASSESS_FIRE.Where(x => x.ASSESSMENT_IDX == aSSESSMENT_IDX).FirstOrDefault();

                //then get based on ESRI unique key which is OBJECTID
                if (e == null)
                    e = ctx.T_INSP_ASSESS_FIRE.Where(x => x.OBJECTID == oBJECTID).FirstOrDefault();

                //insert case
                if (e == null)
                {
                    insInd = true;
                    e = new T_INSP_ASSESS_FIRE();
                    e.ASSESSMENT_IDX = Guid.NewGuid();
                }

                if (sITE_IDX != null) e.SITE_IDX = sITE_IDX.Value;
                if (aSSESSMENT_DT != null) e.ASSESSMENT_DT = aSSESSMENT_DT.Value;
                if (aSSESSED_BY != null) e.ASSESSED_BY = aSSESSED_BY;
                if (oBJECTID != null) e.OBJECTID = oBJECTID.Value;
                if (globalID != null) e.GlobalID = globalID;
                if (title != null) e.Title = title;
                if (contact != null) e.FacContact = contact.SubStringPlus(0,80);
                if (compliance != null) e.Compliance = compliance;
                if (corrective != null) e.Corrective = corrective;

                if (Doc_FireAlarm != null) e.Doc_FireAlarm = Doc_FireAlarm;
                if (Doc_FireSprinkler5 != null) e.Doc_FireSprinkler5 = Doc_FireSprinkler5;
                if (Doc_FireSprinklerAnn != null) e.Doc_FireSprinklerAnn = Doc_FireSprinklerAnn;
                if (Doc_EmergPwr != null) e.Doc_EmergPwr = Doc_EmergPwr;
                if (Doc_EmergLight != null) e.Doc_EmergLight = Doc_EmergLight;
                if (Doc_KitchenHoodSys != null) e.Doc_KitchenHoodSys = Doc_KitchenHoodSys;
                if (Doc_Generator != null) e.Doc_Generator = Doc_Generator;
                if (Doc_EvacDrill != null) e.Doc_EvacDrill = Doc_EvacDrill;

                if (FireDept_Address != null) e.FireDept_Address = FireDept_Address;
                if (FireDept_AccessKeys != null) e.FireDept_AccessKeys = FireDept_AccessKeys;
                if (FireDept_Access != null) e.FireDept_Access = FireDept_Access;
                if (FireDept_Valves != null) e.FireDept_Valves = FireDept_Valves;

                if (Egress_Path != null) e.Egress_Path = Egress_Path;
                if (Egress_Signs != null) e.Egress_Signs = Egress_Signs;
                if (Egress_Latch != null) e.Egress_Latch = Egress_Latch;
                if (Egress_DoorsOpen != null) e.Egress_DoorsOpen = Egress_DoorsOpen;
                if (Egress_DoorsKey != null) e.Egress_DoorsKey = Egress_DoorsKey;
                if (Egress_Evac != null) e.Egress_Evac = Egress_Evac;
                if (Egress_Storage != null) e.Egress_Storage = Egress_Storage;

                if (Storage_Housekeeping != null) e.Storage_Housekeeping = Storage_Housekeeping;
                if (Storage_Cylinders != null) e.Storage_Cylinders = Storage_Cylinders;
                if (Storage_Sprinklers != null) e.Storage_Sprinklers = Storage_Sprinklers;
                if (Storage_Combust != null) e.Storage_Combust = Storage_Combust;
                if (Storage_OilRags != null) e.Storage_OilRags = Storage_OilRags;
                if (Storage_Food != null) e.Storage_Food = Storage_Food;

                if (ELec_Covers != null) e.ELec_Covers = ELec_Covers;
                if (Elec_Panels != null) e.Elec_Panels = Elec_Panels;
                if (Elec_Clearance != null) e.Elec_Clearance = Elec_Clearance;
                if (Elec_Cords != null) e.Elec_Cords = Elec_Cords;
                if (Elec_NoCord != null) e.Elec_NoCord = Elec_NoCord;
                if (Elec_Appliances != null) e.Elec_Appliances = Elec_Appliances;
                if (Elec_Adapters != null) e.Elec_Adapters = Elec_Adapters;
                if (Elec_GFI != null) e.Elec_GFI = Elec_GFI;
                if (Elec_Heater != null) e.Elec_Heater = Elec_Heater;

                if (Hazmat_Quantities != null) e.Hazmat_Quantities = Hazmat_Quantities;
                if (Hazmat_Liquids != null) e.Hazmat_Liquids = Hazmat_Liquids;
                if (Hazmat_Shelving != null) e.Hazmat_Shelving = Hazmat_Shelving;
                if (Hazmat_Tanks != null) e.Hazmat_Tanks = Hazmat_Tanks;
                if (Hazmat_EmptyTanks != null) e.Hazmat_EmptyTanks = Hazmat_EmptyTanks;
                if (Hazmat_SDS != null) e.Hazmat_SDS = Hazmat_SDS;
                if (Hazmat_704 != null) e.Hazmat_704 = Hazmat_704;

                if (Fuel_Valves != null) e.Fuel_Valves = Fuel_Valves;
                if (Fuel_Extinguisher != null) e.Fuel_Extinguisher = Fuel_Extinguisher;
                if (Fuel_Signs != null) e.Fuel_Signs = Fuel_Signs;
                if (Fuel_Materials != null) e.Fuel_Materials = Fuel_Materials;

                if (Vent_Caps != null) e.Vent_Caps = Vent_Caps;
                if (Vent_PullStation != null) e.Vent_PullStation = Vent_PullStation;
                if (Vent_Exhaust != null) e.Vent_Exhaust = Vent_Exhaust;
                if (Vent_GasValve != null) e.Vent_GasValve = Vent_GasValve;
                if (Vent_Gauge != null) e.Vent_Gauge = Vent_Gauge;
                if (Vent_Hydro != null) e.Vent_Hydro = Vent_Hydro.SubStringPlus(0, 200);
                if (Vent_BrandDate != null) e.Vent_BrandDate = Vent_BrandDate;
                if (Vent_Cleanliness != null) e.Vent_Cleanliness = Vent_Cleanliness;
                if (Vent_Extinguishers != null) e.Vent_Extinguishers = Vent_Extinguishers;
                if (Vent_ServiceDate != null) e.Vent_ServiceDate = Vent_ServiceDate;
                if (Vent_ServiceDate?.Year == 1970) e.Vent_ServiceDate = null; //null date handling

                if (FireExt_Doc != null) e.FireExt_Doc = FireExt_Doc;
                if (FireExt_Mounted != null) e.FireExt_Mounted = FireExt_Mounted;
                if (FireExt_Collar != null) e.FireExt_Collar = FireExt_Collar;
                if (FireExt_Sys != null) e.FireExt_Sys = FireExt_Sys;

                if (Risers_Inspected != null) e.Risers_Inspected = Risers_Inspected;
                if (Risers_Inspected == "1/1/1970") e.Risers_Inspected = null; //null date handling
                if (Risers_Tagged != null) e.Risers_Tagged = Risers_Tagged;
                if (Risers_Sprinkler != null) e.Risers_Sprinkler = Risers_Sprinkler;
                if (Risers_Deficiencies != null) e.Risers_Deficiencies = Risers_Deficiencies;
                if (Risers_Plate != null) e.Risers_Plate = Risers_Plate;
                if (Risers_Gauges != null) e.Risers_Gauges = Risers_Gauges;

                if (Sprinkler_Cond != null) e.Sprinkler_Cond = Sprinkler_Cond;
                if (Ceiling_Type != null) e.Ceiling_Type = Ceiling_Type.SubStringPlus(0, 50);
                if (Ceiling_Condition != null) e.Ceiling_Condition = Ceiling_Condition;

                if (AED != null) e.AED = AED;
                if (AED_Expiration != null) e.AED_Expiration = AED_Expiration;
                if (AED_Expiration?.Year == 1970) e.AED_Expiration = null;//null date handling

                if (Facility_Use != null) e.Facility_Use = Facility_Use;

                if (insInd)
                    ctx.T_INSP_ASSESS_FIRE.Add(e);
                ctx.SaveChanges();
                return e.SITE_IDX;
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }

        public T_INSP_ASSESS_FIRE GetT_INSP_ASSESS_FIRE_ByAssessID(Guid id)
        {
            try
            {
                return ctx.T_INSP_ASSESS_FIRE.Where(x => x.ASSESSMENT_IDX == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }


        //************** T_INSP_ASSESS_BUILDING **********************************
        public T_INSP_ASSESS_BUILDING GetT_INSP_ASSESS_BUILDING_ByAssessID(Guid id)
        {
            try
            {
                return ctx.T_INSP_ASSESS_BUILDING.Where(x => x.ASSESSMENT_IDX == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }

        public Guid? InsertUpdateT_INSP_ASSESS_BUILDING(Guid? aSSESSMENT_IDX, Guid? sITE_IDX, DateTime? aSSESSMENT_DT, string aSSESSED_BY, int? oBJECTID, Guid? globalID, string title,
            string Walkway, string Windows, string Doors, string Roof, string Foundation, string Exterior, string Floor, string Walls, string Ceiling, string Stairs, string Panic, 
            string Closures, string Notes, string BuildingType    )
        {
            try
            {
                bool insInd = false;

                //first get based on our unique key
                T_INSP_ASSESS_BUILDING e = ctx.T_INSP_ASSESS_BUILDING.Where(x => x.ASSESSMENT_IDX == aSSESSMENT_IDX).FirstOrDefault();

                //then get based on ESRI unique key which is OBJECTID
                if (e == null)
                    e = ctx.T_INSP_ASSESS_BUILDING.Where(x => x.OBJECTID == oBJECTID).FirstOrDefault();

                //insert case
                if (e == null)
                {
                    insInd = true;
                    e = new T_INSP_ASSESS_BUILDING();
                    e.ASSESSMENT_IDX = Guid.NewGuid();
                }

                if (sITE_IDX != null) e.SITE_IDX = sITE_IDX.Value;
                if (aSSESSMENT_DT != null) e.ASSESSMENT_DT = aSSESSMENT_DT.Value;
                if (aSSESSED_BY != null) e.ASSESSED_BY = aSSESSED_BY;
                if (oBJECTID != null) e.OBJECTID = oBJECTID.Value;
                if (globalID != null) e.GlobalID = globalID;
                if (title != null) e.Title = title;
                if (Walkway != null) e.Walkway = Walkway;
                if (Windows != null) e.Windows = Windows;
                if (Doors != null) e.Doors = Doors;
                if (Roof != null) e.Roof = Roof;
                if (Foundation != null) e.Foundation = Foundation;
                if (Exterior != null) e.Exterior = Exterior;
                if (Floor != null) e.Floor = Floor;
                if (Walls != null) e.Walls = Walls;
                if (Ceiling != null) e.Ceiling = Ceiling;
                if (Stairs != null) e.Stairs = Stairs;
                if (Panic != null) e.Panic = Panic;
                if (Closures != null) e.Closures = Closures;
                if (Notes != null) e.Notes = Notes;
                if (BuildingType != null) e.BuildingType = BuildingType;

                if (insInd)
                    ctx.T_INSP_ASSESS_BUILDING.Add(e);
                ctx.SaveChanges();
                return e.SITE_IDX;
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }


        //************** T_INSP_ASSESS_LIFE **********************************
        public T_INSP_ASSESS_LIFE GetT_INSP_ASSESS_LIFE_ByAssessID(Guid id)
        {
            try
            {
                return ctx.T_INSP_ASSESS_LIFE.Where(x => x.ASSESSMENT_IDX == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }

        public Guid? InsertUpdateT_INSP_ASSESS_LIFE(Guid? aSSESSMENT_IDX, Guid? sITE_IDX, DateTime? aSSESSMENT_DT, string aSSESSED_BY, int? oBJECTID, Guid? globalID, string title,
           string Walkway, string Windows, string Doors, string Roof, string Foundation, string Exterior, string AddressVisible, string Floor, string Walls, string Ceiling, string Stairs, 
           string Panic, string Closures, string Notes, string BuildingType, string Fire_FireAlarm, string Fire_Sprinklers1, string Fire_Sprinklers5, string Fire_Plate, string Fire_EmergLight, 
           string Fire_Exit, string Fire_KitchenHood, string Fire_HoodSup, string Fire_EvacDrill, string Fire_Evac, string Fire_Storage, string Fire_Piled, string Fire_Food, string Fire_Hazard, 
           string Fire_SDS, string Fire_Extinguisher, string Fire_ExtinAnnual, string Fire_Notes, string Plum_Water, string Plum_Drain, string Plum_Sink, string Plum_Toilet, string Plum_Tub, 
           string Plum_Dish, string Plum_Heater, string Plum_Backflow, string Plum_Notes, string Elec_Hazard, string Elec_Panel, string Elec_Label, string Elec_Obstruct, string Elec_Outlet, 
           string Elec_Lighting, string Elec_Surge, string Elec_Ground, string Elec_Sign, string Elec_Notes, string Mech_Filters, string Mech_Register, string Mech_Equip, string Mech_VentAir, 
           string Mech_VentSyst, string Mech_Pipe, string Mech_Therm, string Mech_Duct, string Mech_Notes)
        {
            try
            {
                bool insInd = false;

                //first get based on our unique key
                T_INSP_ASSESS_LIFE e = (from c in ctx.T_INSP_ASSESS_LIFE
                                            where c.ASSESSMENT_IDX == aSSESSMENT_IDX
                                            select c).FirstOrDefault();

                //then get based on ESRI unique key which is OBJECTID
                if (e == null)
                {
                    e = (from c in ctx.T_INSP_ASSESS_LIFE
                         where c.OBJECTID == oBJECTID
                         select c).FirstOrDefault();
                }

                //insert case
                if (e == null)
                {
                    insInd = true;
                    e = new T_INSP_ASSESS_LIFE();
                    e.ASSESSMENT_IDX = Guid.NewGuid();
                }

                if (sITE_IDX != null) e.SITE_IDX = sITE_IDX.Value;
                if (aSSESSMENT_DT != null) e.ASSESSMENT_DT = aSSESSMENT_DT.Value;
                if (aSSESSED_BY != null) e.ASSESSED_BY = aSSESSED_BY;
                if (oBJECTID != null) e.OBJECTID = oBJECTID.Value;
                if (globalID != null) e.GlobalID = globalID;
                if (title != null) e.Title = title;

                if (Walkway != null) e.Walkway = Walkway;
                if (Windows != null) e.Windows = Windows;
                if (Doors != null) e.Doors = Doors;
                if (Roof != null) e.Roof = Roof;
                if (Foundation != null) e.Foundation = Foundation;
                if (Exterior != null) e.Exterior = Exterior;
                if (AddressVisible != null) e.AddressVisible = AddressVisible;
                if (Floor != null) e.Floor = Floor;
                if (Walls != null) e.Walls = Walls;
                if (Ceiling != null) e.Ceiling = Ceiling;
                if (Stairs != null) e.Stairs = Stairs;
                if (Panic != null) e.Panic = Panic;
                if (Closures != null) e.Closures = Closures;
                if (Notes != null) e.Notes = Notes;
                if (BuildingType != null) e.BuildingType = BuildingType;
                if (Fire_FireAlarm != null) e.Fire_FireAlarm = Fire_FireAlarm;
                if (Fire_Sprinklers1 != null) e.Fire_Sprinklers1 = Fire_Sprinklers1;
                if (Fire_Sprinklers5 != null) e.Fire_Sprinklers5 = Fire_Sprinklers5;
                if (Fire_Plate != null) e.Fire_Plate = Fire_Plate;
                if (Fire_EmergLight != null) e.Fire_EmergLight = Fire_EmergLight;
                if (Fire_Exit != null) e.Fire_Exit = Fire_Exit;
                if (Fire_KitchenHood != null) e.Fire_KitchenHood = Fire_KitchenHood;
                if (Fire_HoodSup != null) e.Fire_HoodSup = Fire_HoodSup;
                if (Fire_EvacDrill != null) e.Fire_EvacDrill = Fire_EvacDrill;
                if (Fire_Evac != null) e.Fire_Evac = Fire_Evac;
                if (Fire_Storage != null) e.Fire_Storage = Fire_Storage;
                if (Fire_Piled != null) e.Fire_Piled = Fire_Piled;
                if (Fire_Food != null) e.Fire_Food = Fire_Food;
                if (Fire_Hazard != null) e.Fire_Hazard = Fire_Hazard;
                if (Fire_SDS != null) e.Fire_SDS = Fire_SDS;
                if (Fire_Extinguisher != null) e.Fire_Extinguisher = Fire_Extinguisher;
                if (Fire_ExtinAnnual != null) e.Fire_ExtinAnnual = Fire_ExtinAnnual;
                if (Fire_Notes != null) e.Fire_Notes = Fire_Notes;
                if (Plum_Water != null) e.Plum_Water = Plum_Water;
                if (Plum_Drain != null) e.Plum_Drain = Plum_Drain;
                if (Plum_Sink != null) e.Plum_Sink = Plum_Sink;
                if (Plum_Toilet != null) e.Plum_Toilet = Plum_Toilet;
                if (Plum_Tub != null) e.Plum_Tub = Plum_Tub;
                if (Plum_Dish != null) e.Plum_Dish = Plum_Dish;
                if (Plum_Heater != null) e.Plum_Heater = Plum_Heater;
                if (Plum_Backflow != null) e.Plum_Backflow = Plum_Backflow;
                if (Plum_Notes != null) e.Plum_Notes = Plum_Notes;
                if (Elec_Hazard != null) e.Elec_Hazard = Elec_Hazard;
                if (Elec_Panel != null) e.Elec_Panel = Elec_Panel;
                if (Elec_Label != null) e.Elec_Label = Elec_Label;
                if (Elec_Obstruct != null) e.Elec_Obstruct = Elec_Obstruct;
                if (Elec_Outlet != null) e.Elec_Outlet = Elec_Outlet;
                if (Elec_Lighting != null) e.Elec_Lighting = Elec_Lighting;
                if (Elec_Surge != null) e.Elec_Surge = Elec_Surge;
                if (Elec_Ground != null) e.Elec_Ground = Elec_Ground;
                if (Elec_Sign != null) e.Elec_Sign = Elec_Sign;
                if (Elec_Notes != null) e.Elec_Notes = Elec_Notes;
                if (Mech_Filters != null) e.Mech_Filters = Mech_Filters;
                if (Mech_Register != null) e.Mech_Register = Mech_Register;
                if (Mech_Equip != null) e.Mech_Equip = Mech_Equip;
                if (Mech_VentAir != null) e.Mech_VentAir = Mech_VentAir;
                if (Mech_VentSyst != null) e.Mech_VentSyst = Mech_VentSyst;
                if (Mech_Pipe != null) e.Mech_Pipe = Mech_Pipe;
                if (Mech_Therm != null) e.Mech_Therm = Mech_Therm;
                if (Mech_Duct != null) e.Mech_Duct = Mech_Duct;
                if (Mech_Notes != null) e.Mech_Notes = Mech_Notes;


                if (insInd)
                    ctx.T_INSP_ASSESS_LIFE.Add(e);
                ctx.SaveChanges();
                return e.SITE_IDX;
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }


        //************** V_INSP_ASSESSMENTS **********************************
        public List<V_INSP_ASSESSMENTS> GetV_INSP_ASSESSMENTS(string assessType, string str, string bldg)
        {
            try
            {
                IQueryable<V_INSP_ASSESSMENTS> xxx = ctx.V_INSP_ASSESSMENTS;

                if (!string.IsNullOrEmpty(assessType))
                    xxx = xxx.Where(x => x.INSP_TYP == assessType);

                if (!string.IsNullOrEmpty(str))
                    xxx = xxx.Where(x => x.SITE_NAME.Contains(str));

                if (!string.IsNullOrEmpty(bldg))
                    xxx = xxx.Where(x => x.BUILDING_TYPE.Contains(bldg));

                return xxx.OrderBy(x => x.INSP_TYP).ThenBy(x => x.SITE_NAME).ToList();

                //if (string.IsNullOrEmpty(assessType))
                //    return (from a in ctx.V_INSP_ASSESSMENTS
                //            select a).OrderBy(x=>x.INSP_TYP).ThenBy(x=>x.SITE_NAME).ToList();
                //else
                //    return (from a in ctx.V_INSP_ASSESSMENTS
                //            where a.INSP_TYP == assessType
                //            select a).OrderBy(x => x.INSP_TYP).ThenBy(x => x.SITE_NAME).ToList();
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }

        public V_INSP_ASSESSMENTS GetV_INSP_ASSESSMENTS_ByAssessID(Guid id)
        {
            try
            {
                return ctx.V_INSP_ASSESSMENTS.Where(x=>x.ASSESSMENT_IDX == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }


        public IEnumerable<SelectListItem> get_ddl_AssessType()
        {
            List<SelectListItem> ddl_AssessType = new List<SelectListItem>();

            ddl_AssessType.Add(new SelectListItem() { Value = "BUILDING", Text = "BUILDING" });
            ddl_AssessType.Add(new SelectListItem() { Value = "ELECTRICAL", Text = "ELECTRICAL" });
            ddl_AssessType.Add(new SelectListItem() { Value = "FIRE", Text = "FIRE" });
            ddl_AssessType.Add(new SelectListItem() { Value = "MECHANICAL", Text = "MECHANICAL" });
            ddl_AssessType.Add(new SelectListItem() { Value = "PLUMBING", Text = "PLUMBING" });
            ddl_AssessType.Add(new SelectListItem() { Value = "LIFE", Text = "LIFE SAFETY" });

            return ddl_AssessType;
        }

    }
}
