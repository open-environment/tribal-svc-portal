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
    public class PreFieldViewComponent : ViewComponent
    {
        private readonly IDbPortal _DbPortal;
        private readonly IDbOpenDump _DbOpenDump;
        private readonly IMemoryCache _memoryCache;
        public PreFieldViewComponent(
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
                FieldAssessmentmodel.AssessmentForHealthThreatDropDownList = _DbOpenDump.get_ddl_od_assessmentforhealththreat_by_BySITEIDX((Guid)SiteIdx);
                FieldAssessmentmodel.TOdDumpAssessmentsGridList = _DbOpenDump.GetT_OD_DumpAssessmentList_BySITEIDX((Guid)SiteIdx);

            }
            else
            {
                FieldAssessmentmodel.AssessmentDropDownList = _DbOpenDump.get_ddl_od_dumpassessment_by_BySITEIDX(Guid.NewGuid());
                FieldAssessmentmodel.AssessmentForHealthThreatDropDownList = _DbOpenDump.get_ddl_od_assessmentforhealththreat_by_BySITEIDX((Guid)SiteIdx);
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
                FieldAssessmentmodel.AssessmentForHealthThreatDropDownList = _DbOpenDump.get_ddl_od_assessmentforhealththreat_by_BySITEIDX((Guid)SiteIdx);
                FieldAssessmentmodel.TOdDumpAssessmentsGridList = _DbOpenDump.GetT_OD_DumpAssessmentList_BySITEIDX(oT_OD_DUMP_ASSESSMENTS.SITE_IDX);
                FieldAssessmentmodel.TOdDumpAssessments = oT_OD_DUMP_ASSESSMENTS;
            }
            else if (SiteIdx != null && AssessmentIdx == null)
            {
                FieldAssessmentmodel.TPrtSites = _DbPortal.GetT_PRT_SITES_BySITEIDX((Guid)SiteIdx);
                FieldAssessmentmodel.AssessmentDropDownList = _DbOpenDump.get_ddl_od_dumpassessment_by_BySITEIDX((Guid)SiteIdx);
                FieldAssessmentmodel.AssessmentForHealthThreatDropDownList = _DbOpenDump.get_ddl_od_assessmentforhealththreat_by_BySITEIDX((Guid)SiteIdx);
                FieldAssessmentmodel.TOdDumpAssessmentsGridList = _DbOpenDump.GetT_OD_DumpAssessmentList_BySITEIDX((Guid)SiteIdx);
                FieldAssessmentmodel.TOdDumpAssessments = oT_OD_DUMP_ASSESSMENTS;
                FieldAssessmentmodel.TOdDumpAssessments.DUMP_ASSESSMENTS_IDX = Guid.NewGuid();
            }
            else if (SiteIdx != null && AssessmentIdx != null)
            {
                FieldAssessmentmodel.TPrtSites = _DbPortal.GetT_PRT_SITES_BySITEIDX((Guid)SiteIdx);
                FieldAssessmentmodel.AssessmentDropDownList = _DbOpenDump.get_ddl_od_dumpassessment_by_BySITEIDX((Guid)SiteIdx);
                FieldAssessmentmodel.AssessmentForHealthThreatDropDownList = _DbOpenDump.get_ddl_od_assessmentforhealththreat_by_BySITEIDX((Guid)SiteIdx);
                FieldAssessmentmodel.TOdDumpAssessmentsGridList = _DbOpenDump.GetT_OD_DumpAssessmentList_BySITEIDX((Guid)SiteIdx);
                FieldAssessmentmodel.TOdDumpAssessments = oT_OD_DUMP_ASSESSMENTS;
            }
            else
            {
                FieldAssessmentmodel.AssessmentDropDownList = _DbOpenDump.get_ddl_od_dumpassessment_by_BySITEIDX(Guid.NewGuid());
                FieldAssessmentmodel.AssessmentForHealthThreatDropDownList = _DbOpenDump.get_ddl_od_assessmentforhealththreat_by_BySITEIDX((Guid)SiteIdx);
                //FieldAssessmentmodel.TPrtSites = _DbPortal.GetT_PRT_SITES_BySITEIDX((Guid)SiteIdx);
                FieldAssessmentmodel.TOdDumpAssessments = new T_OD_DUMP_ASSESSMENTS();
                FieldAssessmentmodel.TOdDumpAssessments.DUMP_ASSESSMENTS_IDX = Guid.NewGuid();
                FieldAssessmentmodel.TOdDumpAssessments.ASSESSMENT_DT = DateTime.Now;
            }
           
            return FieldAssessmentmodel;
           
        }
    }
}
