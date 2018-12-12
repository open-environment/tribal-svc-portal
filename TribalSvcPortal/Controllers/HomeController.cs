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
            var model = new HomeViewModel {
                selOrg = id
            };

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
                Task<IdentityResult> chkUser = _userManager.CreateAsync(user, "changeme1");
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
                        TempData["Success"] = "User created with password 'changeme1'";
                    else
                        TempData["Error"] = "User created with password 'changeme1' but verification email failed.";
                }
            }
            else
                TempData["Error"] = "Application has already been initialized.";

            return View();
        }
    }
}
