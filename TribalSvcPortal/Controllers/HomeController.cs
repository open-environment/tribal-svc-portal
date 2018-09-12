using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using TribalSvcPortal.AppLogic.DataAccessLayer;
using TribalSvcPortal.Data.Models;
using TribalSvcPortal.ViewModels;
using TribalSvcPortal.ViewModels.HomeViewModels;

namespace TribalSvcPortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IDbPortal _DbPortal;
        private readonly IMemoryCache _memoryCache;
        public HomeController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
              IMemoryCache memoryCache,
            IDbPortal DbPortal)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _DbPortal = DbPortal;
            _memoryCache = memoryCache;
        }

        public IActionResult Index()
        {
            _DbPortal.GetT_PRT_SYS_LOG();

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
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
                var user = new ApplicationUser();
                user.UserName = model.Email;
                user.Email = model.Email;
                Task<IdentityResult> chkUser = _userManager.CreateAsync(user, "changeme1");
                chkUser.Wait();

                //now add user to the role
                if (chkUser.Result.Succeeded)
                {
                    var result1 = _userManager.AddToRoleAsync(user, "PortalAdmin");
                    result1.Wait();
                    TempData["Success"] = "User created with password 'changeme1'";
                }
            }
            else
                TempData["Error"] = "Application has already been initialized.";

            return View();
        }
    }
}
