using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TribalSvcPortal.AppLogic.DataAccessLayer;

namespace TribalSvcPortal.Data.Models
{
    public class JWTLoginModel
    {
        public string UserId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public bool rememberMe { get; set; }
        public bool isLoggedIn { get; set; }
        public string errMsg { get; set; }
        public List<string> roles { get; set; }
        public bool isLockedOut { get; set; }
        public int? openWaterUserIdx { get; set; }
        public bool isAdmin { get; set; }
        public List<UserOrgDisplayType> orgUsers { get; set; }

        public JWTLoginModel()
        {
            if (orgUsers == null) orgUsers = new List<UserOrgDisplayType>();
        }
    }
}
