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
            IDbPortal DbPortal,
             IMemoryCache memoryCache,
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

        public IActionResult Search()
        {
            IEnumerable<T_PRT_CLIENTS> UserClientDisplayType;
            string _UserIDX;
            bool isUserExist = _memoryCache.TryGetValue("UserID", out _UserIDX);
            if (isUserExist)
            {
                string CacheKey = "UserMenuData" + _UserIDX;

                bool isExist = _memoryCache.TryGetValue(CacheKey, out UserClientDisplayType);
                if (isExist && UserClientDisplayType != null)
                {
                    ViewBag.UserMenuAccess = UserClientDisplayType;
                }
            }

            var model = new SearchViewModel
            {
            };

            return View(model);
        }


        public IActionResult RefData()
        {
            IEnumerable<T_PRT_CLIENTS> UserClientDisplayType;
            string _UserIDX;
            bool isUserExist = _memoryCache.TryGetValue("UserID", out _UserIDX);
            if (isUserExist)
            {
                string CacheKey = "UserMenuData" + _UserIDX;

                bool isExist = _memoryCache.TryGetValue(CacheKey, out UserClientDisplayType);
                if (isExist && UserClientDisplayType != null)
                {
                    ViewBag.UserMenuAccess = UserClientDisplayType;
                }
            }
            return View();
        }


        public IActionResult DumpParcels()
        {
            return View();
        }

    }
}