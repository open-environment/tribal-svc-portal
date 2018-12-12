using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using TribalSvcPortal.Data.Models;

namespace TribalSvcPortal.ViewModels.AdminViewModels
{
    public class RoleEditViewModel
    {
        public IdentityRole T_PRT_ROLES { get; set; }
        public IEnumerable<SelectListItem> Users_In_Role { get; set; }
        public IEnumerable<string> Users_In_Role_Selected { get; set; }
        public IEnumerable<SelectListItem> Users_Not_In_Role { get; set; }
        public IEnumerable<string> Users_Not_In_Role_Selected { get; set; }
    }
}
