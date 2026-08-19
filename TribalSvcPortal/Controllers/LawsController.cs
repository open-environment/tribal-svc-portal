using ClosedXML.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Xml;
using TribalSvcPortal.AppLogic.BusinessLogicLayer;
using TribalSvcPortal.AppLogic.DataAccessLayer;
using TribalSvcPortal.Data.Models;
using TribalSvcPortal.ViewModels.OpenDumpViewModels;

namespace TribalSvcPortal.Controllers
{
    public class LawsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IDbPortal _DbPortal;
        private readonly IHostingEnvironment _hostingEnvironment;

        public LawsController(
            UserManager<ApplicationUser> userManager,
            IDbPortal DbPortal,
            IHostingEnvironment hostingEnvironment)
        {
            _userManager = userManager;
            _DbPortal = DbPortal;
            _hostingEnvironment = hostingEnvironment;
        }


        //public async Task<FileResult> GetFile()
        //{
        //    using (var client = new HttpClient())
        //    {
        //        var response = await client.GetAsync("https://www.govinfo.gov/content/pkg/CFR-2021-title2-vol1/pdf/CFR-2021-title2-vol1-sec1400-1011.pdf");
        //        var contentStream = await response.Content.ReadAsStreamAsync();

        //        return this.File(contentStream, "application/octet-stream", "MyFile.pdf");
        //    }
        //}



        public ActionResult Index(string u)
        {
            return View();

            //************* EPA PROGRAMS
            //e.g. National Emissions Inventory (NEI)
            //e.g. "National Environmental Education Act (NEEA)"

            //************* EPA ORGANIZATIONS
            //e.g. "National Exposure Research Laboratory (NERL)" 4088834
            //e.g. "Air Enforcement Division (AED)" 4088840

            //************* CFR CITATIONS  //search CFR by citation (e.g. 40 CFR 27.30) or title (e.g. "The DOI Debarment Program Director."
            //e.g. 
            //      "id": 6812947,
            //      "name": "CFR-2021-title2-vol1-sec1400-1011",
            //      "citation": "2 CFR 1400.1011",
            //      "title": "The DOI Debarment Program Director."


            //******************  CFR API //return details of a CFR entry
            //          {
            //              "objectName": "CFR-2021-title2-vol1-sec1400-1011",
            //"objectId": 6812947,
            //"objectAcronym": null,
            //"attributes": {
            //                  "AccessID": "CFR-2021-title2-vol1-sec1400-1011",
            //  "CFR Citation Text": "81 FR 65857, Sept. 26, 2016",
            //  "Citation": "2 CFR 1400.1011",
            //  "FR Citation": "81 FR 65857",
            //  "Granule Label": "section",
            //  "Heading": "Section § 1400.1011",
            //  "Publication Date": "2021-01-01",
            //  "Title": "The DOI Debarment Program Director.",
            //  "URL": "https://www.govinfo.gov/content/pkg/CFR-2021-title2-vol1/pdf/CFR-2021-title2-vol1-sec1400-1011.pdf"
            //},



        }



        public ActionResult Widget() {
            return View();

        }
    }
}
