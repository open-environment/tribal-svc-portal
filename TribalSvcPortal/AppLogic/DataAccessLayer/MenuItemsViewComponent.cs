using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Threading;
using TribalSvcPortal.Data.Models;
using TribalSvcPortal.ViewModels;

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
                var _model = new LeftMenuViewModel();

                bool isExist = _memoryCache.TryGetValue(CacheKey, out _model);
                if (!isExist || _model == null)
                {
                    if (_model == null)
                        _model = new LeftMenuViewModel();

                    var cts = new CancellationTokenSource();

                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetPriority(CacheItemPriority.High)
                        .SetSlidingExpiration(TimeSpan.FromHours(1))
                        .SetAbsoluteExpiration(TimeSpan.FromHours(1))
                        .AddExpirationToken(new CancellationChangeToken(cts.Token));

                    _model.IsOrgClientAdmin = _DbPortal.IsUserAnOrgClientAdmin(_UserIDX) || _DbPortal.IsUserAnyOrgAdmin(_UserIDX);
                    _model._clients = _DbPortal.GetT_PRT_ORG_USERS_CLIENT_DistinctClientByUserID(_UserIDX);
                    _model._myOrgAdmins = _DbPortal.GetT_PRT_ORGANIZATIONS_UserIsAdmin(_UserIDX);

                    _memoryCache.Set(CacheKey, _model, cacheEntryOptions);
                }
                return View(_model);
            }
            else
                return View();
        }
     
    }
}
