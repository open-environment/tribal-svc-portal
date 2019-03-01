using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TribalSvcPortal.AppLogic.DataAccessLayer;

namespace TribalSvcPortal.ViewModels.OpenDumpViewModels
{
    public class ImportViewModel
    {
        public IEnumerable<SelectListItem> ddl_Org { get; set; }

        [Required]
        [Display(Name = "Organization")]
        public string selOrg { get; set; }

        public string IMPORT_BLOCK { get; set; }  //raw text imported

        public List<SiteImportType> sites { get; set; }   //in-memory storage of array of sites to import
    }
}
