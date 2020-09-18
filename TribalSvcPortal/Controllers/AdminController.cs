using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TribalSvcPortal.AppLogic.BusinessLogicLayer;
using TribalSvcPortal.AppLogic.DataAccessLayer;
using TribalSvcPortal.Data.Models;
using TribalSvcPortal.Services;
using TribalSvcPortal.ViewModels.AdminViewModels;
using WordPressPCL;
using WordPressPCL.Models;

namespace TribalSvcPortal.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IDbPortal _DbPortal;
        private readonly IConfiguration _config;
        private readonly Ilog _log;
        private readonly IEmailSender _emailSender;

        //private static WordPressClient _clientAuth;

        public AdminController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IDbPortal DbPortal,
            IConfiguration config,
            Ilog log,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _DbPortal = DbPortal;
            _config = config ?? throw new System.ArgumentNullException(nameof(config));
            _log = log ?? throw new ArgumentNullException(nameof(log));
            _emailSender = emailSender;
        }

        public IActionResult Index()
        {
            return View();
        }



        //******************************* ROLES **********************************************************
        [Authorize(Roles = "PortalAdmin")]
        public IActionResult RoleList()
        {
            var roles = _roleManager.Roles.ToList();
            return View(roles);
        }


        [Authorize(Roles = "PortalAdmin")]
        public async Task<IActionResult> RoleEdit(string id)
        {
            IdentityRole _role = await _roleManager.FindByIdAsync(id);

            //users with the assigned role
            var RolesInUser = _DbPortal.GetT_PRT_USERS_BelongingToRole(id);

            //all users
            var allUsers = _userManager.Users.ToList();

            var model = new RoleEditViewModel
            {
                T_PRT_ROLES = _role,
                Users_In_Role = RolesInUser.Select(x => new SelectListItem
                {
                    Value = x.Id,
                    Text = x.Email
                }),
                Users_Not_In_Role = allUsers.Except(RolesInUser).OrderBy(a => a.Email).Select(x => new SelectListItem
                {
                    Value = x.Id,
                    Text = x.Email
                })
            };

            return View(model);
        }


        [Authorize(Roles = "PortalAdmin")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> RoleEdit(RoleEditViewModel model, string submitButton)
        {

            IdentityRole _role = await _roleManager.FindByIdAsync(model.T_PRT_ROLES.Id);
            if (_role != null)
            {
                IdentityResult SuccID = new IdentityResult();

                // ADDING ROLE TO USER
                if (submitButton == "Add")
                {
                    foreach (string u in model.Users_Not_In_Role_Selected)
                        SuccID = await _userManager.AddToRoleAsync(_userManager.FindByIdAsync(u).Result, _role.Name);
                }
                // REMOVE ROLE FROM USER
                else if (submitButton == "Remove")
                {
                    foreach (string u in model.Users_In_Role_Selected)
                        SuccID = await _userManager.RemoveFromRoleAsync(_userManager.FindByIdAsync(u).Result, _role.Name);
                }


                if (SuccID.Succeeded)
                    TempData["Success"] = "Update successful.";
            }

            return RedirectToAction("RoleEdit", new { id = model.T_PRT_ROLES.Id });
        }


        //******************************* USERS **********************************************************
        public IActionResult UserList()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }


        [Authorize(Roles = "PortalAdmin")]
        public async Task<IActionResult> UserEdit(string id)
        {
            //roles the user is assigned 
            var RolesInUser = _DbPortal.GetT_PRT_ROLES_BelongingToUser(id);

            var allRoles = _roleManager.Roles.ToList();  //  get all roles 

            var model = new UserEditViewModel
            {
                appUser = await _userManager.FindByIdAsync(id),
                UserRoles = RolesInUser.Select(x => new SelectListItem
                {
                    Value = x.Name,
                    Text = x.Name
                }),
                RoleNotInUser = allRoles.Except(RolesInUser).OrderBy(a => a.Name).Select(x => new SelectListItem
                {
                    Value = x.Name,
                    Text = x.Name
                }),
                UserOrgs = _DbPortal.GetT_PRT_ORG_USERS_ByUserID(id),
                ddl_Orgs = _DbPortal.GetT_PRT_ORGANIZATIONS().Select(x => new SelectListItem
                {
                    Value = x.ORG_ID.ToString(),
                    Text = x.ORG_NAME
                })
            };

            return View(model);
        }


        [Authorize(Roles = "PortalAdmin")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> UserEdit(UserEditViewModel model, string submitButton)
        {

            ApplicationUser _user = await _userManager.FindByIdAsync(model.appUser.Id);
            if (_user != null)
            {
                IdentityResult SuccID = new IdentityResult();

                // ADDING USER TO ROLE
                if (submitButton == "Add")
                {
                    foreach (string r in model.Role_Not_In_User_Selected)
                        SuccID = await _userManager.AddToRoleAsync(_user, r);
                }
                // REMOVE USER FROM ROLE
                else if (submitButton == "Remove")
                {
                    foreach (string r in model.Users_Role_Selected)
                        SuccID = await _userManager.RemoveFromRoleAsync(_user, r);
                }
                else
                {
                    //update fields
                    _user.FIRST_NAME = model.appUser.FIRST_NAME;
                    _user.LAST_NAME = model.appUser.LAST_NAME;
                    _user.Email = model.appUser.Email;
                    _user.UserName = model.appUser.Email;
                    _user.WordPressUserId = model.appUser.WordPressUserId;
                    SuccID = await _userManager.UpdateAsync(_user);
                }

                if (SuccID.Succeeded)
                    TempData["Success"] = "Update successful.";
            }

            return RedirectToAction("UserEdit", new { id = model.appUser.Id });
        }


        [HttpPost]
        public async Task<JsonResult> UserDelete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult x = await _userManager.DeleteAsync(user);
                if (x.Succeeded)
                    return Json("Success");
                else
                    return Json("Unable to delete user.");
            }
            else
                return Json("Unable to find user to delete.");
        }


        //******************************* ORG USERS **********************************************************
        [HttpPost]
        public async Task<IActionResult> OrgUserEdit(int? edit_oRG_USER_IDX, string uidx, string org_id, string AccessLevel, string StatusInd)
        {
            if (ModelState.IsValid)
            {

                int newID = _DbPortal.InsertUpdateT_PRT_ORG_USERS(edit_oRG_USER_IDX, org_id, uidx, AccessLevel, StatusInd, User.Identity.Name);

                if (newID == 0)
                    TempData["Error"] = "Unable to add user to organization.";
                else
                {
                    WordPressHelper wordPressHelper = new WordPressHelper(_userManager,
                                                                          _roleManager,
                                                                          _DbPortal,
                                                                          _config,
                                                                          _log,
                                                                          _emailSender);
                    int isWPUserAdded = await wordPressHelper.SetupWordPressAccess(uidx, org_id, AccessLevel, StatusInd);
                    TempData["Success"] = "Record successfully added.";
                }

            }
            else
                TempData["Error"] = "Unable to add user to organization.";

            return RedirectToAction("UserEdit", "Admin", new { id = uidx });
        }

        [HttpPost]
        public async Task<JsonResult> OrgUserDelete(int id, string id2)
        {
            T_PRT_ORG_USERS orgUser = _DbPortal.GetT_PRT_ORG_USERS_ByOrgUserID(id);
            int SuccID = _DbPortal.DeleteT_PRT_ORG_USERS(orgUser);
            if (SuccID > 0)
            {
                WordPressHelper.SetUserManager(_userManager);
                ApplicationUser appUser = await WordPressHelper.GetApplicationUser(orgUser.Id);
                WordPressHelper wordPressHelper = new WordPressHelper(_userManager,
                                                                          _roleManager,
                                                                          _DbPortal,
                                                                          _config,
                                                                          _log,
                                                                          _emailSender);
                int OrgUserCount = _DbPortal.GetOrgUsersCount(orgUser.Id);
                if(OrgUserCount == 0)
                {
                    //if we have user in wordpress, make it inactive
                    if(appUser.WordPressUserId > 0)
                    {
                       //string wordPressUri = wordPressHelper.SetWordPressUri(orgUser.ORG_ID);
                       // string userName = wordPressHelper.GetUserName();
                       // string password = wordPressHelper.GetPassword();
                        int wpuid = 0;
                        Int32.TryParse(appUser.WordPressUserId.ToString(), out wpuid);
                        //WordPressClient wordPressClient = await wordPressHelper.GetAuthenticatedWordPressClient(wordPressUri, userName, password);
                        WordPressClient wordPressClient = await wordPressHelper.GetAuthenticatedWordPressClient(orgUser.ORG_ID);
                        bool isUserUpdated = await wordPressHelper.UpdateWordPressUser(appUser, wordPressClient, wpuid, "inactive");
                    }
                }
                else
                {
                    //revoke access from the site/organization from wordpress
                    int wpuid = 0;
                    Int32.TryParse(appUser.WordPressUserId.ToString(), out wpuid);
                    wordPressHelper.AddRemoveUserSite(wpuid, orgUser.ORG_ID, 0);
                }
                return Json("Success");
            }
            else
                return Json("Unable to delete user from organization.");
        }


        //******************************* CLIENT **********************************************************
        [Authorize(Roles = "PortalAdmin")]
        public IActionResult ClientList()
        {

            var model = _DbPortal.GetT_PRT_CLIENTS();
            return View(model);
        }

        [Authorize(Roles = "PortalAdmin")]
        public IActionResult ClientEdit(string id)
        {
            var model = _DbPortal.GetT_PRT_CLIENTS_ByClientID(id);
            return View(model);
        }

        [Authorize(Roles = "PortalAdmin")]
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult ClientEdit(T_PRT_CLIENTS model)
        {

            if (model != null)
            {
                string ClientID = _DbPortal.InsertUpdateT_PRT_CLIENTS(model.CLIENT_ID, model.CLIENT_NAME, model.CLIENT_GRANT_TYPE, model.CLIENT_REDIRECT_URI, model.CLIENT_POST_LOGOUT_URI, model.CLIENT_URL);

                if (ClientID != null)
                    TempData["Success"] = "Update successful.";
                else
                    TempData["Error"] = "Error adding client";

            }

            return RedirectToAction("ClientEdit", new { id = model.CLIENT_ID });
        }



        //******************************* ORGANIZATIONS **********************************************************
        public IActionResult OrgList()
        {
            var model = _DbPortal.GetT_PRT_ORGANIZATIONS();
            return View(model);
        }

        public IActionResult OrgEdit(string id)
        {
            var model = new OrgEditViewModel
            {
                Organization = _DbPortal.GetT_PRT_ORGANIZATIONS_ByOrgID(id),
                OrgEmails = _DbPortal.GetT_PRT_ORG_EMAIL_RULE_ByOrgID(id)
            };

            //handling insert case
            if (model.Organization == null)
            {
                model.Organization = new T_PRT_ORGANIZATIONS();
            }
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult OrgEdit(T_PRT_ORGANIZATIONS org)
        {
            //security check
            string _UserIDX = _userManager.GetUserId(User);
            if (_DbPortal.IsUserAnOrgAdmin(_UserIDX, org.ORG_ID))
            {
                string ClientID = _DbPortal.InsertUpdateT_PRT_ORGANIZATIONS(org.ORG_ID, org.ORG_NAME);

                if (ClientID != null)
                    TempData["Success"] = "Update successful.";
                else
                    TempData["Error"] = "Error editing organization.";

            }
            else
                TempData["Error"] = "You must have admin rights to edit this Organization.";

            return RedirectToAction("OrgEdit", new { id = org.ORG_ID });

        }


        //******************************* ORG EMAIL **********************************************************
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult OrgEditEmail(OrgEditViewModel model)
        {
            string _UserIDX = _userManager.GetUserId(User);

            int SuccID = _DbPortal.InsertT_PRT_ORG_EMAIL_RULE(model.Organization.ORG_ID, model.new_email, _UserIDX);

            if (SuccID == 1)
                TempData["Success"] = "Update successful.";
            else
                TempData["Error"] = "Error updating data.";

            return RedirectToAction("OrgEdit", new { id = model.Organization.ORG_ID });
        }

        // POST: /Admin/RefAgencyEditEmailDelete
        [HttpPost]
        public JsonResult RefAgencyEditEmailDelete(string id, string id2)
        {
            int SuccID = _DbPortal.DeleteT_OE_ORGANIZATION_EMAIL_RULE(id, id2);
            if (SuccID == 0)
                return Json("Unable to delete record.");
            else
                return Json("Success");
        }




        //************************************ ORGANIZATION USER CLIENT  ***************************************
        public IActionResult OrgUserClients(int id)
        {
            T_PRT_ORG_USERS _orgUser = _DbPortal.GetT_PRT_ORG_USERS_ByOrgUserID(id);
            if (_orgUser != null)
            {
                var model = new OrgUserEditViewModel
                {
                    OrgUserIDX = id,
                    OrgUserClients = _DbPortal.GetT_PRT_ORG_USERS_CLIENT_ByOrgUserID(id),
                    UserIDX = _orgUser.Id,
                    ddl_Clients = _DbPortal.GetT_PRT_CLIENTS().Select(x => new SelectListItem
                    {
                        Value = x.CLIENT_ID,
                        Text = x.CLIENT_NAME
                    })
                };

                return View(model);
            }
            else {
                TempData["Error"] = "No matching record found.";
                return RedirectToAction("UserList");
            }
        }

        [HttpPost]
        public IActionResult OrgUserClientEdit(int org_user_idx, string client_id, string AdminInd, string StatusInd, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                //int newID = _DbPortal.InsertUpdateT_PRT_ORG_USERS_CLIENT(null, org_user_idx, client_id, (AdminInd == "1" ? true : false), StatusInd, User.Identity.Name);
                int newID = _DbPortal.InsertUpdateT_PRT_ORG_USERS_CLIENT(null, org_user_idx, client_id, (AdminInd == "True" ? true : false), StatusInd, User.Identity.Name);

                if (newID == 0)
                    TempData["Error"] = "Error adding user";
                else
                    TempData["Success"] = "Record successfully added";
            }
            else
                TempData["Error"] = "Error adding user";

            return RedirectToAction(returnUrl, "Admin", new { id = org_user_idx });
        }

        [HttpPost]
        public IActionResult OrgUserClientDelete(int id, string id2)
        {
            int SuccID = _DbPortal.DeleteT_PRT_ORG_USER_CLIENT(id);
            if (SuccID > 0)
                TempData["Success"] = "Record has been deleted.";
            else
                TempData["Error"] = "Unable to delete client access from user.";

            return RedirectToAction("OrgUserClients", new { id = id2 });
        }

        [HttpPost]
        public IActionResult OrgUserClientDelete2(int id, string id2)
        {
            int SuccID = _DbPortal.DeleteT_PRT_ORG_USER_CLIENT(id);
            if (SuccID > 0)
                TempData["Success"] = "Record has been deleted.";
            else
                TempData["Error"] = "Unable to delete client access from user.";

            return RedirectToAction("ManageUsers", new { id = id2 });
        }


        //************************************ ORGANIZATION USER CLIENT (non-Global Admin)  ***************************************
        public IActionResult ManageUsers(int? id)
        {
            //********* id = OrgUserClientID

            string _UserIDX = _userManager.GetUserId(User);

            var model = new ManageUsersViewModel
            {
                ddl_AdminOfOrgClients = _DbPortal.GetT_PRT_ORG_USERS_CLIENT_AdminByUserID(_UserIDX).Select(x => new SelectListItem
                {
                    Value = x.ORG_USER_CLIENT_IDX.ToString(),
                    Text = x.ORG_CLIENT_ALIAS + " - " + x.CLIENT_ID
                })
            };

            //get users currently listed for the org/client
            //get users for the organization
            if (id != null)
            {
                model.selOrgUserClient = id;

                T_PRT_ORG_USER_CLIENT _ouc = _DbPortal.GetT_PRT_ORG_USERS_CLIENT_ByID((int)id);
                if (_ouc != null)
                {
                    T_PRT_ORG_USERS _ou = _DbPortal.GetT_PRT_ORG_USERS_ByOrgUserID(_ouc.ORG_USER_IDX);
                    if (_ou != null)
                    {
                        model.client_id = _ouc.CLIENT_ID;
                        model.selOrg = _ou.ORG_ID;
                        model.OrgUserClients = _DbPortal.GetT_PRT_ORG_USERS_CLIENT_ByOrgIDandClientID(_ou.ORG_ID, _ouc.CLIENT_ID, false);
                        model.ddl_Users = _DbPortal.GetT_PRT_ORG_USERS_ByOrgID(_ou.ORG_ID).Select(x => new SelectListItem
                        {
                            Value = x.ORG_USER_IDX.ToString(),
                            Text = x.USER_NAME
                        });
                    }
                }
            };

            return View(model);
        }




        //************************************ SETTINGS ***************************************
        [Authorize(Roles = "PortalAdmin")]
        public ActionResult Settings()
        {
            T_PRT_APP_SETTINGS_CUSTOM custSettings = _DbPortal.GetT_PRT_APP_SETTINGS_CUSTOM();
            var model = new SettingsViewModel
            {
                app_settings = _DbPortal.GetT_PRT_APP_SETTING_List(),
                TermsAndConditions = custSettings.TERMS_AND_CONDITIONS,
                Announcements = custSettings.ANNOUNCEMENTS
            };
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Authorize(Roles = "PortalAdmin")]
        public ActionResult Settings(SettingsViewModel model)
        {
            if (ModelState.IsValid)
            {
                string UserID = _userManager.GetUserId(User);

                int SuccID = _DbPortal.InsertUpdateT_PRT_APP_SETTING(model.edit_app_setting.SETTING_IDX, model.edit_app_setting.SETTING_NAME, model.edit_app_setting.SETTING_VALUE, false, null, UserID);
                if (SuccID > 0)
                    TempData["Success"] = "Data Saved.";
                else
                    TempData["Error"] = "Data Not Saved.";
            }

            return RedirectToAction("Settings");
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Authorize(Roles = "PortalAdmin")]
        public ActionResult CustomSettings(SettingsViewModel model)
        {
            if (ModelState.IsValid)
            {
                int SuccID = _DbPortal.InsertUpdateT_PRT_APP_SETTING_CUSTOM(model.TermsAndConditions, null);
                if (SuccID > 0)
                    TempData["Success"] = "Data Saved.";
                else
                    TempData["Error"] = "Data Not Saved.";
            }

            return RedirectToAction("Settings");
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Authorize(Roles = "PortalAdmin")]
        public ActionResult CustomSettingsAnnounce(SettingsViewModel model)
        {
            if (ModelState.IsValid)
            {
                int SuccID = _DbPortal.InsertUpdateT_PRT_APP_SETTING_CUSTOM(null, model.Announcements ?? "");
                if (SuccID > 0)
                    TempData["Success"] = "Data Saved.";
                else
                    TempData["Error"] = "Data Not Saved.";
            }

            return RedirectToAction("Settings");
        }


        //************************************ EMAIL CONFIG  ***************************************
        [Authorize(Roles = "PortalAdmin")]
        public ActionResult EmailConfig(int? id)
        {
            var model = new EmailConfigViewModel();
            model.ddl_EmailTemplate = _DbPortal.get_ddl_T_PRT_REF_EMAIL_TEMPLATE();
            if (id != null)
            {
                model.selTemplate = id;
                model.selEmailTemplate = _DbPortal.GetT_PRT_REF_EMAIL_TEMPLATE_ByID(id ?? 0);
            }
            return View(model);
        }


        [HttpPost, ValidateAntiForgeryToken]
        [Authorize(Roles = "PortalAdmin")]
        public ActionResult EmailConfig(EmailConfigViewModel model)
        {
            if (ModelState.IsValid)
            {
                string UserID = _userManager.GetUserId(User);

                int SuccID = _DbPortal.InsertUpdateT_PRT_REF_EMAIL_TEMPLATE(model.selEmailTemplate.EMAIL_TEMPLATE_ID, model.selEmailTemplate.SUBJ, model.selEmailTemplate.MSG, UserID);
                if (SuccID > 0)
                    TempData["Success"] = "Data Saved.";
                else
                    TempData["Error"] = "Data Not Saved.";
            }

            return RedirectToAction("EmailConfig", new { id = model.selEmailTemplate.EMAIL_TEMPLATE_ID });
        }

        //************************************ SYS LOG  ***************************************
        [Authorize(Roles = "PortalAdmin")]
        public ActionResult SysLog()
        {
            var model = new SysLogViewModel
            {
                T_PRT_SYS_LOGs = _DbPortal.GetT_PRT_SYS_LOG()
            };
            return View(model);
        }


        //************************************ EMAIL LOG  ***************************************
        [Authorize(Roles = "PortalAdmin")]
        public ActionResult EmailLog()
        {
            var model = new EmailLogViewModel
            {
                T_PRT_SYS_EMAIL_LOGs = _DbPortal.GetT_PRT_SYS_EMAIL_LOG()
            };
            return View(model);
        }



    }

}