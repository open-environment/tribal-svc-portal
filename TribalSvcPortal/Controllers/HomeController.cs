using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TribalSvcPortal.AppLogic.BusinessLogicLayer;
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

        public HomeController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IDbPortal DbPortal)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _DbPortal = DbPortal;
        }

        public IActionResult Index(string id)
        {
            string _UserIDX = _userManager.GetUserId(User);

            //initialize model
            var model = new HomeViewModel
            {
                selOrg = id,
                WarnNoClientInd = false,
                Announcement = _DbPortal.GetT_PRT_APP_SETTINGS_CUSTOM().ANNOUNCEMENTS
            };

            //if agency user doesn't have access to any clients yet, display warning
            if (_UserIDX != null)
            {
                List<UserOrgDisplayType> _userOrgs = _DbPortal.GetT_PRT_ORG_USERS_ByUserID(_UserIDX);
                if (_userOrgs != null && _userOrgs.Count == 1 && _userOrgs[0].ACCESS_LEVEL == "U")
                {
                    List<OrgUserClientDisplayType> _orgUserClients = _DbPortal.GetT_PRT_ORG_USERS_CLIENT_ByOrgUserID(_userOrgs[0].ORG_USER_IDX??0);
                    if (_orgUserClients == null || _orgUserClients.Count == 0)
                    {
                        model.WarnNoClientInd = true;
                    }
                }
            }




            return View(model);
        }

        public IActionResult Maps(string id)
        {
            var model = new HomeViewModel
            {
                selOrg = id
            };

            return View(model);
        }

        public IActionResult api(string id)
        {
            var model = new HomeViewModel
            {
                selOrg = id
            };

            return View(model);
        }

        public IActionResult Services(string id)
        {
            var model = new ServiceViewModel
            {
            };

            return View(model);
        }

        public IActionResult Events(string id)
        {
            var model = new HomeViewModel
            {
                selOrg = id
            };

            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public ActionResult TermsAndConditions()
        {
            var model = new TermsAndConditionsViewModel();
            T_PRT_APP_SETTINGS_CUSTOM cust = _DbPortal.GetT_PRT_APP_SETTINGS_CUSTOM();
            model.TermsAndConditions = cust.TERMS_AND_CONDITIONS;

            return View(model);
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
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };
                Task<IdentityResult> chkUser = _userManager.CreateAsync(user, model.Password);
                chkUser.Wait();

                if (chkUser.Result.Succeeded)
                {
                    //now add user to the role
                    var result1 = _userManager.AddToRoleAsync(user, "PortalAdmin");
                    result1.Wait();

                    //send confirmation email
                    string code = _userManager.GenerateEmailConfirmationTokenAsync(user).Result;
                    var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                    bool emailSucc = Utils.SendEmail(null, model.Email, null, null, "Confirm your email", $"Please confirm your account by clicking this link: <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>link</a>", null, null, "");

                    if (emailSucc)
                        TempData["Success"] = "User created and verification email sent";
                    else
                        TempData["Error"] = "User created but verification email failed.";
                }
            }
            else
                TempData["Error"] = "Application has already been initialized.";

            return View();
        }
    }
}
