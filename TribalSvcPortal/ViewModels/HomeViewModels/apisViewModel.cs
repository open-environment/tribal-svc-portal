using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TribalSvcPortal.Data.Models;

namespace TribalSvcPortal.ViewModels.HomeViewModels
{
    public class apisViewModel
    {
        public List<SelectListItem> ddl_apis { get; set; }
        [Required]
        [Display(Name = "API")]
        public string sel_api { get; set; }

        public List<SelectListItem> ddl_Orgs{ get; set; }
        public string selOrg { get; set; }

        public List<SelectListItem> ddl_Status { get; set; }
        public string selStatus { get; set; }

        public List<SelectListItem> ddl_County { get; set; }
        public string selCounty { get; set; }

        public List<SelectListItem> ddl_Score { get; set; }
        public string selScore { get; set; }

        public List<SelectListItem> ddl_Format { get; set; }
        [Required]
        [Display(Name = "Data Format")]
        public string selFormat { get; set; }

    }
}
