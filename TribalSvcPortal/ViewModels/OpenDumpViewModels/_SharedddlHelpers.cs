using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TribalSvcPortal.AppLogic;
using TribalSvcPortal.AppLogic.DataAccessLayer;

namespace TribalSvcPortal.ViewModels.OpenDumpViewModels
{
    public class _SharedddlHelpers
    {
        private readonly IDbOpenDump _DbOpenDump;
        public _SharedddlHelpers(IDbOpenDump DbOpenDump)
        {
            _DbOpenDump = DbOpenDump;
        }

        //public static IEnumerable<SelectListItem> get_ddl_refdata_by_category(string cat_name)
        //{
        //    return DbOpenDump.get_ddl_refdata_by_category(cat_name).Select(x => new SelectListItem
        //    {
        //        Value = x.TAG_IDX.ToString(),
        //        Text = x.TAG_NAME
        //    });
        //}

    }
}
