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
using TribalSvcPortal.Models.AdminViewModels;
using TribalSvcPortal.AppLogic.DataAccessLayer;

namespace TribalSvcPortal.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IDbPortal _DbPortal;

        public AdminController(IDbPortal DbPortal)
        {
            _DbPortal = DbPortal;
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


        //[Authorize(Roles = "PortalAdmin")]
        //public async Task<IActionResult> UserEdit(string id)
        //{
        //    //roles the user is assigned 
        //    var RolesInUser = from itemA in _IDcontext.Roles
        //                      join itemB in _IDcontext.UserRoles on itemA.Id equals itemB.RoleId
        //                      where itemB.UserId == id
        //                      select itemA;

        //    var allRoles = _roleManager.Roles.ToList();  //  get all roles 

        //    var model = new UserEditViewModel
        //    {
        //        appUser = await _userManager.FindByIdAsync(id),
        //        UserRoles = RolesInUser.Select(x => new SelectListItem
        //        {
        //            Value = x.Name,
        //            Text = x.Name
        //        }),
        //        RoleNotInUser = allRoles.Except(RolesInUser).OrderBy(a => a.Name).Select(x => new SelectListItem
        //        {
        //            Value = x.Name,
        //            Text = x.Name
        //        }),
        //        UserTenants = _DbPortal.GetT_PRT_TENANT_USERS_ByUserID(id),
        //        ddl_Tenants = _DbPortal.GetT_PRT_TENANTS().Select(x => new SelectListItem
        //        {
        //            Value = x.TenantId.ToString(),
        //            Text = x.TenantName
        //        })
        //    };

        //    var users = await _userManager.FindByIdAsync(id);
        //    return View(model);
        //}


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

                    if (SuccID.Succeeded)
                        TempData["Success"] = "Update successful.";

                }
                // REMOVE USER FROM ROLE
                else if (submitButton == "Remove")
                {
                    foreach (string r in model.Users_Role_Selected)
                        SuccID = await _userManager.RemoveFromRoleAsync(_user, r);

                    if (SuccID.Succeeded)
                        TempData["Success"] = "Update successful.";
                }
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


        //******************************* TENANT USERS **********************************************************
        [HttpPost]
        public IActionResult TenantUserEdit(string uidx, string tenant_id, string AdminInd, string StatusInd)
        {
            if (ModelState.IsValid)
            {
                int newID = _DbPortal.InsertUpdateT_PRT_TENANT_USERS(null, tenant_id, uidx, (AdminInd == "A" ? true : false), StatusInd, User.Identity.Name);

                if (newID == 0)
                    TempData["Error"] = "Error adding user";
                else
                    TempData["Success"] = "Record successfully added";
            }
            else
                TempData["Error"] = "Error adding user";

            return RedirectToAction("UserEdit", "Admin", new { id = uidx });
        }

        [HttpPost]
        public IActionResult TenantUserDelete(int id, string id2)
        {
            int SuccID = _DbPortal.DeleteT_PRT_TENANT_USERS(id);
            if (SuccID > 0)
                TempData["Success"] = "Record has been deleted.";
            else
                TempData["Error"] = "Unable to delete tenant user.";

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
        public ActionResult ClientEdit(TPrtClients model)
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



        //******************************* TENANTS **********************************************************
        public IActionResult TenantList()
        {
            var model = _DbPortal.GetT_PRT_TENANTS();
            return View(model);
        }


        public IActionResult TenantEdit()
        {
            return View();
        }




        //************************************ TENANT USER CLIENT  ***************************************
        public IActionResult TenantUserClients(int id)
        {
            var model = new TenantUserEditViewModel
            {
                UserIDX = _DbPortal.GetT_PRT_TENANT_USERS_ByTenantUserID(id).Id,
                TenantUserIDX = id,
                TenantUserClients = _DbPortal.GetT_PRT_TENANT_USERS_CLIENT_ByTenantUserID(id),
                ddl_Clients = _DbPortal.GetT_PRT_CLIENTS().Select(x => new SelectListItem
                {
                    Value = x.ClientId,
                    Text = x.ClientName
                })
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult TenantUserClientEdit(int tenant_user_idx, string client_id, string AdminInd, string StatusInd)
        {
            if (ModelState.IsValid)
            {
                int newID = _DbPortal.InsertUpdateT_PRT_TENANT_USERS_CLIENT(null, tenant_user_idx, client_id, (AdminInd == "1" ? true : false), StatusInd, User.Identity.Name);

                if (newID == 0)
                    TempData["Error"] = "Error adding user";
                else
                    TempData["Success"] = "Record successfully added";
            }
            else
                TempData["Error"] = "Error adding user";

            return RedirectToAction("TenantUserClients", "Admin", new { id = tenant_user_idx });
        }

        [HttpPost]
        public IActionResult TenantUserClientDelete(int id, string id2)
        {
            int SuccID = _DbPortal.DeleteT_PRT_TENANT_USER_CLIENT(id);
            if (SuccID > 0)
                TempData["Success"] = "Record has been deleted.";
            else
                TempData["Error"] = "Unable to delete tenant user.";

            return RedirectToAction("TenantUserClients", new { id = id2 });
        }



    }






}