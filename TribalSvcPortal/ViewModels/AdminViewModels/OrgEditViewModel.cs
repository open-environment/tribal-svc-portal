using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TribalSvcPortal.Data.Models;

namespace TribalSvcPortal.ViewModels.AdminViewModels
{
    public class OrgEditViewModel
    {
        public T_PRT_ORGANIZATIONS Organization { get; set; }
        public List<T_PRT_ORG_CLIENT_ALIAS> OrgClientAlias { get; set; }
        public List<T_PRT_ORG_EMAIL_RULE> OrgEmails { get; set; }
        public string new_email { get; set; }

    }
}
