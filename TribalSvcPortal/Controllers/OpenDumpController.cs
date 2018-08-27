using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using TribalSvcPortal.Data.Models;
using TribalSvcPortal.ViewModels.OpenDumpViewModels;
using TribalSvcPortal.AppLogic.DataAccessLayer;

namespace TribalSvcPortal.Controllers
{
    public class OpenDumpController : Controller
    {
        private readonly IDbPortal _DbPortal;
        private readonly IDbOpenDump _DbOpenDump;

        public OpenDumpController(
            IDbPortal DbPortal,
            IDbOpenDump DbOpenDump)
        {
            _DbPortal = DbPortal;
            _DbOpenDump = DbOpenDump;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Search()
        {
            var model = new SearchViewModel
            {
            };

            return View(model);
        }


        public IActionResult RefData()
        {
            return View();
        }
    }
}