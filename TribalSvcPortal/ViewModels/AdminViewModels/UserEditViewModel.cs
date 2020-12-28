using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using TribalSvcPortal.AppLogic.DataAccessLayer;
using TribalSvcPortal.Data.Models;

namespace TribalSvcPortal.ViewModels.AdminViewModels
{
    public class UserEditViewModel
    {
        public ApplicationUser appUser { get; set; }
        public IEnumerable<SelectListItem> UserRoles { get; set; }
        public IEnumerable<SelectListItem> RoleNotInUser { get; set; }
        public IEnumerable<string> Users_Role_Selected { get; set; }
        public IEnumerable<string> Role_Not_In_User_Selected { get; set; }
        public List<UserOrgDisplayType> UserOrgs { get; set; }
        public IEnumerable<SelectListItem> ddl_Orgs { get; set; }
        public int? edit_oRG_USER_IDX { get; set; }
        public string ForgotPasswordURL { get; set; }
    }


}
