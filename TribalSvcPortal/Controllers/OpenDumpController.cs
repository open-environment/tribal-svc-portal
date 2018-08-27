using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TribalSvcPortal.Controllers
{
    public class OpenDumpController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Search()
        {
            return View();
        }


        public IActionResult RefData()
        {
            return View();
        }
    }
}