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
        public List<T_OD_REF_DATA> TOdRefData { get; set; }
        public IEnumerable<SelectListItem> ddl_tag_cats { get; set; }
        public string sel_tag_cat { get; set; }
        public string sel_tag_cat_desc { get; set; }
        public int? edit_tag_idx { get; set; }
        public string edit_tag { get; set; }
        public string edit_tag_desc { get; set; }
        public bool edit_promote_ind { get; set; }


        //INITIALIZE
        public RefDataViewModel()
        {
            //ddl_tag_cats = ddlHelpers.get_ddl_tag_cats();
        }

    }
}
