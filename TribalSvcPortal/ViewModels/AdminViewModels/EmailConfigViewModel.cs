using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using TribalSvcPortal.Data.Models;

namespace TribalSvcPortal.ViewModels.AdminViewModels
{
    public class EmailConfigViewModel
    {
        public List<SelectListItem> ddl_EmailTemplate { get; set; }
        public int? selTemplate { get; set; }
        public T_PRT_REF_EMAIL_TEMPLATE selEmailTemplate { get; set; }
    }
}
