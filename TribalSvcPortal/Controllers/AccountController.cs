﻿using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TribalSvcPortal.AppLogic.BusinessLogicLayer;
using TribalSvcPortal.AppLogic.DataAccessLayer;
using TribalSvcPortal.Data.Models;
using TribalSvcPortal.Services;
using TribalSvcPortal.ViewModels.AccountViewModels;

namespace TribalSvcPortal.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly IIdentityServerInteractionService _interaction;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly IDbPortal _DbPortal;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<AccountController> _logger;
        private readonly Ilog _log;

        public AccountController(
            IIdentityServerInteractionService interaction,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            IDbPortal DbPortal,
            IMemoryCache memoryCache,
            ILogger<AccountController> logger,
            Ilog log)
        {
            _interaction = interaction;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _DbPortal = DbPortal;
            _memoryCache = memoryCache;
            _logger = logger;
            _log = log;
        }

        [TempData]
        public string ErrorMessage { get; set; }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = _userManager.FindByEmailAsync(model.Email).Result;

                if (user != null)
                {
                    if (!_userManager.IsEmailConfirmedAsync(user).Result)
                    {
                        ModelState.AddModelError("", "Email not confirmed!");
                        return View(model);
                    }
                }

                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: true);

                if (result.Succeeded)
                {
                    //update last login datetime
                    _DbPortal.UpdateT_PRT_USERS_LoginDate(user);

                    //remove Left Menu memorycache for user, so it can be repopulated from db
                    string CacheKey = "UserMenuData" + user.Id;
                    _memoryCache.Remove(CacheKey);

                    _logger.LogInformation("User logged in.");

                    return RedirectToLocal(returnUrl);
                }

                if (result.RequiresTwoFactor)
                {
                    return RedirectToAction(nameof(LoginWith2fa), new { returnUrl, model.RememberMe });
                }

                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToAction(nameof(Lockout));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [IgnoreAntiforgeryToken]
        [AcceptVerbs("JWTLogin")]
        public async Task<IActionResult> JWTLogin([FromBody] JWTLoginModel model)
        {
            _log.InsertT_PRT_SYS_LOG("Info", "JWTLogin Method called.");
            string actResult = string.Empty;

            model.isLoggedIn = false;
            model.isLockedOut = false;
            model.roles = null;
            model.errMsg = "";
            Response.StatusCode = StatusCodes.Status500InternalServerError;

            var user = _userManager.FindByNameAsync(model.email).Result;
            if (user == null)
            {
                model.errMsg = "User with email not found!";
                _log.InsertT_PRT_SYS_LOG("Error", "User with email not found!");
                return Ok(model);
            }
            model.UserId = user.Id;
            model.firstName = user.FIRST_NAME;
            model.lastName = user.LAST_NAME;

            if (user != null)
            {
                if (!_userManager.IsEmailConfirmedAsync(user).Result)
                {
                    model.errMsg = "Email not confirmed!";
                    _log.InsertT_PRT_SYS_LOG("Error", "Email not confirmed!");
                    return Ok(model);
                }
            }

            // To enable password failures to trigger account lockout, set lockoutOnFailure: true
            var result = await _signInManager.PasswordSignInAsync(model.email, Utils.Decrypt(model.password), model.rememberMe, lockoutOnFailure: true);

            if (result.Succeeded)
            {
                //update last login datetime
                _DbPortal.UpdateT_PRT_USERS_LoginDate(user);

                //remove Left Menu memorycache for user, so it can be repopulated from db
                //string CacheKey = "UserMenuData" + user.Id;
                //_memoryCache.Remove(CacheKey);

                _logger.LogInformation("User logged in.");
                _log.InsertT_PRT_SYS_LOG("Info", "User logged in.");
                model.isLoggedIn = true;
                var _roles = await _userManager.GetRolesAsync(user);
                model.roles = _roles.ToList();
                model.orgUsers = GetOrgUserWithClients(user.Id, "open_waters"); ;
                model.isAdmin = GetIsAdmin(model.orgUsers); 
                Response.StatusCode = StatusCodes.Status200OK;
                return Ok(model);
                //return RedirectToLocal(returnUrl);
            }
            /*
            if (result.RequiresTwoFactor)
            {
                return RedirectToAction(nameof(LoginWith2fa), new { returnUrl, model.RememberMe });
            }
            */
            if (result.IsLockedOut)
            {
                _logger.LogWarning("User account locked out.");
                _log.InsertT_PRT_SYS_LOG("Info", "User account locked out.");
                model.errMsg = "User account locked out.";
                //return RedirectToAction(nameof(Lockout));
                //return Ok(model);
            }
            else
            {
                //ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                //return View(model);
                model.errMsg = "Invalid login attempt.";
                _log.InsertT_PRT_SYS_LOG("Error", "Invalid login attempt.");
                //return Ok(model);
            }
            // If we got this far, something failed, redisplay form
            if (Response.StatusCode != StatusCodes.Status200OK)
            {
                model.errMsg = "Something went wrong!";
                _log.InsertT_PRT_SYS_LOG("Error", "Something went wrong!");
            }
            return Ok(model);
        }

        private bool GetIsAdmin(List<UserOrgDisplayType> orgUsers)
        {
            bool actResult = false;
            if (orgUsers != null)
            {
                foreach (var o in orgUsers)
                {
                    if (o.OrgUserClientDisplay != null)
                    {
                        foreach (var c in o.OrgUserClientDisplay)
                        {
                            if (c.CLIENT_ID.ToLower() == "open_waters" && c.ADMIN_IND == true)
                            {
                                actResult = true;
                                break;
                            }
                        }
                    }

                }
            }
            return actResult;
        }

        private List<UserOrgDisplayType> GetOrgUserWithClients(string userid, string clientid)
        {
            List<UserOrgDisplayType> actResult = null;
            try
            {
                actResult = _DbPortal.GetT_PRT_ORG_USERS_ByUserID_WithClientList_WithAlias(userid, clientid);
                if(actResult != null)
                {
                    // Include organizations with client assigned as open waters
                    actResult = actResult.Where(ou => ou.OrgUserClientDisplay.Any(oucd => oucd.CLIENT_ID == "open_waters" && oucd.ORG_USER_CLIENT_IDX != 0)).ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return actResult;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> LoginWith2fa(bool rememberMe, string returnUrl = null)
        {
            // Ensure the user has gone through the username & password screen first
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();

            if (user == null)
            {
                throw new ApplicationException($"Unable to load two-factor authentication user.");
            }

            var model = new LoginWith2faViewModel { RememberMe = rememberMe };
            ViewData["ReturnUrl"] = returnUrl;

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginWith2fa(LoginWith2faViewModel model, bool rememberMe, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var authenticatorCode = model.TwoFactorCode.Replace(" ", string.Empty).Replace("-", string.Empty);

            var result = await _signInManager.TwoFactorAuthenticatorSignInAsync(authenticatorCode, rememberMe, model.RememberMachine);

            if (result.Succeeded)
            {
                _logger.LogInformation("User with ID {UserId} logged in with 2fa.", user.Id);
                return RedirectToLocal(returnUrl);
            }
            else if (result.IsLockedOut)
            {
                _logger.LogWarning("User with ID {UserId} account locked out.", user.Id);
                return RedirectToAction(nameof(Lockout));
            }
            else
            {
                _logger.LogWarning("Invalid authenticator code entered for user with ID {UserId}.", user.Id);
                ModelState.AddModelError(string.Empty, "Invalid authenticator code.");
                return View();
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> LoginWithRecoveryCode(string returnUrl = null)
        {
            // Ensure the user has gone through the username & password screen first
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                throw new ApplicationException($"Unable to load two-factor authentication user.");
            }

            ViewData["ReturnUrl"] = returnUrl;

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginWithRecoveryCode(LoginWithRecoveryCodeViewModel model, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                throw new ApplicationException($"Unable to load two-factor authentication user.");
            }

            var recoveryCode = model.RecoveryCode.Replace(" ", string.Empty);

            var result = await _signInManager.TwoFactorRecoveryCodeSignInAsync(recoveryCode);

            if (result.Succeeded)
            {
                _logger.LogInformation("User with ID {UserId} logged in with a recovery code.", user.Id);
                return RedirectToLocal(returnUrl);
            }
            if (result.IsLockedOut)
            {
                _logger.LogWarning("User with ID {UserId} account locked out.", user.Id);
                return RedirectToAction(nameof(Lockout));
            }
            else
            {
                _logger.LogWarning("Invalid recovery code entered for user with ID {UserId}", user.Id);
                ModelState.AddModelError(string.Empty, "Invalid recovery code entered.");
                return View();
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Lockout()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult PortalRegister(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            T_PRT_APP_SETTINGS_CUSTOM cust = _DbPortal.GetT_PRT_APP_SETTINGS_CUSTOM();
            var model = new RegisterViewModel
            {
                termsConditions = cust.TERMS_AND_CONDITIONS
            };

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PortalRegister(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FIRST_NAME = model.FirstName,
                    LAST_NAME = model.LastName
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    //Encrypt and store password to database
                    //used for WordPress user management
                    var _user = await _userManager.FindByEmailAsync(model.Email);
                    if (_user != null)
                    {
                        _DbPortal.UpdateT_PRT_USERS_PasswordEncrypt(_user, model.Password);
                    }

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                    bool emailSucc = _emailSender.SendEmail(null, model.Email, null, null, null, null, "EMAIL_CONFIRM", "callbackUrl", callbackUrl);
                    //_log.InsertT_PRT_SYS_LOG("info-cburl", callbackUrl);

                    //if users email is associated with an organization, then associate user with org
                    List<T_PRT_ORGANIZATIONS> orgs = _DbPortal.GetT_PRT_ORGANIZATIONS_ByEmail(model.Email);
                    if (orgs != null && orgs.Count == 1)
                    {
                        _DbPortal.InsertUpdateT_PRT_ORG_USERS(null, orgs[0].ORG_ID, user.Id, "U", "A", user.Id);
                    }

                    TempData["Success"] = "Account has been created. Please check your email to verify your account.";
                    TempData["toastrTimeout"] = "true";
                    return RedirectToLocal(returnUrl);
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }


        [AllowAnonymous]
        public JsonResult LookupAgencyEmail(string email)
        {
            List<T_PRT_ORGANIZATIONS> orgs = _DbPortal.GetT_PRT_ORGANIZATIONS_ByEmail(email);
            if (orgs != null && orgs.Count == 1)
            {
                return Json(new
                {
                    msg = "Success",
                    orgid = orgs[0].ORG_ID,
                    orgname = orgs[0].ORG_NAME
                });
            }

            return Json(new { msg = "None" });
        }


        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            // build a model so the logout page knows what to display
            var vm = new LogoutViewModel
            {
                LogoutId = logoutId,
                ShowLogoutPrompt = true
            };

            var user = Request.HttpContext.User;

            if (user == null || user.Identity.IsAuthenticated == false)
                vm.ShowLogoutPrompt = false;
            else
            {
                var context = await _interaction.GetLogoutContextAsync(logoutId);
                if (context?.ShowSignoutPrompt == false)
                    vm.ShowLogoutPrompt = false;
            }

            if (vm.ShowLogoutPrompt == false)
            {
                // if the request for logout was properly authenticated from IdentityServer, then
                // we don't need to show the prompt and can just log the user out directly.
                return await Logout();
            }
            else
            {
                return View(vm);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            //remove left menu from memory cache
            _memoryCache.Remove("UserMenuData" + _userManager.GetUserId(Request.HttpContext.User));

            //log out (delete local authentication cookie)
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");

            //redirect user to main page
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult ExternalLogin(string provider, string returnUrl = null)
        {
            // Request a redirect to the external login provider.
            var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Account", new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            if (remoteError != null)
            {
                ErrorMessage = $"Error from external provider: {remoteError}";
                return RedirectToAction(nameof(Login));
            }
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction(nameof(Login));
            }

            // Sign in the user with this external login provider if the user already has a login.
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if (result.Succeeded)
            {
                _logger.LogInformation("User logged in with {Name} provider.", info.LoginProvider);
                return RedirectToLocal(returnUrl);
            }
            if (result.IsLockedOut)
            {
                return RedirectToAction(nameof(Lockout));
            }
            else
            {
                // If the user does not have an account, then ask the user to create an account.
                ViewData["ReturnUrl"] = returnUrl;
                ViewData["LoginProvider"] = info.LoginProvider;
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                return View("ExternalLogin", new ExternalLoginViewModel { Email = email });
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExternalLoginConfirmation(ExternalLoginViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await _signInManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    throw new ApplicationException("Error loading external login information during confirmation.");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await _userManager.AddLoginAsync(user, info);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        _logger.LogInformation("User created an account using {Name} provider.", info.LoginProvider);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View(nameof(ExternalLogin), model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                TempData["Error"] = "Confirmation link is not valid. Please try again.";
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{userId}'.");
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                // if user doesn't exist don't reveal and silently return confirmation
                if (user == null)
                {
                    return RedirectToAction(nameof(ForgotPasswordConfirmation));
                }


                //if user has registered but not confirmed email, resend email confirmation
                if (await _userManager.IsEmailConfirmedAsync(user) == false)
                {
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                    bool EmailSucc = _emailSender.SendEmail(null, model.Email, null, null, null, null, "EMAIL_CONFIRM", "callbackUrl", callbackUrl);
                    return RedirectToAction(nameof(ResendVerificationEmailConfirmation));
                }
                else
                {
                    var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var callbackUrl = Url.ResetPasswordCallbackLink(user.Id, code, Request.Scheme);
                    _emailSender.SendEmail(null, model.Email, null, null, null, null, "RESET_PASSWORD", "callbackUrl", callbackUrl);
                    return RedirectToAction(nameof(ForgotPasswordConfirmation));
                }

            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResendVerificationEmailConfirmation()
        {
            return View();
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string code = null)
        {
            if (code == null)
            {
                throw new ApplicationException("A code must be supplied for password reset.");
            }
            var model = new ResetPasswordViewModel { Code = code };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction(nameof(ResetPasswordConfirmation));
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                _DbPortal.UpdateT_PRT_USERS_PasswordEncrypt(user, model.Password);
                return RedirectToAction(nameof(ResetPasswordConfirmation));
            }
            AddErrors(result);
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }


        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [IgnoreAntiforgeryToken]
        [AcceptVerbs("GetNewUserData")]
        public async Task<IActionResult> GetNewUserData([FromQuery] string userid)
        {
            string actResult = string.Empty;
            JWTLoginModel model = new JWTLoginModel();
            Response.StatusCode = StatusCodes.Status500InternalServerError;

            var user = _userManager.FindByIdAsync(userid).Result;
            if (user != null)
            {
                model.UserId = user.Id;
                model.email = user.Email;
                model.firstName = user.FIRST_NAME;
                model.lastName = user.LAST_NAME;
                model.password = user.PasswordEncrypt;
                var _roles = await _userManager.GetRolesAsync(user);
                model.roles = _roles.ToList();
                model.orgUsers = GetOrgUserWithClients(user.Id, "open_waters");
                model.isAdmin = GetIsAdmin(model.orgUsers);
                return Ok(model);
            }

            // If we got this far, something failed, redisplay form
            model.errMsg = "Something went wrong!";
            return Ok(model);
        }

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            else
                return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        #endregion
    }
}
