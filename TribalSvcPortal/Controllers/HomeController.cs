using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TribalSvcPortal.AppLogic.DataAccessLayer;
using TribalSvcPortal.ViewModels;

namespace TribalSvcPortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDbPortal _DbPortal;
        public HomeController(IDbPortal DbPortal)
        {
            _DbPortal = DbPortal;
        }

        public IActionResult Index()
        {
            _DbPortal.GetT_OE_SYS_LOG();

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
