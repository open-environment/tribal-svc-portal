using iTextSharp.xmp.impl.xpath;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using TribalSvcPortal.AppLogic.DataAccessLayer;

namespace TribalSvcPortal.AppLogic.BusinessLogicLayer
{
    public class PlumbingInspections
    {
        public int OBJECTID { get; set; }
        public string FacilityName { get; set; }
        public string FacilitlyID { get; set; }
        public string Inspector { get; set; }
        public string Title { get; set; }
        public string Date { get; set; }
        public string County { get; set; }
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
        public Guid? GlobalID { get; set; }
        public string Editor { get; set; }
        public long? EditDate { get; set; }
        // Add all mapped properties
    }

    public class ElectricalInspections
    {
        public int OBJECTID { get; set; }
        public string FacilityName { get; set; }
        public string FacilityID { get; set; }
        public string Inspector { get; set; }
        public string Title { get; set; }
        public string Date { get; set; }
        public string County { get; set; }
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
        public string BuildingUse { get; set; }
        public Guid? GlobalID { get; set; }
        public string Editor { get; set; }
        public long? EditDate { get; set; }
    }


    public class MechanicalInspections
    {
        public int OBJECTID { get; set; }
        public string FacilityName { get; set; }
        public string FacilityID { get; set; }
        public string Inspector { get; set; }
        public string Title { get; set; }
        public string Date { get; set; }
        public string County { get; set; }
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
        public string Comp_Logs { get; set; }
        public string Corrective { get; set; }
        public string BuildingUse { get; set; }
        public Guid? GlobalID { get; set; }
        public string Editor { get; set; }
        public long? EditDate { get; set; }
    }

    public class FireInspections
    {
        public int OBJECTID { get; set; }
        public string FacilityName { get; set; }
        public string Facility_ID { get; set; }
        public string InspectorName { get; set; }
        public string InspectorTitle { get; set; }
        public string Date { get; set; }
        public string County { get; set; }
        public string FacilityContact { get; set; }
        public string Compliance { get; set; }
        public string CorrectiveAction { get; set; }
        public string Corrective { get; set; }

        public string Doc_FireAlarm { get; set; }
        public string Doc__FireSprinklers { get; set; }
        public string Doc_SprinklerTest { get; set; }
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
        public string Storage_Combustible { get; set; }
        public string Storage_OilRags { get; set; }
        public string Storage_Food { get; set; }

        public string Elec_Covers { get; set; }
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
        public string Vent_BrandDate { get; set; }  //longer text
        public string Vent_Cleanliness { get; set; }
        public string Vent_Extinguishers { get; set; }
        public string Vent_Service { get; set; }  //service date on tag

        public string FireExt_Doc { get; set; }
        public string FireExt_Mounted { get; set; }
        public string FireExt_CollarTag { get; set; }
        public string FireExt_Sys { get; set; }

        public string Risers_Inspected { get; set; }
        public string Risers_Tagged { get; set; }
        public string Risers_Sprinkler { get; set; }
        public string Risers_Deficiencies { get; set; }
        public string Risers_Plate { get; set; }
        public string Risers_Gauges { get; set; }

        public string Sprinkler_Cond { get; set; }
        public string CeilingType { get; set; }
        public string CeilingCond { get; set; }

        public string AED { get; set; }
        public string AEDExpiration { get; set; }

        public Guid? GlobalID { get; set; }
        public string Editor { get; set; }
        public long? EditDate { get; set; }

        public string Facility_Use { get; set; }
    }

    public class BuildingInspections
    {
        public int OBJECTID { get; set; }
        public string FacilityName { get; set; }
        public string FacilityID { get; set; }
        public string Inspector { get; set; }
        public string Title { get; set; }
        public string Date { get; set; }
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
        public string BuildingUse { get; set; }
        public Guid? GlobalID { get; set; }
        public string Editor { get; set; }
        public long? EditDate { get; set; }
    }


    public class LifeInspections
    {
        public int OBJECTID { get; set; }
        public string FacilityName { get; set; }
        public string FacilityID { get; set; }
        public string BuildingUse { get; set; }
        public string Inspector { get; set; }
        public string InspectorTitle { get; set; }
        public string Date { get; set; }

        public string bldg_sidewalks { get; set; }
        public string Bldg_Windows { get; set; }
        public string Bldg_Doors { get; set; }
        public string Bldg_Roof { get; set; }
        public string Bldg_Foundation { get; set; }
        public string Bldg_Exterior { get; set; }
        public string Bldg_Address { get; set; }
        public string Bldg_Floor { get; set; }
        public string Bldg_Walls { get; set; }
        public string Bldg_Ceiling { get; set; }
        public string Bldg_Stairs { get; set; }
        public string Bldg_Panic { get; set; }
        public string Bldg_Closures { get; set; }
        public string Bldg_Notes { get; set; }

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



        public Guid? GlobalID { get; set; }
        public string Editor { get; set; }
        public long? EditDate { get; set; }
    }

    public class esriHelper
    {
        private readonly IDbPortal _DbPortal;

        public esriHelper(IDbPortal DbPortal)
        {
            _DbPortal = DbPortal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appPath"></param>
        /// <returns></returns>
        public string getToken(string appPath)
        {
            string username = _DbPortal.GetT_PRT_APP_SETTING("ARCGIS_USER");
            string password = _DbPortal.GetT_PRT_APP_SETTING("ARCGIS_PWD");

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return null;

            var client = new RestClient();
            var request = new RestRequest("https://geomcn.maps.arcgis.com/sharing/generateToken/", Method.POST)
            {
                RequestFormat = DataFormat.Json
            };

            request.AddParameter("f", "json");
            request.AddParameter("username", username);
            request.AddParameter("password", password);
            request.AddParameter("client", "referer");

            if (appPath.Contains("gis11", StringComparison.OrdinalIgnoreCase))
                appPath = "https://okm-gis11.mcn.com/";
            request.AddParameter("referer", appPath);

            var response = client.Execute(request);

            if (!response.IsSuccessful || string.IsNullOrWhiteSpace(response.Content))
                return null;

            try
            {
                var json = JsonConvert.DeserializeObject<dynamic>(response.Content);
                if (json?.token != null)
                    return json?.token;
                else
                {
                    var ddd = json.error;
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="appPath"></param>
        /// <param name="outfields"></param>
        /// <param name="endpointId">3 Fire, 10 Plumbing, 11 Electrical , 12 Mechanical, 13 Building</param>
        /// <returns></returns>
        public List<T> QueryInspection<T>(string appPath, int endpointId) where T : new()
        {
            var token = getToken(appPath);
            if (string.IsNullOrWhiteSpace(token))
                return null;

            string whereClause = "1=1";
            string outFields = "*";

            var baseUrl = $"https://services3.arcgis.com/yBwJ5BxqvbumespK/arcgis/rest/services/Fire_SafetyInspection_WFL1/FeatureServer/{endpointId}/query";
            var queryString = $"where={Uri.EscapeDataString(whereClause)}&returnExceededLimitFeatures=false&returnGeometry=false&returnIdsOnly=false&outFields={outFields}";
            var requestUrl = $"{baseUrl}?{queryString}";

            var client = new RestClient();
            var request = new RestRequest(requestUrl, Method.POST) { RequestFormat = DataFormat.Json };

            request.AddParameter("f", "json");
            request.AddHeader("X-Esri-Authorization", $"Bearer {token}");
            request.AddParameter("referer", appPath);

            var response = client.Execute(request);
            if (!response.IsSuccessful || string.IsNullOrWhiteSpace(response.Content))
                return null;

            try
            {
                var data = JsonConvert.DeserializeObject<JObject>(response.Content);
                var featuresArray = data?["features"] as JArray;
                if (featuresArray == null)
                    return null;

                var entities = new List<T>();

                foreach (var feature in featuresArray)
                {
                    var entity = new T();
                    var attributes = feature["attributes"] as JObject;
                    if (attributes == null)
                        continue;

                    foreach (var property in attributes.Properties())
                    {
                        var propInfo = typeof(T).GetProperty(property.Name);
                        if (propInfo == null || property.Value == null || property.Type == JTokenType.Null)
                            continue;

                        try
                        {
                            var convertedVal = Convert.ChangeType(property.Value.ToString(), propInfo.PropertyType);
                            propInfo.SetValue(entity, convertedVal);
                        }
                        catch
                        {
                            // Skip invalid property assignment
                        }
                    }

                    entities.Add(entity);
                }

                return entities;
            }
            catch
            {
                return null;
            }
        }


        public List<PlumbingInspections> queryPlumbing(string appPath, string globalID, string[] outfields)
        {
            var token = getToken(appPath);
            if (string.IsNullOrWhiteSpace(token))
                return null;

            var whereClause = !string.IsNullOrWhiteSpace(globalID)
                ? $"GlobalID='{globalID}'"
                : "1=1";

            string outFields = string.Join(",", outfields);

            //10 PlumbingInspections  now 4
            //11 Electrical   now 5
            //12 Mechanical   now 6
            //13 Building     now 7
            //   Life         now 8
            //   Fire         now 9
            var baseUrl = "https://services3.arcgis.com/yBwJ5BxqvbumespK/arcgis/rest/services/Fire_SafetyInspection_WFL1/FeatureServer/4/query";
            var queryString = $"where={Uri.EscapeDataString(whereClause)}&returnExceededLimitFeatures=false&returnGeometry=false&returnIdsOnly=false&outFields={outFields}";

            var requestUrl = $"{baseUrl}?{queryString}";

            var client = new RestClient();
            var request = new RestRequest(requestUrl, Method.POST)
            {
                RequestFormat = DataFormat.Json

            };
            request.AddParameter("f", "json");
            request.AddHeader("X-Esri-Authorization", $"Bearer {token}");
            request.AddParameter("referer", appPath);

            var response = client.Execute(request);
            if (!response.IsSuccessful || string.IsNullOrWhiteSpace(response.Content))
                return null;

            try
            {
                var data = JsonConvert.DeserializeObject<JObject>(response.Content);
                var featuresArray = data?["features"] as JArray;
                if (featuresArray == null)
                    return null;

                var entities = new List<PlumbingInspections>();

                foreach (var feature in featuresArray)
                {
                    var entity = new PlumbingInspections();
                    var attributes = feature["attributes"] as JObject;
                    if (attributes == null)
                        continue;

                    foreach (var property in attributes.Properties())
                    {
                        var propInfo = typeof(PlumbingInspections).GetProperty(property.Name);
                        if (propInfo == null || property.Value == null || property.Type == JTokenType.Null)
                            continue;

                        try
                        {
                            var convertedVal = Convert.ChangeType(property.Value.ToString(), propInfo.PropertyType);
                            propInfo.SetValue(entity, convertedVal);
                        }
                        catch
                        {
                            // Skip invalid property assignment
                        }
                    }

                    entities.Add(entity);
                }

                return entities;
            }
            catch
            {
                return null;
            }


        }

        public List<ElectricalInspections> queryElectrical(string appPath, string globalID, string[] outfields)
        {
            var token = getToken(appPath);
            if (string.IsNullOrWhiteSpace(token))
                return null;

            var whereClause = !string.IsNullOrWhiteSpace(globalID)
                ? $"GlobalID='{globalID}'"
                : "1=1";

            string outFields = string.Join(",", outfields);

            //10 PlumbingInspections  now 4
            //11 Electrical   now 5
            //12 Mechanical   now 6
            //13 Building     now 7
            //   Life         now 8
            //   Fire         now 9
            var baseUrl = "https://services3.arcgis.com/yBwJ5BxqvbumespK/arcgis/rest/services/Fire_SafetyInspection_WFL1/FeatureServer/5/query";
            var queryString = $"where={Uri.EscapeDataString(whereClause)}&returnExceededLimitFeatures=false&returnGeometry=false&returnIdsOnly=false&outFields={outFields}";

            var requestUrl = $"{baseUrl}?{queryString}";

            var client = new RestClient();
            var request = new RestRequest(requestUrl, Method.POST) { RequestFormat = DataFormat.Json };

            request.AddParameter("f", "json");
            request.AddHeader("X-Esri-Authorization", $"Bearer {token}");
            request.AddParameter("referer", appPath);

            var response = client.Execute(request);
            if (!response.IsSuccessful || string.IsNullOrWhiteSpace(response.Content))
                return null;

            try
            {
                var data = JsonConvert.DeserializeObject<JObject>(response.Content);
                var featuresArray = data?["features"] as JArray;
                if (featuresArray == null)
                    return null;

                var entities = new List<ElectricalInspections>();

                foreach (var feature in featuresArray)
                {
                    var entity = new ElectricalInspections();
                    var attributes = feature["attributes"] as JObject;
                    if (attributes == null)
                        continue;

                    foreach (var property in attributes.Properties())
                    {
                        var propInfo = typeof(ElectricalInspections).GetProperty(property.Name);
                        if (propInfo == null || property.Value == null || property.Type == JTokenType.Null)
                            continue;

                        try
                        {
                            var convertedVal = Convert.ChangeType(property.Value.ToString(), propInfo.PropertyType);
                            propInfo.SetValue(entity, convertedVal);
                        }
                        catch
                        {
                            // Skip invalid property assignment
                        }
                    }

                    entities.Add(entity);
                }

                return entities;
            }
            catch
            {
                return null;
            }


        }

        public List<MechanicalInspections> queryMechanical(string appPath, string globalID, string[] outfields)
        {
            var token = getToken(appPath);
            if (string.IsNullOrWhiteSpace(token))
                return null;

            var whereClause = !string.IsNullOrWhiteSpace(globalID)
                ? $"GlobalID='{globalID}'"
                : "1=1";

            string outFields = string.Join(",", outfields);

            //10 PlumbingInspections  now 4
            //11 Electrical   now 5
            //12 Mechanical   now 6
            //13 Building     now 7
            //   Life         now 8
            //   Fire         now 9
            var baseUrl = "https://services3.arcgis.com/yBwJ5BxqvbumespK/arcgis/rest/services/Fire_SafetyInspection_WFL1/FeatureServer/6/query";
            var queryString = $"where={Uri.EscapeDataString(whereClause)}&returnExceededLimitFeatures=false&returnGeometry=false&returnIdsOnly=false&outFields={outFields}";

            var requestUrl = $"{baseUrl}?{queryString}";

            var client = new RestClient();
            var request = new RestRequest(requestUrl, Method.POST) { RequestFormat = DataFormat.Json };

            request.AddParameter("f", "json");
            request.AddHeader("X-Esri-Authorization", $"Bearer {token}");
            request.AddParameter("referer", appPath);

            var response = client.Execute(request);
            if (!response.IsSuccessful || string.IsNullOrWhiteSpace(response.Content))
                return null;

            try
            {
                var data = JsonConvert.DeserializeObject<JObject>(response.Content);
                var featuresArray = data?["features"] as JArray;
                if (featuresArray == null)
                    return null;

                var entities = new List<MechanicalInspections>();

                foreach (var feature in featuresArray)
                {
                    var entity = new MechanicalInspections();
                    var attributes = feature["attributes"] as JObject;
                    if (attributes == null)
                        continue;

                    foreach (var property in attributes.Properties())
                    {
                        var propInfo = typeof(MechanicalInspections).GetProperty(property.Name);
                        if (propInfo == null || property.Value == null || property.Type == JTokenType.Null)
                            continue;

                        try
                        {
                            var convertedVal = Convert.ChangeType(property.Value.ToString(), propInfo.PropertyType);
                            propInfo.SetValue(entity, convertedVal);
                        }
                        catch
                        {
                            // Skip invalid property assignment
                        }
                    }

                    entities.Add(entity);
                }

                return entities;
            }
            catch
            {
                return null;
            }
        }

        public List<FireInspections> queryFire(string appPath, string globalID, string[] outfields)
        {
            var token = getToken(appPath);
            if (string.IsNullOrWhiteSpace(token))
                return null;

            var whereClause = !string.IsNullOrWhiteSpace(globalID)
                ? $"GlobalID='{globalID}'"
                : "1=1";
            string outFields = string.Join(",", outfields);

            //10 PlumbingInspections  now 4
            //11 Electrical   now 5
            //12 Mechanical   now 6
            //13 Building     now 7
            //   Life         now 8
            //   Fire         now 9
            var baseUrl = "https://services3.arcgis.com/yBwJ5BxqvbumespK/arcgis/rest/services/Fire_SafetyInspection_WFL1/FeatureServer/9/query";
            var queryString = $"where={Uri.EscapeDataString(whereClause)}&returnExceededLimitFeatures=false&returnGeometry=false&returnIdsOnly=false&outFields={outFields}";

            var requestUrl = $"{baseUrl}?{queryString}";

            var client = new RestClient();
            var request = new RestRequest(requestUrl, Method.POST) { RequestFormat = DataFormat.Json };

            request.AddParameter("f", "json");
            request.AddHeader("X-Esri-Authorization", $"Bearer {token}");
            request.AddParameter("referer", appPath);

            var response = client.Execute(request);
            if (!response.IsSuccessful || string.IsNullOrWhiteSpace(response.Content))
                return null;

            try
            {
                var data = JsonConvert.DeserializeObject<JObject>(response.Content);
                var featuresArray = data?["features"] as JArray;
                if (featuresArray == null)
                    return null;

                var entities = new List<FireInspections>();

                foreach (var feature in featuresArray)
                {
                    var entity = new FireInspections();
                    var attributes = feature["attributes"] as JObject;
                    if (attributes == null)
                        continue;

                    foreach (var property in attributes.Properties())
                    {
                        var propInfo = typeof(FireInspections).GetProperty(property.Name);
                        if (propInfo == null || property.Value == null || property.Type == JTokenType.Null)
                            continue;

                        try
                        {
                            var convertedVal = Convert.ChangeType(property.Value.ToString(), propInfo.PropertyType);
                            propInfo.SetValue(entity, convertedVal);
                        }
                        catch
                        {
                            // Skip invalid property assignment
                        }
                    }

                    entities.Add(entity);
                }

                return entities;
            }
            catch
            {
                return null;
            }


        }


        public List<BuildingInspections> queryBuilding(string appPath, string globalID, string[] outfields)
        {
            var token = getToken(appPath);
            if (string.IsNullOrWhiteSpace(token))
                return null;

            var whereClause = !string.IsNullOrWhiteSpace(globalID)
                ? $"GlobalID='{globalID}'"
                : "1=1";

            string outFields = string.Join(",", outfields);

            //10 PlumbingInspections  now 4
            //11 Electrical   now 5
            //12 Mechanical   now 6
            //13 Building     now 7
            //   Life         now 8
            //   Fire         now 9
            var baseUrl = "https://services3.arcgis.com/yBwJ5BxqvbumespK/arcgis/rest/services/Fire_SafetyInspection_WFL1/FeatureServer/7/query";
            var queryString = $"where={Uri.EscapeDataString(whereClause)}&returnExceededLimitFeatures=false&returnGeometry=false&returnIdsOnly=false&outFields={outFields}";

            var requestUrl = $"{baseUrl}?{queryString}";

            var client = new RestClient();
            var request = new RestRequest(requestUrl, Method.POST) { RequestFormat = DataFormat.Json };

            request.AddParameter("f", "json");
            request.AddHeader("X-Esri-Authorization", $"Bearer {token}");
            request.AddParameter("referer", appPath);

            var response = client.Execute(request);
            if (!response.IsSuccessful || string.IsNullOrWhiteSpace(response.Content))
                return null;

            try
            {
                var data = JsonConvert.DeserializeObject<JObject>(response.Content);
                var featuresArray = data?["features"] as JArray;
                if (featuresArray == null)
                    return null;

                var entities = new List<BuildingInspections>();

                foreach (var feature in featuresArray)
                {
                    var entity = new BuildingInspections();
                    var attributes = feature["attributes"] as JObject;
                    if (attributes == null)
                        continue;

                    foreach (var property in attributes.Properties())
                    {
                        var propInfo = typeof(BuildingInspections).GetProperty(property.Name);
                        if (propInfo == null || property.Value == null || property.Type == JTokenType.Null)
                            continue;

                        try
                        {
                            var convertedVal = Convert.ChangeType(property.Value.ToString(), propInfo.PropertyType);
                            propInfo.SetValue(entity, convertedVal);
                        }
                        catch
                        {
                            // Skip invalid property assignment
                        }
                    }

                    entities.Add(entity);
                }

                return entities;
            }
            catch
            {
                return null;
            }


        }



        public List<LifeInspections> queryLife(string appPath, string globalID, string[] outfields)
        {
            var token = getToken(appPath);
            if (string.IsNullOrWhiteSpace(token))
                return null;

            var whereClause = !string.IsNullOrWhiteSpace(globalID)
                ? $"GlobalID='{globalID}'"
                : "1=1";

            string outFields = string.Join(",", outfields);

            //10 PlumbingInspections  now 4
            //11 Electrical   now 5
            //12 Mechanical   now 6
            //13 Building     now 7
            //   Life         now 8
            //   Fire         now 9
            var baseUrl = "https://services3.arcgis.com/yBwJ5BxqvbumespK/arcgis/rest/services/Fire_SafetyInspection_WFL1/FeatureServer/8/query";
            var queryString = $"where={Uri.EscapeDataString(whereClause)}&returnExceededLimitFeatures=false&returnGeometry=false&returnIdsOnly=false&outFields={outFields}";

            var requestUrl = $"{baseUrl}?{queryString}";

            var client = new RestClient();
            var request = new RestRequest(requestUrl, Method.POST) { RequestFormat = DataFormat.Json };

            request.AddParameter("f", "json");
            request.AddHeader("X-Esri-Authorization", $"Bearer {token}");
            request.AddParameter("referer", appPath);

            var response = client.Execute(request);
            if (!response.IsSuccessful || string.IsNullOrWhiteSpace(response.Content))
                return null;

            try
            {
                var data = JsonConvert.DeserializeObject<JObject>(response.Content);
                var featuresArray = data?["features"] as JArray;
                if (featuresArray == null)
                    return null;

                var entities = new List<LifeInspections>();

                foreach (var feature in featuresArray)
                {
                    var entity = new LifeInspections();
                    var attributes = feature["attributes"] as JObject;
                    if (attributes == null)
                        continue;

                    foreach (var property in attributes.Properties())
                    {
                        var propInfo = typeof(LifeInspections).GetProperty(property.Name);
                        if (propInfo == null || property.Value == null || property.Type == JTokenType.Null)
                            continue;

                        try
                        {
                            var convertedVal = Convert.ChangeType(property.Value.ToString(), propInfo.PropertyType);
                            propInfo.SetValue(entity, convertedVal);
                        }
                        catch
                        {
                            // Skip invalid property assignment
                        }
                    }

                    entities.Add(entity);
                }

                return entities;
            }
            catch
            {
                return null;
            }


        }


        public List<(byte[] Data, string FileName)> DownloadAttachments(string appPath, int LayerID, int ObjectID)
        {
            var token = getToken(appPath);
            var url = $"https://services3.arcgis.com/yBwJ5BxqvbumespK/arcgis/rest/services/Fire_SafetyInspection_WFL1/FeatureServer/{LayerID}/{ObjectID}/attachments";
            var client = new RestClient(url);
            var request = new RestRequest();
            request.AddParameter("f", "json");
            request.AddParameter("token", token);
            var response = client.Get(request);
            var json = JObject.Parse(response.Content);
            var attachments = (JArray)json["attachmentInfos"];
            if (attachments != null && attachments.Count > 0)
            {
                var x = new List<(byte[], string)>();

                foreach (var attachment in attachments)
                {
                    var contentType = attachment["contentType"].ToString();
                    if (contentType == "image/jpeg")
                    {
                        var attachmentUrl = attachment["id"].ToString();
                        var atturl = $"https://services3.arcgis.com/yBwJ5BxqvbumespK/arcgis/rest/services/Fire_SafetyInspection_WFL1/FeatureServer/{LayerID}/{ObjectID}/attachments/{attachmentUrl}?w=500";

                        using (var httpClient = new HttpClient())
                        {
                            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                            byte[] fileBytes = httpClient.GetByteArrayAsync(atturl).GetAwaiter().GetResult();

                            var caption = attachment["name"].ToString();
                            x.Add((fileBytes, caption));
                        }
                    }
                }
                return x;
            }
            else
                return null;
        }
    }
}
