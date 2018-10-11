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

            FieldAssessmentmodel.AverageRainfallList = _DbOpenDump.get_ddl_refthreatfactor_by_factortype("Rainfall");
            FieldAssessmentmodel.BurningList = _DbOpenDump.get_ddl_refthreatfactor_by_factortype("Burning");
            FieldAssessmentmodel.ConcernList = _DbOpenDump.get_ddl_refthreatfactor_by_factortype("Concern");
            FieldAssessmentmodel.DrainageList = _DbOpenDump.get_ddl_refthreatfactor_by_factortype("Drainage");
            FieldAssessmentmodel.FloodingList = _DbOpenDump.get_ddl_refthreatfactor_by_factortype("Flooding");
            FieldAssessmentmodel.FencedList = _DbOpenDump.get_ddl_refthreatfactor_by_factortype("Fenced");
            FieldAssessmentmodel.AccessList = _DbOpenDump.get_ddl_refthreatfactor_by_factortype("Access");
            FieldAssessmentmodel.TOdRefThreatFactorList = _DbOpenDump.get_ddl_refthreatfactor();

            FieldAssessmentmodel.ContentCheckBoxList = _DbOpenDump.get_checkbox_refwastetype_by_wastetypecat("Hazard Factor", AssessmentIdx);

            PreFieldmodel.SiteSettingsList = _DbOpenDump.get_ddl_refdata_by_category("Site Setting");
            PreFieldmodel.CommunityList = _DbOpenDump.get_ddl_refdata_by_category("Community");

            PreFieldmodel.AquiferList = _DbOpenDump.get_ddl_refthreatfactor_by_factortype("Aquifer");
            PreFieldmodel.SurfaceWaterList = _DbOpenDump.get_ddl_refthreatfactor_by_factortype("Surface Water");
            PreFieldmodel.HomesList = _DbOpenDump.get_ddl_refthreatfactor_by_factortype("Homes");
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
            if (AssessmentIdx != null && SiteIdx == null)
            {
                PreFieldmodel.TPrtSites = _DbPortal.GetT_PRT_SITES_BySITEIDX(oT_OD_DUMP_ASSESSMENTS.SITE_IDX);
                PreFieldmodel.TOdSites = _DbOpenDump.GetT_OD_SITES_BySITEIDX(oT_OD_DUMP_ASSESSMENTS.SITE_IDX);
                FieldAssessmentmodel.TPrtSites = _DbPortal.GetT_PRT_SITES_BySITEIDX(oT_OD_DUMP_ASSESSMENTS.SITE_IDX);
                FieldAssessmentmodel.AssessmentDropDownList = _DbOpenDump.get_ddl_od_dumpassessment_by_BySITEIDX(oT_OD_DUMP_ASSESSMENTS.SITE_IDX);
                FieldAssessmentmodel.TOdDumpAssessmentsGridList = _DbOpenDump.GetT_OD_DumpAssessmentList_BySITEIDX(oT_OD_DUMP_ASSESSMENTS.SITE_IDX);
                FieldAssessmentmodel.TOdDumpAssessments = oT_OD_DUMP_ASSESSMENTS;
                if (FieldAssessmentmodel.TOdDumpAssessments.HF_RAINFALL!=null)
                {
                    FieldAssessmentmodel.RainfallSubScore = FieldAssessmentmodel.TOdRefThreatFactorList.Where(x => x.THREAT_FACTOR_IDX == FieldAssessmentmodel.TOdDumpAssessments.HF_RAINFALL).FirstOrDefault().THREAT_FACTOR_SCORE;
                }
                if (FieldAssessmentmodel.TOdDumpAssessments.HF_DRAINAGE != null)
                {
                    FieldAssessmentmodel.DrainageSubScore = FieldAssessmentmodel.TOdRefThreatFactorList.Where(x => x.THREAT_FACTOR_IDX == FieldAssessmentmodel.TOdDumpAssessments.HF_DRAINAGE).FirstOrDefault().THREAT_FACTOR_SCORE;
                }
                if (FieldAssessmentmodel.TOdDumpAssessments.HF_FLOODING != null)
                {
                    FieldAssessmentmodel.FloodingSubScore = FieldAssessmentmodel.TOdRefThreatFactorList.Where(x => x.THREAT_FACTOR_IDX == FieldAssessmentmodel.TOdDumpAssessments.HF_FLOODING).FirstOrDefault().THREAT_FACTOR_SCORE;
                }
                if (FieldAssessmentmodel.TOdDumpAssessments.HF_BURNING != null)
                {
                    FieldAssessmentmodel.BurningSubScore = FieldAssessmentmodel.TOdRefThreatFactorList.Where(x => x.THREAT_FACTOR_IDX == FieldAssessmentmodel.TOdDumpAssessments.HF_BURNING).FirstOrDefault().THREAT_FACTOR_SCORE;
                }
                if (FieldAssessmentmodel.TOdDumpAssessments.HF_FENCING != null)
                {
                    FieldAssessmentmodel.FencedSubScore = FieldAssessmentmodel.TOdRefThreatFactorList.Where(x => x.THREAT_FACTOR_IDX == FieldAssessmentmodel.TOdDumpAssessments.HF_FENCING).FirstOrDefault().THREAT_FACTOR_SCORE;
                }
                if (FieldAssessmentmodel.TOdDumpAssessments.HF_ACCESS_CONTROL != null)
                {
                    FieldAssessmentmodel.AccessSubScore = FieldAssessmentmodel.TOdRefThreatFactorList.Where(x => x.THREAT_FACTOR_IDX == FieldAssessmentmodel.TOdDumpAssessments.HF_ACCESS_CONTROL).FirstOrDefault().THREAT_FACTOR_SCORE;
                }
                if (FieldAssessmentmodel.TOdDumpAssessments.HF_PUBLIC_CONCERN != null)
                {
                    FieldAssessmentmodel.ConcernSubScore = FieldAssessmentmodel.TOdRefThreatFactorList.Where(x => x.THREAT_FACTOR_IDX == FieldAssessmentmodel.TOdDumpAssessments.HF_PUBLIC_CONCERN).FirstOrDefault().THREAT_FACTOR_SCORE;
                }
                FieldAssessmentmodel.files_existing = _DbPortal.GetT_PRT_DOCUMENTS_ByDumpAssessmentsIDx((Guid)AssessmentIdx);
                FieldAssessmentmodel.filesPhoto_existing = _DbPortal.GetT_PRT_DOCUMENTS_Photos_ByDumpAssessmentsIDx((Guid)AssessmentIdx);
            }
            else if (SiteIdx != null && AssessmentIdx == null)
            {
                PreFieldmodel.TPrtSites = _DbPortal.GetT_PRT_SITES_BySITEIDX((Guid)SiteIdx);
                PreFieldmodel.TOdSites = _DbOpenDump.GetT_OD_SITES_BySITEIDX((Guid)SiteIdx);
                FieldAssessmentmodel.TPrtSites = _DbPortal.GetT_PRT_SITES_BySITEIDX((Guid)SiteIdx);
                FieldAssessmentmodel.AssessmentDropDownList = _DbOpenDump.get_ddl_od_dumpassessment_by_BySITEIDX((Guid)SiteIdx);
                FieldAssessmentmodel.TOdDumpAssessmentsGridList = _DbOpenDump.GetT_OD_DumpAssessmentList_BySITEIDX((Guid)SiteIdx);
                FieldAssessmentmodel.TOdDumpAssessments = oT_OD_DUMP_ASSESSMENTS;
                FieldAssessmentmodel.TOdDumpAssessments.DUMP_ASSESSMENTS_IDX = Guid.NewGuid();
            }
            else if (SiteIdx != null && AssessmentIdx != null)
            {
                PreFieldmodel.TPrtSites = _DbPortal.GetT_PRT_SITES_BySITEIDX((Guid)SiteIdx);
                PreFieldmodel.TOdSites = _DbOpenDump.GetT_OD_SITES_BySITEIDX((Guid)SiteIdx);
                FieldAssessmentmodel.TPrtSites = _DbPortal.GetT_PRT_SITES_BySITEIDX((Guid)SiteIdx);
                FieldAssessmentmodel.AssessmentDropDownList = _DbOpenDump.get_ddl_od_dumpassessment_by_BySITEIDX((Guid)SiteIdx);
                FieldAssessmentmodel.TOdDumpAssessmentsGridList = _DbOpenDump.GetT_OD_DumpAssessmentList_BySITEIDX((Guid)SiteIdx);
                FieldAssessmentmodel.TOdDumpAssessments = oT_OD_DUMP_ASSESSMENTS;
                FieldAssessmentmodel.files_existing = _DbPortal.GetT_PRT_DOCUMENTS_ByDumpAssessmentsIDx((Guid)AssessmentIdx);
                FieldAssessmentmodel.filesPhoto_existing = _DbPortal.GetT_PRT_DOCUMENTS_Photos_ByDumpAssessmentsIDx((Guid)AssessmentIdx);
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
                FieldAssessmentmodel.TOdDumpAssessments = new T_OD_DUMP_ASSESSMENTS();
                FieldAssessmentmodel.TOdDumpAssessments.DUMP_ASSESSMENTS_IDX = Guid.NewGuid();               
            }
            openDumpViewModel.oPreFieldViewModel = PreFieldmodel;
            openDumpViewModel.oFieldAssessmentViewModel = FieldAssessmentmodel;
            openDumpViewModel.ActiveTab = OpenDumpTab.HealthThreat;
            return View(openDumpViewModel);
        }
    }
}
