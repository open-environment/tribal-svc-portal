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
                //set cache key value
                string CacheKey = "UserMenuData" + _UserIDX;

                //check if memory cache exists
                IEnumerable<T_PRT_CLIENTS> _clients;
                bool isExist = _memoryCache.TryGetValue(CacheKey, out _clients);
                if (!isExist || _clients == null)
                {
                    var cts = new CancellationTokenSource();

                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetPriority(CacheItemPriority.High)
                        .SetSlidingExpiration(TimeSpan.FromHours(1))
                        .SetAbsoluteExpiration(TimeSpan.FromHours(1))
                        .AddExpirationToken(new CancellationChangeToken(cts.Token));

                    _clients = _DbPortal.GetT_PRT_ORG_USERS_CLIENT_DistinctClientByUserID(_UserIDX);
                    _memoryCache.Set(CacheKey, _clients, cacheEntryOptions);
                }
                return View(_clients);
            }
            else
                return View();
        }
     
    }
}
