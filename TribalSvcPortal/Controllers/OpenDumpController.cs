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

        
        [HttpGet]
        public IActionResult Search(string selStr, string selOrg)
        {
           
            string _UserIDX;
            bool isUserExist = _memoryCache.TryGetValue("UserID", out _UserIDX);
           
            if (selStr == null && selOrg == null)
            {
                SearchViewModel model = new SearchViewModel();
                model.ddl_Org = _DbOpenDump.get_ddl_organizations(_UserIDX);
                //if (model.ddl_Org.Count() == 1)
                //{
                //    model.selOrg = model.ddl_Org.Te
                //}               
                return View(model);
            }
            else
            {
                var model = new SearchViewModel
                {
                    ddl_Org = _DbOpenDump.get_ddl_organizations(_UserIDX),
                    searchResults = _DbOpenDump.get_OpenDump_Sites_By_Organization_SiteName(selStr, selOrg)
            };
              //  var model = _DbOpenDump.get_OpenDump_Sites_By_Organization_SiteName(selStr, selOrg);
                return View(model);
            }

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