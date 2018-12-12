using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Threading;
using TribalSvcPortal.Data.Models;

namespace TribalSvcPortal.AppLogic.DataAccessLayer
{
    public class MenuItemsViewComponent:ViewComponent
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IDbPortal _DbPortal;
        private readonly IMemoryCache _memoryCache;

        public MenuItemsViewComponent(
            UserManager<ApplicationUser> userManager,
            IDbPortal DbPortal,
            IMemoryCache memoryCache)
        {
            _userManager = userManager;
            _DbPortal = DbPortal;
            _memoryCache = memoryCache;
        }
      
        public IViewComponentResult Invoke()
        {
            string _UserIDX = _userManager.GetUserId(Request.HttpContext.User);
            if (_UserIDX != null)
            {
                string CacheKey = "UserMenuData" + _UserIDX;
                var cts = new CancellationTokenSource();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetPriority(CacheItemPriority.High)
                    .SetSlidingExpiration(TimeSpan.FromHours(1))
                    .SetAbsoluteExpiration(TimeSpan.FromHours(1))
                    .AddExpirationToken(new CancellationChangeToken(cts.Token));

                IEnumerable<T_PRT_CLIENTS> UserClientDisplayType = _DbPortal.GetT_PRT_ORG_USERS_CLIENT_DistinctClientByUserID(_UserIDX);
                _memoryCache.Set(CacheKey, UserClientDisplayType, cacheEntryOptions);
                return View(UserClientDisplayType);
            }
            else
                return View();
        }
     
    }
}
