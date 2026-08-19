using System;
using System.Collections.Generic;

namespace TribalSvcPortal.Data.Models
{
    public partial class T_PRT_USERS
    {
        public T_PRT_USERS()
        {
            T_PRT_ORG_USERS = new HashSet<T_PRT_ORG_USERS>();
            T_PRT_USER_CLAIMS = new HashSet<T_PRT_USER_CLAIMS>();
            T_PRT_USER_LOGINS = new HashSet<T_PRT_USER_LOGINS>();
            Role = new HashSet<T_PRT_ROLES>();
        }

        public string Id { get; set; }
        public int AccessFailedCount { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool LockoutEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public string NormalizedEmail { get; set; }
        public string NormalizedUserName { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public string SecurityStamp { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public string UserName { get; set; }
        public string FIRST_NAME { get; set; }
        public string LAST_NAME { get; set; }
        public DateTime? LAST_LOGIN_DT { get; set; }
        public string PasswordEncrypt { get; set; }
        public int? WordPressUserId { get; set; }

        public virtual ICollection<T_PRT_ORG_USERS> T_PRT_ORG_USERS { get; set; }
        public virtual ICollection<T_PRT_USER_CLAIMS> T_PRT_USER_CLAIMS { get; set; }
        public virtual ICollection<T_PRT_USER_LOGINS> T_PRT_USER_LOGINS { get; set; }

        public virtual ICollection<T_PRT_ROLES> Role { get; set; }
    }
}
