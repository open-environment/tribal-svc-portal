using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace TribalSvcPortal.Data.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string FIRST_NAME { get; set; }
        public string LAST_NAME { get; set; }
        public DateTime? LAST_LOGIN_DT { get; set; }
        public string PasswordEncrypt { get; set; }
        public int? WordPressUserId { get; set; }
        public int? OpenWaterUserIDX { get; set; }
    }

}
