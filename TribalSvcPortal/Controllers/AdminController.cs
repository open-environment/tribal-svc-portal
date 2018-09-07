using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using TribalSvcPortal.Data.Models;
using TribalSvcPortal.ViewModels;
using TribalSvcPortal.ViewModels.AdminViewModels;
using TribalSvcPortal.AppLogic.DataAccessLayer;
using Microsoft.Extensions.Caching.Memory;

namespace TribalSvcPortal.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IDbPortal _DbPortal;
        private readonly IMemoryCache _memoryCache;
        public AdminController(
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
            return View();
        }



        //******************************* ROLES **********************************************************
        public IActionResult RoleList()
        {
            var roles = _roleManager.Roles.ToList();
            return View(roles);
        }


        [Authorize(Roles = "PortalAdmin")]
        public IActionResult RoleEdit()
        {
            return View();
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
                    Value = x.OrgId.ToString(),
                    Text = x.OrgName
                })
            };

            var users = await _userManager.FindByIdAsync(id);
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
                    SuccID = await _userManager.UpdateAsync(_user);
                }

                if (SuccID.Succeeded)
                    TempData["Success"] = "Update successful.";
            }

            return RedirectToAction("UserEdit", new { id = model.appUser.Id });
        }


        [HttpPost]
        public async Task<IActionResult> UserDelete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                var IdentityResult = await _userManager.DeleteAsync(user);
                return View(IdentityResult);
            }
            else
                return null;
        }


        //******************************* ORG USERS **********************************************************
        [HttpPost]
        public IActionResult OrgUserEdit(string uidx, string org_id, string AdminInd, string StatusInd)
        {
            if (ModelState.IsValid)
            {
                int newID = _DbPortal.InsertUpdateT_PRT_ORG_USERS(null, org_id, uidx, (AdminInd == "A" ? true : false), StatusInd, User.Identity.Name);

                if (newID == 0)
                    TempData["Error"] = "Unable to add user to organization.";
                else
                    TempData["Success"] = "Record successfully added.";
            }
            else
                TempData["Error"] = "Unable to add user to organization.";

            return RedirectToAction("UserEdit", "Admin", new { id = uidx });
        }

        [HttpPost]
        public IActionResult OrgUserDelete(int id, string id2)
        {
            int SuccID = _DbPortal.DeleteT_PRT_ORG_USERS(id);
            if (SuccID > 0)
                TempData["Success"] = "Record has been deleted.";
            else
                TempData["Error"] = "Unable to delete user from organization.";

            return RedirectToAction("UserEdit", new { id = id2 });
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

                string ClientID = _DbPortal.InsertUpdateT_PRT_CLIENTS(model.ClientId, model.ClientName, model.ClientGrantType, model.ClientRedirectUri, model.ClientPostLogoutUri, model.ClientUrl);

                if (ClientID != null)
                    TempData["Success"] = "Update successful.";
                else
                    TempData["Error"] = "Error adding client";

            }

            return RedirectToAction("ClientEdit", new { id = model.ClientId });
        }



        //******************************* ORGANIZATIONS **********************************************************
        public IActionResult OrgList()
        {
            var model = _DbPortal.GetT_PRT_ORGANIZATIONS();
            return View(model);
        }


        public IActionResult OrgEdit()
        {
            return View();
        }




        //************************************ ORGANIZATION USER CLIENT  ***************************************
        public IActionResult OrgUserClients(int id)
        {
            var model = new OrgUserEditViewModel
            {
                UserIDX = _DbPortal.GetT_PRT_ORG_USERS_ByOrgUserID(id).Id,
                OrgUserIDX = id,
                OrgUserClients = _DbPortal.GetT_PRT_ORG_USERS_CLIENT_ByOrgUserID(id),
                ddl_Clients = _DbPortal.GetT_PRT_CLIENTS().Select(x => new SelectListItem
                {
                    Value = x.ClientId,
                    Text = x.ClientName
                })
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult OrgUserClientEdit(int org_user_idx, string client_id, string AdminInd, string StatusInd)
        {
            if (ModelState.IsValid)
            {
                int newID = _DbPortal.InsertUpdateT_PRT_ORG_USERS_CLIENT(null, org_user_idx, client_id, (AdminInd == "1" ? true : false), StatusInd, User.Identity.Name);

                if (newID == 0)
                    TempData["Error"] = "Error adding user";
                else
                    TempData["Success"] = "Record successfully added";
            }
            else
                TempData["Error"] = "Error adding user";

            return RedirectToAction("OrgUserClients", "Admin", new { id = org_user_idx });
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



        //************************************ SETTINGS ***************************************
        public ActionResult Settings()
        {
            //T_OE_APP_SETTINGS_CUSTOM custSettings = _DbPortal.g db_Ref.GetT_OE_APP_SETTING_CUSTOM();
            IEnumerable<T_PRT_CLIENTS> UserClientDisplayType;
            string _UserIDX;
            bool isUserExist = _memoryCache.TryGetValue("UserID", out _UserIDX);
            if (isUserExist)
            {
                string CacheKey = "UserMenuData" + _UserIDX;

                bool isExist = _memoryCache.TryGetValue(CacheKey, out UserClientDisplayType);
                if (isExist && UserClientDisplayType != null)
                {
                    ViewBag.UserMenuAccess = UserClientDisplayType;
                }
            }
            var model = new SettingsViewModel
            {                
            app_settings = _DbPortal.GetT_PRT_APP_SETTING_List(),
                //TermsAndConditions = custSettings.TERMS_AND_CONDITIONS,
                //Announcements = custSettings.ANNOUNCEMENTS
            };
            return View(model);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public ActionResult Settings(SettingsViewModel model)
    {
        if (ModelState.IsValid)
        {
            string UserID = _userManager.GetUserId(User);

            int SuccID = _DbPortal.InsertUpdateT_PRT_APP_SETTING(model.edit_app_setting.SettingIdx, model.edit_app_setting.SettingName, model.edit_app_setting.SettingValue, false, null, UserID);
            if (SuccID > 0)
                TempData["Success"] = "Data Saved.";
            else
                TempData["Error"] = "Data Not Saved.";
        }

        return RedirectToAction("Settings");
    }

    //[HttpPost, ValidateAntiForgeryToken]
    //public ActionResult CustomSettings(SettingsViewModel model)
    //{
    //    if (ModelState.IsValid)
    //    {
    //        int SuccID = _DbPortal.InsertUpdateT_PRT_APP_SETTING_CUSTOM(model.TermsAndConditions, null);
    //        if (SuccID > 0)
    //            TempData["Success"] = "Data Saved.";
    //        else
    //            TempData["Error"] = "Data Not Saved.";
    //    }

    //    return RedirectToAction("Settings");
    //}

    //[HttpPost, ValidateAntiForgeryToken]
    //public ActionResult CustomSettingsAnnounce(SettingsViewModel model)
    //{
    //    if (ModelState.IsValid)
    //    {
    //        int SuccID = _DbPortal.InsertUpdateT_PRT_APP_SETTING_CUSTOM(null, model.Announcements ?? "");
    //        if (SuccID > 0)
    //            TempData["Success"] = "Data Saved.";
    //        else
    //            TempData["Error"] = "Data Not Saved.";
    //    }

    //    return RedirectToAction("Settings");
    //}

}






}