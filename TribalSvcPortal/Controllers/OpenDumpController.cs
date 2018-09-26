using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using TribalSvcPortal.Data.Models;
using TribalSvcPortal.ViewModels.OpenDumpViewModels;
using TribalSvcPortal.AppLogic.DataAccessLayer;
using Microsoft.Extensions.Caching.Memory;

namespace TribalSvcPortal.Controllers
{
    public class OpenDumpController : Controller
    {

        private readonly IDbPortal _DbPortal;
        private readonly IDbOpenDump _DbOpenDump;
        private readonly IMemoryCache _memoryCache;

        public OpenDumpController(
            IMemoryCache memoryCache,
            IDbPortal DbPortal,
            IDbOpenDump DbOpenDump)
        {
            _DbPortal = DbPortal;
            _DbOpenDump = DbOpenDump;
            _memoryCache = memoryCache;
        }

        public IActionResult Index()
        {
            return View();
        }


        public ActionResult Search(string selStr, string selOrg)
        {           
            string _UserIDX;
            bool isUserExist = _memoryCache.TryGetValue("UserID", out _UserIDX);
           
            if (selStr == null && selOrg == null)
            {
                SearchViewModel model = new SearchViewModel();
                model.ddl_Org = _DbOpenDump.get_ddl_od_organizations(_UserIDX);
                model.searchResults = _DbOpenDump.get_AllOpenDump_Sites(_UserIDX);
                
                if (model.ddl_Org.Count()==1)
                {
                    foreach (var orgid in model.ddl_Org)
                    {
                        model.selOrg = orgid.Value;
                    }
                }
                return View(model);
            }
            else
            {
                var model = new SearchViewModel
                {
                    ddl_Org = _DbOpenDump.get_ddl_od_organizations(_UserIDX),
                    searchResults = _DbOpenDump.get_OpenDump_Sites_By_Organization_SiteName(selStr, selOrg)
            };
                if (model.ddl_Org.Count() == 1)
                {
                    foreach (var orgid in model.ddl_Org)
                    {
                        model.selOrg = orgid.Value;
                    }
                }
                return View(model);
            }

        }


        // GET: /OpenDump/PreField
        public ActionResult PreField(Guid? SiteIdx, string returnURL)
        {
            string _UserIDX;
            bool isUserExist = _memoryCache.TryGetValue("UserID", out _UserIDX);

            var model = new PreFieldViewModel();
            model.SiteSettingsList = _DbOpenDump.get_ddl_refdata_by_category("Site Setting");
            model.CommunityList = _DbOpenDump.get_ddl_refdata_by_category("Community");
            model.AquiferList = _DbOpenDump.get_ddl_refthreatfactor_by_factortype("Aquifer");
            model.SurfaceWaterList = _DbOpenDump.get_ddl_refthreatfactor_by_factortype("Surface Water");
            model.HomesList = _DbOpenDump.get_ddl_refthreatfactor_by_factortype("Homes");
            model.OrgList = _DbOpenDump.get_ddl_od_organizations(_UserIDX);
            model.returnURL = returnURL ?? "Search";
           
            if (SiteIdx != null)
            {
                model.TPrtSites = _DbPortal.GetT_PRT_SITES_BySITEIDX((Guid)SiteIdx);
                model.TOdSites = _DbOpenDump.GetT_OD_SITES_BySITEIDX((Guid)SiteIdx);
            }
            else
            {                
                model.TPrtSites = new T_PRT_SITES();
                if (model.OrgList.Count() == 1)
                {
                    foreach (var orgid in model.OrgList)
                    {
                        model.TPrtSites.OrgId = orgid.Value;
                    }
                }
                model.TPrtSites.SiteIdx = Guid.NewGuid();
               
            }           
            return View(model);
        }


        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult OpenDumpEdit(PreFieldViewModel model)
        {
            //if (model.TPrtSites.SiteName == null)
            //    ModelState.AddModelError("SiteName", "SiteName is required");

            string _UserIDX;
            bool isUserExist = _memoryCache.TryGetValue("UserID", out _UserIDX);

            Guid? newSiteID = _DbPortal.InsertUpdateT_PRT_SITES(model.TPrtSites.SiteIdx, model.TPrtSites.OrgId, model.TPrtSites.SiteName ?? "",
                    model.TPrtSites.EpaId ?? "", model.TPrtSites.Latitude, model.TPrtSites.Longitude, model.TPrtSites.SiteAddress ?? "", _UserIDX);

            if (newSiteID != null)
            {
                Guid? SiteID = _DbOpenDump.InsertUpdateT_OD_SITES((Guid)newSiteID, model.TOdSites.REPORTED_BY, model.TOdSites.REPORTED_ON, model.TOdSites.COMMUNITY_IDX, 
                    model.TOdSites.SITE_SETTING_IDX, model.TOdSites.PF_AQUIFER_VERT_DIST, model.TOdSites.PF_SURF_WATER_HORIZ_DIST, model.TOdSites.PF_HOMES_DIST);

                TempData["Success"] = "Update successful.";
                return RedirectToAction("PreField", "OpenDump", new { SiteIdx = newSiteID, returnURL = model.returnURL });
            }
            else
                TempData["Error"] = "Error updating data.";
            return RedirectToAction(model.returnURL ?? "Search", new { selStr = "", selOrg="" });
        }


        [HttpPost]
        public JsonResult PreFieldDelete(Guid id)
        {
            if (id != null)
            {               
                int SuccID = _DbPortal.DeleteT_PRT_SITES(id);

                if (SuccID == 1)
                    return Json("Success");
            }

            return Json("Unable to delete");
        }


        public IActionResult RefData()
        {           
            return View();
        }


        public IActionResult DumpParcels()
        {
            return View();
        }

    }
}