﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using TribalSvcPortal.Data.Models;
using TribalSvcPortal.ViewModels.OpenDumpViewModels;
using TribalSvcPortal.AppLogic.DataAccessLayer;
using Microsoft.Extensions.Caching.Memory;

namespace TribalSvcPortal.Controllers
{
    public class OpenDumpController : Controller
    {

        private readonly IDbPortal _DbPortal;
        private readonly IDbOpenDump _DbOpenDump;
        private readonly IMemoryCache _memoryCache;

        public OpenDumpController(
            IMemoryCache memoryCache,
            IDbPortal DbPortal,
            IDbOpenDump DbOpenDump)
        {
            _DbPortal = DbPortal;
            _DbOpenDump = DbOpenDump;
            _memoryCache = memoryCache;
        }

        public IActionResult Index()
        {
            return View();
        }


        public ActionResult Search(string selStr, string selOrg)
        {
            string _UserIDX;
            bool isUserExist = _memoryCache.TryGetValue("UserID", out _UserIDX);

            if (selStr == null && selOrg == null)
            {
                SearchViewModel model = new SearchViewModel();
                model.ddl_Org = _DbOpenDump.get_ddl_od_organizations(_UserIDX);
                model.searchResults = _DbOpenDump.get_AllOpenDump_Sites(_UserIDX);

                if (model.ddl_Org.Count() == 1)
                {
                    foreach (var orgid in model.ddl_Org)
                    {
                        model.selOrg = orgid.Value;
                    }
                }
                return View(model);
            }
            else
            {
                var model = new SearchViewModel
                {
                    ddl_Org = _DbOpenDump.get_ddl_od_organizations(_UserIDX),
                    searchResults = _DbOpenDump.get_OpenDump_Sites_By_Organization_SiteName(selStr, selOrg)
                };
                if (model.ddl_Org.Count() == 1)
                {
                    foreach (var orgid in model.ddl_Org)
                    {
                        model.selOrg = orgid.Value;
                    }
                }
                return View(model);
            }

        }


        // GET: /OpenDump/PreField
        public ActionResult PreField(Guid? SiteIdx, string returnURL, Guid? AssessmentIdx, bool CreateAssessment, OpenDumpTab activeTab = OpenDumpTab.Prefield)
        {
            string _UserIDX;
            bool isUserExist = _memoryCache.TryGetValue("UserID", out _UserIDX);
            OpenDumpViewModel openDumpViewModel = new OpenDumpViewModel();
            var PreFieldmodel = new PreFieldViewModel();
            var FieldAssessmentmodel = new FieldAssessmentViewModel();
            FieldAssessmentmodel.AssessmentTypeList = _DbOpenDump.get_ddl_refdata_by_category("Assessment Type");

            PreFieldmodel.SiteSettingsList = _DbOpenDump.get_ddl_refdata_by_category("Site Setting");
            PreFieldmodel.CommunityList = _DbOpenDump.get_ddl_refdata_by_category("Community");
            PreFieldmodel.AquiferList = _DbOpenDump.get_ddl_refthreatfactor_by_factortype("Aquifer");
            PreFieldmodel.SurfaceWaterList = _DbOpenDump.get_ddl_refthreatfactor_by_factortype("Surface Water");
            PreFieldmodel.HomesList = _DbOpenDump.get_ddl_refthreatfactor_by_factortype("Homes");
            PreFieldmodel.OrgList = _DbOpenDump.get_ddl_od_organizations(_UserIDX);
            PreFieldmodel.returnURL = returnURL ?? "Search";
            string IDx = "98567684-a5d5-4742-ac6d-1dd5080f76a7";
            FieldAssessmentmodel.selDumpAssessmentIdx = Guid.Parse(IDx);
            if (SiteIdx != null)
            {
                PreFieldmodel.TPrtSites = _DbPortal.GetT_PRT_SITES_BySITEIDX((Guid)SiteIdx);
                PreFieldmodel.TOdSites = _DbOpenDump.GetT_OD_SITES_BySITEIDX((Guid)SiteIdx);
                FieldAssessmentmodel.TPrtSites = _DbPortal.GetT_PRT_SITES_BySITEIDX((Guid)SiteIdx);
                FieldAssessmentmodel.AssessmentDropDownList = _DbOpenDump.get_ddl_od_dumpassessment_by_BySITEIDX((Guid)SiteIdx);
                FieldAssessmentmodel.TOdDumpAssessmentsGridList = _DbOpenDump.GetT_OD_DumpAssessmentList_BySITEIDX((Guid)SiteIdx);

            }
            else
            {
                FieldAssessmentmodel.AssessmentDropDownList = _DbOpenDump.get_ddl_od_dumpassessment_by_BySITEIDX(Guid.NewGuid());
                // FieldAssessmentmodel.AssessmentDropDownList = _DbOpenDump.get_ddl_od_dumpassessment_by_BySITEIDX(Guid.Parse(IDx));
                PreFieldmodel.TPrtSites = new T_PRT_SITES();
                if (PreFieldmodel.OrgList.Count() == 1)
                {
                    foreach (var orgid in PreFieldmodel.OrgList)
                    {
                        PreFieldmodel.TPrtSites.OrgId = orgid.Value;
                    }
                }
                PreFieldmodel.TPrtSites.SiteIdx = Guid.NewGuid();
                FieldAssessmentmodel.TOdDumpAssessments = new T_OD_DUMP_ASSESSMENTS();
                FieldAssessmentmodel.TOdDumpAssessments.DUMP_ASSESSMENTS_IDX = Guid.NewGuid();
                FieldAssessmentmodel.TOdDumpAssessments.ASSESSMENT_DT = DateTime.Now;
            }
            if ((AssessmentIdx != null || CreateAssessment) && AssessmentIdx != Guid.Parse(IDx))
                FieldAssessmentmodel = this.GetFieldAssessment(AssessmentIdx, SiteIdx);
            openDumpViewModel.oPreFieldViewModel = PreFieldmodel;
            openDumpViewModel.oFieldAssessmentViewModel = FieldAssessmentmodel;
            openDumpViewModel.ActiveTab = activeTab;
            return View(openDumpViewModel);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult OpenDumpEdit(OpenDumpViewModel model)
        {
            //if (model.TPrtSites.SiteName == null)
            //    ModelState.AddModelError("SiteName", "SiteName is required");

            string _UserIDX;
            bool isUserExist = _memoryCache.TryGetValue("UserID", out _UserIDX);

            Guid? newSiteID = _DbPortal.InsertUpdateT_PRT_SITES(model.oPreFieldViewModel.TPrtSites.SiteIdx, model.oPreFieldViewModel.TPrtSites.OrgId, model.oPreFieldViewModel.TPrtSites.SiteName ?? "",
                    model.oPreFieldViewModel.TPrtSites.EpaId ?? "", model.oPreFieldViewModel.TPrtSites.Latitude, model.oPreFieldViewModel.TPrtSites.Longitude, model.oPreFieldViewModel.TPrtSites.SiteAddress ?? "", _UserIDX);

            if (newSiteID != null)
            {
                Guid? SiteID = _DbOpenDump.InsertUpdateT_OD_SITES((Guid)newSiteID, model.oPreFieldViewModel.TOdSites.REPORTED_BY, model.oPreFieldViewModel.TOdSites.REPORTED_ON, model.oPreFieldViewModel.TOdSites.COMMUNITY_IDX,
                    model.oPreFieldViewModel.TOdSites.SITE_SETTING_IDX, model.oPreFieldViewModel.TOdSites.PF_AQUIFER_VERT_DIST, model.oPreFieldViewModel.TOdSites.PF_SURF_WATER_HORIZ_DIST, model.oPreFieldViewModel.TOdSites.PF_HOMES_DIST);

                TempData["Success"] = "Update successful.";
                return RedirectToAction("PreField", "OpenDump", new { SiteIdx = newSiteID, returnURL = model.oPreFieldViewModel.returnURL });
            }
            else
                TempData["Error"] = "Error updating data.";
            return RedirectToAction(model.oPreFieldViewModel.returnURL ?? "Search", new { selStr = "", selOrg = "" });
        }
        [HttpPost]
        public JsonResult PreFieldDelete(Guid id)
        {
            if (id != null)
            {
                int SuccID = _DbPortal.DeleteT_PRT_SITES(id);

                if (SuccID == 1)
                    return Json("Success");
            }

            return Json("Unable to delete");
        }

        // GET: /OpenDump/FieldAssessment
        [HttpGet]
        public ActionResult FieldAssessments(Guid? AssessmentIdx, Guid? SiteIdx)
        {
            //string _UserIDX;
            //OpenDumpViewModel openDumpViewModel = new OpenDumpViewModel();
            //bool isUserExist = _memoryCache.TryGetValue("UserID", out _UserIDX);
            //T_OD_DUMP_ASSESSMENTS oT_OD_DUMP_ASSESSMENTS = new T_OD_DUMP_ASSESSMENTS();
            //if (AssessmentIdx != null)
            //{
            //    oT_OD_DUMP_ASSESSMENTS = _DbOpenDump.GetT_OD_DumpAssessment_ByDumpAssessmentIDX((Guid)AssessmentIdx);
            //}
            ////OpenDumpViewModel openDumpViewModel = new OpenDumpViewModel();
            ////var PreFieldmodel = new PreFieldViewModel();
            //var FieldAssessmentmodel = new FieldAssessmentViewModel();

            //FieldAssessmentmodel.AssessmentTypeList = _DbOpenDump.get_ddl_refdata_by_category("Assessment Type");

            ////model.SiteSettingsList = _DbOpenDump.get_ddl_refdata_by_category("Site Setting");
            ////model.CommunityList = _DbOpenDump.get_ddl_refdata_by_category("Community");
            ////model.AquiferList = _DbOpenDump.get_ddl_refthreatfactor_by_factortype("Aquifer");
            ////model.SurfaceWaterList = _DbOpenDump.get_ddl_refthreatfactor_by_factortype("Surface Water");
            ////model.HomesList = _DbOpenDump.get_ddl_refthreatfactor_by_factortype("Homes");
            ////model.OrgList = _DbOpenDump.get_ddl_od_organizations(_UserIDX);
            ////model.returnURL = "Search";
            //if (AssessmentIdx != null)
            //{
            //    FieldAssessmentmodel.selDumpAssessmentIdx = (Guid)AssessmentIdx;
            //}
            //else
            //{
            //    FieldAssessmentmodel.selDumpAssessmentIdx = Guid.NewGuid();
            //}
            //if (AssessmentIdx != null && SiteIdx == null)
            //{
            //    FieldAssessmentmodel.TPrtSites = _DbPortal.GetT_PRT_SITES_BySITEIDX(oT_OD_DUMP_ASSESSMENTS.SITE_IDX);
            //    FieldAssessmentmodel.AssessmentDropDownList = _DbOpenDump.get_ddl_od_dumpassessment_by_BySITEIDX(oT_OD_DUMP_ASSESSMENTS.SITE_IDX);
            //    FieldAssessmentmodel.TOdDumpAssessmentsGridList = _DbOpenDump.GetT_OD_DumpAssessmentList_BySITEIDX(oT_OD_DUMP_ASSESSMENTS.SITE_IDX);
            //    FieldAssessmentmodel.TOdDumpAssessments = oT_OD_DUMP_ASSESSMENTS;
            //}
            //else if (SiteIdx != null && AssessmentIdx == null)
            //{
            //    FieldAssessmentmodel.TPrtSites = _DbPortal.GetT_PRT_SITES_BySITEIDX((Guid)SiteIdx);
            //    FieldAssessmentmodel.AssessmentDropDownList = _DbOpenDump.get_ddl_od_dumpassessment_by_BySITEIDX((Guid)SiteIdx);
            //    FieldAssessmentmodel.TOdDumpAssessmentsGridList = _DbOpenDump.GetT_OD_DumpAssessmentList_BySITEIDX((Guid)SiteIdx);
            //    FieldAssessmentmodel.TOdDumpAssessments = oT_OD_DUMP_ASSESSMENTS;
            //    FieldAssessmentmodel.TOdDumpAssessments.DUMP_ASSESSMENTS_IDX = Guid.NewGuid();
            //}
            //else if (SiteIdx != null && AssessmentIdx != null)
            //{
            //    FieldAssessmentmodel.TPrtSites = _DbPortal.GetT_PRT_SITES_BySITEIDX((Guid)SiteIdx);
            //    FieldAssessmentmodel.AssessmentDropDownList = _DbOpenDump.get_ddl_od_dumpassessment_by_BySITEIDX((Guid)SiteIdx);
            //    FieldAssessmentmodel.TOdDumpAssessmentsGridList = _DbOpenDump.GetT_OD_DumpAssessmentList_BySITEIDX((Guid)SiteIdx);
            //    FieldAssessmentmodel.TOdDumpAssessments = oT_OD_DUMP_ASSESSMENTS;
            //}
            //else
            //{
            //    FieldAssessmentmodel.AssessmentDropDownList = _DbOpenDump.get_ddl_od_dumpassessment_by_BySITEIDX(Guid.NewGuid());
            //    //FieldAssessmentmodel.TPrtSites = _DbPortal.GetT_PRT_SITES_BySITEIDX((Guid)SiteIdx);
            //    FieldAssessmentmodel.TOdDumpAssessments = new T_OD_DUMP_ASSESSMENTS();
            //    FieldAssessmentmodel.TOdDumpAssessments.DUMP_ASSESSMENTS_IDX = Guid.NewGuid();
            //}
            //openDumpViewModel.oFieldAssessmentViewModel = FieldAssessmentmodel;
            //return View(openDumpViewModel);
            //  return PartialView("_FieldAssessments", FieldAssessmentmodel);
            // return PartialView("_FieldAssessments", FieldAssessmentmodel);
            //return new EmptyResult();
            return RedirectToAction(nameof(PreField), new { SiteIdx = SiteIdx, returnURL = "Search", AssessmentIdx = AssessmentIdx, CreateAssessment = AssessmentIdx == null ? true : false, activeTab = OpenDumpTab.FieldAssessment });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult FieldAssessmentEdit(OpenDumpViewModel model)
        {
            if (model != null)
            {
                Guid? SiteID = _DbOpenDump.InsertUpdateT_OD_DumpAssessment(model.oFieldAssessmentViewModel.selDumpAssessmentIdx, model.oPreFieldViewModel.TPrtSites.SiteIdx, model.oFieldAssessmentViewModel.TOdDumpAssessments.ASSESSMENT_DT, model.oFieldAssessmentViewModel.TOdDumpAssessments.ASSESSED_BY,
                    model.oFieldAssessmentViewModel.TOdDumpAssessments.ASSESSMENT_TYPE_IDX, model.oFieldAssessmentViewModel.TOdDumpAssessments.ACTIVE_SITE_IND, model.oFieldAssessmentViewModel.TOdDumpAssessments.SITE_DESCRIPTION, model.oFieldAssessmentViewModel.TOdDumpAssessments.ASSESSMENT_NOTES);

                TempData["Success"] = "Update successful.";
                return RedirectToAction("PreField", "OpenDump", new { SiteIdx = model.oPreFieldViewModel.TPrtSites.SiteIdx, returnURL = "Search" });
            }
            else
                TempData["Error"] = "Error updating data.";
            return RedirectToAction("Search", new { selStr = "", selOrg = "" });
        }
        [HttpPost]
        public JsonResult FieldAssessmentDelete(Guid id)
        {
            if (id != null)
            {
                int SuccID = _DbOpenDump.DeleteT_OD_DumpAssessment(id);

                if (SuccID == 1)
                    return Json("Success");
            }

            return Json("Unable to delete");
        }
        public IActionResult RefData()
        {
            return View();
        }
        public IActionResult DumpParcels()
        {
            return View();
        }
        //[NonAction]
        public FieldAssessmentViewModel GetFieldAssessment(Guid? AssessmentIdx, Guid? SiteIdx)
        {
            string _UserIDX;
            OpenDumpViewModel openDumpViewModel = new OpenDumpViewModel();
            bool isUserExist = _memoryCache.TryGetValue("UserID", out _UserIDX);
            T_OD_DUMP_ASSESSMENTS oT_OD_DUMP_ASSESSMENTS = new T_OD_DUMP_ASSESSMENTS();
            if (AssessmentIdx != null)
            {
                oT_OD_DUMP_ASSESSMENTS = _DbOpenDump.GetT_OD_DumpAssessment_ByDumpAssessmentIDX((Guid)AssessmentIdx);
            }
            //OpenDumpViewModel openDumpViewModel = new OpenDumpViewModel();
            //var PreFieldmodel = new PreFieldViewModel();
            var FieldAssessmentmodel = new FieldAssessmentViewModel();

            FieldAssessmentmodel.AssessmentTypeList = _DbOpenDump.get_ddl_refdata_by_category("Assessment Type");

            //model.SiteSettingsList = _DbOpenDump.get_ddl_refdata_by_category("Site Setting");
            //model.CommunityList = _DbOpenDump.get_ddl_refdata_by_category("Community");
            //model.AquiferList = _DbOpenDump.get_ddl_refthreatfactor_by_factortype("Aquifer");
            //model.SurfaceWaterList = _DbOpenDump.get_ddl_refthreatfactor_by_factortype("Surface Water");
            //model.HomesList = _DbOpenDump.get_ddl_refthreatfactor_by_factortype("Homes");
            //model.OrgList = _DbOpenDump.get_ddl_od_organizations(_UserIDX);
            //model.returnURL = "Search";
            if (AssessmentIdx != null)
            {
                FieldAssessmentmodel.selDumpAssessmentIdx = (Guid)AssessmentIdx;
            }
            else
            {
                FieldAssessmentmodel.selDumpAssessmentIdx = Guid.NewGuid();
            }
            if (AssessmentIdx != null && SiteIdx == null)
            {
                FieldAssessmentmodel.TPrtSites = _DbPortal.GetT_PRT_SITES_BySITEIDX(oT_OD_DUMP_ASSESSMENTS.SITE_IDX);
                FieldAssessmentmodel.AssessmentDropDownList = _DbOpenDump.get_ddl_od_dumpassessment_by_BySITEIDX(oT_OD_DUMP_ASSESSMENTS.SITE_IDX);
                FieldAssessmentmodel.TOdDumpAssessmentsGridList = _DbOpenDump.GetT_OD_DumpAssessmentList_BySITEIDX(oT_OD_DUMP_ASSESSMENTS.SITE_IDX);
                FieldAssessmentmodel.TOdDumpAssessments = oT_OD_DUMP_ASSESSMENTS;
            }
            else if (SiteIdx != null && AssessmentIdx == null)
            {
                FieldAssessmentmodel.TPrtSites = _DbPortal.GetT_PRT_SITES_BySITEIDX((Guid)SiteIdx);
                FieldAssessmentmodel.AssessmentDropDownList = _DbOpenDump.get_ddl_od_dumpassessment_by_BySITEIDX((Guid)SiteIdx);
                FieldAssessmentmodel.TOdDumpAssessmentsGridList = _DbOpenDump.GetT_OD_DumpAssessmentList_BySITEIDX((Guid)SiteIdx);
                FieldAssessmentmodel.TOdDumpAssessments = oT_OD_DUMP_ASSESSMENTS;
                FieldAssessmentmodel.TOdDumpAssessments.DUMP_ASSESSMENTS_IDX = Guid.NewGuid();
            }
            else if (SiteIdx != null && AssessmentIdx != null)
            {
                FieldAssessmentmodel.TPrtSites = _DbPortal.GetT_PRT_SITES_BySITEIDX((Guid)SiteIdx);
                FieldAssessmentmodel.AssessmentDropDownList = _DbOpenDump.get_ddl_od_dumpassessment_by_BySITEIDX((Guid)SiteIdx);
                FieldAssessmentmodel.TOdDumpAssessmentsGridList = _DbOpenDump.GetT_OD_DumpAssessmentList_BySITEIDX((Guid)SiteIdx);
                FieldAssessmentmodel.TOdDumpAssessments = oT_OD_DUMP_ASSESSMENTS;
            }
            else
            {
                FieldAssessmentmodel.AssessmentDropDownList = _DbOpenDump.get_ddl_od_dumpassessment_by_BySITEIDX(Guid.NewGuid());
                //FieldAssessmentmodel.TPrtSites = _DbPortal.GetT_PRT_SITES_BySITEIDX((Guid)SiteIdx);
                FieldAssessmentmodel.TOdDumpAssessments = new T_OD_DUMP_ASSESSMENTS();
                FieldAssessmentmodel.TOdDumpAssessments.DUMP_ASSESSMENTS_IDX = Guid.NewGuid();
                FieldAssessmentmodel.TOdDumpAssessments.ASSESSMENT_DT = DateTime.Now;
            }
            //openDumpViewModel.oFieldAssessmentViewModel = FieldAssessmentmodel;
            //return View(openDumpViewModel);
            //  return PartialView("_FieldAssessments", FieldAssessmentmodel);
            // return PartialView("_FieldAssessments", FieldAssessmentmodel);
            //return new EmptyResult();
            return FieldAssessmentmodel;
            //  return RedirectToAction(nameof(PreField), new { SiteIdx = SiteIdx, returnURL = "Search", assessmentModel = FieldAssessmentmodel });
        }
    }
}