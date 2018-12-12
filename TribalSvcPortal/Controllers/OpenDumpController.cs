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

namespace TribalSvcPortal.Controllers
{
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


        #region SEARCH PAGE CONTROLLERS **********************************************************************
        public ActionResult Search(string selStr, string selOrg)
        {
            string _UserIDX = _userManager.GetUserId(User);

            SearchViewModel model = new SearchViewModel {
                ddl_Org = _DbPortal.get_ddl_T_PRT_ORG_USERS_CLIENT_ByUserIDandClient(_UserIDX, "open_dump"),
                searchResults = _DbOpenDump.getT_OD_SITES_ListBySearch(_UserIDX, selStr, selOrg)
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

        #endregion


        #region PREFIELD TAB CONTROLLERS **********************************************************************
        // GET: /OpenDump/PreField
        public ActionResult PreField(Guid? id, string returnURL)
        {
            string _UserIDX = _userManager.GetUserId(User);

            //TODO add security

            var model = new PreFieldViewModel {
                SiteSettingsList = _DbOpenDump.get_ddl_T_OD_REF_DATA_by_category("Site Setting"),
                CommunityList = _DbOpenDump.get_ddl_T_OD_REF_DATA_by_category("Community"),
                AquiferList = _DbOpenDump.get_ddl_OD_REF_THREAT_FACTORS_by_type("Aquifer"),
                SurfaceWaterList = _DbOpenDump.get_ddl_OD_REF_THREAT_FACTORS_by_type("Surface Water"),
                HomesList = _DbOpenDump.get_ddl_OD_REF_THREAT_FACTORS_by_type("Homes"),
                OrgList = _DbPortal.get_ddl_T_PRT_ORG_USERS_CLIENT_ByUserIDandClient(_UserIDX, "open_dump"),
                returnURL = returnURL ?? "Search"
            };

            if (id != null) {
                model.TPrtSite = _DbPortal.GetT_PRT_SITES_BySITEIDX((Guid)id);
                model.TOdSite = _DbOpenDump.getT_OD_SITES_BySITEIDX((Guid)id);

                //fail if site ID provided but not found
                if (model.TOdSite == null)
                {
                    TempData["Error"] = "Site not found.";
                    return RedirectToAction("Search", "OpenDump");
                }
            }
            else //insert case
            {
                model.TPrtSite = new T_PRT_SITES();
                model.TOdSite = new T_OD_SITES();

                //if one org, then prepopulate it in the list
                if (model.OrgList.Count() == 1)
                    model.TPrtSite.ORG_ID = model.OrgList.First().Value;
            }

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult PreField(PreFieldViewModel model)
        {
            string _UserIDX = _userManager.GetUserId(User);

            Guid? newSiteID = _DbPortal.InsertUpdateT_PRT_SITES(model.TPrtSite.SITE_IDX, model.TPrtSite.ORG_ID, model.TPrtSite.SITE_NAME ?? "",
                    model.TPrtSite.EPA_ID ?? "", model.TPrtSite.LATITUDE, model.TPrtSite.LONGITUDE, model.TPrtSite.SITE_ADDRESS ?? "", _UserIDX);

            if (newSiteID != null)
            {
                newSiteID = _DbOpenDump.InsertUpdateT_OD_SITES((Guid)newSiteID, model.TOdSite.REPORTED_BY, model.TOdSite.REPORTED_ON, model.TOdSite.COMMUNITY_IDX, model.TOdSite.SITE_SETTING_IDX,
                    model.TOdSite.PF_AQUIFER_VERT_DIST, model.TOdSite.PF_SURF_WATER_HORIZ_DIST, model.TOdSite.PF_HOMES_DIST);

                if (newSiteID != null)
                {
                    TempData["Success"] = "Update successful.";
                    return RedirectToAction("PreField", "OpenDump", new { SiteIdx = newSiteID, returnURL = model.returnURL });
                }
            }

            //error if got this far
            TempData["Error"] = "Error updating data.";
            return RedirectToAction("PreField", "OpenDump", new { SiteIdx = newSiteID, returnURL = model.returnURL });
        }

        #endregion


        #region ASSESSMENT TAB CONTROLLERS ************************************************************
        // GET: /OpenDump/Assessments
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
                        T_OD_DUMP_ASSESSMENTS = _DbOpenDump.getT_OD_DUMP_ASSESSMENTS_BySITEIDX(id.ConvertOrDefault<Guid>())
                    };
                    return View(model);
                }
            }

            //error if got this far
            TempData["Error"] = "Invalid site.";
            return RedirectToAction("Search", "OpenDump");
        }

        [HttpPost]
        public JsonResult AssessmentDelete(Guid id)
        {
            if (id != null)
            {
                int SuccID = _DbOpenDump.deleteT_OD_DumpAssessment(id);
                if (SuccID == 1)
                    return Json("Success");
            }

            return Json("Unable to delete");
        }

        public IActionResult IHSReport(Guid? id)
        {

            dynamic data = _DbOpenDump.getT_OD_DUMP_ASSESSMENTS_ByDumpAssessmentIDX(id.ConvertOrDefault<Guid>());

            var rpt = new PDFReportGen(_hostingEnvironment);
            byte[] pdf = rpt.CreatePDFReport("rpt_OpenDumpSurvey.xml", data);
            if (pdf != null)
            {
                var content = new System.IO.MemoryStream(pdf);
                return File(pdf, "application/pdf", "report.pdf");
            }
            else
            {
                TempData["Error"] = "Unable to generate document.";
                return RedirectToAction("Search", new { selStr = "", selOrg = "" });
            }
        }

        public ActionResult AssessmentDetails(Guid? id, Guid? SiteIdx)
        {
            //string _UserIDX = _userManager.GetUserId(User);

            var model = new AssessmentDetailViewModel {
                ddl_AssessmentTypeList = _DbOpenDump.get_ddl_T_OD_REF_DATA_by_category("Assessment Type")
            };

            //update case
            if (id != null && SiteIdx == null)
            {
                model.Assessment = _DbOpenDump.getT_OD_DUMP_ASSESSMENTS_ByDumpAssessmentIDX((Guid)id);
                model.files_existing = _DbOpenDump.GetT_PRT_DOCUMENTS_ByDumpAssessmentsIDx((Guid)id);
                model.filesPhoto_existing = _DbOpenDump.GetT_PRT_DOCUMENTS_Photos_ByDumpAssessmentsIDx((Guid)id);
            }
            //insert case
            else if (id == null && SiteIdx != null)
            {
                model.Assessment = new T_OD_DUMP_ASSESSMENTS {                    
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

            //populate other stuffs
            model.ddl_Assessments = _DbOpenDump.get_ddl_T_OD_DUMP_ASSESSMENTS_by_BySITEIDX(model.Assessment.SITE_IDX);

            //fail if no assessment found or created
            if (model.Assessment == null)
            {
                TempData["Error"] = "Assessment not found.";
                return RedirectToAction("Search", "OpenDump");
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


                Guid? SuccIDX = _DbOpenDump.InsertUpdateT_OD_DumpAssessment(a.DUMP_ASSESSMENTS_IDX, a.SITE_IDX, a.ASSESSMENT_DT, a.ASSESSED_BY,
                    a.ASSESSMENT_TYPE_IDX, a.ACTIVE_SITE_IND, a.SITE_DESCRIPTION, a.ASSESSMENT_NOTES, null, null, null, null, null, null, null, null, null, null,
                    null, null, null, null, null, null);

                if (SuccIDX != null) {

                    foreach (T_PRT_DOCUMENTS docs in model.filesPhoto_existing ?? Enumerable.Empty<T_PRT_DOCUMENTS>())
                        _DbPortal.InsertUpdateT_PRT_DOCUMENTS(docs.DOC_IDX, docs.ORG_ID, null, null, "Assessment", null, null, docs.DOC_COMMENT, null, null, null, _UserIDX);
                    foreach (T_PRT_DOCUMENTS docs in model.files_existing ?? Enumerable.Empty<T_PRT_DOCUMENTS>())
                        _DbPortal.InsertUpdateT_PRT_DOCUMENTS(docs.DOC_IDX, docs.ORG_ID, null, null, "Assessment", null, null, docs.DOC_COMMENT, null, null, null, _UserIDX);

                    TempData["Success"] = "Update successful.";
                    return RedirectToAction("AssessmentDetails", "OpenDump", new { id = SuccIDX });
                }

            }


            TempData["Error"] = "Error updating data.";
            return RedirectToAction("Search");
        }


        #endregion


        #region WASTE PROFILE TAB CONTROLLERS **********************************************************************
        public ActionResult WasteProfile(Guid? id)
        {
            //id is Dump Assessment ID

            var model = new WasteProfileViewModel
            {
                Assessment = _DbOpenDump.getT_OD_DUMP_ASSESSMENTS_ByDumpAssessmentIDX((Guid)id),
                ContentCheckBoxList = _DbOpenDump.getT_OD_DUMP_ASSESSMENT_CONTENT_by_AssessIDX(id),
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

                model.ddl_Assessments = _DbOpenDump.get_ddl_T_OD_DUMP_ASSESSMENTS_by_BySITEIDX(model.Assessment.SITE_IDX);

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
            return RedirectToAction("Search");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult WasteProfile(WasteProfileViewModel model)
        {
            if (model != null && model.Assessment != null)
            {
                var a = model.Assessment;
                Guid? DUMP_ASSESSMENTS_IDX = _DbOpenDump.InsertUpdateT_OD_DumpAssessment(a.DUMP_ASSESSMENTS_IDX, null, null, null, null, true, null, null, a.AREA_ACRES, 
                    a.VOLUME_CU_YD, a.HF_RAINFALL, a.HF_DRAINAGE, a.HF_FLOODING, a.HF_BURNING, a.HF_FENCING, a.HF_ACCESS_CONTROL, a.HF_PUBLIC_CONCERN, a.HEALTH_THREAT_SCORE,
                    null, null, null, null, null, null);

                foreach (SelectedWasteTypeDisplay oNew in model.ContentCheckBoxList)
                {
                    _DbOpenDump.InsertUpdateT_OD_DumpAssessment_Content(null, a.DUMP_ASSESSMENTS_IDX, oNew.T_OD_REF_WASTE_TYPE.REF_WASTE_TYPE_IDX, null, null, null, null, oNew.IsChecked);
                }
                TempData["Success"] = "Update successful.";
            }
            else
                TempData["Error"] = "Error updating data.";

            return RedirectToAction("WasteProfile", new { id = model.Assessment.DUMP_ASSESSMENTS_IDX });
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


        #region SITE CLEANUP TAB CONTROLLERS **********************************************************************
        public ActionResult Cleanup(Guid? id)
        {
            var model = new CleanupViewModel
            {
                Assessment = _DbOpenDump.getT_OD_DUMP_ASSESSMENTS_ByDumpAssessmentIDX((Guid)id),
                DumpContents = _DbOpenDump.getT_OD_DUMP_ASSESSMENT_CONTENT_ByDumpAssessmentIDX((Guid)id),
                ddl_DisposalMethod = _DbOpenDump.get_ddl_ref_disposal()
            };

            if (model.Assessment != null)
            {
                model.ddl_Assessments = _DbOpenDump.get_ddl_T_OD_DUMP_ASSESSMENTS_by_BySITEIDX(model.Assessment.SITE_IDX);

                //populate site name
                T_PRT_SITES s = _DbPortal.GetT_PRT_SITES_BySITEIDX(model.Assessment.SITE_IDX);
                if (s != null)
                {
                    model.SiteName = s.SITE_NAME;
                }

                return View(model);
            }


            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Cleanup(CleanupViewModel model)
        {
            if (model != null && model.Assessment != null && model.Assessment.DUMP_ASSESSMENTS_IDX != null)
            {
                foreach (AssessmentContentTypeDisplay oNew in model.DumpContents)
                {
                    _DbOpenDump.InsertUpdateT_OD_DumpAssessment_Content(oNew.DUMP_ASSESSMENTS_CONTENT_IDX, null, null, oNew.WASTE_AMT, oNew.UNIT_MSR_IDX, oNew.WASTE_DISPOSAL_METHOD, oNew.WASTE_DISPOSAL_DIST, true);
                }

                //recalculate costs
                _DbOpenDump.CalcCleanup(model.Assessment.DUMP_ASSESSMENTS_IDX);
                TempData["Success"] = "Update successful.";

            }
            else
                TempData["Error"] = "Error updating data.";

            return RedirectToAction("Cleanup", new { id = model.Assessment.DUMP_ASSESSMENTS_IDX });
        }

        public ActionResult Cleanup2(Guid? id)
        {
            var model = new Cleanup2ViewModel
            {
                Assessment = _DbOpenDump.getT_OD_DUMP_ASSESSMENTS_ByDumpAssessmentIDX((Guid)id),
                AssessmentCleanups = _DbOpenDump.getT_OD_DUMP_ASSESSMENT_CLEANUP_by_AssessIDX((Guid)id)
            };

            return View(model);
        }

        public ActionResult CleanupRestoration(Guid? id, string Cat)
        {
            var model = new CleanupRestorationViewModel
            {
                rESTORE_CAT = Cat,
                Assessment = _DbOpenDump.getT_OD_DUMP_ASSESSMENTS_ByDumpAssessmentIDX((Guid)id),
                Restores = _DbOpenDump.getT_OD_DUMP_ASSESSMENT_RESTORE_by_DumpAssessIDXandCat((Guid)id, Cat)
            };
            return View(model);
        }
        
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult RestoreAdd(CleanupRestorationViewModel model)
        {
            if (model != null && model.Assessment.DUMP_ASSESSMENTS_IDX != null)
            {
                Guid? SuccID = _DbOpenDump.InsertUpdateT_OD_DUMP_ASSESSMENT_RESTORE(model.edit_restore_idx, model.Assessment.DUMP_ASSESSMENTS_IDX, model.rESTORE_CAT, model.newRestoreActivity, model.newRestoreAmt, null);
                if (SuccID != null)
                {
                    //update restoration total 
                    _DbOpenDump.InsertUpdateT_OD_DumpAssessment(model.Assessment.DUMP_ASSESSMENTS_IDX, null, null, null, null, null, null, null, null, null, null, null, null,
                        null, null, null, null, null, null, null, null, 
                        model.rESTORE_CAT == "Restore" ? _DbOpenDump.getT_OD_DUMP_ASSESSMENT_RESTORE_Sum_by_AssessIDX(model.Assessment.DUMP_ASSESSMENTS_IDX, "Restore") : null,
                        model.rESTORE_CAT == "Surveil" ? _DbOpenDump.getT_OD_DUMP_ASSESSMENT_RESTORE_Sum_by_AssessIDX(model.Assessment.DUMP_ASSESSMENTS_IDX, "Surveil") : null,
                        null);

                    TempData["Success"] = "Update successful.";
                }
                else
                    TempData["Error"] = "Error updating data.";
            }
            else
                TempData["Error"] = "Error updating data.";

            return RedirectToAction("CleanupRestoration", new { id = model.Assessment.DUMP_ASSESSMENTS_IDX, Cat = model.rESTORE_CAT });
        }

        [HttpPost]
        public JsonResult CleanupRestoreDelete(Guid id)
        {
            T_OD_DUMP_ASSESSMENT_RESTORE r = _DbOpenDump.getT_OD_DUMP_ASSESSMENT_RESTORE_by_IDX(id);
            if (r != null)
            {
                int SuccID = _DbOpenDump.DeleteT_OD_DUMP_ASSESSMENT_RESTORE(id);
                if (SuccID == 1)
                {
                    //update restoration total 
                    _DbOpenDump.InsertUpdateT_OD_DumpAssessment(r.DUMP_ASSESSMENTS_IDX, null, null, null, null, null, null, null, null, null, null, null, null,
                        null, null, null, null, null, null, null, null,
                        r.RESTORE_CAT == "Restore" ? _DbOpenDump.getT_OD_DUMP_ASSESSMENT_RESTORE_Sum_by_AssessIDX(r.DUMP_ASSESSMENTS_IDX, "Restore") : null,
                        r.RESTORE_CAT == "Surveil" ? _DbOpenDump.getT_OD_DUMP_ASSESSMENT_RESTORE_Sum_by_AssessIDX(r.DUMP_ASSESSMENTS_IDX, "Surveil") : null,
                        null);

                    return Json("Success");
                }
            }

            return Json("Unable to delete");
        }

        #endregion




        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult DocUpload(AssessmentDetailViewModel model)
        {
            if (model != null)
            {
                if (model.files != null)
                {                   
                    byte[] fileBytes = null;

                    if (model.files != null)
                    {
                        if (model.files == null || model.files.Length == 0)
                            return Content("file not selected");

                        var path = Path.Combine(
                                    Directory.GetCurrentDirectory(), "wwwroot",
                                    model.files.FileName);
                        MemoryStream memoryStream = new MemoryStream();
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            model.files.CopyToAsync(memoryStream);
                            fileBytes = memoryStream.ToArray();

                            string _UserIDX = _userManager.GetUserId(User);

                            //insert to database
                            T_PRT_SITES s = _DbPortal.GetT_PRT_SITES_BySITEIDX(model.Assessment.SITE_IDX);

                            Guid? DocIDx = _DbPortal.InsertUpdateT_PRT_DOCUMENTS(null, s.ORG_ID, fileBytes, model.files.FileName, "Assessment", model.files.ContentType, fileBytes.Length, model.FileDescription, null, null, null, _UserIDX);
                            _DbOpenDump.InsertUpdateT_OD_DUMP_ASSESSMENT_DOCUMENTS(DocIDx, model.Assessment.DUMP_ASSESSMENTS_IDX);

                        }
                    }
                   
                }
              
                TempData["Success"] = "Update successful.";
                return RedirectToAction("AssessmentDetails", "OpenDump", new { AssessmentIdx = model.Assessment.DUMP_ASSESSMENTS_IDX });
            }
            else
                TempData["Error"] = "Error updating data.";
            return RedirectToAction("Search", new { selStr = "", selOrg = "" });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult PhotoUpload(AssessmentDetailViewModel model)
        {
            if (model != null)
            {
                if (model.filesPhoto != null)
                {
                    byte[] fileBytes = null;

                    if (model.filesPhoto != null)
                    {
                        if (model.filesPhoto == null || model.filesPhoto.Length == 0)
                            return Content("file not selected");

                        var path = Path.Combine(
                                    Directory.GetCurrentDirectory(), "wwwroot",
                                    model.filesPhoto.FileName);
                        MemoryStream memoryStream = new MemoryStream();
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            model.filesPhoto.CopyToAsync(memoryStream);
                            fileBytes = memoryStream.ToArray();

                            string _UserIDX = _userManager.GetUserId(User);
                            //insert to database
                            T_PRT_SITES s = _DbPortal.GetT_PRT_SITES_BySITEIDX(model.Assessment.SITE_IDX);

                            Guid? DocIDx = _DbPortal.InsertUpdateT_PRT_DOCUMENTS(null, s.ORG_ID, fileBytes, model.filesPhoto.FileName, "Assessment", model.filesPhoto.ContentType, fileBytes.Length, model.FileDescription, null, null, null, _UserIDX);
                            _DbOpenDump.InsertUpdateT_OD_DUMP_ASSESSMENT_DOCUMENTS(DocIDx, model.Assessment.DUMP_ASSESSMENTS_IDX);

                        }
                    }

                }

                TempData["Success"] = "Update successful.";
                //return RedirectToAction("AssessmentDetails", "OpenDump", new { AssessmentIdx = DUMP_ASSESSMENTS_IDX });
            }
            else
                TempData["Error"] = "Error updating data.";
            return RedirectToAction("Search", new { selStr = "", selOrg = "" });
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
            return RedirectToAction("Search", new { selStr = "", selOrg = "" });
        }

        public ActionResult FileDelete(Guid? id, Guid SiteIdx)
        {
            // int UserIDX = db_Accounts.GetUserIDX();

            //get project, then org to check permissions
            T_PRT_DOCUMENTS doc = _DbPortal.GetT_PRT_DOCUMENTS_ByID((Guid)id);
            if (doc != null)
            {
                int SuccID = _DbPortal.DeleteT_PRT_DOCUMENTS((Guid)id);
                if (SuccID > 0)
                {
                    TempData["Success"] = "File removed.";
                    //return RedirectToAction("AssessmentDetails", "OpenDump", new { AssessmentIdx = model.Assessment.DUMP_ASSESSMENTS_IDX });
                }
               
            }                   

            TempData["Error"] = "Unable to delete document.";
            return RedirectToAction("Search", new { selStr = "", selOrg = "" });
        }




        public IActionResult RefData()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult DumpParcels()
        {
            //var identity = (ClaimsIdentity)User.Identity;
            //IEnumerable<Claim> claims = identity.Claims;

            return View();
        }

    }
}