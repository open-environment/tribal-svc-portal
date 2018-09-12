using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TribalSvcPortal.Data.Models;

namespace TribalSvcPortal.AppLogic.DataAccessLayer
{
    public class MenuItemsViewComponent:ViewComponent
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IDbPortal _DbPortal;
        private readonly IMemoryCache _memoryCache;

        public MenuItemsViewComponent(SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IDbPortal DbPortal,
            IMemoryCache memoryCache)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _DbPortal = DbPortal;
            _memoryCache = memoryCache;
        }
        //public async Task<IViewComponentResult> InvokeAsync()
        public IViewComponentResult Invoke()
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
                    return View(UserClientDisplayType);
                }
                else
                {
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                   .SetSlidingExpiration(TimeSpan.FromMinutes(20));

                    UserClientDisplayType = _DbPortal.GetT_PRT_ORG_USERS_CLIENT_DistinctClientByUserID(_UserIDX);
                    _memoryCache.Set(CacheKey, UserClientDisplayType, cacheEntryOptions);                  
                    return View(UserClientDisplayType);
                }
            }
            return View();           
        }
    }
}
