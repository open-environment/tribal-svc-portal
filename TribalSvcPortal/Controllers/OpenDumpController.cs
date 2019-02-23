using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using TribalSvcPortal.AppLogic.BusinessLogicLayer;
using TribalSvcPortal.AppLogic.DataAccessLayer;
using TribalSvcPortal.Data.Models;
using TribalSvcPortal.ViewModels.OpenDumpViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using iTextSharp.text.pdf;
using iTextSharp.text;
using ClosedXML.Excel;
using System.Data;
using Microsoft.AspNetCore.Http;

namespace TribalSvcPortal.Controllers
{
    public class RptDisplayType
    {
        public byte[] rptContent { get; set; }
        public string rptName { get; set; }
    }

    [Authorize]
    public class OpenDumpController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IDbPortal _DbPortal;
        private readonly IDbOpenDump _DbOpenDump;
        private readonly IHostingEnvironment _hostingEnvironment;

        public OpenDumpController(
            UserManager<ApplicationUser> userManager,
            IDbPortal DbPortal,
            IDbOpenDump DbOpenDump,
            IHostingEnvironment hostingEnvironment)
        {
            _userManager = userManager;
            _DbPortal = DbPortal;
            _DbOpenDump = DbOpenDump;
            _hostingEnvironment = hostingEnvironment;
        }


        #region SITE CONTROLLERS **********************************************************************
        public ActionResult Sites(string selStr, string selOrg, string selStatus, string tab)
        {
            string _UserIDX = _userManager.GetUserId(User);

            SitesViewModel model = new SitesViewModel
            {
                tab = tab ?? "1",
                ddl_Org = _DbPortal.get_ddl_T_PRT_ORG_USERS_CLIENT_ByUserIDandClient(_UserIDX, "open_dump"),
                ddl_Status = _DbOpenDump.get_ddl_T_OD_SITE_STATUS(),
                sites = _DbOpenDump.getT_OD_SITES_ListBySearch(_UserIDX, selStr, selOrg, selStatus)
            };

            model.selOrg = selOrg ?? (model.ddl_Org.Count() == 1 ? model.ddl_Org.ToList()[0].Value : null);
            return View(model);
        }

        [HttpPost]
        public JsonResult SiteDelete(Guid id)
        {
            if (id != null)
            {
                int SuccID = _DbPortal.DeleteT_PRT_SITES(id);

                if (SuccID == 1)
                    return Json("Success");
            }

            return Json("Unable to delete");
        }

        public FileResult SiteExport(string selStr, string selOrg, string selStatus) {

            string _UserIDX = _userManager.GetUserId(User);

            //formatting the data table
            DataTable dt = new DataTable("GridView_Data");

            dt.Columns.AddRange(new DataColumn[9] {
                new DataColumn("ID"),
                new DataColumn("Organization"),
                new DataColumn("Site Name"),
                new DataColumn("Address"),
                new DataColumn("Reported"),
                new DataColumn("Status"),
                new DataColumn("Last Assessed"),
                new DataColumn("Health Threat"),
                new DataColumn("Cleanup Estimate")
            });

            List<OpenDumpSiteListDisplay> _sites = _DbOpenDump.getT_OD_SITES_ListBySearch(_UserIDX, selStr, selOrg, selStatus);
            foreach (OpenDumpSiteListDisplay _site in _sites)
            {
                dt.Rows.Add(_site.SiteIdx, _site.OrgName, _site.SiteName, _site.SiteAddress, _site.ReportedOn, _site.CurrentSiteStatus, _site.LastAssessment?.ASSESSMENT_DT, _site.HEALTH_THREAT_SCORE, _site.LatestCleanupProject?.COST_TOTAL_AMT);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                //add data table to worksheet
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Sites.xlsx");
                }
            }

        }

        #endregion


        #region PREFIELD TAB CONTROLLERS **********************************************************************
        // GET: /OpenDump/PreField
        public ActionResult PreField(Guid? id, string returnURL)
        {
            string _UserIDX = _userManager.GetUserId(User);

            //TODO add security

            var model = new PreFieldViewModel {
                SiteSettingsList = _DbOpenDump.get_ddl_T_OD_REF_DATA_by_category("Site Setting", null),
                AquiferList = _DbOpenDump.get_ddl_OD_REF_THREAT_FACTORS_by_type("Aquifer"),
                SurfaceWaterList = _DbOpenDump.get_ddl_OD_REF_THREAT_FACTORS_by_type("Surface Water"),
                HomesList = _DbOpenDump.get_ddl_OD_REF_THREAT_FACTORS_by_type("Homes"),
                OrgList = _DbPortal.get_ddl_T_PRT_ORG_USERS_CLIENT_ByUserIDandClient(_UserIDX, "open_dump"),
                ddl_LandStatus = _DbPortal.get_ddl_T_PRT_LAND_STATUS(),
                returnURL = returnURL ?? "Sites"
            };

            //update case
            if (id != null) {
                model.TPrtSite = _DbPortal.GetT_PRT_SITES_BySITEIDX((Guid)id);
                model.TOdSite = _DbOpenDump.getT_OD_SITES_BySITEIDX((Guid)id);

                model.CommunityList = _DbOpenDump.get_ddl_T_OD_REF_DATA_by_category("Community", model.TPrtSite.ORG_ID);

                //fail if site ID provided but not found
                if (model.TOdSite == null)
                {
                    TempData["Error"] = "Site not found.";
                    return RedirectToAction("Sites", "OpenDump");
                }
            }
            else //insert case
            {
                model.TPrtSite = new T_PRT_SITES();
                model.TOdSite = new T_OD_SITES();

                //if one org, then prepopulate it in the list
                if (model.OrgList.Count() == 1)
                    model.TPrtSite.ORG_ID = model.OrgList.First().Value;

                //community dropdown list
                if (model.OrgList.Count() == 1)
                    model.CommunityList = _DbOpenDump.get_ddl_T_OD_REF_DATA_by_category("Community", model.OrgList.ToList()[0].Value);
                else
                    model.CommunityList = Enumerable.Empty<SelectListItem>();
            }

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult PreField(PreFieldViewModel model)
        {
            string _UserIDX = _userManager.GetUserId(User);

            Guid? newSiteID = _DbPortal.InsertUpdateT_PRT_SITES(model.TPrtSite.SITE_IDX, model.TPrtSite.ORG_ID, model.TPrtSite.SITE_NAME ?? "",
                    model.TPrtSite.EPA_ID ?? "", model.TPrtSite.LATITUDE, model.TPrtSite.LONGITUDE, model.TPrtSite.SITE_ADDRESS ?? "", _UserIDX, model.TPrtSite.LAND_STATUS);

            if (newSiteID != null)
            {
                newSiteID = _DbOpenDump.InsertUpdateT_OD_SITES((Guid)newSiteID, model.TOdSite.REPORTED_BY, model.TOdSite.REPORTED_ON, model.TOdSite.COMMUNITY_IDX, model.TOdSite.SITE_SETTING_IDX,
                    model.TOdSite.PF_AQUIFER_VERT_DIST, model.TOdSite.PF_SURF_WATER_HORIZ_DIST, model.TOdSite.PF_HOMES_DIST, null);

                if (newSiteID != null)
                {
                    TempData["Success"] = "Update successful.";
                    return RedirectToAction("PreField", "OpenDump", new { id = newSiteID, returnURL = model.returnURL });
                }
            }

            //error if got this far
            TempData["Error"] = "Error updating data.";
            return RedirectToAction("PreField", "OpenDump", new { id = newSiteID, returnURL = model.returnURL });
        }

        [HttpGet]
        public JsonResult GetCommunityDDL(string orgID)
        {
            IEnumerable<SelectListItem> refdata = _DbOpenDump.get_ddl_T_OD_REF_DATA_by_category("Community", orgID);

            if (refdata !=  null && refdata.Count() > 0)
                return Json(new { data = refdata });
            else
                return Json(new { data = "" });

        }
        #endregion


        #region ASSESSMENT CONTROLLERS ************************************************************
        // GET: /OpenDump/Assessments
        [HttpGet]
        public ActionResult AssessmentList()
        {
            string _UserIDX = _userManager.GetUserId(User);

            var model = new AssessmentsViewModel
            {
                SiteIDX = null,
                SiteName = null,
                OrgName = null,
                Assessments = _DbOpenDump.getT_OD_ASSESSMENTS_ByUser(_UserIDX)
            };
            return View(model);

        }
               
        [HttpGet]
        public ActionResult Assessments(Guid? id)
        {
            T_PRT_SITES s = _DbPortal.GetT_PRT_SITES_BySITEIDX(id.ConvertOrDefault<Guid>());
            if (s != null)
            {
                T_PRT_ORGANIZATIONS o = _DbPortal.GetT_PRT_ORGANIZATIONS_ByOrgID(s.ORG_ID);
                if (o != null)
                {
                    var model = new AssessmentsViewModel
                    {
                        SiteIDX = id,
                        SiteName = s.SITE_NAME,
                        OrgName = o.ORG_NAME,
                        Assessments = _DbOpenDump.getT_OD_ASSESSMENTS_BySITEIDX(id.ConvertOrDefault<Guid>())
                    };
                    return View(model);
                }
            }

            //error if got this far
            TempData["Error"] = "Invalid site.";
            return RedirectToAction("Sites", "OpenDump");
        }

        [HttpPost]
        public JsonResult AssessmentDelete(Guid id)
        {
            if (id != null)
            {
                int SuccID = _DbOpenDump.deleteT_OD_Assessment(id);
                if (SuccID == 1)
                    return Json("Success");
            }

            return Json("Unable to delete");
        }

        public IActionResult IHSReport(Guid? id)
        {
            var _rpt = GenRpt(id.ConvertOrDefault<Guid>());
            byte[] rptData = _rpt.rptContent;

            if (rptData != null)
            {
                var content = new System.IO.MemoryStream(rptData);
                return File(rptData, "application/pdf", _rpt.rptName + ".pdf");
            }
            else
            {
                //if got this far, it failed
                TempData["Error"] = "Unable to generate document.";
                return RedirectToAction("Sites");
            }
        }
        
        public ActionResult AssessmentDetails(Guid? id, Guid? SiteIdx)
        {
            var model = new AssessmentDetailViewModel {
                ddl_AssessmentTypeList = _DbOpenDump.get_ddl_T_OD_REF_DATA_by_category("Assessment Type", null),
                ddl_SiteStatus = _DbOpenDump.get_ddl_T_OD_SITE_STATUS()
            };

            //update case
            if (id != null)
            {
                model.Assessment = _DbOpenDump.getT_OD_ASSESSMENTS_ByAssessmentIDX((Guid)id);
                model.files_existing = _DbOpenDump.GetT_PRT_DOCUMENTS_ByAssessmentIDX((Guid)id, "Open Dump-Assess File");
                model.filesPhoto_existing = _DbOpenDump.GetT_PRT_DOCUMENTS_ByAssessmentIDX((Guid)id, "Open Dump-Assess Photo");
            }
            //insert case
            else if (id == null && SiteIdx != null)
            {
                model.Assessment = new T_OD_ASSESSMENTS {                    
                    SITE_IDX = SiteIdx.ConvertOrDefault<Guid>(),
                    ASSESSMENT_DT = System.DateTime.Today
                };
            }

            //populate site name and org name
            T_PRT_SITES s = _DbPortal.GetT_PRT_SITES_BySITEIDX(model.Assessment.SITE_IDX);
            if (s != null)
            {
                T_PRT_ORGANIZATIONS o = _DbPortal.GetT_PRT_ORGANIZATIONS_ByOrgID(s.ORG_ID);
                if (o != null)
                {
                    model.SiteName = s.SITE_NAME;
                    model.OrgName = o.ORG_NAME;
                }
            }

            //populate list of assessments for dropdown
            model.ddl_Assessments = _DbOpenDump.get_ddl_T_OD_ASSESSMENTS_by_BySITEIDX(model.Assessment.SITE_IDX);

            //fail if no assessment found or created
            if (model.Assessment == null)
            {
                TempData["Error"] = "Assessment not found.";
                return RedirectToAction("Sites", "OpenDump");
            }

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult AssessmentDetails(AssessmentDetailViewModel model)
        {
            if (model != null && model.Assessment != null && model.Assessment.SITE_IDX != null)
            {
                var a = model.Assessment;
                string _UserIDX = _userManager.GetUserId(User);

                //security check
                //_DbOpenDump.GetT_OD_SITES_BySITEIDX

                Guid? SuccIDX = _DbOpenDump.InsertUpdateT_OD_ASSESSMENTS(a.ASSESSMENT_IDX, a.SITE_IDX, a.ASSESSMENT_DT, a.ASSESSED_BY, a.ASSESSMENT_TYPE_IDX, 
                    a.CURRENT_SITE_STATUS, a.SITE_DESCRIPTION, a.ASSESSMENT_NOTES, null, null, null, null, null,null, null, null, null, null, a.CLEANED_CLOSED_DT);

                if (SuccIDX != null) {

                    //foreach (T_PRT_DOCUMENTS docs in model.filesPhoto_existing ?? Enumerable.Empty<T_PRT_DOCUMENTS>())
                    //    _DbPortal.InsertUpdateT_PRT_DOCUMENTS(docs.DOC_IDX, docs.ORG_ID, null, null, "Assessment", null, null, docs.DOC_COMMENT, null, null, null, _UserIDX);
                    //foreach (T_PRT_DOCUMENTS docs in model.files_existing ?? Enumerable.Empty<T_PRT_DOCUMENTS>())
                    //    _DbPortal.InsertUpdateT_PRT_DOCUMENTS(docs.DOC_IDX, docs.ORG_ID, null, null, "Assessment", null, null, docs.DOC_COMMENT, null, null, null, _UserIDX);

                    TempData["Success"] = "Update successful.";
                    return RedirectToAction("AssessmentDetails", "OpenDump", new { id = SuccIDX });
                }

            }


            TempData["Error"] = "Error updating data.";
            return RedirectToAction("Sites");
        }

        #endregion


        #region WASTE PROFILE TAB CONTROLLERS **********************************************************************
        public ActionResult WasteProfile(Guid? id)
        {
            //id is Dump Assessment ID

            var model = new WasteProfileViewModel
            {
                Assessment = _DbOpenDump.getT_OD_ASSESSMENTS_ByAssessmentIDX((Guid)id),
                ContentCheckBoxList = _DbOpenDump.getT_OD_ASSESSMENT_CONTENT_by_AssessIDX(id),
                AverageRainfallList = _DbOpenDump.get_ddl_OD_REF_THREAT_FACTORS_by_type("Rainfall"),
                BurningList = _DbOpenDump.get_ddl_OD_REF_THREAT_FACTORS_by_type("Burning"),
                ConcernList = _DbOpenDump.get_ddl_OD_REF_THREAT_FACTORS_by_type("Concern"),
                DrainageList = _DbOpenDump.get_ddl_OD_REF_THREAT_FACTORS_by_type("Drainage"),
                FloodingList = _DbOpenDump.get_ddl_OD_REF_THREAT_FACTORS_by_type("Flooding"),
                FencedList = _DbOpenDump.get_ddl_OD_REF_THREAT_FACTORS_by_type("Fenced"),
                AccessList = _DbOpenDump.get_ddl_OD_REF_THREAT_FACTORS_by_type("Access"),
            };

            if (model.Assessment != null)
            {
                if (model.Assessment.HF_RAINFALL != null) model.RainfallSubScore = _DbOpenDump.getT_OD_REF_THREAT_FACTOR_ByID((Guid)model.Assessment.HF_RAINFALL).THREAT_FACTOR_SCORE;
                if (model.Assessment.HF_DRAINAGE != null) model.DrainageSubScore = _DbOpenDump.getT_OD_REF_THREAT_FACTOR_ByID((Guid)model.Assessment.HF_DRAINAGE).THREAT_FACTOR_SCORE;
                if (model.Assessment.HF_FLOODING != null) model.FloodingSubScore = _DbOpenDump.getT_OD_REF_THREAT_FACTOR_ByID((Guid)model.Assessment.HF_FLOODING).THREAT_FACTOR_SCORE;
                if (model.Assessment.HF_BURNING != null) model.BurningSubScore = _DbOpenDump.getT_OD_REF_THREAT_FACTOR_ByID((Guid)model.Assessment.HF_BURNING).THREAT_FACTOR_SCORE;
                if (model.Assessment.HF_FENCING != null) model.FencedSubScore = _DbOpenDump.getT_OD_REF_THREAT_FACTOR_ByID((Guid)model.Assessment.HF_FENCING).THREAT_FACTOR_SCORE;
                if (model.Assessment.HF_ACCESS_CONTROL != null) model.AccessSubScore = _DbOpenDump.getT_OD_REF_THREAT_FACTOR_ByID((Guid)model.Assessment.HF_ACCESS_CONTROL).THREAT_FACTOR_SCORE;
                if (model.Assessment.HF_PUBLIC_CONCERN != null) model.ConcernSubScore = _DbOpenDump.getT_OD_REF_THREAT_FACTOR_ByID((Guid)model.Assessment.HF_PUBLIC_CONCERN).THREAT_FACTOR_SCORE;

                model.ddl_Assessments = _DbOpenDump.get_ddl_T_OD_ASSESSMENTS_by_BySITEIDX(model.Assessment.SITE_IDX);

                //populate site name
                T_PRT_SITES s = _DbPortal.GetT_PRT_SITES_BySITEIDX(model.Assessment.SITE_IDX);
                if (s != null)
                {
                    model.SiteName = s.SITE_NAME;

                    //populate proximity subscore
                    T_OD_SITES od = _DbOpenDump.getT_OD_SITES_BySITEIDX(s.SITE_IDX);
                    int? Prox1 = (od.PF_AQUIFER_VERT_DIST != null ? _DbOpenDump.getT_OD_REF_THREAT_FACTOR_ByID((Guid)od.PF_AQUIFER_VERT_DIST).THREAT_FACTOR_SCORE : 0);
                    int? Prox2 = (od.PF_SURF_WATER_HORIZ_DIST != null ? _DbOpenDump.getT_OD_REF_THREAT_FACTOR_ByID((Guid)od.PF_SURF_WATER_HORIZ_DIST).THREAT_FACTOR_SCORE : 0);
                    int? Prox3 = (od.PF_HOMES_DIST != null ? _DbOpenDump.getT_OD_REF_THREAT_FACTOR_ByID((Guid)od.PF_HOMES_DIST).THREAT_FACTOR_SCORE : 0);
                    model.ProximityScore = (Prox1??0) + (Prox2??0) + (Prox3??0);
                }

                return View(model);
            }

            TempData["Error"] = "Unable to find assessment.";
            return RedirectToAction("Sites");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult WasteProfile(WasteProfileViewModel model)
        {
            if (model != null && model.Assessment != null)
            {
                var a = model.Assessment;
                Guid? DUMP_ASSESSMENTS_IDX = _DbOpenDump.InsertUpdateT_OD_ASSESSMENTS(a.ASSESSMENT_IDX, null, null, null, null, null, null, null, a.AREA_ACRES, 
                    a.VOLUME_CU_YD, a.HF_RAINFALL, a.HF_DRAINAGE, a.HF_FLOODING, a.HF_BURNING, a.HF_FENCING, a.HF_ACCESS_CONTROL, a.HF_PUBLIC_CONCERN, a.HEALTH_THREAT_SCORE, null);

                foreach (SelectedWasteTypeDisplay oNew in model.ContentCheckBoxList)
                {
                    _DbOpenDump.InsertUpdateT_OD_Assessment_Content(null, a.ASSESSMENT_IDX, oNew.T_OD_REF_WASTE_TYPE.REF_WASTE_TYPE_IDX, null, null, null, null, oNew.IsChecked);
                }
                TempData["Success"] = "Update successful.";
            }
            else
                TempData["Error"] = "Error updating data.";

            return RedirectToAction("WasteProfile", new { id = model.Assessment.ASSESSMENT_IDX });
        }

        [HttpPost]
        public JsonResult GetHealthThreatScore(Guid? threatFactor)
        {
            T_OD_REF_THREAT_FACTORS h = _DbOpenDump.getT_OD_REF_THREAT_FACTOR_ByID((Guid)threatFactor);
            if (h != null)
                return Json(new { msg = h.THREAT_FACTOR_SCORE.ToString() });
            else
                return Json(new { msg = "Error" });
        }

        #endregion


        #region WASTE CONTENT TAB CONTROLLERS **********************************************************************
        [HttpGet]
        public ActionResult WasteContent(Guid? id)
        {
            string _UserIDX = _userManager.GetUserId(User);

            var model = new WasteContentViewModel
            {
                Assessment = _DbOpenDump.getT_OD_ASSESSMENTS_ByAssessmentIDX((Guid)id),
                ddl_DisposalMethod = _DbOpenDump.get_ddl_ref_disposal(),
                RecalcInd = false
            };

            if (model.Assessment != null)
            {
                model.DumpContents = _DbOpenDump.getT_OD_ASSESSMENT_CONTENT_ByDumpAssessmentIDX(model.Assessment.ASSESSMENT_IDX);
                model.ddl_Assessments = _DbOpenDump.get_ddl_T_OD_ASSESSMENTS_by_BySITEIDX(model.Assessment.SITE_IDX);
                model.CleanupEstimatesInd = _DbOpenDump.getT_OD_CLEANUP_PROJECT_Estimate_by_Assessment(model.Assessment.ASSESSMENT_IDX);

                //populate site name
                T_PRT_SITES s = _DbPortal.GetT_PRT_SITES_BySITEIDX(model.Assessment.SITE_IDX);
                if (s != null)
                    model.SiteName = s.SITE_NAME;

                return View(model);
            }

            //if got this far, unable to retrieve data
            TempData["Error"] = "Unable to find cleanup project.";
            return RedirectToAction("AssessmentList");
        }


        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult WasteContent(WasteContentViewModel model)
        {
            if (model != null && model.Assessment != null && model.Assessment.ASSESSMENT_IDX != null)
            {
                foreach (AssessmentContentTypeDisplay oNew in model.DumpContents)
                {
                    _DbOpenDump.InsertUpdateT_OD_Assessment_Content(oNew.DUMP_ASSESSMENTS_CONTENT_IDX, null, null, oNew.WASTE_AMT, oNew.UNIT_MSR_IDX, oNew.WASTE_DISPOSAL_METHOD, oNew.WASTE_DISPOSAL_DIST, true);
                }

                //recalculate costs
                if (model.RecalcInd)
                {
                    //TOODO
                    //_DbOpenDump.proj
                    _DbOpenDump.CalcCleanupEstimate(model.Assessment.ASSESSMENT_IDX, true);
                }
                TempData["Success"] = "Update successful.";

            }
            else
                TempData["Error"] = "Error updating data.";

            return RedirectToAction("WasteContent", new { id = model.Assessment.ASSESSMENT_IDX });
        }

        #endregion


        #region CLEANUP CONTROLLERS **********************************************************************
        [HttpGet]
        public ActionResult CleanupProjects()
        {
            string _UserIDX = _userManager.GetUserId(User);

            var model = new CleanupProjectsViewModel
            {
                CleanupProjects = _DbOpenDump.getT_OD_CLEANUP_PROJECT_by_User(_UserIDX),
                ddlAssessments = _DbOpenDump.get_ddl_T_OD_ASSESSMENTS_by_ByUser(_UserIDX),
                ddlCleanupType = _DbOpenDump.get_ddl_CLEANUP_PROJECT_TYPE()
            };
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult CleanupProjectAdd(CleanupProjectsViewModel model)
        {
            if (model.selAssessID != null && model.selCleanupType != null)
            {
                string _UserIDX = _userManager.GetUserId(User);

                Guid? SuccIDX = _DbOpenDump.InsertUpdateT_OD_CLEANUP_PROJECT(null, model.selAssessID, model.selCleanupType, (model.selCleanupType == "Estimate" ? "Cleanup Estimation" : null), System.DateTime.Today, null, null, null, null, null, null, null, _UserIDX, null, null);                
                if (SuccIDX != null)
                {
                    if (model.selCleanupType == "Estimate")
                    {
                        //if estimate, then calculate based on waste contents
                        _DbOpenDump.CalcCleanupEstimate((Guid)SuccIDX, true);
                    }

                    TempData["Success"] = "Created successfully.";

                    if (model.selCleanupType == "Estimate")
                        return RedirectToAction("Cleanup", "OpenDump", new { id = SuccIDX });
                    else
                        return RedirectToAction("CleanupActual", "OpenDump", new { id = SuccIDX });
                }
            }

            TempData["Error"] = "Unable to create cleanup project.";
            return RedirectToAction("CleanupProjects");
        }

        [HttpPost]
        public JsonResult CleanupProjectDelete(Guid id)
        {
            if (id != null)
            {
                int SuccID = _DbOpenDump.deleteT_OD_CLEANUP_PROJECT(id);
                if (SuccID == 1)
                    return Json("Success");
            }

            return Json("Unable to delete");
        }

        public ActionResult Cleanup(Guid? id)
        {
            var model = new CleanupViewModel
            {
                CleanupProject = _DbOpenDump.getT_OD_CLEANUP_PROJECT_by_IDX(id),
            };

            if (model.CleanupProject != null && model.CleanupProject.ASSESSMENT_IDX != null)
            {
                model.Assessment = _DbOpenDump.getT_OD_ASSESSMENTS_ByAssessmentIDX(model.CleanupProject.ASSESSMENT_IDX);
                model.DumpContents = _DbOpenDump.getT_OD_ASSESSMENT_CONTENT_ByDumpAssessmentIDX_readonly(model.CleanupProject.ASSESSMENT_IDX);
            }

            if (model.Assessment != null)
            {
                T_PRT_SITES s = _DbPortal.GetT_PRT_SITES_BySITEIDX(model.Assessment.SITE_IDX);
                if (s != null)
                    model.SiteName = s.SITE_NAME;

                return View(model);
            }

            //if got this far, unable to retrieve data
            TempData["Error"] = "Unable to find cleanup project.";
            return RedirectToAction("CleanupProjects");
        }

        public ActionResult CleanupActual(Guid? id)
        {
            var model = new CleanupActualViewModel
            {
                CleanupProject = _DbOpenDump.getT_OD_CLEANUP_PROJECT_by_IDX(id),
            };

            if (model.CleanupProject != null && model.CleanupProject.ASSESSMENT_IDX != null)
            {
                model.Assessment = _DbOpenDump.getT_OD_ASSESSMENTS_ByAssessmentIDX(model.CleanupProject.ASSESSMENT_IDX);
                if (model.Assessment != null)
                {
                    model.CleanupActivities = _DbOpenDump.getT_OD_CLEANUP_ACTIVITIES_by_Project_and_Cat((Guid)id, "Cleanup");
                    model.picsBefore_existing = _DbOpenDump.GetT_PRT_DOCUMENTS_ByCleanupProjectIDX((Guid)id, "Open Dump Cleanup - Before");
                    model.picsAfter_existing = _DbOpenDump.GetT_PRT_DOCUMENTS_ByCleanupProjectIDX((Guid)id, "Open Dump Cleanup - After");

                    T_PRT_SITES s = _DbPortal.GetT_PRT_SITES_BySITEIDX(model.Assessment.SITE_IDX);
                    if (s != null)
                        model.SiteName = s.SITE_NAME;

                    return View(model);
                }
            }


            //if got this far, unable to retrieve data
            TempData["Error"] = "Unable to find cleanup project.";
            return RedirectToAction("CleanupProjects");
        }

        [HttpPost]
        public ActionResult CleanupActual(CleanupActualViewModel model)
        {
            if (model != null && model.CleanupProject != null)
            {
                T_OD_CLEANUP_PROJECT _proj = _DbOpenDump.getT_OD_CLEANUP_PROJECT_by_IDX(model.CleanupProject.CLEANUP_PROJECT_IDX);
                if (_proj != null)
                {
                    string _UserIDX = _userManager.GetUserId(User);

                    Guid? _succIDX = _DbOpenDump.InsertUpdateT_OD_CLEANUP_PROJECT(_proj.CLEANUP_PROJECT_IDX, null, null, model.CleanupProject.PROJECT_DESCRIPTION ?? "", model.CleanupProject.START_DATE,
                        model.CleanupProject.COMPLETION_DATE ?? DateTime.MinValue, null, null, null, null, null, null, _UserIDX, model.CleanupProject.CLEANUP_BY ?? "", model.CleanupProject.CLEANUP_BY_TITLE ?? "");

                    if (_succIDX != null)
                    {
                        TempData["Success"] = "Created successfully.";
                        return RedirectToAction("CleanupActual", "OpenDump", new { id = model.CleanupProject.CLEANUP_PROJECT_IDX });
                    }
                }

            }

            TempData["Error"] = "Unable to update.";
            return RedirectToAction("CleanupActual", "OpenDump", new { id = model.CleanupProject.CLEANUP_PROJECT_IDX });
        }
        
        public ActionResult Cleanup2(Guid? id)
        {
            var model = new Cleanup2ViewModel
            {
                CleanupProject = _DbOpenDump.getT_OD_CLEANUP_PROJECT_by_IDX((Guid)id),
                AssessmentCleanups = _DbOpenDump.getT_OD_CLEANUP_CLEANUP_DTL_by_ProjectIDX((Guid)id)
            };

            return View(model);
        }

        [HttpPost]
        public JsonResult Cleanup2Delete(Guid id, Guid? id2)
        {
            if (id != null & id2 != null)
            {
                int SuccID = _DbOpenDump.deleteT_OD_CLEANUP_CLEANUP_DTL(id);
                if (SuccID == 1)
                {
                    //recalculate 
                    _DbOpenDump.CalcCleanupEstimate((Guid)id2, false);
                    return Json("Success");
                }
            }

            return Json("Unable to delete");
        }

        public ActionResult CleanupTransport(Guid? id)
        {
            var model = new CleanupTransportViewModel
            {
            };
            return View(model);
        }

        public ActionResult CleanupDisposal(Guid? id)
        {
            var model = new CleanupDisposalViewModel
            {
            };
            return View(model);
        }

        public IActionResult CleanupEstRpt(Guid? id)
        {
            var _rpt = GenCleanEstRpt(id.ConvertOrDefault<Guid>());
            byte[] rptData = _rpt.rptContent;

            if (rptData != null)
            {
                var content = new System.IO.MemoryStream(rptData);
                return File(rptData, "application/pdf", _rpt.rptName + ".pdf");
            }
            else
            {
                //if got this far, it failed
                TempData["Error"] = "Unable to generate document.";
                return RedirectToAction("CleanupProjects");
            }
        }


        public IActionResult CleanupActRpt(Guid? id)
        {
            var _rpt = GenCleanActRpt(id.ConvertOrDefault<Guid>());
            byte[] rptData = _rpt.rptContent;

            if (rptData != null)
            {
                var content = new System.IO.MemoryStream(rptData);
                return File(rptData, "application/pdf", _rpt.rptName + ".pdf");
            }
            else
            {
                //if got this far, it failed
                TempData["Error"] = "Unable to generate document.";
                return RedirectToAction("CleanupProjects");
            }
        }

        public ActionResult CleanupActivities(Guid? id, string Cat)
        {
            var model = new CleanupActivitiesViewModel
            {
                CleanupCategory = Cat,
                CleanupProject = _DbOpenDump.getT_OD_CLEANUP_PROJECT_by_IDX(id),
                CleanupActivities = _DbOpenDump.getT_OD_CLEANUP_ACTIVITIES_by_Project_and_Cat((Guid)id, Cat)
            };
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult CleanupActivityAdd(CleanupActivitiesViewModel model)
        {
            if (model != null && model.CleanupProject.CLEANUP_PROJECT_IDX != null)
            {
                string _UserIDX = _userManager.GetUserId(User);

                Guid? SuccID = _DbOpenDump.InsertUpdateT_OD_CLEANUP_ACTIVITIES(model.edit_cleanupActivityIdx, model.CleanupProject.CLEANUP_PROJECT_IDX, model.CleanupCategory ?? "Cleanup", 
                    model.newCleanupActivityName, model.newCleanupActivityAmt, _UserIDX, model.newCleanupActivityUnitCost, model.newCleanupActivityQuantity, model.newCleanupActivityQuantityUnit);

                if (SuccID != null)
                {
                    //update cleanup totals 
                    _DbOpenDump.InsertUpdateT_OD_CLEANUP_PROJECT(model.CleanupProject.CLEANUP_PROJECT_IDX, null, null, null, null, null,
                        model.returnURL == "CleanupActual" ? _DbOpenDump.getT_OD_CLEANUP_ACTIVITIES_Sum_by_Project_and_Cat(model.CleanupProject.CLEANUP_PROJECT_IDX, "Cleanup") : null, 
                        null, null,
                        model.CleanupCategory == "Restoration" ? _DbOpenDump.getT_OD_CLEANUP_ACTIVITIES_Sum_by_Project_and_Cat(model.CleanupProject.CLEANUP_PROJECT_IDX, "Restoration") : null,
                        model.CleanupCategory == "Surveillance" ? _DbOpenDump.getT_OD_CLEANUP_ACTIVITIES_Sum_by_Project_and_Cat(model.CleanupProject.CLEANUP_PROJECT_IDX, "Surveillance") : null,
                        null, null, null, null);

                    TempData["Success"] = "Update successful.";
                }
                else
                    TempData["Error"] = "Error updating data.";
            }
            else
                TempData["Error"] = "Error updating data.";

            if (model.returnURL == null)
                return RedirectToAction("CleanupActivities", new { id = model.CleanupProject.CLEANUP_PROJECT_IDX, Cat = model.CleanupCategory });
            else
                return RedirectToAction(model.returnURL, new { id = model.CleanupProject.CLEANUP_PROJECT_IDX });
        }

        [HttpPost]
        public JsonResult CleanupActivityDelete(Guid id)
        {
            T_OD_CLEANUP_ACTIVITIES r = _DbOpenDump.getT_OD_CLEANUP_ACTIVITIES_by_IDX(id);
            if (r != null)
            {
                int SuccID = _DbOpenDump.DeleteT_OD_CLEANUP_ACTIVITIES(id);
                if (SuccID == 1)
                {
                    //update restoration total 
                    _DbOpenDump.InsertUpdateT_OD_CLEANUP_PROJECT(r.CLEANUP_PROJECT_IDX, null, null, null, null, null, null, null, null, 
                        r.CLEANUP_CAT == "Restoration" ? _DbOpenDump.getT_OD_CLEANUP_ACTIVITIES_Sum_by_Project_and_Cat(r.CLEANUP_PROJECT_IDX, "Restoration") : null,
                        r.CLEANUP_CAT == "Surveillance" ? _DbOpenDump.getT_OD_CLEANUP_ACTIVITIES_Sum_by_Project_and_Cat(r.CLEANUP_PROJECT_IDX, "Surveillance") : null,
                        null, null, null, null);

                    return Json("Success");
                }
            }

            return Json("Unable to delete");
        }

        #endregion


        #region DOCUMENT/PHOTO HANDLING

        private byte[] GetByteArrayFromImage(IFormFile file)
        {
            using (var target = new MemoryStream())
            {
                file.CopyTo(target);
                return target.ToArray();
            }
        }

        [HttpGet]
        public FileStreamResult ViewImage(Guid id)
        {
            T_PRT_DOCUMENTS _doc = _DbPortal.GetT_PRT_DOCUMENTS_ByID(id);
            if (_doc != null && _doc.DOC_CONTENT != null)
            {
                MemoryStream ms = new MemoryStream(_doc.DOC_CONTENT);
                ms.Position = 0;
                return new FileStreamResult(ms, _doc.DOC_FILE_TYPE);
            }
            else
                return null;
        }
        
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult PhotoUpload(AssessmentDetailViewModel model)
        {
            if (model != null)
            {
                if (model.filesPhoto == null)
                {
                    TempData["Error"] = "Please select a file.";
                    return RedirectToAction("AssessmentDetails", "OpenDump", new { id = model.Assessment.ASSESSMENT_IDX });
                }

                T_PRT_SITES s = _DbPortal.GetT_PRT_SITES_BySITEIDX(model.Assessment.SITE_IDX);
                byte[] fileBytes = GetByteArrayFromImage(model.filesPhoto);
                string _UserIDX = _userManager.GetUserId(User);

                //insert to database
                string fileType = model.filesPhoto.ContentType.Contains("image") ? "Open Dump-Assess Photo" : "Open Dump-Assess File";
                Guid? DocIDx = _DbPortal.InsertUpdateT_PRT_DOCUMENTS(null, s.ORG_ID, fileBytes, model.filesPhoto.FileName, fileType, model.filesPhoto.ContentType, fileBytes.Length, model.FilePhotoDescription, null, null, null, _UserIDX);
                Guid? SuccID = _DbOpenDump.InsertUpdateT_OD_ASSESSMENT_DOCUMENTS(DocIDx, model.Assessment.ASSESSMENT_IDX);

                if (SuccID != null)
                {
                    TempData["Success"] = "Update successful.";
                    return RedirectToAction("AssessmentDetails", "OpenDump", new { id = model.Assessment.ASSESSMENT_IDX });
                }
            }

            //error if got this far
            TempData["Error"] = "Error updating data.";
            return RedirectToAction("Sites", new { selStr = "", selOrg = "" });
        }
        
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult PhotoUploadCleanupProject(CleanupActualViewModel model)
        {
            if (model != null)
            {
                if (model.filesPhoto == null)
                {
                    TempData["Error"] = "Please select a file.";
                    return RedirectToAction("CleanupActual", "OpenDump", new { id = model.CleanupProject.CLEANUP_PROJECT_IDX });
                }

                T_PRT_SITES s = _DbPortal.GetT_PRT_SITES_BySITEIDX(model.Assessment.SITE_IDX);
                byte[] fileBytes = GetByteArrayFromImage(model.filesPhoto);
                string _UserIDX = _userManager.GetUserId(User);

                //insert to database
                Guid? DocIDx = _DbPortal.InsertUpdateT_PRT_DOCUMENTS(null, s.ORG_ID, fileBytes, model.filesPhoto.FileName, model.FilePhotoType, model.filesPhoto.ContentType, fileBytes.Length, model.FilePhotoDescription, null, null, null, _UserIDX);
                Guid? SuccID = _DbOpenDump.InsertUpdateT_OD_CLEANUP_DOCS(DocIDx, model.CleanupProject.CLEANUP_PROJECT_IDX);

                if (SuccID != null)
                {
                    TempData["Success"] = "Update successful.";
                    return RedirectToAction("CleanupActual", "OpenDump", new { id = model.CleanupProject.CLEANUP_PROJECT_IDX });
                }
            }

            //error if got this far
            TempData["Error"] = "Error updating data.";
            return RedirectToAction("CleanupProjects");
        }

        public ActionResult FileDownload(Guid? id)
        {
            try
            {
                T_PRT_DOCUMENTS doc = _DbPortal.GetT_PRT_DOCUMENTS_ByID((Guid)id);
                var cd = new System.Net.Mime.ContentDisposition
                {
                    FileName = doc.DOC_NAME,
                    Inline = false
                };

                Response.Headers["Content-Disposition"] =  cd.ToString();
                if (doc.DOC_CONTENT != null)
                    return File(doc.DOC_CONTENT, doc.DOC_FILE_TYPE ?? "application/octet-stream");
            }
            catch (Exception ex)
            {
                throw ex;
            }

            TempData["Error"] = "Unable to download document.";
            return RedirectToAction("Sites", new { selStr = "", selOrg = "" });
        }

        public ActionResult FileDelete(Guid? id, Guid? returnIDX, string returnAction)
        {
            // int UserIDX = db_Accounts.GetUserIDX();

            //get project, then org to check permissions
            T_PRT_DOCUMENTS doc = _DbPortal.GetT_PRT_DOCUMENTS_ByID((Guid)id);
            if (doc != null)
            {
                int SuccID = _DbPortal.DeleteT_PRT_DOCUMENTS((Guid)id);
                if (SuccID > 0)
                    TempData["Success"] = "File removed.";
                else
                    TempData["Error"] = "Unable to delete document.";
            }
            else
                TempData["Error"] = "Unable to delete document.";

            return RedirectToAction(returnAction, "OpenDump", new { id = returnIDX });
        }

        #endregion


        #region IMPORT CONTROLLERS
        [HttpGet]
        public ActionResult Import()
        {
            var model = new ImportViewModel
            {
            };

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Import(ImportViewModel model)
        {
            string _UserIDX = _userManager.GetUserId(User);

            //set dictionaries used to store stuff in memory
            Dictionary<string, int> colMapping = new Dictionary<string, int>();  //identifies the column number for each field to be imported
            string path = Path.Combine(_hostingEnvironment.ContentRootPath, "Docs", "ImportColumnsConfig.xml");

            //initialize variables
            bool headInd = true;
            bool anyError = false;
            //loop through each row
            foreach (string row in model.IMPORT_BLOCK.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries))
            {
                //split row's columns into string array
                string[] cols = row.Split(new char[] { '\t' }, StringSplitOptions.None);

                if (cols.Length > 0) //skip blank rows
                {
                    if (headInd)
                    {
                        //**********************************************************
                        //HEADER ROW - LOGIC TO DETERMINE WHAT IS IN EACH COLUMN
                        //**********************************************************
                        colMapping = Utils.GetColumnMapping("P", cols, path);

                        headInd = false;

                        model.sites = new List<SiteImportType>();
                    }
                    else
                    {
                        //**********************************************************
                        //NOT HEADER ROW - READING IN VALUES
                        //**********************************************************
                        var colList = cols.Select((value, index) => new { value, index });
                        var colDataIndexed = (from f in colMapping
                                              join c in colList on f.Value equals c.index
                                              select new
                                              {
                                                  _Name = f.Key,
                                                  _Val = c.value
                                              }).ToList();

                        Dictionary<string, string> fieldValuesDict = new Dictionary<string, string>();  //identifies the column number for each field to be imported

                        //loop through all values and insert to list
                        foreach (var c in colDataIndexed)
                            fieldValuesDict.Add(c._Name, c._Val);

                        //VALIDATE ROW AND INSERT TO LOCAL OBJECT
                        SiteImportType p = _DbOpenDump.InsertOrUpdate_T_OD_SITE_local(_UserIDX, fieldValuesDict, path);
                        if (p.VALIDATE_CD == false)
                            anyError = true;

                        model.sites.Add(p);
                    }
                }
            } //end each row

            //if no errors, just import. otherwise 
            if (!anyError)
            {
                foreach (SiteImportType ps in model.sites)
                {
                    //import prt site
                    T_PRT_SITES x = ps.T_PRT_SITES;
                    Guid? SiteIDX = _DbPortal.InsertUpdateT_PRT_SITES(x.SITE_IDX, x.ORG_ID, x.SITE_NAME, x.EPA_ID, x.LATITUDE, x.LONGITUDE, x.SITE_ADDRESS, x.CREATE_USER_ID, x.LAND_STATUS);

                    //import OD site
                    if (SiteIDX != null)
                    {
                        T_OD_SITES y = ps.T_OD_SITES;
                        _DbOpenDump.InsertUpdateT_OD_SITES((Guid)SiteIDX, y.REPORTED_BY, y.REPORTED_ON, y.COMMUNITY_IDX, y.SITE_SETTING_IDX, y.PF_AQUIFER_VERT_DIST, y.PF_SURF_WATER_HORIZ_DIST, y.PF_HOMES_DIST, y.CURRENT_SITE_STATUS);
                    }

                }

                //clear form
                model.IMPORT_BLOCK = "";
                model.sites = null;
                TempData["Success"] = "Data imported successfully";
            }
            return View(model);
        }

        #endregion

        #region REF DATA CONTROLLERS **********************************************************************

        public IActionResult RefData(string selTag)
        {
            var model = new RefDataViewModel();
            model.ddl_ref_cats = _DbOpenDump.get_ddl_T_OD_REF_DATA_CATEGORIES();

            if (selTag != null)
            {
                model.sel_ref_cat = selTag;
                model.TOdRefData = _DbOpenDump.get_T_OD_REF_DATA_by_category(selTag);
            }
            return View(model);
        }

        #endregion


        #region REPORTS **********************************************************************
        internal RptDisplayType GenRpt(Guid AssessId)
        {
            T_OD_ASSESSMENTS _assess = _DbOpenDump.getT_OD_ASSESSMENTS_ByAssessmentIDX_wNav(AssessId);
            if (_assess != null)
            {
                T_PRT_SITES _site = _DbPortal.GetT_PRT_SITES_BySITEIDX(_assess.SITE_IDX);
                if (_site != null)
                {
                    T_OD_SITES _odsite = _DbOpenDump.getT_OD_SITES_BySITEIDX(_assess.SITE_IDX);

                    T_PRT_ORGANIZATIONS _org = _DbPortal.GetT_PRT_ORGANIZATIONS_ByOrgID(_site.ORG_ID);
                    if (_org != null)
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            // create a new PDF reader based on the PDF template document  
                            PdfReader pdfReader = new PdfReader(Path.Combine(_hostingEnvironment.WebRootPath, "files", "OpenDumpSurveyForm.pdf"));

                            //stamper and memorystream
                            PdfStamper pdfStamper = new PdfStamper(pdfReader, ms);

                            //change file metadata
                            var info = pdfReader.Info;
                            info["Producer"] = "Open Environment Software";
                            info["Title"] = "Open Dump Survey Form";
                            pdfStamper.MoreInfo = info;

                            //fill in form data
                            pdfStamper.AcroFields.SetField("Site Name", _site.SITE_NAME ?? "");
                            pdfStamper.AcroFields.SetField("Community", _odsite.COMMUNITY_IDXNavigation?.REF_DATA_VAL ?? "");
                            pdfStamper.AcroFields.SetField("Tribe", _org.ORG_NAME);

                            if (_assess.CURRENT_SITE_STATUS == "Active")
                                pdfStamper.AcroFields.SetField("Site Status", "Active");
                            else if (_assess.CURRENT_SITE_STATUS == "Inactive - Cleaned Up" || _assess.CURRENT_SITE_STATUS == "Inactive - Closed")
                                pdfStamper.AcroFields.SetField("Site Status", "Inactive");

                            pdfStamper.AcroFields.SetField("Latitude N", _site.LATITUDE.ToString() ?? "");
                            pdfStamper.AcroFields.SetField("Longitude W", _site.LONGITUDE.ToString() ?? "");
                            pdfStamper.AcroFields.SetField("Land Status", _site.LAND_STATUS ?? "");
                            pdfStamper.AcroFields.SetField("Survey Date", _assess.ASSESSMENT_DT.ToShortDateString() ?? "");

                            if (_assess.CURRENT_SITE_STATUS == "Inactive - Cleaned Up")
                                pdfStamper.AcroFields.SetField("Date Site Was Cleaned or Closed", "Cleaned");
                            if (_assess.CURRENT_SITE_STATUS == "Inactive - Closed")
                                pdfStamper.AcroFields.SetField("Date Site Was Cleaned or Closed", "Closed");

                            if (_assess.CURRENT_SITE_STATUS == "Inactive - Cleaned Up" || _assess.CURRENT_SITE_STATUS == "Inactive - Closed")
                                if (_assess.CLEANED_CLOSED_DT != null)
                                    pdfStamper.AcroFields.SetField("Cleaned or Closed Date", _assess.CLEANED_CLOSED_DT.ConvertOrDefault<DateTime>().ToShortDateString() ?? "");

                            pdfStamper.AcroFields.SetField("Condition of Open Dump Site", "Surface Waste");  //hard coded
                            pdfStamper.AcroFields.SetField("Surface Area - # of Acres", _assess.AREA_ACRES.ToString() ?? "");
                            pdfStamper.AcroFields.SetField("Surface Volume - Yd3", _assess.VOLUME_CU_YD.ToString() ?? "");


                            //hazard factors
                            List<AssessmentContentTypeDisplay> _conts = _DbOpenDump.getT_OD_ASSESSMENT_CONTENT_ByDumpAssessmentIDX(_assess.ASSESSMENT_IDX);
                            if (_conts != null)
                            {
                                foreach (AssessmentContentTypeDisplay _cont in _conts)
                                {
                                    if (_cont.REF_WASTE_TYPE_NAME == "Abandoned automobiles")
                                        pdfStamper.AcroFields.SetField("Autos", "Yes");
                                    if (_cont.REF_WASTE_TYPE_NAME == "Abandoned trailers")
                                        pdfStamper.AcroFields.SetField("Trailers", "Yes");
                                    if (_cont.REF_WASTE_TYPE_NAME == "Animal carcasses")
                                        pdfStamper.AcroFields.SetField("Animal", "Yes");
                                    if (_cont.REF_WASTE_TYPE_NAME == "Appliances/white goods")
                                        pdfStamper.AcroFields.SetField("Appliances/White goods", "Yes");
                                    if (_cont.REF_WASTE_TYPE_NAME == "Construction and demolition wastes")
                                        pdfStamper.AcroFields.SetField("Construction/Demo", "Yes");
                                    if (_cont.REF_WASTE_TYPE_NAME == "Drums/containers of unknowns/pesticide containers")
                                        pdfStamper.AcroFields.SetField("Drums/Containers Unknown/Pesticide", "Yes");
                                    if (_cont.REF_WASTE_TYPE_NAME == "Electronics")
                                        pdfStamper.AcroFields.SetField("Electronics", "Yes");
                                    if (_cont.REF_WASTE_TYPE_NAME == "Fluorescent light bulbs")
                                        pdfStamper.AcroFields.SetField("Fluorescent lights", "Yes");
                                    if (_cont.REF_WASTE_TYPE_NAME == "Furniture")
                                        pdfStamper.AcroFields.SetField("Furniture", "Yes");
                                    if (_cont.REF_WASTE_TYPE_NAME == "Lead acid batteries")
                                        pdfStamper.AcroFields.SetField("Lead acid batteries", "Yes");
                                    if (_cont.REF_WASTE_TYPE_NAME == "Medical wastes")
                                        pdfStamper.AcroFields.SetField("Medical", "Yes");
                                    if (_cont.REF_WASTE_TYPE_NAME == "Meth-lab wastes")
                                        pdfStamper.AcroFields.SetField("Meth-lab", "Yes");
                                    if (_cont.REF_WASTE_TYPE_NAME == "Municipal solid waste")
                                        pdfStamper.AcroFields.SetField("Municipal solid", "Yes");
                                    if (_cont.REF_WASTE_TYPE_NAME == "Scrap tires")
                                        pdfStamper.AcroFields.SetField("Tires", "Yes");
                                    if (_cont.REF_WASTE_TYPE_NAME == "Sewage sludge/septic-tank pumpings")
                                        pdfStamper.AcroFields.SetField("Sewage sludge/septic", "Yes");
                                    if (_cont.REF_WASTE_TYPE_NAME == "Suspected asbestos or lead containing materials")
                                        pdfStamper.AcroFields.SetField("Suspect asbestos or lead", "Yes");
                                    if (_cont.REF_WASTE_TYPE_NAME == "Suspected RCRA Subtitle C hazardous wastes (treated wood, paints, solvents)")
                                        pdfStamper.AcroFields.SetField("Suspect RCRA Sub. C Hazwaste", "Yes");
                                    if (_cont.REF_WASTE_TYPE_NAME == "Waste oils/oily wastes")
                                        pdfStamper.AcroFields.SetField("Oil/Oily", "Yes");
                                    if (_cont.REF_WASTE_TYPE_NAME == "Yard/green wastes")
                                        pdfStamper.AcroFields.SetField("Yard/Green waste", "Yes");
                                }
                            }


                            //rainfall
                            if (_assess.HF_RAINFALLNavigation?.THREAT_FACTOR_NAME == "Low (<10 in/yr)")
                                pdfStamper.AcroFields.SetField("Rainfall", "Low");
                            else if (_assess.HF_RAINFALLNavigation?.THREAT_FACTOR_NAME == "Medium (10-25 in/yr)")
                                pdfStamper.AcroFields.SetField("Rainfall", "Medium");
                            else if (_assess.HF_RAINFALLNavigation?.THREAT_FACTOR_NAME == "High (>25 in/yr)")
                                pdfStamper.AcroFields.SetField("Rainfall", "High");

                            //drainage
                            if (_assess.HF_DRAINAGENavigation?.THREAT_FACTOR_NAME == "Site drainage protects ground or surface water")
                                pdfStamper.AcroFields.SetField("Drainage and Leachate Potential", "Protected");
                            else if (_assess.HF_DRAINAGENavigation?.THREAT_FACTOR_NAME == "Limited ponding, drainage effects are largely neutral")
                                pdfStamper.AcroFields.SetField("Drainage and Leachate Potential", "Neutral/Limited");
                            else if (_assess.HF_DRAINAGENavigation?.THREAT_FACTOR_NAME == "Site drainage increases ground or surface water contamination")
                                pdfStamper.AcroFields.SetField("Drainage and Leachate Potential", "Increased");

                            //flooding
                            if (_assess.HF_FLOODINGNavigation?.THREAT_FACTOR_NAME == "No potential for flooding")
                                pdfStamper.AcroFields.SetField("Flood Potential", "None");
                            else if (_assess.HF_FLOODINGNavigation?.THREAT_FACTOR_NAME == "Debris movement from flooding unlikely")
                                pdfStamper.AcroFields.SetField("Flood Potential", "Unlikely");
                            else if (_assess.HF_FLOODINGNavigation?.THREAT_FACTOR_NAME == "Debris movement from flooding likely")
                                pdfStamper.AcroFields.SetField("Flood Potential", "Likely");

                            //burning
                            if (_assess.HF_BURNINGNavigation?.THREAT_FACTOR_NAME == "Burning does not occur")
                                pdfStamper.AcroFields.SetField("Frequency of Burning", "None");
                            else if (_assess.HF_BURNINGNavigation?.THREAT_FACTOR_NAME == "Burning less frequently than weekly")
                                pdfStamper.AcroFields.SetField("Frequency of Burning", "Less than weekly");
                            else if (_assess.HF_BURNINGNavigation?.THREAT_FACTOR_NAME == "Burning more frequently than weekly")
                                pdfStamper.AcroFields.SetField("Frequency of Burning", "More than weekly");

                            //fencing
                            if (_assess.HF_FENCINGNavigation?.THREAT_FACTOR_NAME == "Fenced In")
                                pdfStamper.AcroFields.SetField("Fenced Site?", "Yes");
                            else if (_assess.HF_FENCINGNavigation?.THREAT_FACTOR_NAME == "Not Fenced In")
                                pdfStamper.AcroFields.SetField("Fenced Site?", "No");

                            //controlled access
                            if (_assess.HF_ACCESS_CONTROLNavigation?.THREAT_FACTOR_NAME == "Effectively controlled access")
                                pdfStamper.AcroFields.SetField("Controlled Access?", "Effective");
                            else if (_assess.HF_ACCESS_CONTROLNavigation?.THREAT_FACTOR_NAME == "Ineffective controls or poorly restricted access")
                                pdfStamper.AcroFields.SetField("Controlled Access?", "Ineffective");
                            else if (_assess.HF_ACCESS_CONTROLNavigation?.THREAT_FACTOR_NAME == "Unrestricted access")
                                pdfStamper.AcroFields.SetField("Controlled Access?", "Unrestricted");


                            //public concern
                            if (_assess.HF_PUBLIC_CONCERNNavigation?.THREAT_FACTOR_NAME == "No concern voiced")
                                pdfStamper.AcroFields.SetField("Public Concern", "None");
                            else if (_assess.HF_PUBLIC_CONCERNNavigation?.THREAT_FACTOR_NAME == "Little concern voiced by the public")
                                pdfStamper.AcroFields.SetField("Public Concern", "Little");
                            else if (_assess.HF_PUBLIC_CONCERNNavigation?.THREAT_FACTOR_NAME == "Concern frequently voiced by the public")
                                pdfStamper.AcroFields.SetField("Public Concern", "Frequent");



                            //vert distance
                            if (_odsite.PF_AQUIFER_VERT_DISTNavigation?.THREAT_FACTOR_NAME == "Greater than 600 feet")
                                pdfStamper.AcroFields.SetField("Vertical Dist. to Aquifer", "+600 ft.");
                            else if (_odsite.PF_AQUIFER_VERT_DISTNavigation?.THREAT_FACTOR_NAME == "51-599 feet")
                                pdfStamper.AcroFields.SetField("Vertical Dist. to Aquifer", "51-599 ft.");
                            else if (_odsite.PF_AQUIFER_VERT_DISTNavigation?.THREAT_FACTOR_NAME == "Less than 50 feet")
                                pdfStamper.AcroFields.SetField("Vertical Dist. to Aquifer", "-50 ft.");


                            //horiz distance
                            if (_odsite.PF_SURF_WATER_HORIZ_DISTNavigation?.THREAT_FACTOR_NAME == "Greater than 1,000 feet")
                                pdfStamper.AcroFields.SetField("Horizontal Distance to Surface Water", "+1000 ft.");
                            else if (_odsite.PF_SURF_WATER_HORIZ_DISTNavigation?.THREAT_FACTOR_NAME == "51-1,000 feet")
                                pdfStamper.AcroFields.SetField("Horizontal Distance to Surface Water", "51-1000 ft,");
                            else if (_odsite.PF_SURF_WATER_HORIZ_DISTNavigation?.THREAT_FACTOR_NAME == "Less than 50 feet")
                                pdfStamper.AcroFields.SetField("Horizontal Distance to Surface Water", "-50 ft.");

                            //distance homes
                            if (_odsite.PF_HOMES_DISTNavigation?.THREAT_FACTOR_NAME == "Greater than 5,000 feet")
                                pdfStamper.AcroFields.SetField("Distance to Homes", "+5000 ft.");
                            else if (_odsite.PF_HOMES_DISTNavigation?.THREAT_FACTOR_NAME == "1,000-5,000 feet")
                                pdfStamper.AcroFields.SetField("Distance to Homes", "1000-5000 ft.");
                            else if (_odsite.PF_HOMES_DISTNavigation?.THREAT_FACTOR_NAME == "Less than 1,000 feet")
                                pdfStamper.AcroFields.SetField("Distance to Homes", "-1000 ft.");


                            pdfStamper.AcroFields.SetField("General Description", _assess.SITE_DESCRIPTION ?? "");
                            pdfStamper.AcroFields.SetField("Comments", _assess.ASSESSMENT_NOTES ?? "");
                            pdfStamper.AcroFields.SetField("Surveyor Contact Information", (_assess.ASSESSED_BY ?? "") + ", " + _org.ORG_NAME);


                            // flatten the form to remove editting options, set it to false  
                            // to leave the form open to subsequent manual edits  
                            pdfStamper.FormFlattening = false;

                            // close the pdf  
                            pdfStamper.Close();

                            //return value
                            var _rpt = new RptDisplayType
                            {
                                rptContent = ms.ToArray(),
                                rptName = _site.SITE_NAME + "_" + _assess.ASSESSMENT_DT.ToShortDateString()
                            };
                            foreach (var c in Path.GetInvalidFileNameChars()) { _rpt.rptName = _rpt.rptName.Replace(c, '-'); }
                            return _rpt;
                        }

                    }

                }
            }

            return null;
        }

        internal RptDisplayType GenCleanEstRpt(Guid CleanupProjectID)
        {
            T_OD_CLEANUP_PROJECT _project = _DbOpenDump.getT_OD_CLEANUP_PROJECT_by_IDX(CleanupProjectID);
            if (_project != null)
            {
                T_OD_ASSESSMENTS _assess = _DbOpenDump.getT_OD_ASSESSMENTS_ByAssessmentIDX_wNav(_project.ASSESSMENT_IDX);
                if (_assess != null)
                {
                    T_PRT_SITES _site = _DbPortal.GetT_PRT_SITES_BySITEIDX(_assess.SITE_IDX);
                    if (_site != null)
                    {
                        T_OD_SITES _odsite = _DbOpenDump.getT_OD_SITES_BySITEIDX(_assess.SITE_IDX);

                        T_PRT_ORGANIZATIONS _org = _DbPortal.GetT_PRT_ORGANIZATIONS_ByOrgID(_site.ORG_ID);
                        if (_org != null)
                        {
                            using (MemoryStream ms = new MemoryStream())
                            {
                                // Create an instance of the document class which represents the PDF document itself.
                                Document document = new Document(PageSize.A4, 25, 25, 25, 25);

                                // Create an instance to the PDF file by creating an instance of the PDF Writer class using the document and the filestrem in the constructor.
                                PdfWriter writer = PdfWriter.GetInstance(document, ms);

                                // Add meta information to the document
                                document.AddAuthor("Tribal Services Portal");
                                document.AddCreator("Open Environment Software");
                                document.AddKeywords("Open Dump");
                                document.AddSubject("Open Dump Cleanup");
                                document.AddTitle("Open Dump Cleanup Estimate Report");

                                // Open the document to enable you to write to the document
                                document.Open();

                                //define standard font styles used in the document
                                BaseFont bfHelv = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false);
                                Font font_head = new Font(bfHelv, 15, Font.NORMAL, BaseColor.BLACK);
                                Font font_subhead = new Font(bfHelv, 12, Font.NORMAL, BaseColor.BLACK);
                                Font font_main = new Font(bfHelv, 10, Font.NORMAL, BaseColor.BLACK);
                                Font font_main_bold = new Font(bfHelv, 10, Font.BOLD, BaseColor.BLACK);
                                Font font_small = new Font(bfHelv, 8, Font.NORMAL, BaseColor.DARK_GRAY);
                                Font font_small_bold = new Font(bfHelv, 8, Font.BOLD, BaseColor.DARK_GRAY);


                                // Add document title
                                Paragraph _p_temp = new Paragraph("ILLEGAL DUMPING ECONOMIC ASSESSMENT (IDEA) MODEL" + Environment.NewLine, font_head);
                                _p_temp.Alignment = Element.ALIGN_CENTER;
                                document.Add(_p_temp);

                                // Add document subtitle
                                _p_temp = new Paragraph("Cost Estimate of an Individual Site*" + Environment.NewLine + Environment.NewLine, font_subhead);
                                _p_temp.Alignment = Element.ALIGN_CENTER;
                                document.Add(_p_temp);


                                //*********************************** site name and address **********************************
                                PdfPTable table2 = new PdfPTable(3);
                                table2.WidthPercentage = 100f;
                                AddPDFTableHeader("", font_main, table2, 0);
                                AddPDFTableField("Site", _site.SITE_NAME + Environment.NewLine + _site.SITE_ADDRESS, font_main, font_small, table2, 1, null, false);

                                Chunk c1 = new Chunk("Assessment Date" + Environment.NewLine, font_small);
                                Chunk c2 = new Chunk("  " + _assess.ASSESSMENT_DT.ToShortDateString() + Environment.NewLine, font_main);
                                Chunk c3 = new Chunk("Date Printed" + Environment.NewLine, font_small);
                                Chunk c4 = new Chunk("  " + System.DateTime.Now.ToShortDateString(), font_main);
                                Phrase p1 = new Phrase();
                                p1.Add(c1);
                                p1.Add(c2);
                                p1.Add(c3);
                                p1.Add(c4);
                                PdfPCell cell = new PdfPCell(p1) { Padding = 5 };
                                cell.Colspan = 1;
                                table2.AddCell(cell);

                                AddPDFTableField("Est. Cleanup Cost", "$ " + _project.COST_TOTAL_AMT.ToString(), font_head, font_small, table2);
                                document.Add(table2);

                                document.Add(new Paragraph("  ", font_small));


                                // Illegal Dump Site Features Section ************************************************************
                                _p_temp = new Paragraph("Waste Quantities and Type" + Environment.NewLine, font_main_bold);
                                document.Add(_p_temp);

                                _p_temp = new Paragraph(_assess.AREA_ACRES + " square feet" + Environment.NewLine + Environment.NewLine, font_small);
                                document.Add(_p_temp);

                                PdfPTable table3 = new PdfPTable(2);
                                table3.WidthPercentage = 75f;
                                table3.SetWidths(new float[] { 70f, 30f });

                                AddPDFTableField(null, "Waste Type", font_main_bold, font_main_bold, table3, 1, null, true, true);
                                AddPDFTableField(null, "Amount", font_main_bold, font_main_bold, table3, 1, null, true, true);

                                List<AssessmentContentTypeDisplay> _content = _DbOpenDump.getT_OD_ASSESSMENT_CONTENT_ByDumpAssessmentIDX(_project.ASSESSMENT_IDX);
                                foreach (AssessmentContentTypeDisplay _item in _content)
                                {
                                    if (_item.UNIT_MSR_IDX != null)
                                    {
                                        T_PRT_REF_UNITS _unit = _DbPortal.get_T_PRT_REF_UNITS_ByID(_item.UNIT_MSR_IDX);

                                        AddPDFTableField(null, _item.REF_WASTE_TYPE_NAME, font_main, font_small, table3);
                                        AddPDFTableField(null, _item.WASTE_AMT + " " + _unit.UNIT_MSR_CD, font_main, font_small, table3);
                                    }
                                }
                                document.Add(table3);


                                // Cleanup, Waste Transport, and Disposal Costs Section ************************************************************
                                _p_temp = new Paragraph("Cleanup, Waste Transport, and Disposal Costs:" + Environment.NewLine + Environment.NewLine, font_main_bold);
                                document.Add(_p_temp);

                                PdfPTable table4 = new PdfPTable(5);
                                table4.WidthPercentage = 95f;
                                table4.SetWidths(new float[] { 15f, 45f, 15f, 20f, 15f });

                                AddPDFTableField(null, "Category", font_main_bold, font_main_bold, table4, 1, null, true, true);
                                AddPDFTableField(null, "Asset", font_main_bold, font_main_bold, table4, 1, null, true, true);
                                AddPDFTableField(null, "Hourly Rate", font_main_bold, font_main_bold, table4, 1, null, true, true);
                                AddPDFTableField(null, "Processing Rate", font_main_bold, font_main_bold, table4, 1, null, true, true);
                                AddPDFTableField(null, "Cost", font_main_bold, font_main_bold, table4, 1, null, true, true);

                                List<AssessmentCleanupDisplayType> _cleanup = _DbOpenDump.getT_OD_CLEANUP_CLEANUP_DTL_by_ProjectIDX(CleanupProjectID);
                                foreach (AssessmentCleanupDisplayType _item in _cleanup)
                                {
                                    AddPDFTableField(null, _item.REF_WASTE_TYPE_CAT, font_main, font_small, table4);
                                    AddPDFTableField(null, _item.REF_ASSET_NAME, font_main, font_small, table4);
                                    AddPDFTableField(null, _item.ASSET_HOURLY_RATE.ToString(), font_main, font_small, table4);
                                    AddPDFTableField(null, _item.PROCESS_RATE_PER_HR.ToString() + " " + _item.PROCESS_RATE_UNIT, font_main, font_small, table4);
                                    AddPDFTableField(null, "$" + _item.CLEANUP_COST.ToString(), font_main, font_small, table4);
                                }
                                AddPDFTableField(null, "", font_main, font_small, table4, 4, null, true, true);
                                AddPDFTableField(null, "$" + _project.COST_CLEANUP_AMT, font_main_bold, font_main_bold, table4, 1, null, true, true);

                                document.Add(table4);


                                // Post-Cleanup Costs Section ************************************************************
                                _p_temp = new Paragraph("Post-Cleanup Costs:" + Environment.NewLine + Environment.NewLine, font_main_bold);
                                document.Add(_p_temp);


                                //*********************************************
                                //Restoration *********************************
                                //*********************************************
                                List<T_OD_CLEANUP_ACTIVITIES> _restore = _DbOpenDump.getT_OD_CLEANUP_ACTIVITIES_by_Project_and_Cat(CleanupProjectID, "Restore");
                                if (_restore != null && _restore.Count > 0)
                                {
                                    PdfPTable tblRestore = new PdfPTable(2);
                                    tblRestore.WidthPercentage = 95f;
                                    float[] widthRestore = new float[] { 60f, 20f };
                                    tblRestore.SetWidths(widthRestore);
                                    AddPDFTableField(null, "Restoration Activity", font_main_bold, font_main_bold, tblRestore, 1, null, true, true);
                                    AddPDFTableField(null, "Cost Estimate", font_main_bold, font_main_bold, tblRestore, 1, null, true, true);

                                    foreach (T_OD_CLEANUP_ACTIVITIES _item in _restore)
                                    {
                                        AddPDFTableField(null, _item.CLEANUP_ACTIVITY, font_main, font_small, tblRestore);
                                        AddPDFTableField(null, "$" + _item.CLEANUP_COST.ToString(), font_main, font_small, tblRestore);
                                    }
                                    AddPDFTableField(null, "", font_main, font_small, tblRestore, 1, null, true, true);
                                    AddPDFTableField(null, "$" + _project.COST_RESTORE_AMT, font_main_bold, font_main_bold, tblRestore, 1, null, true, true);

                                    document.Add(tblRestore);
                                }

                                document.Add(new Paragraph("  ", font_small));

                                //*********************************************
                                //Surveilance *********************************
                                //*********************************************
                                List<T_OD_CLEANUP_ACTIVITIES> _surveil = _DbOpenDump.getT_OD_CLEANUP_ACTIVITIES_by_Project_and_Cat(CleanupProjectID, "Surveil");
                                if (_surveil != null && _surveil.Count > 0)
                                {
                                    PdfPTable tblSurveil = new PdfPTable(2);
                                    tblSurveil.WidthPercentage = 95f;
                                    float[] widthRestore = new float[] { 60f, 20f };
                                    tblSurveil.SetWidths(widthRestore);
                                    AddPDFTableField(null, "Surveilance Activity", font_main_bold, font_main_bold, tblSurveil, 1, null, true, true);
                                    AddPDFTableField(null, "Cost Estimate", font_main_bold, font_main_bold, tblSurveil, 1, null, true, true);

                                    foreach (T_OD_CLEANUP_ACTIVITIES _item in _surveil)
                                    {
                                        AddPDFTableField(null, _item.CLEANUP_ACTIVITY, font_main, font_small, tblSurveil);
                                        AddPDFTableField(null, "$" + _item.CLEANUP_COST.ToString(), font_main, font_small, tblSurveil);
                                    }
                                    AddPDFTableField(null, "", font_main, font_small, tblSurveil, 1, null, true, true);
                                    AddPDFTableField(null, "$" + _project.COST_SURVEIL_AMT, font_main_bold, font_main_bold, tblSurveil, 1, null, true, true);

                                    document.Add(tblSurveil);
                                }


                                // Close the document
                                document.Close();

                                // Close the writer instance
                                writer.Close();

                                //return value
                                var _rpt = new RptDisplayType
                                {
                                    rptContent = ms.ToArray(),
                                    rptName = _site.SITE_NAME + "_CleanupEstimate"
                                };

                                foreach (var c in Path.GetInvalidFileNameChars()) { _rpt.rptName = _rpt.rptName.Replace(c, '-'); }
                                return _rpt;
                            }

                        }

                    }
                }
            }

            return null;
        }

        internal RptDisplayType GenCleanActRpt(Guid CleanupProjectID)
        {
            T_OD_CLEANUP_PROJECT _project = _DbOpenDump.getT_OD_CLEANUP_PROJECT_by_IDX(CleanupProjectID);
            if (_project != null)
            {
                T_OD_ASSESSMENTS _assess = _DbOpenDump.getT_OD_ASSESSMENTS_ByAssessmentIDX_wNav(_project.ASSESSMENT_IDX);
                if (_assess != null)
                {
                    T_PRT_SITES _site = _DbPortal.GetT_PRT_SITES_BySITEIDX(_assess.SITE_IDX);
                    if (_site != null)
                    {
                        T_OD_SITES _odsite = _DbOpenDump.getT_OD_SITES_BySITEIDX(_assess.SITE_IDX);

                        T_PRT_ORGANIZATIONS _org = _DbPortal.GetT_PRT_ORGANIZATIONS_ByOrgID(_site.ORG_ID);
                        if (_org != null)
                        {
                            using (MemoryStream ms = new MemoryStream())
                            {
                                // Create an instance of the document class which represents the PDF document itself.
                                Document document = new Document(PageSize.A4, 25, 25, 25, 25);

                                // Create an instance to the PDF file by creating an instance of the PDF Writer class using the document and the filestrem in the constructor.
                                PdfWriter writer = PdfWriter.GetInstance(document, ms);

                                // Add meta information to the document
                                document.AddAuthor("Tribal Services Portal");
                                document.AddCreator("Open Environment Software");
                                document.AddKeywords("Open Dump");
                                document.AddSubject("Open Dump Cleanup");
                                document.AddTitle("Open Dump Cleanup Project Report");

                                // Open the document to enable you to write to the document
                                document.Open();

                                //define standard font styles used in the document
                                BaseFont bfHelv = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false);
                                Font font_head = new Font(bfHelv, 15, Font.NORMAL, BaseColor.BLACK);
                                Font font_subhead = new Font(bfHelv, 12, Font.NORMAL, BaseColor.BLACK);
                                Font font_main = new Font(bfHelv, 10, Font.NORMAL, BaseColor.BLACK);
                                Font font_main_bold = new Font(bfHelv, 10, Font.BOLD, BaseColor.BLACK);
                                Font font_small = new Font(bfHelv, 8, Font.NORMAL, BaseColor.DARK_GRAY);
                                Font font_small_bold = new Font(bfHelv, 8, Font.BOLD, BaseColor.DARK_GRAY);


                                // Add document title
                                Paragraph _p_temp = new Paragraph("CLEANUP PROJECT REPORT" + Environment.NewLine, font_head);
                                _p_temp.Alignment = Element.ALIGN_CENTER;
                                document.Add(_p_temp);

                                // Add document subtitle
                                _p_temp = new Paragraph("Cleanup Summary for " + _site.SITE_NAME + Environment.NewLine + Environment.NewLine, font_subhead);
                                _p_temp.Alignment = Element.ALIGN_CENTER;
                                document.Add(_p_temp);


                                //*********************************** site name and address **********************************
                                PdfPTable table2 = new PdfPTable(3);
                                table2.WidthPercentage = 100f;
                                AddPDFTableHeader("", font_main, table2, 0);
                                AddPDFTableField("Dump Site Location", _site.SITE_NAME + Environment.NewLine + _site.SITE_ADDRESS, font_main, font_small, table2, 1, null, false);

                                Chunk c1 = new Chunk("Project Start / End Dates" + Environment.NewLine, font_small);
                                string startDt = _project.START_DATE != null ? _project.START_DATE.ConvertOrDefault<DateTime>().ToShortDateString() : "";
                                string endDt = _project.COMPLETION_DATE != null ? _project.COMPLETION_DATE.ConvertOrDefault<DateTime>().ToShortDateString() : "";
                                Chunk c2 = new Chunk("  " + startDt + " - " + endDt + Environment.NewLine, font_main);
                                Chunk c3 = new Chunk("Work Performed By" + Environment.NewLine, font_small);
                                string title = (_project.CLEANUP_BY_TITLE != null && _project.CLEANUP_BY_TITLE.Length > 0) ? " (" + _project.CLEANUP_BY_TITLE + ")" : "";
                                Chunk c4 = new Chunk("  " + _project.CLEANUP_BY + title, font_main);
                                Phrase p1 = new Phrase();
                                p1.Add(c1);
                                p1.Add(c2);
                                p1.Add(c3);
                                p1.Add(c4);
                                PdfPCell cell = new PdfPCell(p1) { Padding = 5 };
                                cell.Colspan = 1;
                                table2.AddCell(cell);

                                AddPDFTableField("Cleanup Cost", "$ " + _project.COST_TOTAL_AMT.ToString(), font_head, font_small, table2);
                                document.Add(table2);

                                document.Add(new Paragraph("  ", font_small));


                                //*********************************************
                                //CLEANUP ACTIVITIES *********************************
                                //*********************************************
                                List<T_OD_CLEANUP_ACTIVITIES> _restore = _DbOpenDump.getT_OD_CLEANUP_ACTIVITIES_by_Project_and_Cat(CleanupProjectID, "Cleanup");
                                if (_restore != null && _restore.Count > 0)
                                {
                                    PdfPTable tblRestore = new PdfPTable(4);
                                    tblRestore.WidthPercentage = 100f;
                                    float[] widthRestore = new float[] { 64f, 12f, 12f, 12f };
                                    tblRestore.SetWidths(widthRestore);
                                    AddPDFTableField(null, "Cleanup Activity", font_main, font_main, tblRestore, 1, null, true, true);
                                    AddPDFTableField(null, "Quantity", font_main, font_main, tblRestore, 1, null, true, true);
                                    AddPDFTableField(null, "Unit Cost", font_main, font_main, tblRestore, 1, null, true, true);
                                    AddPDFTableField(null, "Cost", font_main, font_main, tblRestore, 1, null, true, true);

                                    foreach (T_OD_CLEANUP_ACTIVITIES _item in _restore)
                                    {
                                        AddPDFTableField(null, _item.CLEANUP_ACTIVITY, font_main, font_small, tblRestore);
                                        AddPDFTableField(null, _item.QUANTITY + _item.QUANTITY_UNIT, font_main, font_small, tblRestore);
                                        AddPDFTableField(null, "$" + _item.CLEANUP_UNIT_COST.ToString(), font_main, font_small, tblRestore);
                                        AddPDFTableField(null, "$" + _item.CLEANUP_COST.ToString(), font_main, font_small, tblRestore);
                                    }

                                    //footer
                                    AddPDFTableField(null, "", font_main, font_small, tblRestore, 1, null, true, true);
                                    AddPDFTableField(null, "", font_main, font_small, tblRestore, 1, null, true, true);
                                    AddPDFTableField(null, "", font_main, font_small, tblRestore, 1, null, true, true);
                                    AddPDFTableField(null, "$" + _project.COST_TOTAL_AMT, font_main_bold, font_main_bold, tblRestore, 1, null, true, true);

                                    document.Add(tblRestore);
                                }

                                document.Add(new Paragraph("  ", font_small));


                                document.Add(new Paragraph("Summary of Cleanup", font_main));
                                document.Add(new Paragraph(_project.PROJECT_DESCRIPTION, font_small));

                                //*************************************************
                                //IMAGES ******************************************
                                //*************************************************
                                List<T_PRT_DOCUMENTS> _picsBefore = _DbOpenDump.GetT_PRT_DOCUMENTS_ByCleanupProjectIDX(_project.CLEANUP_PROJECT_IDX, "Open Dump Cleanup - Before");
                                if (_picsBefore != null && _picsBefore.Count > 0)
                                {
                                    document.NewPage();
                                    document.Add(new Paragraph("BEFORE PICTURES", font_head));

                                    foreach (T_PRT_DOCUMENTS _pic in _picsBefore)
                                    {
                                        Image pic = Image.GetInstance(_pic.DOC_CONTENT);
                                        float percentage = 0.0f;

                                        if (pic.Height > pic.Width)
                                            percentage = 700 / pic.Height;
                                        else
                                            percentage = 540 / pic.Width; 
                                        if (percentage > 1) percentage = 1;
                                        pic.ScalePercent(percentage * 100);

                                        document.Add(pic);
                                    }
                                }

                                List<T_PRT_DOCUMENTS> _picsAfter = _DbOpenDump.GetT_PRT_DOCUMENTS_ByCleanupProjectIDX(_project.CLEANUP_PROJECT_IDX, "Open Dump Cleanup - After");
                                if (_picsAfter != null && _picsAfter.Count > 0)
                                {
                                    document.NewPage();
                                    document.Add(new Paragraph("AFTER PICTURES", font_head));

                                    foreach (T_PRT_DOCUMENTS _pic in _picsAfter)
                                    {
                                        Image pic = Image.GetInstance(_pic.DOC_CONTENT);
                                        float percentage = 0.0f;

                                        if (pic.Height > pic.Width)
                                            percentage = 700 / pic.Height;
                                        else
                                            percentage = 540 / pic.Width;
                                        if (percentage > 1) percentage = 1;
                                        pic.ScalePercent(percentage * 100);

                                        document.Add(pic);
                                    }
                                }





                                // Close the document
                                document.Close();

                                // Close the writer instance
                                writer.Close();

                                //return value
                                var _rpt = new RptDisplayType
                                {
                                    rptContent = ms.ToArray(),
                                    rptName = _site.SITE_NAME + "_CleanupProject"
                                };

                                foreach (var c in Path.GetInvalidFileNameChars()) { _rpt.rptName = _rpt.rptName.Replace(c, '-'); }
                                return _rpt;
                            }

                        }

                    }
                }
            }

            return null;
        }


        private static void AddPDFTableField(string Label, string Value, Font fontVal, Font fontLabel, PdfPTable table, int ColSpan = 1, float? minHeight = null, bool indentValue = true, bool shadedInd = false)
        {
            Chunk c1 = new Chunk(Label + (Label != null ? Environment.NewLine : ""), fontLabel);
            Chunk c2 = new Chunk((indentValue == true ? "  " : "") + Value, fontVal);
            Phrase p1 = new Phrase();
            p1.Add(c1);
            p1.Add(c2);

            PdfPCell cell = new PdfPCell(p1) { Padding = 5 };
            cell.Colspan = ColSpan;
            if (shadedInd)
                cell.BackgroundColor = new BaseColor(128, 128, 128);
            if (minHeight != null)
                cell.MinimumHeight = minHeight.ConvertOrDefault<float>();
            table.AddCell(cell);
        }

        /// <summary>
        /// Add a header to a PDF table
        /// </summary>
        /// <param name="Header"></param>
        /// <param name="font_header"></param>
        /// <param name="table"></param>
        /// <param name="HorizAlign">0=Left, 1=Centre, 2=Right</param>
        private static void AddPDFTableHeader(string Header, Font font_header, PdfPTable table, int HorizAlign)
        {            
            PdfPCell cell = new PdfPCell(new Phrase(Header, font_header)) { Padding = 5 };
            cell.BackgroundColor = new BaseColor(128, 128, 128);
            cell.Colspan = table.NumberOfColumns;
            cell.HorizontalAlignment = HorizAlign;
            table.AddCell(cell);
        }

        #endregion


        [AllowAnonymous]
        public IActionResult DumpParcels()
        {
            //var identity = (ClaimsIdentity)User.Identity;
            //IEnumerable<Claim> claims = identity.Claims;

            return View();
        }





    }
}