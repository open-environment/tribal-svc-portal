﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using TribalSvcPortal.AppLogic.BusinessLogicLayer;
using TribalSvcPortal.AppLogic.DataAccessLayer;
using TribalSvcPortal.Data.Models;
using TribalSvcPortal.Services;
using TribalSvcPortal.ViewModels.ManageViewModels;
using WordPressPCL;

namespace TribalSvcPortal.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class ManageController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly UrlEncoder _urlEncoder;
        private readonly IDbPortal _DbPortal;
        private readonly IConfiguration _config;
        private readonly Ilog _log;

        private const string AuthenticatorUriFormat = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6";
        private const string RecoveryCodesKey = nameof(RecoveryCodesKey);

        public ManageController(
          UserManager<ApplicationUser> userManager,
          RoleManager<IdentityRole> roleManager,
          SignInManager<ApplicationUser> signInManager,
          IEmailSender emailSender,
          ILogger<ManageController> logger,
          UrlEncoder urlEncoder, 
          IDbPortal DbPortal,
          IConfiguration config,
          Ilog log)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
            _urlEncoder = urlEncoder;
            _DbPortal = DbPortal;
            _log = log;
            _config = config;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var model = new IndexViewModel
            {
                Username = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                IsEmailConfirmed = user.EmailConfirmed,
                StatusMessage = StatusMessage
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(IndexViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var email = user.Email;
            if (model.Email != email)
            {
                var setEmailResult = await _userManager.SetEmailAsync(user, model.Email);
                if (!setEmailResult.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occurred setting email for user with ID '{user.Id}'.");
                }
            }

            var phoneNumber = user.PhoneNumber;
            if (model.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, model.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occurred setting phone number for user with ID '{user.Id}'.");
                }
            }

            StatusMessage = "Your profile has been updated";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendVerificationEmail(IndexViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
            bool emailSucc = _emailSender.SendEmail(null, model.Email, null, null, null, null, "EMAIL_CONFIRM", "callbackUrl", callbackUrl);

            StatusMessage = "Verification email sent. Please check your email.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var hasPassword = await _userManager.HasPasswordAsync(user);
            if (!hasPassword)
            {
                return RedirectToAction(nameof(SetPassword));
            }

            var model = new ChangePasswordViewModel { StatusMessage = StatusMessage };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                AddErrors(changePasswordResult);
                return View(model);
            }
            _log.InsertT_PRT_SYS_LOG("Info", "Password changed successfully, begin wordpress activities.");
            string wpMessage = "";
            WordPressHelper wordPressHelper = new WordPressHelper(_userManager, _DbPortal, _log, _emailSender);
            //We need this password to setup in WordPress
            _DbPortal.UpdateT_PRT_USERS_PasswordEncrypt(user, model.NewPassword);
            if (user.WordPressUserId == null || user.WordPressUserId <= 0)
            {
                _log.InsertT_PRT_SYS_LOG("Info", "WordPressUserId not set, hence create new user.");
                List<UserOrgDisplayType> userOrgDisplayTypes = _DbPortal.GetT_PRT_ORG_USERS_ByUserID(user.Id);
                if(userOrgDisplayTypes != null && userOrgDisplayTypes.Count > 0)
                {
                    _log.InsertT_PRT_SYS_LOG("Info", "User-Org relation found.");
                    int isWordPressUserCreated = 0;
                    foreach(UserOrgDisplayType uodt in userOrgDisplayTypes)
                    {
                        IList<string> sites = "ABSHAWNEE,KICKAPOO,MCNCREEK,SFNOES".Split(",");
                        if (sites.Contains(uodt.ORG_ID.Trim().ToUpper()))
                        {
                            if (uodt.ACCESS_LEVEL == "A" && uodt.STATUS_IND == "A")
                            {
                                //_log.InsertT_PRT_SYS_LOG("Info", "Create user for org:" + uodt.ORG_NAME);
                                if (isWordPressUserCreated == 0)
                                {
                                    isWordPressUserCreated = await wordPressHelper.SetupWordPressAccess(user.Id, uodt.ORG_ID, uodt.ACCESS_LEVEL, uodt.STATUS_IND);
                                    if (isWordPressUserCreated == 0)
                                    {
                                        //_log.InsertT_PRT_SYS_LOG("Info", "User could not be created for org:" + uodt.ORG_NAME);
                                        wpMessage = "(Something went wrong with WordPress related activity!)";
                                    }
                                    //_log.InsertT_PRT_SYS_LOG("Info", "User created for org:" + uodt.ORG_NAME);
                                }
                                else
                                {
                                    //_log.InsertT_PRT_SYS_LOG("Info", "Assign user to remaining sites/organizations: " + uodt.ORG_NAME);
                                    //Assign user to remaining sites
                                    int wpuid = 0;
                                    Int32.TryParse(user.WordPressUserId.ToString(), out wpuid);
                                    var isUserUpdated = wordPressHelper.AddRemoveUserSite(wpuid, uodt.ORG_ID, 1);
                                    if (isUserUpdated == false)
                                    {
                                        //_log.InsertT_PRT_SYS_LOG("Info", "User could not be assigned to remaining sites/organizations for: " + uodt.ORG_NAME);
                                        wpMessage = "(Something went wrong with WordPress related activity!)";
                                    }
                                    //_log.InsertT_PRT_SYS_LOG("Info", "User assigned to remaining sites/organizations for: " + uodt.ORG_NAME);
                                }

                            }
                        }
                            
                    }
                }
            } else
            {
                _log.InsertT_PRT_SYS_LOG("Info", "WordPressUserId is set hence we update password for all the sites/organizations.");
                List<UserOrgDisplayType> userOrgDisplayTypes = _DbPortal.GetT_PRT_ORG_USERS_ByUserID(user.Id);
                Boolean isPasswordUpdated = false;
                foreach (UserOrgDisplayType uodt in userOrgDisplayTypes)
                {
                    IList<string> sites = "ABSHAWNEE,KICKAPOO,MCNCREEK,SFNOES".Split(",");

                    if (sites.Contains(uodt.ORG_ID.Trim().ToUpper()))
                    {
                        if (uodt.ACCESS_LEVEL == "A" && uodt.STATUS_IND == "A")
                        {
                            int wpuid = 0;
                            Int32.TryParse(user.WordPressUserId.ToString(), out wpuid);
                            WordPressClient wordPressClient = await wordPressHelper.GetAuthenticatedWordPressClient(uodt.ORG_ID);
                            string role = "administrator";
                            if (uodt.ACCESS_LEVEL != "A" || uodt.STATUS_IND != "A") role = "inactive";
                            isPasswordUpdated = await wordPressHelper.UpdateWordPressUser(user, wordPressClient, wpuid, role);
                            if (isPasswordUpdated == false)
                            {
                                _log.InsertT_PRT_SYS_LOG("Info", "Password could not be updated for org: " + uodt.ORG_NAME);
                                wpMessage = "(Something went wrong with WordPress related activity!)";
                            }
                            _log.InsertT_PRT_SYS_LOG("Info", "Password updated for org: " + uodt.ORG_NAME);
                        }
                    }
                }
            }
            await _signInManager.SignInAsync(user, isPersistent: false);
            _logger.LogInformation("User changed their password successfully.");
            StatusMessage = "Your password has been changed. " + wpMessage;

            return RedirectToAction(nameof(ChangePassword));
        }

        [HttpGet]
        public async Task<IActionResult> AccessRights()
        {
            var user = await _userManager.GetUserAsync(User);
            var _roles = await _userManager.GetRolesAsync(user);

            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }


            var model = new AccessRightsViewModel
            {
                AccessRights = _DbPortal.GetT_PRT_ORG_USERS_ByUserID_WithClientList(user.Id),
                Clients = _DbPortal.GetT_PRT_CLIENTS(),
                Roles = _roles.ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AccessRightsRequest(int? orgUser, string client)
        {
            string _UserIDX = _userManager.GetUserId(User);

            T_PRT_ORG_USERS _ou = _DbPortal.GetT_PRT_ORG_USERS_ByOrgUserID(orgUser ?? -1);
            if (_ou != null)
            {
                int SuccID = _DbPortal.InsertUpdateT_PRT_ORG_USERS_CLIENT(null, orgUser, client, false, "R", _UserIDX);

                //return response
                if (SuccID > 0)
                {
                    //send email
                    List<string> _emailRecipients = new List<string>();

                    //**************first try to send to org / client admins
                    List<OrgUserClientDisplayType> _orgUserClientAdmins = _DbPortal.GetT_PRT_ORG_USERS_CLIENT_ByOrgIDandClientID(_ou.ORG_ID, client, true);
                    if (_orgUserClientAdmins != null && _orgUserClientAdmins.Count > 0)
                    {
                        foreach (OrgUserClientDisplayType _orgUserClientAdmin in _orgUserClientAdmins)
                        {
                            ApplicationUser _u = _userManager.FindByIdAsync(_orgUserClientAdmin.UserID).Result;
                            if (_u != null)
                                _emailRecipients.Add(_u.Email);
                        }
                    }

                    //**************if none found, then send to org admins

                    //**************finally send to portal admins
                    if (_emailRecipients.Count == 0)
                    {
                        IdentityRole _r = _roleManager.FindByNameAsync("PortalAdmin").Result;

                        IEnumerable<ApplicationUser> _us = _DbPortal.GetT_PRT_USERS_BelongingToRole(_r.Id);
                        if (_us != null)
                        {
                            foreach (ApplicationUser _u in _us)
                                _emailRecipients.Add(_u.Email);
                        }
                    }

                    string _UserName = _userManager.GetUserName(User);

                    //construct email parameters
                    List<emailParam> emailParams = new List<emailParam>()
                    {
                        new emailParam() { PARAM_NAME = "userName", PARAM_VAL = _UserName },
                        new emailParam() { PARAM_NAME = "client", PARAM_VAL = client },
                        new emailParam() { PARAM_NAME = "orgID", PARAM_VAL = _ou.ORG_ID }
                    };

                    foreach (string _emailRecipient in _emailRecipients)
                        _emailSender.SendEmail(null, _emailRecipient, null, null, null, null, "ACCESS_REQUEST", emailParams);


                    return Json(new
                    {
                        msg = "Success",
                        redirectUrl = Url.Action("AccessRights", "Manage")
                    });
                }
            }

            //if got this far, it failed
            return Json(new { msg = "Unable to request access." });
        }


        [HttpGet]
        public async Task<IActionResult> SetPassword()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var hasPassword = await _userManager.HasPasswordAsync(user);

            if (hasPassword)
            {
                return RedirectToAction(nameof(ChangePassword));
            }

            var model = new SetPasswordViewModel { StatusMessage = StatusMessage };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var addPasswordResult = await _userManager.AddPasswordAsync(user, model.NewPassword);
            if (!addPasswordResult.Succeeded)
            {
                AddErrors(addPasswordResult);
                return View(model);
            }

            await _signInManager.SignInAsync(user, isPersistent: false);
            StatusMessage = "Your password has been set.";

            return RedirectToAction(nameof(SetPassword));
        }

        [HttpGet]
        public async Task<IActionResult> ExternalLogins()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var model = new ExternalLoginsViewModel { CurrentLogins = await _userManager.GetLoginsAsync(user) };
            model.OtherLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync())
                .Where(auth => model.CurrentLogins.All(ul => auth.Name != ul.LoginProvider))
                .ToList();
            model.ShowRemoveButton = await _userManager.HasPasswordAsync(user) || model.CurrentLogins.Count > 1;
            model.StatusMessage = StatusMessage;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LinkLogin(string provider)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            // Request a redirect to the external login provider to link a login for the current user
            var redirectUrl = Url.Action(nameof(LinkLoginCallback));
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl, _userManager.GetUserId(User));
            return new ChallengeResult(provider, properties);
        }

        [HttpGet]
        public async Task<IActionResult> LinkLoginCallback()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var info = await _signInManager.GetExternalLoginInfoAsync(user.Id);
            if (info == null)
            {
                throw new ApplicationException($"Unexpected error occurred loading external login info for user with ID '{user.Id}'.");
            }

            var result = await _userManager.AddLoginAsync(user, info);
            if (!result.Succeeded)
            {
                throw new ApplicationException($"Unexpected error occurred adding external login for user with ID '{user.Id}'.");
            }

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            StatusMessage = "The external login was added.";
            return RedirectToAction(nameof(ExternalLogins));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveLogin(RemoveLoginViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var result = await _userManager.RemoveLoginAsync(user, model.LoginProvider, model.ProviderKey);
            if (!result.Succeeded)
            {
                throw new ApplicationException($"Unexpected error occurred removing external login for user with ID '{user.Id}'.");
            }

            await _signInManager.SignInAsync(user, isPersistent: false);
            StatusMessage = "The external login was removed.";
            return RedirectToAction(nameof(ExternalLogins));
        }

        [HttpGet]
        public async Task<IActionResult> TwoFactorAuthentication()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var model = new TwoFactorAuthenticationViewModel
            {
                HasAuthenticator = await _userManager.GetAuthenticatorKeyAsync(user) != null,
                Is2faEnabled = user.TwoFactorEnabled,
                RecoveryCodesLeft = await _userManager.CountRecoveryCodesAsync(user),
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Disable2faWarning()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!user.TwoFactorEnabled)
            {
                throw new ApplicationException($"Unexpected error occured disabling 2FA for user with ID '{user.Id}'.");
            }

            return View(nameof(Disable2fa));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Disable2fa()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var disable2faResult = await _userManager.SetTwoFactorEnabledAsync(user, false);
            if (!disable2faResult.Succeeded)
            {
                throw new ApplicationException($"Unexpected error occured disabling 2FA for user with ID '{user.Id}'.");
            }

            _logger.LogInformation("User with ID {UserId} has disabled 2fa.", user.Id);
            return RedirectToAction(nameof(TwoFactorAuthentication));
        }

        [HttpGet]
        public async Task<IActionResult> EnableAuthenticator()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var model = new EnableAuthenticatorViewModel();
            await LoadSharedKeyAndQrCodeUriAsync(user, model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EnableAuthenticator(EnableAuthenticatorViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadSharedKeyAndQrCodeUriAsync(user, model);
                return View(model);
            }

            // Strip spaces and hypens
            var verificationCode = model.Code.Replace(" ", string.Empty).Replace("-", string.Empty);

            var is2faTokenValid = await _userManager.VerifyTwoFactorTokenAsync(
                user, _userManager.Options.Tokens.AuthenticatorTokenProvider, verificationCode);

            if (!is2faTokenValid)
            {
                ModelState.AddModelError("Code", "Verification code is invalid.");
                await LoadSharedKeyAndQrCodeUriAsync(user, model);
                return View(model);
            }

            await _userManager.SetTwoFactorEnabledAsync(user, true);
            _logger.LogInformation("User with ID {UserId} has enabled 2FA with an authenticator app.", user.Id);
            var recoveryCodes = await _userManager.GenerateNewTwoFactorRecoveryCodesAsync(user, 10);
            TempData[RecoveryCodesKey] = recoveryCodes.ToArray();

            return RedirectToAction(nameof(ShowRecoveryCodes));
        }

        [HttpGet]
        public IActionResult ShowRecoveryCodes()
        {
            var recoveryCodes = (string[])TempData[RecoveryCodesKey];
            if (recoveryCodes == null)
            {
                return RedirectToAction(nameof(TwoFactorAuthentication));
            }

            var model = new ShowRecoveryCodesViewModel { RecoveryCodes = recoveryCodes };
            return View(model);
        }

        [HttpGet]
        public IActionResult ResetAuthenticatorWarning()
        {
            return View(nameof(ResetAuthenticator));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetAuthenticator()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await _userManager.SetTwoFactorEnabledAsync(user, false);
            await _userManager.ResetAuthenticatorKeyAsync(user);
            _logger.LogInformation("User with id '{UserId}' has reset their authentication app key.", user.Id);

            return RedirectToAction(nameof(EnableAuthenticator));
        }

        [HttpGet]
        public async Task<IActionResult> GenerateRecoveryCodesWarning()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!user.TwoFactorEnabled)
            {
                throw new ApplicationException($"Cannot generate recovery codes for user with ID '{user.Id}' because they do not have 2FA enabled.");
            }

            return View(nameof(GenerateRecoveryCodes));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GenerateRecoveryCodes()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!user.TwoFactorEnabled)
            {
                throw new ApplicationException($"Cannot generate recovery codes for user with ID '{user.Id}' as they do not have 2FA enabled.");
            }

            var recoveryCodes = await _userManager.GenerateNewTwoFactorRecoveryCodesAsync(user, 10);
            _logger.LogInformation("User with ID {UserId} has generated new 2FA recovery codes.", user.Id);

            var model = new ShowRecoveryCodesViewModel { RecoveryCodes = recoveryCodes.ToArray() };

            return View(nameof(ShowRecoveryCodes), model);
        }

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private string FormatKey(string unformattedKey)
        {
            var result = new StringBuilder();
            int currentPosition = 0;
            while (currentPosition + 4 < unformattedKey.Length)
            {
                result.Append(unformattedKey.Substring(currentPosition, 4)).Append(" ");
                currentPosition += 4;
            }
            if (currentPosition < unformattedKey.Length)
            {
                result.Append(unformattedKey.Substring(currentPosition));
            }

            return result.ToString().ToLowerInvariant();
        }

        private string GenerateQrCodeUri(string email, string unformattedKey)
        {
            return string.Format(
                AuthenticatorUriFormat,
                _urlEncoder.Encode("TribalSvcPortal"),
                _urlEncoder.Encode(email),
                unformattedKey);
        }

        private async Task LoadSharedKeyAndQrCodeUriAsync(ApplicationUser user, EnableAuthenticatorViewModel model)
        {
            var unformattedKey = await _userManager.GetAuthenticatorKeyAsync(user);
            if (string.IsNullOrEmpty(unformattedKey))
            {
                await _userManager.ResetAuthenticatorKeyAsync(user);
                unformattedKey = await _userManager.GetAuthenticatorKeyAsync(user);
            }

            model.SharedKey = FormatKey(unformattedKey);
            model.AuthenticatorUri = GenerateQrCodeUri(user.Email, unformattedKey);
        }

        #endregion
    }
}
