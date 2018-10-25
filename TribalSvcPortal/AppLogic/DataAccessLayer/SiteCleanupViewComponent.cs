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
    public class SiteCleanupViewComponent: ViewComponent
    {
        private readonly IDbPortal _DbPortal;
        private readonly IDbOpenDump _DbOpenDump;
        private readonly IMemoryCache _memoryCache;
        public SiteCleanupViewComponent(
           IDbPortal DbPortal,
           IMemoryCache memoryCache,
            IDbOpenDump DbOpenDump
           )
        {
            _memoryCache = memoryCache;
            _DbPortal = DbPortal;
            _DbOpenDump = DbOpenDump;
        }
        public IViewComponentResult Invoke(Guid? SiteIdx, Guid? AssessmentIdx, OpenDumpTab activeTab = OpenDumpTab.Prefield)
        {
            string _UserIDX;
            OpenDumpViewModel openDumpViewModel = new OpenDumpViewModel();
            bool isUserExist = _memoryCache.TryGetValue("UserID", out _UserIDX);
            T_OD_DUMP_ASSESSMENTS oT_OD_DUMP_ASSESSMENTS = new T_OD_DUMP_ASSESSMENTS();
            if (AssessmentIdx != null)
            {
                oT_OD_DUMP_ASSESSMENTS = _DbOpenDump.GetT_OD_DumpAssessment_ByDumpAssessmentIDX((Guid)AssessmentIdx);
            }
            var PreFieldmodel = new PreFieldViewModel();
            var FieldAssessmentmodel = new FieldAssessmentViewModel();

            FieldAssessmentmodel.AssessmentTypeList = _DbOpenDump.get_ddl_refdata_by_category("Assessment Type");
            FieldAssessmentmodel.DisposalMethodList = _DbOpenDump.get_ddl_ref_disposal();
            //PreFieldmodel.SiteSettingsList = _DbOpenDump.get_ddl_refdata_by_category("Site Setting");
            //PreFieldmodel.CommunityList = _DbOpenDump.get_ddl_refdata_by_category("Community");
            //PreFieldmodel.AquiferList = _DbOpenDump.get_ddl_refthreatfactor_by_factortype("Aquifer");
            //PreFieldmodel.SurfaceWaterList = _DbOpenDump.get_ddl_refthreatfactor_by_factortype("Surface Water");
            //PreFieldmodel.HomesList = _DbOpenDump.get_ddl_refthreatfactor_by_factortype("Homes");
            PreFieldmodel.OrgList = _DbOpenDump.get_ddl_od_organizations(_UserIDX);
            PreFieldmodel.returnURL = "Search";
            if (AssessmentIdx != null)
            {
                FieldAssessmentmodel.selDumpAssessmentIdx = (Guid)AssessmentIdx;
            }
            else
            {
                string IDx = "98567684-a5d5-4742-ac6d-1dd5080f76a7";
                FieldAssessmentmodel.selDumpAssessmentIdx = Guid.Parse(IDx);             
            }
            FieldAssessmentmodel.DisposalMethodList = _DbOpenDump.get_ddl_ref_disposal();
            if (AssessmentIdx != null && SiteIdx == null)
            {
                PreFieldmodel.TPrtSites = _DbPortal.GetT_PRT_SITES_BySITEIDX(oT_OD_DUMP_ASSESSMENTS.SITE_IDX);
                PreFieldmodel.TOdSites = _DbOpenDump.GetT_OD_SITES_BySITEIDX(oT_OD_DUMP_ASSESSMENTS.SITE_IDX);
                FieldAssessmentmodel.TPrtSites = _DbPortal.GetT_PRT_SITES_BySITEIDX(oT_OD_DUMP_ASSESSMENTS.SITE_IDX);
                FieldAssessmentmodel.AssessmentDropDownList = _DbOpenDump.get_ddl_od_dumpassessment_by_BySITEIDX(oT_OD_DUMP_ASSESSMENTS.SITE_IDX);
                FieldAssessmentmodel.AssessmentForHealthThreatDropDownList = _DbOpenDump.get_ddl_od_assessmentforhealththreat_by_BySITEIDX((Guid)SiteIdx);
                FieldAssessmentmodel.TOdDumpAssessmentsGridList = _DbOpenDump.GetT_OD_DumpAssessmentList_BySITEIDX(oT_OD_DUMP_ASSESSMENTS.SITE_IDX);
                FieldAssessmentmodel.TOdDumpAssessments = oT_OD_DUMP_ASSESSMENTS;
                FieldAssessmentmodel.files_existing = _DbPortal.GetT_PRT_DOCUMENTS_ByDumpAssessmentsIDx((Guid)AssessmentIdx);
                FieldAssessmentmodel.filesPhoto_existing = _DbPortal.GetT_PRT_DOCUMENTS_Photos_ByDumpAssessmentsIDx((Guid)AssessmentIdx);
                FieldAssessmentmodel.WasteAmountList = _DbOpenDump.GetT_OD_DumpAssessmentContent_ByDumpAssessmentIDX((Guid)AssessmentIdx);
            }
            else if (SiteIdx != null && AssessmentIdx == null)
            {
                PreFieldmodel.TPrtSites = _DbPortal.GetT_PRT_SITES_BySITEIDX((Guid)SiteIdx);
                PreFieldmodel.TOdSites = _DbOpenDump.GetT_OD_SITES_BySITEIDX((Guid)SiteIdx);
                FieldAssessmentmodel.TPrtSites = _DbPortal.GetT_PRT_SITES_BySITEIDX((Guid)SiteIdx);
                FieldAssessmentmodel.AssessmentDropDownList = _DbOpenDump.get_ddl_od_dumpassessment_by_BySITEIDX((Guid)SiteIdx);
                FieldAssessmentmodel.AssessmentForHealthThreatDropDownList = _DbOpenDump.get_ddl_od_assessmentforhealththreat_by_BySITEIDX((Guid)SiteIdx);
                FieldAssessmentmodel.TOdDumpAssessmentsGridList = _DbOpenDump.GetT_OD_DumpAssessmentList_BySITEIDX((Guid)SiteIdx);
               
                FieldAssessmentmodel.TOdDumpAssessments = new T_OD_DUMP_ASSESSMENTS();
                FieldAssessmentmodel.TOdDumpAssessments.DUMP_ASSESSMENTS_IDX = Guid.NewGuid();
                //FieldAssessmentmodel.TOdDumpAssessments.ASSESSMENT_DT = DateTime.Now;
            }
            else if (SiteIdx != null && AssessmentIdx != null)
            {
                PreFieldmodel.TPrtSites = _DbPortal.GetT_PRT_SITES_BySITEIDX((Guid)SiteIdx);
                PreFieldmodel.TOdSites = _DbOpenDump.GetT_OD_SITES_BySITEIDX((Guid)SiteIdx);
                FieldAssessmentmodel.TPrtSites = _DbPortal.GetT_PRT_SITES_BySITEIDX((Guid)SiteIdx);
                FieldAssessmentmodel.AssessmentDropDownList = _DbOpenDump.get_ddl_od_dumpassessment_by_BySITEIDX((Guid)SiteIdx);
                FieldAssessmentmodel.AssessmentForHealthThreatDropDownList = _DbOpenDump.get_ddl_od_assessmentforhealththreat_by_BySITEIDX((Guid)SiteIdx);
                FieldAssessmentmodel.TOdDumpAssessmentsGridList = _DbOpenDump.GetT_OD_DumpAssessmentList_BySITEIDX((Guid)SiteIdx);
                FieldAssessmentmodel.TOdDumpAssessments = oT_OD_DUMP_ASSESSMENTS;
                FieldAssessmentmodel.files_existing = _DbPortal.GetT_PRT_DOCUMENTS_ByDumpAssessmentsIDx((Guid)AssessmentIdx);
                FieldAssessmentmodel.filesPhoto_existing = _DbPortal.GetT_PRT_DOCUMENTS_Photos_ByDumpAssessmentsIDx((Guid)AssessmentIdx);
                FieldAssessmentmodel.WasteAmountList = _DbOpenDump.GetT_OD_DumpAssessmentContent_ByDumpAssessmentIDX((Guid)AssessmentIdx);
            }
            else
            {
                PreFieldmodel.TPrtSites = new T_PRT_SITES();
                if (PreFieldmodel.OrgList.Count() == 1)
                {
                    foreach (var orgid in PreFieldmodel.OrgList)
                    {
                        PreFieldmodel.TPrtSites.OrgId = orgid.Value;
                    }
                }
                PreFieldmodel.TPrtSites.SiteIdx = Guid.NewGuid();
                FieldAssessmentmodel.AssessmentDropDownList = _DbOpenDump.get_ddl_od_dumpassessment_by_BySITEIDX(Guid.NewGuid());
                FieldAssessmentmodel.AssessmentForHealthThreatDropDownList = _DbOpenDump.get_ddl_od_assessmentforhealththreat_by_BySITEIDX((Guid)SiteIdx);
                FieldAssessmentmodel.TOdDumpAssessments = new T_OD_DUMP_ASSESSMENTS();
                FieldAssessmentmodel.TOdDumpAssessments.DUMP_ASSESSMENTS_IDX = Guid.NewGuid();
                //FieldAssessmentmodel.TOdDumpAssessments.ASSESSMENT_DT = DateTime.Now;
            }
            openDumpViewModel.oPreFieldViewModel = PreFieldmodel;
            openDumpViewModel.oFieldAssessmentViewModel = FieldAssessmentmodel;
            openDumpViewModel.ActiveTab = OpenDumpTab.SiteCleanUp;
            return View(openDumpViewModel);

        }
    }
}