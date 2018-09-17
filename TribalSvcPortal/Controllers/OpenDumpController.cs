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
       
        private readonly IDbOpenDump _DbOpenDump;
        private readonly IMemoryCache _memoryCache;
        public OpenDumpController(
           
             IMemoryCache memoryCache,
            IDbOpenDump DbOpenDump)
        {
           
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
                model.ddl_Org = _DbOpenDump.get_ddl_organizations(_UserIDX);              
                return View(model);
            }
            else
            {
                var model = new SearchViewModel
                {
                    ddl_Org = _DbOpenDump.get_ddl_organizations(_UserIDX),
                    searchResults = _DbOpenDump.get_OpenDump_Sites_By_Organization_SiteName(selStr, selOrg)
            };             
                return View(model);
            }

        }
        // GET: /OpenDump/PreField
        public ActionResult PreField(Guid? SiteIdx, string selOrg)
        {
            var model = new PreFieldViewModel();
            model.SiteSettingsList = _DbOpenDump.get_ddl_refdata_by_category("Site Setting");


            if (SiteIdx != null)
            {
                model.TPrtSites = _DbOpenDump.GetT_PRT_SITES_BySITEIDX((Guid)SiteIdx);
                model.TOdSites = _DbOpenDump.GetT_OD_SITES_BySITEIDX((Guid)SiteIdx);
            }
            //else //add new case
            //{
            //    model.agency = new T_OE_ORGANIZATION();
            //    model.agency.ORG_IDX = Guid.NewGuid();
            //    if (typ != null)
            //        model.agency.ORG_TYPE = typ;
            //    model.agency.ACT_IND = true;
            //    model.agency_emails = new List<T_OE_ORGANIZATION_EMAIL_RULE>();
            //}

            //model.GovInd = typ;
            return View(model);
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