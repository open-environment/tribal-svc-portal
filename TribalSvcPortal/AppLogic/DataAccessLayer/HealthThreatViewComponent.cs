using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TribalSvcPortal.Data.Models;
using TribalSvcPortal.ViewModels.OpenDumpViewModels;

namespace TribalSvcPortal.AppLogic.DataAccessLayer
{
    public class HealthThreatViewComponent : ViewComponent
    {
        private readonly IDbPortal _DbPortal;
        private readonly IDbOpenDump _DbOpenDump;
        private readonly IMemoryCache _memoryCache;
        public HealthThreatViewComponent(
           IDbPortal DbPortal,
           IMemoryCache memoryCache,
            IDbOpenDump DbOpenDump
           )
        {
            _memoryCache = memoryCache;
            _DbPortal = DbPortal;
            _DbOpenDump = DbOpenDump;
        }
        public IViewComponentResult Invoke(Guid? SiteIdx, string returnURL, Guid? AssessmentIdx, bool CreateAssessment, OpenDumpTab activeTab = OpenDumpTab.Prefield)
        {
            return View();
        }
    }
}
