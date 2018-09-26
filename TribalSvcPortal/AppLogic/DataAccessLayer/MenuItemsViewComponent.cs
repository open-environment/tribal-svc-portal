using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
      
        public IViewComponentResult Invoke()
        {
            IEnumerable<T_PRT_CLIENTS> UserClientDisplayType;
            string _UserIDX;
            bool isUserExist = _memoryCache.TryGetValue("UserID", out _UserIDX);
            if (!isUserExist)
            {
                if (_signInManager.IsSignedIn(HttpContext.User))
                {
                    var user = _userManager.GetUserAsync(HttpContext.User);
                    var email = ((System.Security.Claims.ClaimsIdentity)HttpContext.User.Identity).Name;
                    var userInfo = _userManager.FindByNameAsync(email).Result;
                    _UserIDX = userInfo.Id;

                    string CacheKey = "UserMenuData" + _UserIDX;
                    var cts = new CancellationTokenSource();

                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                   
                    .SetPriority(CacheItemPriority.High)
                     .SetSlidingExpiration(TimeSpan.FromHours(1))
                        .SetAbsoluteExpiration(TimeSpan.FromHours(1))
                    .AddExpirationToken(new CancellationChangeToken(cts.Token));

                    UserClientDisplayType = _DbPortal.GetT_PRT_ORG_USERS_CLIENT_DistinctClientByUserID(_UserIDX);
                    _memoryCache.Set(CacheKey, UserClientDisplayType);
                    _memoryCache.Set("UserID", _UserIDX, cacheEntryOptions);
                    return View(UserClientDisplayType);
                }              

            }
            else
            {
                string CacheKey = "UserMenuData" + _UserIDX;

                bool isExist = _memoryCache.TryGetValue(CacheKey, out UserClientDisplayType);
                if (isExist && UserClientDisplayType != null)
                {
                    return View(UserClientDisplayType);              
                }
                else
                {
                    var cts = new CancellationTokenSource();

                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                  
                    .SetPriority(CacheItemPriority.High)
                    .SetSlidingExpiration(TimeSpan.FromHours(1))
                        .SetAbsoluteExpiration(TimeSpan.FromHours(1))
                    .AddExpirationToken(new CancellationChangeToken(cts.Token));

                    UserClientDisplayType = _DbPortal.GetT_PRT_ORG_USERS_CLIENT_DistinctClientByUserID(_UserIDX);
                    _memoryCache.Set(CacheKey, UserClientDisplayType, cacheEntryOptions);
                    return View(UserClientDisplayType);
                }
            }           
         return View();           
        }
     
    }
}
