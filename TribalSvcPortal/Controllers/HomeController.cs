using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TribalSvcPortal.AppLogic.BusinessLogicLayer;
using TribalSvcPortal.AppLogic.DataAccessLayer;
using TribalSvcPortal.Data.Models;
using TribalSvcPortal.ViewModels;
using TribalSvcPortal.ViewModels.HomeViewModels;
using TribalSvcPortal.Services;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System.Text.Encodings.Web;
using Microsoft.Extensions.Configuration;

namespace TribalSvcPortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IDbPortal _DbPortal;
        private readonly IDbOpenDump _DbOpenDump;
        private readonly IEmailSender _emailSender;
        private readonly UrlEncoder _urlEncoder;
        private readonly IConfiguration _config;

        public HomeController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IDbPortal DbPortal,
            IDbOpenDump DbOpenDump,
            IEmailSender emailSender,
            UrlEncoder urlEncoder,
            IConfiguration config)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _DbPortal = DbPortal;
            _DbOpenDump = DbOpenDump;
            _emailSender = emailSender;
            _urlEncoder = urlEncoder;
            _config = config;
        }

        public IActionResult Index(string id)
        {
            string _UserIDX = _userManager.GetUserId(User);

            //initialize model
            var model = new HomeViewModel
            {
                selOrg = id,
                WarnNoClientInd = false,
                Announcement = _DbPortal.GetT_PRT_APP_SETTINGS_CUSTOM().ANNOUNCEMENTS
            };

            if (_UserIDX != null)
            {
                //retrieve listing of clients user has access to
                model._clients = _DbPortal.GetT_PRT_ORG_USERS_CLIENT_DistinctClientByUserID(_UserIDX);


                //if agency user doesn't have access to any clients yet, display warning
                List<UserOrgDisplayType> _userOrgs = _DbPortal.GetT_PRT_ORG_USERS_ByUserID(_UserIDX);
                if (_userOrgs != null && _userOrgs.Count == 1 && _userOrgs[0].ACCESS_LEVEL == "U")
                {
                    List<OrgUserClientDisplayType> _orgUserClients = _DbPortal.GetT_PRT_ORG_USERS_CLIENT_ByOrgUserID(_userOrgs[0].ORG_USER_IDX ?? 0);
                    if (_orgUserClients == null || _orgUserClients.Count == 0)
                    {
                        model.WarnNoClientInd = true;
                    }
                }
            }

            return View(model);
        }

        public IActionResult api(string id)
        {
            var model = new apisViewModel
            {
                ddl_apis = _DbPortal.get_ddl_APIS(),
                ddl_Orgs = _DbOpenDump.getT_OD_ORGANIZATIONS(),
                ddl_Format = _DbPortal.get_ddl_APIformat(),
                selFormat = "X",
                ddl_County = _DbPortal.getT_PRT_SITES_UniqueCounties(),
                ddl_Status = _DbOpenDump.get_ddl_T_OD_SITE_STATUS().ToList(),
                ddl_Score = _DbOpenDump.get_ddl_HealthThreatScore().ToList()

            };

            return View(model);
        }

        [HttpPost]
        public IActionResult api(apisViewModel model)
        {
            if (model.sel_api == "OD_Sites" || model.sel_api == "OD_Assess")
                return RedirectToAction("apidataod", "Home", new { api = model.sel_api, org = model.selOrg, county = model.selCounty, status = model.selStatus, score = model.selScore, fileType = model.selFormat });

            else
                return null;
        }


        [AllowAnonymous]
        public FileResult apidataod(string api, string org, string county, string status, string score, string fileType)
        {
            try
            {

                if (fileType == "X")
                {
                    XNamespace gml = "http://www.opengis.net/gml";
                    XNamespace od = "http://www.exchangenetwork.net/schema/od/2";
                    XmlDocument xmlDoc = new XmlDocument();

                    //root
                    XmlElement root = xmlDoc.CreateElement("od", "OpenDumpList", "http://www.exchangenetwork.net/schema/od/2");
                    root.SetAttribute("xmlns:gml", "http://www.opengis.net/gml");
                    root.SetAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
                    root.SetAttribute("xmlns:od", "http://www.exchangenetwork.net/schema/od/2");
                    root.SetAttribute("xmlns:facid", "http://www.exchangenetwork.net/schema/facilityid/3");
                    xmlDoc.AppendChild(root);


                    List<V_OD_SITES> _sites = _DbOpenDump.getV_OD_SITES_Search(org, county, status, score);

                    foreach (var _site in _sites)
                    {
                        XmlElement _OpenDump = xmlDoc.CreateElement("od", "OpenDump", "http://www.exchangenetwork.net/schema/od/2");

                        //Facility Site Identity
                        XmlElement _FacilitySiteIdentity = xmlDoc.CreateElement("od", "FacilitySiteIdentity", "http://www.exchangenetwork.net/schema/od/2");
                        AddElement(xmlDoc, _FacilitySiteIdentity, "FacilitySiteIdentifier", _site.FacilitySiteIdentitifer);
                        AddElement(xmlDoc, _FacilitySiteIdentity, "FacilitySiteName", _site.FacilitySiteName);
                        _OpenDump.AppendChild(_FacilitySiteIdentity);

                        //Data Source
                        XmlElement _DataSource = xmlDoc.CreateElement("facid", "DataSource", "http://www.exchangenetwork.net/schema/facilityid/3");
                        AddElementFACID(xmlDoc, _DataSource, "OriginatingPartnerName", _site.OriginatingPartnerName);
                        AddElementFACID(xmlDoc, _DataSource, "InformationSystemAcronymName", _site.InformationSystemAcronymName);

                        if (_site.LastUpdatedDate != null)
                            AddElementFACID(xmlDoc, _DataSource, "LastUpdatedDate", _site.LastUpdatedDate.GetValueOrDefault().ToString("yyyy-MM-dd"));
                        _OpenDump.AppendChild(_DataSource);


                        //Location Address
                        XmlElement _LocationAddress = xmlDoc.CreateElement("facid", "LocationAddress", "http://www.exchangenetwork.net/schema/facilityid/3");
                        AddElementFACID(xmlDoc, _LocationAddress, "LocationAddressText", _site.LocationAddressText);

                        if (_site.StateName != null)
                        {
                            XmlElement _State = xmlDoc.CreateElement("facid", "StateIdentity", "http://www.exchangenetwork.net/schema/facilityid/3");
                            AddElementFACID(xmlDoc, _State, "StateName", _site.StateName);
                            _LocationAddress.AppendChild(_State);
                        }

                        if (_site.CountyName != null)
                        {
                            XmlElement _County = xmlDoc.CreateElement("facid", "CountyIdentity", "http://www.exchangenetwork.net/schema/facilityid/3");
                            AddElementFACID(xmlDoc, _County, "CountyName", _site.CountyName);
                            _LocationAddress.AppendChild(_County);
                        }

                        _OpenDump.AppendChild(_LocationAddress);


                        //Lat/long
                        if (_site.Latitude != null)
                        {
                            XmlElement _latLongGP = xmlDoc.CreateElement("facid", "FacilityPrimaryGeographicLocationDescription", "http://www.exchangenetwork.net/schema/facilityid/3");
                            XmlElement _latLongP = xmlDoc.CreateElement("gml", "Point", "http://www.opengis.net/gml");
                            XmlElement _latLong = xmlDoc.CreateElement("gml", "pos", "http://www.opengis.net/gml");
                            _latLong.InnerText = _site.Latitude + " " + _site.Longitude;
                            _latLongP.AppendChild(_latLong);
                            _latLongGP.AppendChild(_latLongP);
                            _OpenDump.AppendChild(_latLongGP);
                        }

                        AddElement(xmlDoc, _OpenDump, "SiteCommunityName", _site.CommunityName);
                        AddElement(xmlDoc, _OpenDump, "TribalLandStatusText", _site.TribalLandStatusText);
                        AddElement(xmlDoc, _OpenDump, "SiteSettingName", _site.SiteSettingName);
                        AddElement(xmlDoc, _OpenDump, "SiteDistanceToHomesText", _site.SiteDistanceToHomesText);
                        AddElement(xmlDoc, _OpenDump, "SiteVerticalDistanceToAquiferText", _site.SiteVerticalDistanceToAquiferText);
                        AddElement(xmlDoc, _OpenDump, "SiteHorizontalDistanceToSurfaceWaterText", _site.SiteHorizontalDistanceToSurfaceWaterText);
                        AddElement(xmlDoc, _OpenDump, "SiteInitialReportedByText", _site.SiteInitialReportedByText);
                        if (_site.SiteInitialReportedDate != null)
                            AddElement(xmlDoc, _OpenDump, "SiteInitialReportedDate", _site.SiteInitialReportedDate.GetValueOrDefault().ToString("yyyy-MM-dd"));


                        //ASSESSMENTS**********************
                        if (api == "OD_Assess")
                        {
                            List<V_OD_ASSESSMENTS> _assesses = _DbOpenDump.getV_OD_ASSESSMENTS_BySiteIDX(_site.SITE_IDX);
                            if (_assesses != null)
                            {
                                foreach (V_OD_ASSESSMENTS _assess in _assesses)
                                {
                                    XmlElement _OpenDumpAssessment = xmlDoc.CreateElement("od", "OpenDumpAssessment", "http://www.exchangenetwork.net/schema/od/2");
                                    AddElement(xmlDoc, _OpenDumpAssessment, "SiteAssessmentDate", _assess.SiteAssessmentDate.GetValueOrDefault().ToString("yyyy-MM-dd"));
                                    AddElement(xmlDoc, _OpenDumpAssessment, "SiteAssessedBy", _assess.SiteAssessedBy);
                                    AddElement(xmlDoc, _OpenDumpAssessment, "AssessmentTypeText", _assess.AssessmentTypeText);
                                    AddElement(xmlDoc, _OpenDumpAssessment, "SiteStatusText", _assess.SiteStatusText);
                                    if (_assess.SiteClosedDate != null)
                                        AddElement(xmlDoc, _OpenDumpAssessment, "SiteClosedDate", _assess.SiteClosedDate.GetValueOrDefault().ToString("yyyy-MM-dd"));
                                    AddElement(xmlDoc, _OpenDumpAssessment, "AssessmentDescription", _assess.AssessmentDescription);
                                    AddElement(xmlDoc, _OpenDumpAssessment, "AssessmentSiteObservations", _assess.AssessmentSiteObservations);
                                    if (_assess.SiteSurfaceAreaValue != null)
                                        AddElement(xmlDoc, _OpenDumpAssessment, "SiteSurfaceAreaValue", _assess.SiteSurfaceAreaValue.ToString());
                                    if (_assess.SiteVolumeValue != null)
                                        AddElement(xmlDoc, _OpenDumpAssessment, "SiteVolumeValue", _assess.SiteVolumeValue.ToString());
                                    AddElement(xmlDoc, _OpenDumpAssessment, "SiteHealthThreatScoreSummaryText", _assess.SiteHealthThreatScoreSummaryText);
                                    if (_assess.SiteHealthThreatScoreValue != null)
                                        AddElement(xmlDoc, _OpenDumpAssessment, "SiteHealthThreatScoreValue", _assess.SiteHealthThreatScoreValue.ToString());

                                    //health factors
                                    if (_assess.HFRainfall != null)
                                    {
                                        //Data Source
                                        XmlElement _HF = xmlDoc.CreateElement("od", "OpenDumpHazardFactor", "http://www.exchangenetwork.net/schema/od/2");
                                        AddElement(xmlDoc, _HF, "HazardFactorType", "Rainfall");
                                        AddElement(xmlDoc, _HF, "HazardFactorValueText", _assess.HFRainfall);
                                        _OpenDumpAssessment.AppendChild(_HF);
                                    }
                                    if (_assess.HFDrainage != null)
                                    {
                                        //Data Source
                                        XmlElement _HF = xmlDoc.CreateElement("od", "OpenDumpHazardFactor", "http://www.exchangenetwork.net/schema/od/2");
                                        AddElement(xmlDoc, _HF, "HazardFactorType", "Drainage");
                                        AddElement(xmlDoc, _HF, "HazardFactorValueText", _assess.HFDrainage);
                                        _OpenDumpAssessment.AppendChild(_HF);
                                    }
                                    if (_assess.HFFlooding != null)
                                    {
                                        //Data Source
                                        XmlElement _HF = xmlDoc.CreateElement("od", "OpenDumpHazardFactor", "http://www.exchangenetwork.net/schema/od/2");
                                        AddElement(xmlDoc, _HF, "HazardFactorType", "Flooding");
                                        AddElement(xmlDoc, _HF, "HazardFactorValueText", _assess.HFFlooding);
                                        _OpenDumpAssessment.AppendChild(_HF);
                                    }
                                    if (_assess.HFBurning != null)
                                    {
                                        //Data Source
                                        XmlElement _HF = xmlDoc.CreateElement("od", "OpenDumpHazardFactor", "http://www.exchangenetwork.net/schema/od/2");
                                        AddElement(xmlDoc, _HF, "HazardFactorType", "Burning");
                                        AddElement(xmlDoc, _HF, "HazardFactorValueText", _assess.HFBurning);
                                        _OpenDumpAssessment.AppendChild(_HF);
                                    }
                                    if (_assess.HFFencing != null)
                                    {
                                        //Data Source
                                        XmlElement _HF = xmlDoc.CreateElement("od", "OpenDumpHazardFactor", "http://www.exchangenetwork.net/schema/od/2");
                                        AddElement(xmlDoc, _HF, "HazardFactorType", "Fencing");
                                        AddElement(xmlDoc, _HF, "HazardFactorValueText", _assess.HFFencing);
                                        _OpenDumpAssessment.AppendChild(_HF);
                                    }
                                    if (_assess.HFAccess != null)
                                    {
                                        //Data Source
                                        XmlElement _HF = xmlDoc.CreateElement("od", "OpenDumpHazardFactor", "http://www.exchangenetwork.net/schema/od/2");
                                        AddElement(xmlDoc, _HF, "HazardFactorType", "Access Control");
                                        AddElement(xmlDoc, _HF, "HazardFactorValueText", _assess.HFAccess);
                                        _OpenDumpAssessment.AppendChild(_HF);
                                    }
                                    if (_assess.HFPublic != null)
                                    {
                                        //Data Source
                                        XmlElement _HF = xmlDoc.CreateElement("od", "OpenDumpHazardFactor", "http://www.exchangenetwork.net/schema/od/2");
                                        AddElement(xmlDoc, _HF, "HazardFactorType", "Public Concern");
                                        AddElement(xmlDoc, _HF, "HazardFactorValueText", _assess.HFPublic);
                                        _OpenDumpAssessment.AppendChild(_HF);
                                    }

                                    _OpenDump.AppendChild(_OpenDumpAssessment);
                                }
                            }
                        }


                        xmlDoc.DocumentElement.AppendChild(_OpenDump);
                    }


                    byte[] bytes = Encoding.Default.GetBytes(xmlDoc.OuterXml);
                    return File(bytes, System.Net.Mime.MediaTypeNames.Application.Octet, "OpenDumpData.xml");
                }
                else if (fileType == "J")
                {
                    if (api == "OD_Sites")
                    {
                        var _sites = _DbOpenDump.getV_OD_SITES_Search(org, county, status, score);
                        string _siteJson = JsonConvert.SerializeObject(_sites);
                        byte[] bytes = Encoding.Default.GetBytes(_siteJson);
                        return File(bytes, System.Net.Mime.MediaTypeNames.Application.Octet, "OpenDumpData.json");
                    }
                    else if (api == "OD_Assess")
                    {
                        var _s = _DbOpenDump.getV_OD_ASSESSMENTS_Search(org, county, status, score);
                        string _siteJson = JsonConvert.SerializeObject(_s);
                        byte[] bytes = Encoding.Default.GetBytes(_siteJson);
                        return File(bytes, System.Net.Mime.MediaTypeNames.Application.Octet, "OpenDumpData.json");
                    }
                }



            }
            catch
            { }

            return null;

        }

        private static void AddElement(XmlDocument xmlDoc, XmlElement _parentElement, string _fieldName, string _fieldVal)
        {
            XmlElement _e = xmlDoc.CreateElement("od", _fieldName, "http://www.exchangenetwork.net/schema/od/2");
            _e.InnerText = _fieldVal;
            _parentElement.AppendChild(_e);
        }

        private static void AddElementFACID(XmlDocument xmlDoc, XmlElement _parentElement, string _fieldName, string _fieldVal)
        {
            XmlElement _e = xmlDoc.CreateElement("facid", _fieldName, "http://www.exchangenetwork.net/schema/facilityid/3");
            _e.InnerText = _fieldVal;
            _parentElement.AppendChild(_e);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public ActionResult TermsAndConditions()
        {
            var model = new TermsAndConditionsViewModel();
            T_PRT_APP_SETTINGS_CUSTOM cust = _DbPortal.GetT_PRT_APP_SETTINGS_CUSTOM();
            model.TermsAndConditions = cust.TERMS_AND_CONDITIONS;

            return View(model);
        }

        public IActionResult Initialize()
        {
            if (_userManager.Users.Count() > 0)
                TempData["Error"] = "System has already been initialized";

            return View();
        }

        [HttpPost]
        public IActionResult Initialize(InitializeViewModel model)
        {
            if (_userManager.Users.Count() == 0)
            {
                //Check that there is a Portal Administrator and create if not
                Task<bool> hasAdminRole = _roleManager.RoleExistsAsync("PortalAdmin");
                hasAdminRole.Wait();

                if (!hasAdminRole.Result)
                {
                    Task<IdentityResult> chkRole = _roleManager.CreateAsync(new IdentityRole("PortalAdmin"));
                    chkRole.Wait();
                }

                //now create master user
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };
                Task<IdentityResult> chkUser = _userManager.CreateAsync(user, model.Password);
                chkUser.Wait();

                if (chkUser.Result.Succeeded)
                {
                    //now add user to the role
                    var result1 = _userManager.AddToRoleAsync(user, "PortalAdmin");
                    result1.Wait();

                    //send confirmation email
                    var code = _userManager.GenerateEmailConfirmationTokenAsync(user).Result;
                    var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                    bool emailSucc = _emailSender.SendEmail(null, model.Email, null, null, null, null, "EMAIL_CONFIRM", "callbackUrl", callbackUrl);

                    if (emailSucc)
                        TempData["Success"] = "User created and verification email sent";
                    else
                        TempData["Error"] = "User created but verification email failed.";
                }
            }
            else
                TempData["Error"] = "Application has already been initialized.";

            return View();
        }

        [AllowAnonymous]
        //public async Task<IActionResult> GetOpenWatersLinkAsync(string clientid)
        public IActionResult GetOpenWatersLink(string clientid)
        {
            var url = string.Format("{0}", _config["ClientAppExternalLoginEndpoint"]);
            //string _UserIDX = _userManager.GetUserId(User);
            //ApplicationUser user = await _userManager.FindByIdAsync(_UserIDX);
            //var payload = user.UserName + "###" + user.PasswordEncrypt + "###" + _UserIDX;
            //Encoding unicode = Encoding.Unicode;
            //var plbytes = unicode.GetBytes(payload);
            //var plb64 = Convert.ToBase64String(plbytes);
            //payload = _urlEncoder.Encode(plb64);
            //var url = string.Format("{0}?pl={1}", _config["ClientAppExternalLoginEndpoint"], payload);
            return Ok(url);
        }
    }
}
