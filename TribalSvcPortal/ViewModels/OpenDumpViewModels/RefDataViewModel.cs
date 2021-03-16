using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TribalSvcPortal.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TribalSvcPortal.ViewModels.OpenDumpViewModels
{
    public class RefDataViewModel
    {
        public IEnumerable<SelectListItem> ddl_ref_cats { get; set; }
        public string sel_ref_cat { get; set; }
        public List<T_OD_REF_DATA> TOdRefData { get; set; }
        public Guid? edit_tag_idx { get; set; }
        public string edit_tag { get; set; }
        public string edit_tag_desc { get; set; }

    }
}
