using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TribalSvcPortal.AppLogic;

namespace TribalSvcPortal.ViewModels.OpenDumpViewModels
{
    public static class _SharedddlHelpers
    {
        public static IEnumerable<SelectListItem> get_ddl_refdata_by_category(string cat_name)
        {
            return null;
            //return db_Ref.GetT_OE_REF_TAGS_ByCategory(cat_name).Select(x => new SelectListItem
            //{
            //    Value = x.TAG_IDX.ToString(),
            //    Text = x.TAG_NAME
            //});
        }

    }
}
