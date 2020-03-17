using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using TribalSvcPortal.Data.Models;
using TribalSvcPortal.AppLogic.BusinessLogicLayer;
using Microsoft.EntityFrameworkCore;
using static TribalSvcPortal.AppLogic.BusinessLogicLayer.Utils;

namespace TribalSvcPortal.AppLogic.DataAccessLayer
{
    public class OpenDumpSiteListDisplay
    {
        public string OrgName { get; set; }
        public Guid SiteIdx { get; set; }
        public string SiteName { get; set; }
        public string SiteAddress { get; set; }
        public string ReportedBy { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? ReportedOn { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string CurrentSiteStatus { get; set; }
        public T_OD_ASSESSMENTS LastAssessment { get; set; }
        public int? HEALTH_THREAT_SCORE { get; set; }
        public T_OD_CLEANUP_PROJECT LatestCleanupProject { get; set; }
    }

    public class SelectedWasteTypeDisplay
    {
        public T_OD_REF_WASTE_TYPE T_OD_REF_WASTE_TYPE { get; set; }
        public bool IsChecked { get; set; }
    }

    public class CatSums
    {
        public string Category { get; set; }
        public decimal? Amount { get; set; }
        public decimal? AmountWt { get; set; }
    }

    public class DispSums
    {
        public Guid? DisposalType { get; set; }
        public decimal? Amount { get; set; }
        public decimal? AmountWt { get; set; }
    }

    public class AssessmentContentTypeDisplay
    {
        public Guid DUMP_ASSESSMENTS_CONTENT_IDX { get; set; }
        public Guid ASSESSMENT_IDX { get; set; }
        public Guid REF_WASTE_TYPE_IDX { get; set; }
        public string REF_WASTE_TYPE_NAME { get; set; }
        public string REF_WASTE_TYPE_CAT { get; set; }
        public decimal? WASTE_AMT { get; set; }
        public decimal? WASTE_WEIGHT_LBS { get; set; }
        public Guid? UNIT_MSR_IDX { get; set; }
        public string UNIT_MSR_CD { get; set; }
        public Guid? WASTE_DISPOSAL_METHOD { get; set; }
        public string WASTE_DISPOSAL_METHOD_TXT { get; set; }
        public string WASTE_DISPOSAL_DIST { get; set; }
        public int? TRANSPORT_AMT_PER_LOAD { get; set; }
        public IEnumerable<SelectListItem> ddl_Unit { get; set; }
    }

    public class AssessmentCleanupDisplayType {
        public Guid CLEANUP_CLEANUP_DTL_IDX { get; set; }
        public Guid ASSESSMENT_IDX { get; set; }
        public Guid CLEANUP_PROJECT_IDX { get; set; }
        public string REF_WASTE_TYPE_CAT { get; set; }
        public string REF_ASSET_NAME { get; set; }
        public decimal? CLEANUP_COST { get; set; }
        public Guid REF_WASTE_TYPE_CAT_CLEANUP_IDX { get; set; }
        public decimal? PROCESS_RATE_PER_HR { get; set; }
        public string PROCESS_RATE_UNIT { get; set; }
        public decimal? ASSET_HOURLY_RATE { get; set; }
        public int? ASSET_COUNT { get; set; }
        public bool? PER_UNIT_IND { get; set; }
        public decimal? sumCat { get; set; }
    }

    public class AssessmentSummaryDisplayType
    {
        public Guid ASSESSMENT_IDX { get; set; }
        public DateTime ASSESSMENT_DT { get; set; }
        public string ASSESSED_BY { get; set; }
        public string ASSESSMENT_NOTES { get; set; }
        public int? HEALTH_THREAT_SCORE { get; set; }
        public T_OD_CLEANUP_PROJECT LatestCleanupProject { get; set; }
        public string ORG_NAME { get; set; }
        public string SITE_NAME { get; set; }
        public string CURRENT_SITE_STATUS { get; set; }

    }

    public class CleanupProjectsDisplayType {
        public T_OD_CLEANUP_PROJECT  T_OD_CLEANUP_PROJECT { get; set; }
        public string ORG_NAME { get; set; }
        public string SITE_NAME { get; set; }
        public DateTime ASSESSMENT_DT { get; set; }
    }

    public class CleanupTransportDetailsType
    {
        public T_OD_CLEANUP_TRANSPORT_DTL T_OD_CLEANUP_TRANSPORT_DTL { get; set; }
        public string REF_WASTE_TYPE_NAME { get; set; }
    }

    public class CleanupDisposalDetailsType
    {
        public T_OD_CLEANUP_DISPOSAL_DTL T_OD_CLEANUP_DISPOSAL_DTL { get; set; }
        public string DISPOSAL_NAME { get; set; }
    }

    public class SiteImportType
    {
        public T_PRT_SITES T_PRT_SITES { get; set; }
        public T_OD_SITES T_OD_SITES { get; set; }
        public T_OD_ASSESSMENTS T_OD_ASSESSMENTS { get; set; }
        public bool VALIDATE_CD { get; set; }
        public string VALIDATE_MSG { get; set; }

        //INITIALIZE
        public SiteImportType()
        {
            T_PRT_SITES = new T_PRT_SITES();
            T_OD_SITES = new T_OD_SITES();
            VALIDATE_CD = true;
        }
    }

    public interface IDbOpenDump
    {
        //************** ORGANIZATIONS **********************************
        List<SelectListItem> getT_OD_ORGANIZATIONS();

        //************** T_OD_SITES **********************************
        T_OD_SITES getT_OD_SITES_BySITEIDX(Guid Siteidx);
        List<OpenDumpSiteListDisplay> getT_OD_SITES_ListBySearch(string id, string searchStr, string selOrg, string selStatus);
        Guid? InsertUpdateT_OD_SITES(Guid sITE_IDX, string rEPORTED_BY, DateTime? rEPORTED_ON, Guid? cOMMUNITY_IDX, Guid? sITE_SETTING_IDX, Guid? pF_AQUIFER_VERT_DIST, Guid? pF_SURF_WATER_HORIZ_DIST, Guid? pF_HOMES_DIST, string cURRENT_SITE_STATUS);
        string UpdateT_OD_SITES_Status(Guid sITE_IDX);


        //************** V_OD_SITES **********************************
        List<V_OD_SITES> getV_OD_SITES_Search(string org, string county, string status, string score);
        //************** V_OD_ASSESSMENTS **********************************
        List<V_OD_ASSESSMENTS> getV_OD_ASSESSMENTS_Search(string org, string county, string status, string score);
        List<V_OD_ASSESSMENTS> getV_OD_ASSESSMENTS_BySiteIDX(Guid Siteidx);


        //************** T_OD_SITE_PARCELS **********************************
        List<T_OD_SITE_PARCELS> getT_OD_SITE_PARCELS_BySITEIDX(Guid Siteidx);
        Guid? InsertUpdateT_OD_SITE_PARCELS(Guid? sITE_PARCEL_IDX, Guid? sITE_IDX, string pARCEL_NUM, string oWNER, string aCRES);

        //************** T_OD_DUMP_ASSESSMENTS **********************************
        T_OD_ASSESSMENTS getT_OD_ASSESSMENTS_ByAssessmentIDX(Guid aSSESSMENT_IDX);
        T_OD_ASSESSMENTS getT_OD_ASSESSMENTS_ByAssessmentIDX_wNav(Guid aSSESSMENT_IDX);
        List<AssessmentSummaryDisplayType> getT_OD_ASSESSMENTS_BySITEIDX(Guid Siteidx);
        List<AssessmentSummaryDisplayType> getT_OD_ASSESSMENTS_ByUser(string UserID);
        IEnumerable<SelectListItem> get_ddl_T_OD_ASSESSMENTS_by_BySITEIDX(Guid? Siteidx);
        IEnumerable<SelectListItem> get_ddl_T_OD_ASSESSMENTS_by_ByUser(string UserID);
        int deleteT_OD_Assessment(Guid aSSESSMENT_IDX);
        Guid? InsertUpdateT_OD_ASSESSMENTS(Guid aSSESSMENT_IDX, Guid? sITE_IDX, DateTime? aSSESSMENT_DT, string aSSESSED_BY, Guid? ASSESSMENT_TYPE_IDX, string cURRENT_SITE_STATUS, string SITE_DESCRIPTION,
                                                        string ASSESSMENT_NOTES, decimal? aREA_ACRES, decimal? vOLUMN_CU_YD, Guid? hF_RAINFALL, Guid? hF_DRAINAGE, Guid? hF_FLOODING, Guid? hF_BURNING, Guid? hF_FENCING,
                                                        Guid? hF_ACCESS_CONTROL, Guid? hF_PUBLIC_CONCERN, int? hEALTH_THREAT_SCORE, DateTime? cLEANED_CLOSED_DT);
        IEnumerable<SelectListItem> get_ddl_T_OD_SITE_STATUS();

        IEnumerable<SelectListItem> get_ddl_HealthThreatScore();
        

        //************** T_OD_DUMP_ASSESSMENT_CONTENT **********************************
        List<SelectedWasteTypeDisplay> getT_OD_ASSESSMENT_CONTENT_by_AssessIDX(Guid? aSSESSMENT_IDX);
        List<AssessmentContentTypeDisplay> getT_OD_ASSESSMENT_CONTENT_ByDumpAssessmentIDX(Guid aSSESSMENT_IDX);
        List<AssessmentContentTypeDisplay> getT_OD_ASSESSMENT_CONTENT_ByDumpAssessmentIDX_readonly(Guid aSSESSMENT_IDX);
        List<AssessmentContentTypeDisplay> getT_OD_ASSESSMENT_CONTENT_ByDumpAssessmentIDX_TransportDetails(Guid aSSESSMENT_IDX);
        Guid? InsertUpdateT_OD_Assessment_Content(Guid? aSSESSMENT_CONTENT_IDX, Guid? aSSESSMENT_IDX, Guid? rEF_WASTE_TYPE_IDX, decimal? wASTE_AMT, Guid? wASTE_UNIT_MSR, Guid? wASTE_DISPOSAL_METHOD, string wASTE_DISPOSAL_DIST, bool IS_CHECKED);
        List<CatSums> getT_OD_ASSESSMENT_CONTENT_DistinctCatSums(Guid aSSESSMENTS_IDX, string Cat);

        //************** T_OD_DUMP_ASSESSMENT_DOCUMENTS *********************************
        Guid? InsertUpdateT_OD_ASSESSMENT_DOCUMENTS(Guid? dOC_IDX, Guid aSSESSMENT_IDX);
        List<T_PRT_DOCUMENTS> GetT_PRT_DOCUMENTS_ByAssessmentIDX(Guid aSSESSMENT_IDX, string docType);

        //****************T_OD_CLEANUP_PROJECT ***********************************
        Guid? InsertUpdateT_OD_CLEANUP_PROJECT(Guid? cLEANUP_PROJECT_IDX, Guid? aSSESSMENT_IDX, string pROJECT_TYPE, string pROJECT_DESCRIPTION, DateTime? sTART_DATE,
            DateTime? cOMPLETION_DATE, decimal? cOST_CLEANUP_AMT, decimal? cOST_TRANSPORT_AMT, decimal? cOST_DISPOSAL_AMT, decimal? cOST_RESTORE_AMT, decimal? cOST_SURVEIL_AMT,
            decimal? cOST_TOTAL_AMT, string mODIFY_USERID, string cLEANUP_BY, string cLEANUP_BY_TITLE);
        T_OD_CLEANUP_PROJECT getT_OD_CLEANUP_PROJECT_by_IDX(Guid? cLEANUP_PROJECT_IDX);
        bool getT_OD_CLEANUP_PROJECT_Estimate_by_Assessment(Guid? aSSESSMENT_IDX);
        List<T_OD_CLEANUP_PROJECT> getT_OD_CLEANUP_PROJECT_Estimate_by_Assessment_List(Guid? aSSESSMENT_IDX);
        List<CleanupProjectsDisplayType> getT_OD_CLEANUP_PROJECT_by_User(string UserID);
        int deleteT_OD_CLEANUP_PROJECT(Guid cLEANUP_PROJECT_IDX);

        //************** T_OD_CLEANUP_DOCUMENTS **********************************
        Guid? InsertUpdateT_OD_CLEANUP_DOCS(Guid? dOC_IDX, Guid cLEANUP_PROJECT_IDX);
        List<T_PRT_DOCUMENTS> GetT_PRT_DOCUMENTS_ByCleanupProjectIDX(Guid cLEANUP_PROJECT_IDX, string docType);

        //************** T_OD_CLEANUP_CLEANUP_DTL **********************************
        Guid? InsertUpdateT_OD_CLEANUP_CLEANUP_DTL(Guid? cLEANUP_CLEANUP_DTL_IDX, Guid? cLEANUP_PROJECT_IDX, string rEF_WASTE_TYPE_CAT, string rEF_ASSET_NAME, decimal? cLEANUP_COST);
        List<AssessmentCleanupDisplayType> getT_OD_CLEANUP_CLEANUP_DTL_by_ProjectIDX(Guid? cLEANUP_PROJECT_IDX);
        decimal? getT_OD_CLEANUP_CLEANUP_DTL_Sum_by_ProjectIDX(Guid? cLEANUP_PROJECT_IDX);
        int deleteT_OD_CLEANUP_CLEANUP_DTL(Guid cLEANUP_CLEANUP_DTL_IDX);

        //************** T_OD_CLEANUP_TRANSPORT_DTL **********************************
        List<CleanupTransportDetailsType> getT_OD_CLEANUP_TRANSPORT_DTL_by_ProjectIDX(Guid? cLEANUP_PROJECT_IDX);

        //************** T_OD_CLEANUP_DISPOSAL_DTL **********************************
        Guid? InsertUpdateT_OD_CLEANUP_DISPOSAL_DTL(Guid? cLEANUP_DISPOSAL_DTL_IDX, Guid? cLEANUP_PROJECT_IDX, Guid? rEF_DISPOSAL_IDX, decimal? dISPOSAL_WEIGHT_LBS, decimal? dISPOSAL_COST, decimal? pRICE_PER_TON);
        List<CleanupDisposalDetailsType> getT_OD_CLEANUP_DISPOSAL_DTL_by_ProjectIDX(Guid? cLEANUP_PROJECT_IDX);

        //****************T_OD_CLEANUP_ACTIVITIES **********************************
        T_OD_CLEANUP_ACTIVITIES getT_OD_CLEANUP_ACTIVITIES_by_IDX(Guid? cLEANUP_ACTIVITY_IDX);
        List<T_OD_CLEANUP_ACTIVITIES> getT_OD_CLEANUP_ACTIVITIES_by_Project_and_Cat(Guid? cLEANUP_PROJECT_IDX, string cLEANUP_CAT);
        decimal? getT_OD_CLEANUP_ACTIVITIES_Sum_by_Project_and_Cat(Guid? cLEANUP_PROJECT_IDX, string cLEANUP_CAT);
        Guid? InsertUpdateT_OD_CLEANUP_ACTIVITIES(Guid? cLEANUP_ACTIVITY_IDX, Guid? cLEANUP_PROJECT_IDX, string cLEANUP_CAT, string cLEANUP_ACTIVITY, decimal? cLEANUP_COST,
            string mODIFY_BY, decimal? cLEANUP_UNIT_COST, string qUANTITY, string qUANTITY_UNIT);
        int DeleteT_OD_CLEANUP_ACTIVITIES(Guid cLEANUP_ACTIVITY_IDX);

        //************** T_OD_REF_DATA **************************************************
        IEnumerable<SelectListItem> get_ddl_T_OD_REF_DATA_by_category(string cat_name, string org_id);
        List<T_OD_REF_DATA> get_T_OD_REF_DATA_by_category(string cat_name);
        List<SelectListItem> get_ddl_T_OD_REF_DATA_CATEGORIES();


        //************** T_OD_REF_THREAT_FACTORS ****************************************
        List<T_OD_REF_THREAT_FACTORS> getT_OD_REF_THREAT_FACTORS();
        IEnumerable<SelectListItem> get_ddl_OD_REF_THREAT_FACTORS_by_type(string factor_type);
        T_OD_REF_THREAT_FACTORS getT_OD_REF_THREAT_FACTOR_ByID(Guid threatFactorIDX);

        //************** T_OD_REF_WASTE_TYPE **************************************
        T_OD_REF_WASTE_TYPE get_T_OD_REF_WASTE_TYPE_by_WasteType(Guid rEF_WASTE_TYPE_IDX);


        //************** T_OD_REF_WASTE_TYPE_UNITS **************************************
        IEnumerable<SelectListItem> get_ddl_T_OD_REF_WASTE_TYPE_UNITS_by_WasteType(Guid rEF_WASTE_TYPE_IDX);

        //************** T_OD_REF_DISPOSAL **********************************************
        IEnumerable<SelectListItem> get_ddl_ref_disposal();
        T_OD_REF_DISPOSAL getT_OD_REF_DISPOSAL_byID(Guid rEF_DISPOSAL_IDX);

        //************** REF_CLEANUP_TYPE **********************************
        IEnumerable<SelectListItem> get_ddl_CLEANUP_PROJECT_TYPE();

        //util
        int CalcCleanupEstimate(Guid CleanupProjectIDX, bool CalcCleanupCleanupDtl, bool CalcDisposalDtl, bool CalcTransportDtl);
        SiteImportType InsertOrUpdate_T_OD_SITE_local(string UserIDX, Dictionary<string, string> colVals, string path);

    }

    public class DbOpenDump : IDbOpenDump
    {
        private readonly ApplicationDbContext ctx;
        private readonly Ilog _log;
        
        public DbOpenDump(ApplicationDbContext _context, Ilog log)
        {
            ctx = _context;
            _log = log ?? throw new ArgumentNullException(nameof(log));        
        }


        //************** T_OD ORGANIZATIONS **********************************
        public List<SelectListItem> getT_OD_ORGANIZATIONS()
        {
            try
            {
                var xxx = (from a in ctx.T_OD_SITES
                        join b in ctx.T_PRT_SITES on a.SITE_IDX equals b.SITE_IDX
                        join o in ctx.T_PRT_ORGANIZATIONS on b.ORG_ID equals o.ORG_ID
                        select new SelectListItem
                        {
                            Value = o.ORG_ID,
                            Text = o.ORG_NAME
                        }).Distinct().ToList();

                return xxx;
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }



        //************** T_OD_SITES **********************************
        public T_OD_SITES getT_OD_SITES_BySITEIDX(Guid Siteidx)
        {
            try
            {
                return (from a in ctx.T_OD_SITES
                        .Where(s => s.SITE_IDX == Siteidx)
                        .Include(s => s.COMMUNITY_IDXNavigation)
                        .Include(s => s.PF_AQUIFER_VERT_DISTNavigation)
                        .Include(s => s.PF_HOMES_DISTNavigation)
                        .Include(s => s.PF_SURF_WATER_HORIZ_DISTNavigation)
                        select a).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }

        public List<OpenDumpSiteListDisplay> getT_OD_SITES_ListBySearch(string id, string searchStr, string selOrg, string selStatus)
        {
            try
            {
                return (from a in ctx.T_PRT_ORG_USERS
                        join b in ctx.T_PRT_ORGANIZATIONS on a.ORG_ID equals b.ORG_ID
                        join d in ctx.T_PRT_SITES on a.ORG_ID equals d.ORG_ID
                        join e in ctx.T_OD_SITES on d.SITE_IDX equals e.SITE_IDX
                        where a.Id == id
                        && (selOrg == null || a.ORG_ID == selOrg)
                        && (selStatus == null || e.CURRENT_SITE_STATUS == selStatus)
                        && (searchStr == null || (d.SITE_NAME.ToUpper().Contains(searchStr.ToUpper())
                           || d.SITE_ADDRESS.ToUpper().Contains(searchStr.ToUpper()) ))
                        orderby b.ORG_NAME, d.SITE_NAME
                        select new OpenDumpSiteListDisplay
                        {
                            OrgName = b.ORG_NAME,
                            SiteIdx = d.SITE_IDX,
                            SiteName = d.SITE_NAME,
                            SiteAddress = d.SITE_ADDRESS,
                            CurrentSiteStatus = e.CURRENT_SITE_STATUS,
                            ReportedBy = e.REPORTED_BY,
                            ReportedOn = e.REPORTED_ON,
                            Latitude = d.LATITUDE,
                            Longitude = d.LONGITUDE,
                            LastAssessment = (from v1 in ctx.T_OD_ASSESSMENTS
                                             where v1.SITE_IDX == d.SITE_IDX
                                             orderby v1.ASSESSMENT_DT descending
                                             select v1).FirstOrDefault(),
                            HEALTH_THREAT_SCORE = (from v1 in ctx.T_OD_ASSESSMENTS
                                                   where v1.SITE_IDX == d.SITE_IDX
                                                   && v1.HEALTH_THREAT_SCORE != null
                                                   orderby v1.ASSESSMENT_DT descending
                                                   select v1.HEALTH_THREAT_SCORE).FirstOrDefault(),
                            LatestCleanupProject = (from v1 in ctx.T_OD_CLEANUP_PROJECT
                                                 join v2 in ctx.T_OD_ASSESSMENTS on v1.ASSESSMENT_IDX equals v2.ASSESSMENT_IDX
                                                 where v2.SITE_IDX == d.SITE_IDX
                                                 && v1.COST_TOTAL_AMT != null
                                                 orderby v1.CREATE_DT descending
                                                 select v1).FirstOrDefault()
                        }).ToList();
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }

        public Guid? InsertUpdateT_OD_SITES(Guid sITE_IDX, string rEPORTED_BY, DateTime? rEPORTED_ON, Guid? cOMMUNITY_IDX, Guid? sITE_SETTING_IDX, Guid? pF_AQUIFER_VERT_DIST,
            Guid? pF_SURF_WATER_HORIZ_DIST, Guid? pF_HOMES_DIST, string cURRENT_SITE_STATUS)
        {
            try
            {
                Boolean insInd = false;

                T_OD_SITES e = (from c in ctx.T_OD_SITES
                                where c.SITE_IDX == sITE_IDX
                                select c).FirstOrDefault();

                //insert case
                if (e == null)
                {
                    insInd = true;
                    e = new T_OD_SITES();
                    e.SITE_IDX = sITE_IDX;
                }

                if (rEPORTED_BY != null) e.REPORTED_BY = rEPORTED_BY;
                if (rEPORTED_ON != null) e.REPORTED_ON = rEPORTED_ON;
                if (cOMMUNITY_IDX != null) e.COMMUNITY_IDX = cOMMUNITY_IDX;
                if (sITE_SETTING_IDX != null) e.SITE_SETTING_IDX = sITE_SETTING_IDX;
                if (pF_AQUIFER_VERT_DIST != null) e.PF_AQUIFER_VERT_DIST = pF_AQUIFER_VERT_DIST;
                if (pF_SURF_WATER_HORIZ_DIST != null) e.PF_SURF_WATER_HORIZ_DIST = pF_SURF_WATER_HORIZ_DIST;
                if (pF_HOMES_DIST != null) e.PF_HOMES_DIST = pF_HOMES_DIST;
                if (cURRENT_SITE_STATUS != null) e.CURRENT_SITE_STATUS = cURRENT_SITE_STATUS;

                if (insInd)
                    ctx.T_OD_SITES.Add(e);
                ctx.SaveChanges();
                return e.SITE_IDX;
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }

        public string UpdateT_OD_SITES_Status(Guid sITE_IDX)
        {
            try
            {
                T_OD_ASSESSMENTS lastAssessment = ctx.T_OD_ASSESSMENTS
                                .Where(c => c.SITE_IDX == sITE_IDX)
                                .OrderByDescending(t => t.ASSESSMENT_DT)
                                .FirstOrDefault();

                string _lastSiteStatus = null;

                if (lastAssessment != null && lastAssessment.ASSESSMENT_DT != null)
                    _lastSiteStatus = lastAssessment.CURRENT_SITE_STATUS;

                T_OD_SITES e = (from c in ctx.T_OD_SITES
                                where c.SITE_IDX == sITE_IDX
                                select c).FirstOrDefault();

                e.CURRENT_SITE_STATUS = _lastSiteStatus;
                ctx.SaveChanges();
                return _lastSiteStatus;
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }


        //************** V_OD_SITES **********************************
        public List<V_OD_SITES> getV_OD_SITES_Search(string org, string county, string status, string score)
        {
            try
            {
                return (from a in ctx.V_OD_SITES
                        where (org != null ? a.OriginatingPartnerName == org : true)
                        && (county != null ? a.CountyName == county : true)
                        && (status != null ? a.SiteStatusText == status : true)
                        && (score == "Low" ? a.SiteHealthThreatScoreValue < 451 : true)
                        && (score == "Medium" ? a.SiteHealthThreatScoreValue > 451 : true)
                        && (score == "High" ? a.SiteHealthThreatScoreValue > 900 : true)
                        select a).ToList();
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }

        public List<V_OD_ASSESSMENTS> getV_OD_ASSESSMENTS_BySiteIDX(Guid Siteidx)
        {
            try
            {
                return (from a in ctx.V_OD_ASSESSMENTS
                        where a.SITE_IDX == Siteidx
                        select a).ToList();
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }


        //************** V_OD_ASSESSMENTS **********************************
        public List<V_OD_ASSESSMENTS> getV_OD_ASSESSMENTS_Search(string org, string county, string status, string score)
        {
            try
            {
                return (from a in ctx.V_OD_SITES
                        join b in ctx.V_OD_ASSESSMENTS on a.SITE_IDX equals b.SITE_IDX
                        where (org != null ? a.OriginatingPartnerName == org : true)
                        && (county != null ? a.CountyName == county : true)
                        && (status != null ? a.SiteStatusText == status : true)
                        && (score != null ? b.SiteHealthThreatScoreSummaryText == score : true)
                        select b).ToList();
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }



        //************** T_OD_SITE_PARCELS **********************************
        public List<T_OD_SITE_PARCELS> getT_OD_SITE_PARCELS_BySITEIDX(Guid Siteidx)
        {
            try
            {
                return (from a in ctx.T_OD_SITE_PARCELS
                        .Where(s => s.SITE_IDX == Siteidx)
                        select a).ToList();
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }

        public Guid? InsertUpdateT_OD_SITE_PARCELS(Guid? sITE_PARCEL_IDX, Guid? sITE_IDX, string pARCEL_NUM, string oWNER, string aCRES)
        {
            try
            {
                Boolean insInd = false;

                T_OD_SITE_PARCELS e = (from c in ctx.T_OD_SITE_PARCELS
                                where c.SITE_PARCEL_IDX == sITE_PARCEL_IDX
                                select c).FirstOrDefault();

                if (e == null)
                    e = (from c in ctx.T_OD_SITE_PARCELS
                         where c.SITE_IDX == sITE_IDX
                         && e.PARCEL_NUM == pARCEL_NUM
                         select c).FirstOrDefault();

                //insert case
                if (e == null)
                {
                    insInd = true;
                    e = new T_OD_SITE_PARCELS();
                    e.SITE_PARCEL_IDX = Guid.NewGuid();
                    e.SITE_IDX = (Guid)sITE_IDX;
                }

                if (pARCEL_NUM != null) e.PARCEL_NUM = pARCEL_NUM;
                if (oWNER != null) e.OWNER = oWNER;
                if (aCRES != null) e.ACRES = aCRES;

                if (insInd)
                    ctx.T_OD_SITE_PARCELS.Add(e);
                ctx.SaveChanges();
                return e.SITE_PARCEL_IDX;
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }



        //************** T_OD_DUMP_ASSESSMENTS **********************************
        public T_OD_ASSESSMENTS getT_OD_ASSESSMENTS_ByAssessmentIDX(Guid aSSESSMENT_IDX)
        {
            try
            {
                return ctx.T_OD_ASSESSMENTS
                    .Where(s => s.ASSESSMENT_IDX == aSSESSMENT_IDX)
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }

        public T_OD_ASSESSMENTS getT_OD_ASSESSMENTS_ByAssessmentIDX_wNav(Guid aSSESSMENT_IDX)
        {
            try
            {
                var xxx = ctx.T_OD_ASSESSMENTS
                    .Where(s => s.ASSESSMENT_IDX == aSSESSMENT_IDX)
                    .Include(s => s.HF_ACCESS_CONTROLNavigation)
                    .Include(s => s.HF_BURNINGNavigation)
                    .Include(s => s.HF_DRAINAGENavigation)
                    .Include(s => s.HF_FENCINGNavigation)
                    .Include(s => s.HF_FLOODINGNavigation)
                    .Include(s => s.HF_RAINFALLNavigation)
                    .Include(s => s.HF_PUBLIC_CONCERNNavigation)
                    .FirstOrDefault();

                return xxx;
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }

        public List<AssessmentSummaryDisplayType> getT_OD_ASSESSMENTS_BySITEIDX(Guid Siteidx)
        {
            try
            {
                return (from a in ctx.T_OD_ASSESSMENTS
                        where a.SITE_IDX == Siteidx
                        orderby a.ASSESSMENT_DT descending
                        select new AssessmentSummaryDisplayType
                        {
                            ASSESSMENT_IDX = a.ASSESSMENT_IDX,
                            ASSESSMENT_DT = a.ASSESSMENT_DT,
                            ASSESSED_BY = a.ASSESSED_BY,
                            ASSESSMENT_NOTES = a.ASSESSMENT_NOTES,
                            HEALTH_THREAT_SCORE = a.HEALTH_THREAT_SCORE,
                            ORG_NAME = null,
                            SITE_NAME = null,
                            CURRENT_SITE_STATUS = a.CURRENT_SITE_STATUS,
                            LatestCleanupProject = (from v1 in ctx.T_OD_CLEANUP_PROJECT
                                                    where v1.ASSESSMENT_IDX == a.ASSESSMENT_IDX
                                                    && v1.COST_TOTAL_AMT != null
                                                    orderby v1.CREATE_DT descending
                                                    select v1).FirstOrDefault()
                        }).ToList();
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }

        public List<AssessmentSummaryDisplayType> getT_OD_ASSESSMENTS_ByUser(string UserID)
        {
            try
            {
                return (from a in ctx.T_OD_ASSESSMENTS
                        join b in ctx.T_PRT_SITES on a.SITE_IDX equals b.SITE_IDX
                        join c in ctx.T_PRT_ORG_USERS on b.ORG_ID equals c.ORG_ID
                        where c.Id == UserID
                        orderby a.ASSESSMENT_DT descending
                        select new AssessmentSummaryDisplayType {
                            ASSESSMENT_IDX= a.ASSESSMENT_IDX,
                            ASSESSMENT_DT = a.ASSESSMENT_DT,
                            ASSESSED_BY = a.ASSESSED_BY,
                            ASSESSMENT_NOTES = a.ASSESSMENT_NOTES,
                            HEALTH_THREAT_SCORE = a.HEALTH_THREAT_SCORE,
                            ORG_NAME = c.ORG_ID,
                            SITE_NAME = b.SITE_NAME,
                            CURRENT_SITE_STATUS = a.CURRENT_SITE_STATUS,
                            LatestCleanupProject = (from v1 in ctx.T_OD_CLEANUP_PROJECT
                                                    where v1.ASSESSMENT_IDX == a.ASSESSMENT_IDX
                                                    && v1.COST_TOTAL_AMT != null
                                                    orderby v1.CREATE_DT descending
                                                    select v1).FirstOrDefault()
                        }).ToList();
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }
               
        public IEnumerable<SelectListItem> get_ddl_T_OD_ASSESSMENTS_by_BySITEIDX(Guid? Siteidx)
        {
            try
            {

                return (from a in ctx.T_OD_ASSESSMENTS
                        where a.SITE_IDX == Siteidx
                        orderby a.ASSESSMENT_DT descending
                        select new SelectListItem
                        {
                            Value = a.ASSESSMENT_IDX.ToString(),
                            Text = a.ASSESSMENT_DT.ToString("MM/dd/yyyy")
                        }).ToList();
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }

        public IEnumerable<SelectListItem> get_ddl_T_OD_ASSESSMENTS_by_ByUser(string UserID)
        {
            try
            {
                return (from a in ctx.T_OD_ASSESSMENTS
                        join b in ctx.T_PRT_SITES on a.SITE_IDX equals b.SITE_IDX
                        join c in ctx.T_PRT_ORG_USERS on b.ORG_ID equals c.ORG_ID
                        where c.Id == UserID
                        orderby a.ASSESSMENT_DT descending
                        select new SelectListItem
                        {
                            Value = a.ASSESSMENT_IDX.ToString(),
                            Text = b.SITE_NAME + " - " + a.ASSESSMENT_DT.ToString("MM/dd/yyyy")
                        }).ToList();
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }

        public int deleteT_OD_Assessment(Guid aSSESSMENT_IDX)
        {
            try
            {
                T_OD_ASSESSMENTS tda = new T_OD_ASSESSMENTS { ASSESSMENT_IDX = aSSESSMENT_IDX };
                ctx.Entry(tda).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                ctx.SaveChanges();

                //then update site status
                UpdateT_OD_SITES_Status(tda.SITE_IDX);


                return 1;
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return 0;
            }
        }

        public Guid? InsertUpdateT_OD_ASSESSMENTS(Guid aSSESSMENT_IDX, Guid? sITE_IDX, DateTime? aSSESSMENT_DT, string aSSESSED_BY, Guid? ASSESSMENT_TYPE_IDX, string cURRENT_SITE_STATUS, string SITE_DESCRIPTION,
                                                        string ASSESSMENT_NOTES, decimal? aREA_ACRES, decimal? vOLUMN_CU_YD, Guid? hF_RAINFALL, Guid? hF_DRAINAGE, Guid? hF_FLOODING, Guid? hF_BURNING, Guid? hF_FENCING, 
                                                        Guid? hF_ACCESS_CONTROL, Guid? hF_PUBLIC_CONCERN, int? hEALTH_THREAT_SCORE, DateTime? cLEANED_CLOSED_DT)
        {
            try
            {
                Boolean insInd = false;
                T_OD_ASSESSMENTS e = (from c in ctx.T_OD_ASSESSMENTS
                                      where c.ASSESSMENT_IDX == aSSESSMENT_IDX
                                      select c).FirstOrDefault();

                //insert case
                if (e == null)
                {
                    insInd = true;
                    e = new T_OD_ASSESSMENTS();
                    e.ASSESSMENT_IDX = Guid.NewGuid();
                }

                if (sITE_IDX != null) e.SITE_IDX = (Guid)sITE_IDX;
                if (aSSESSMENT_DT != null) e.ASSESSMENT_DT = (DateTime)aSSESSMENT_DT;
                if (aSSESSED_BY != null) e.ASSESSED_BY = aSSESSED_BY;
                if (ASSESSMENT_TYPE_IDX != null) e.ASSESSMENT_TYPE_IDX = ASSESSMENT_TYPE_IDX;
                if (cURRENT_SITE_STATUS != null) e.CURRENT_SITE_STATUS = cURRENT_SITE_STATUS;
                if (SITE_DESCRIPTION != null) e.SITE_DESCRIPTION = SITE_DESCRIPTION;
                if (ASSESSMENT_NOTES != null) e.ASSESSMENT_NOTES = ASSESSMENT_NOTES;
                if (aREA_ACRES != null) e.AREA_ACRES = aREA_ACRES;
                if (vOLUMN_CU_YD != null) e.VOLUME_CU_YD = vOLUMN_CU_YD;
                if (hF_RAINFALL != null) e.HF_RAINFALL = hF_RAINFALL;
                if (hF_DRAINAGE != null) e.HF_DRAINAGE = hF_DRAINAGE;
                if (hF_FLOODING != null) e.HF_FLOODING = hF_FLOODING;
                if (hF_BURNING != null) e.HF_BURNING = hF_BURNING;
                if (hF_FENCING != null) e.HF_FENCING = hF_FENCING;
                if (hF_ACCESS_CONTROL != null) e.HF_ACCESS_CONTROL = hF_ACCESS_CONTROL;
                if (hF_PUBLIC_CONCERN != null) e.HF_PUBLIC_CONCERN = hF_PUBLIC_CONCERN;
                if (hEALTH_THREAT_SCORE != null) e.HEALTH_THREAT_SCORE = hEALTH_THREAT_SCORE;
                if (cLEANED_CLOSED_DT != null) e.CLEANED_CLOSED_DT = cLEANED_CLOSED_DT;

                if (insInd)
                    ctx.T_OD_ASSESSMENTS.Add(e);

                ctx.SaveChanges();

                //if site status changed, then update site table
                if (cURRENT_SITE_STATUS != null)
                    UpdateT_OD_SITES_Status(e.SITE_IDX);

                return e.ASSESSMENT_IDX;
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }

        }

        public IEnumerable<SelectListItem> get_ddl_T_OD_SITE_STATUS()
        {
            List<SelectListItem> ddl_SiteStatus = new List<SelectListItem>();

            ddl_SiteStatus.Add(new SelectListItem() { Value = "Active", Text = "Active" });
            ddl_SiteStatus.Add(new SelectListItem() { Value = "Active - Assessment Phase", Text = "Active - Assessment Phase" });
            ddl_SiteStatus.Add(new SelectListItem() { Value = "Active - Cleanup Phase", Text = "Active - Cleanup Phase" });
            ddl_SiteStatus.Add(new SelectListItem() { Value = "Inactive - Cleaned Up", Text = "Inactive - Cleaned Up" });
            ddl_SiteStatus.Add(new SelectListItem() { Value = "Inactive - Closed", Text = "Inactive - Closed" });

            return ddl_SiteStatus;
        }

        public IEnumerable<SelectListItem> get_ddl_HealthThreatScore()
        {
            List<SelectListItem> ddl_SiteStatus = new List<SelectListItem>();

            ddl_SiteStatus.Add(new SelectListItem() { Value = "Low", Text = "Low" });
            ddl_SiteStatus.Add(new SelectListItem() { Value = "Medium", Text = "Medium" });
            ddl_SiteStatus.Add(new SelectListItem() { Value = "High", Text = "High" });

            return ddl_SiteStatus;
        }


        //************** T_OD_ASSESSMENT_CONTENT **********************************
        public List<SelectedWasteTypeDisplay> getT_OD_ASSESSMENT_CONTENT_by_AssessIDX(Guid? aSSESSMENT_IDX)
        {
            try
            {
                return (from a in ctx.T_OD_REF_WASTE_TYPE
                        join b in ctx.T_OD_ASSESSMENT_CONTENT.Where(o => o.ASSESSMENT_IDX == aSSESSMENT_IDX) on a.REF_WASTE_TYPE_IDX equals b.REF_WASTE_TYPE_IDX
                        into sr
                        from x in sr.DefaultIfEmpty()  //left join
                        orderby a.REF_WASTE_TYPE_NAME
                        select new SelectedWasteTypeDisplay
                        {
                            T_OD_REF_WASTE_TYPE = a,
                            IsChecked = (x.ASSESSMENT_CONTENT_IDX != null)
                        }).ToList();
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }

        public List<AssessmentContentTypeDisplay> getT_OD_ASSESSMENT_CONTENT_ByDumpAssessmentIDX(Guid aSSESSMENT_IDX)
        {
            try
            {
                return (from a in ctx.T_OD_ASSESSMENT_CONTENT
                        join b in ctx.T_OD_REF_WASTE_TYPE on a.REF_WASTE_TYPE_IDX equals b.REF_WASTE_TYPE_IDX
                        where a.ASSESSMENT_IDX == aSSESSMENT_IDX
                        orderby b.REF_WASTE_TYPE_CAT, b.REF_WASTE_TYPE_NAME
                        select new AssessmentContentTypeDisplay {
                            DUMP_ASSESSMENTS_CONTENT_IDX = a.ASSESSMENT_CONTENT_IDX,
                            ASSESSMENT_IDX = a.ASSESSMENT_IDX,
                            REF_WASTE_TYPE_IDX = a.REF_WASTE_TYPE_IDX,
                            REF_WASTE_TYPE_NAME = b.REF_WASTE_TYPE_NAME,
                            REF_WASTE_TYPE_CAT = b.REF_WASTE_TYPE_CAT,
                            WASTE_AMT = a.WASTE_AMT,
                            WASTE_WEIGHT_LBS = a.WASTE_WEIGHT_LBS,
                            UNIT_MSR_IDX = a.UNIT_MSR_IDX ?? b.DEFAULT_UNIT_MSR_IDX,
                            WASTE_DISPOSAL_METHOD = a.WASTE_DISPOSAL_METHOD,
                            WASTE_DISPOSAL_DIST = a.WASTE_DISPOSAL_DIST,
                            ddl_Unit = (from aa in ctx.T_OD_REF_WASTE_TYPE_UNITS
                                        join bb in ctx.T_PRT_REF_UNITS on aa.UNIT_MSR_IDX equals bb.UNIT_MSR_IDX
                                        where aa.REF_WASTE_TYPE_IDX == b.REF_WASTE_TYPE_IDX
                                        select new SelectListItem
                                        {
                                            Value = aa.UNIT_MSR_IDX.ToString(),
                                            Text = bb.UNIT_MSR_CD
                                        }).ToList()
            }).ToList();
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }

        public List<AssessmentContentTypeDisplay> getT_OD_ASSESSMENT_CONTENT_ByDumpAssessmentIDX_readonly(Guid aSSESSMENT_IDX)
        {
            try
            {
                return (from a in ctx.T_OD_ASSESSMENT_CONTENT
                        join b in ctx.T_OD_REF_WASTE_TYPE on a.REF_WASTE_TYPE_IDX equals b.REF_WASTE_TYPE_IDX
                        where a.ASSESSMENT_IDX == aSSESSMENT_IDX
                        orderby b.REF_WASTE_TYPE_CAT, b.REF_WASTE_TYPE_NAME
                        select new AssessmentContentTypeDisplay
                        {
                            DUMP_ASSESSMENTS_CONTENT_IDX = a.ASSESSMENT_CONTENT_IDX,
                            ASSESSMENT_IDX = a.ASSESSMENT_IDX,
                            REF_WASTE_TYPE_IDX = a.REF_WASTE_TYPE_IDX,
                            REF_WASTE_TYPE_NAME = b.REF_WASTE_TYPE_NAME,
                            REF_WASTE_TYPE_CAT = b.REF_WASTE_TYPE_CAT,
                            WASTE_AMT = a.WASTE_AMT,
                            WASTE_WEIGHT_LBS = a.WASTE_WEIGHT_LBS,
                            UNIT_MSR_CD = (from bb in ctx.T_PRT_REF_UNITS 
                                            where bb.UNIT_MSR_IDX == a.UNIT_MSR_IDX
                                            select bb.UNIT_MSR_CD).FirstOrDefault(),
                            WASTE_DISPOSAL_METHOD = a.WASTE_DISPOSAL_METHOD,
                            WASTE_DISPOSAL_METHOD_TXT = (from cc in ctx.T_OD_REF_DISPOSAL
                                                         where cc.REF_DISPOSAL_IDX == a.WASTE_DISPOSAL_METHOD
                                                         select cc.DISPOSAL_NAME).FirstOrDefault(),
                            WASTE_DISPOSAL_DIST = a.WASTE_DISPOSAL_DIST
                        }).ToList();
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }

        public List<AssessmentContentTypeDisplay> getT_OD_ASSESSMENT_CONTENT_ByDumpAssessmentIDX_TransportDetails(Guid aSSESSMENT_IDX)
        {
            try
            {
                return (from a in ctx.T_OD_ASSESSMENT_CONTENT
                        join b in ctx.T_OD_REF_WASTE_TYPE on a.REF_WASTE_TYPE_IDX equals b.REF_WASTE_TYPE_IDX
                        where a.ASSESSMENT_IDX == aSSESSMENT_IDX
                        orderby b.REF_WASTE_TYPE_CAT, b.REF_WASTE_TYPE_NAME
                        select new AssessmentContentTypeDisplay
                        {
                            DUMP_ASSESSMENTS_CONTENT_IDX = a.ASSESSMENT_CONTENT_IDX,
                            ASSESSMENT_IDX = a.ASSESSMENT_IDX,
                            REF_WASTE_TYPE_IDX = a.REF_WASTE_TYPE_IDX,
                            WASTE_AMT = a.WASTE_AMT,
                            WASTE_DISPOSAL_DIST = a.WASTE_DISPOSAL_DIST,
                            TRANSPORT_AMT_PER_LOAD = b.TRANSPORT_AMT_PER_LOAD
                        }).ToList();
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }

        public Guid? InsertUpdateT_OD_Assessment_Content(Guid? aSSESSMENT_CONTENT_IDX, Guid? aSSESSMENT_IDX, Guid? rEF_WASTE_TYPE_IDX, decimal? wASTE_AMT, Guid? wASTE_UNIT_MSR, Guid? wASTE_DISPOSAL_METHOD, 
            string wASTE_DISPOSAL_DIST, bool IS_CHECKED)
        {
            try
            {
                Boolean insInd = false;

                //first try grabbing from PK
                T_OD_ASSESSMENT_CONTENT e = (from c in ctx.T_OD_ASSESSMENT_CONTENT
                                                  where c.ASSESSMENT_CONTENT_IDX == aSSESSMENT_CONTENT_IDX
                                             select c).FirstOrDefault();

                //then try grabbing from composite key
                if (e == null)
                    e = (from c in ctx.T_OD_ASSESSMENT_CONTENT
                         where c.ASSESSMENT_IDX == aSSESSMENT_IDX
                         && c.REF_WASTE_TYPE_IDX == rEF_WASTE_TYPE_IDX
                         select c).FirstOrDefault();

                //delete case
                if (e != null && IS_CHECKED == false)
                {
                    ctx.Entry(e).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                    ctx.SaveChanges();
                }
                else if (IS_CHECKED)
                {
                    //insert case
                    if (e == null)
                    {
                        insInd = true;
                        e = new T_OD_ASSESSMENT_CONTENT();
                        e.ASSESSMENT_CONTENT_IDX = Guid.NewGuid();
                        e.ASSESSMENT_IDX = (Guid)aSSESSMENT_IDX;
                        e.REF_WASTE_TYPE_IDX = (Guid)rEF_WASTE_TYPE_IDX;
                    }
                    if (wASTE_AMT != null) e.WASTE_AMT = wASTE_AMT;
                    if (wASTE_UNIT_MSR != null) e.UNIT_MSR_IDX = wASTE_UNIT_MSR;
                    if (wASTE_DISPOSAL_METHOD != null) e.WASTE_DISPOSAL_METHOD = wASTE_DISPOSAL_METHOD;
                    if (wASTE_DISPOSAL_DIST != null) e.WASTE_DISPOSAL_DIST = wASTE_DISPOSAL_DIST;

                    //retrieve density of the waste type to update the waste weight
                    T_OD_REF_WASTE_TYPE _typ = get_T_OD_REF_WASTE_TYPE_by_WasteType(e.REF_WASTE_TYPE_IDX);
                    if (_typ != null)
                    {
                        e.WASTE_WEIGHT_LBS = e.WASTE_AMT * (_typ.DENSITY_LBS_UNIT ?? _typ.DENSITY_LBS_CUYD); 
                    }

                    if (insInd)
                        ctx.T_OD_ASSESSMENT_CONTENT.Add(e);
                    ctx.SaveChanges();
                }

                return aSSESSMENT_IDX;
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }

        }

        public List<CatSums> getT_OD_ASSESSMENT_CONTENT_DistinctCatSums(Guid aSSESSMENT_IDX, string Cat) {
            try
            {
                return (from a in ctx.T_OD_ASSESSMENT_CONTENT
                        join b in ctx.T_OD_REF_WASTE_TYPE on a.REF_WASTE_TYPE_IDX equals b.REF_WASTE_TYPE_IDX
                        where a.ASSESSMENT_IDX == aSSESSMENT_IDX
                        && (Cat == null ? true : b.REF_WASTE_TYPE_CAT == Cat)
                        group a by new { b.REF_WASTE_TYPE_CAT } into grp
                        select new CatSums
                        {
                            Category = grp.Key.REF_WASTE_TYPE_CAT,
                            Amount = grp.Sum(a => a.WASTE_AMT),
                            AmountWt = grp.Sum(a => a.WASTE_WEIGHT_LBS)
                        }).ToList();
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }

        public List<DispSums> getT_OD_ASSESSMENT_CONTENT_DistinctDisposalSums(Guid aSSESSMENT_IDX)
        {
            try
            {
                return (from a in ctx.T_OD_ASSESSMENT_CONTENT
                        where a.ASSESSMENT_IDX == aSSESSMENT_IDX
                        group a by new { a.WASTE_DISPOSAL_METHOD } into grp
                        select new DispSums
                        {
                            DisposalType = grp.Key.WASTE_DISPOSAL_METHOD,
                            Amount = grp.Sum(a => a.WASTE_AMT),
                            AmountWt = grp.Sum(a => a.WASTE_WEIGHT_LBS)
                        }).ToList();
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }


        //************** T_OD_ASSESSMENT_DOCUMENTS **********************************
        public Guid? InsertUpdateT_OD_ASSESSMENT_DOCUMENTS(Guid? dOC_IDX, Guid aSSESSMENT_IDX)
        {

            try
            {
                Boolean insInd = false;

                T_OD_ASSESSMENT_DOCS e = (from c in ctx.T_OD_ASSESSMENT_DOCS
                                               where c.DOC_IDX == dOC_IDX
                                               select c).FirstOrDefault();

                //insert case
                if (e == null)
                {
                    insInd = true;
                    e = new T_OD_ASSESSMENT_DOCS();

                }

                if (dOC_IDX != null) e.DOC_IDX = (Guid)dOC_IDX;
                if (aSSESSMENT_IDX != null) e.ASSESSMENT_IDX = aSSESSMENT_IDX;

                if (insInd)
                    ctx.T_OD_ASSESSMENT_DOCS.Add(e);

                ctx.SaveChanges();
                return e.DOC_IDX;
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }

        }

        public List<T_PRT_DOCUMENTS> GetT_PRT_DOCUMENTS_ByAssessmentIDX(Guid aSSESSMENT_IDX, string docType)
        {
            try
            {
                return (from a in ctx.T_OD_ASSESSMENT_DOCS
                        join b in ctx.T_PRT_DOCUMENTS on a.DOC_IDX equals b.DOC_IDX
                        where a.ASSESSMENT_IDX == aSSESSMENT_IDX
                        && b.DOC_TYPE == docType
                        orderby b.CREATE_DT
                        select b).ToList();
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }

        }


        //****************T_OD_CLEANUP_PROJECT ***********************************
        public Guid? InsertUpdateT_OD_CLEANUP_PROJECT(Guid? cLEANUP_PROJECT_IDX, Guid? aSSESSMENT_IDX, string pROJECT_TYPE, string pROJECT_DESCRIPTION, DateTime? sTART_DATE, 
            DateTime? cOMPLETION_DATE, decimal? cOST_CLEANUP_AMT, decimal? cOST_TRANSPORT_AMT, decimal? cOST_DISPOSAL_AMT, decimal? cOST_RESTORE_AMT, decimal? cOST_SURVEIL_AMT, 
            decimal? cOST_TOTAL_AMT, string mODIFY_USERID, string cLEANUP_BY, string cLEANUP_BY_TITLE)
        {
            try
            {
                Boolean insInd = false;
                T_OD_CLEANUP_PROJECT e = (from c in ctx.T_OD_CLEANUP_PROJECT
                                          where c.CLEANUP_PROJECT_IDX == cLEANUP_PROJECT_IDX
                                          select c).FirstOrDefault();

                //insert case
                if (e == null)
                {
                    insInd = true;
                    e = new T_OD_CLEANUP_PROJECT();
                    e.CLEANUP_PROJECT_IDX = Guid.NewGuid();
                    e.CREATE_USER_ID = mODIFY_USERID;
                    e.CREATE_DT = System.DateTime.Now;
                }
                else
                {
                    e.MODIFY_USER_ID = mODIFY_USERID;
                    e.MODIFY_DT = System.DateTime.Now;
                }

                if (aSSESSMENT_IDX != null) e.ASSESSMENT_IDX = (Guid)aSSESSMENT_IDX;
                if (pROJECT_TYPE != null) e.PROJECT_TYPE = pROJECT_TYPE;
                if (pROJECT_DESCRIPTION != null) e.PROJECT_DESCRIPTION = pROJECT_DESCRIPTION;
                if (sTART_DATE != null) e.START_DATE = sTART_DATE;
                if (cOMPLETION_DATE != null) e.COMPLETION_DATE = cOMPLETION_DATE;
                if (cOMPLETION_DATE == DateTime.MinValue) e.COMPLETION_DATE = null;  //allow nulling
                if (cLEANUP_BY != null) e.CLEANUP_BY = cLEANUP_BY;
                if (cLEANUP_BY_TITLE != null) e.CLEANUP_BY_TITLE = cLEANUP_BY_TITLE;
                if (cOST_CLEANUP_AMT != null) e.COST_CLEANUP_AMT = cOST_CLEANUP_AMT;
                if (cOST_TRANSPORT_AMT != null) e.COST_TRANSPORT_AMT = cOST_TRANSPORT_AMT;
                if (cOST_DISPOSAL_AMT != null) e.COST_DISPOSAL_AMT = cOST_DISPOSAL_AMT;
                if (cOST_RESTORE_AMT != null) e.COST_RESTORE_AMT = cOST_RESTORE_AMT;
                if (cOST_SURVEIL_AMT != null) e.COST_SURVEIL_AMT = cOST_SURVEIL_AMT;
                if (cOST_TOTAL_AMT != null)
                    e.COST_TOTAL_AMT = cOST_TOTAL_AMT;
                else if (cOST_CLEANUP_AMT != null || cOST_TRANSPORT_AMT != null || cOST_DISPOSAL_AMT != null || cOST_RESTORE_AMT != null || cOST_SURVEIL_AMT != null)
                    e.COST_TOTAL_AMT = (e.COST_CLEANUP_AMT ?? 0) + (e.COST_TRANSPORT_AMT ?? 0) + (e.COST_DISPOSAL_AMT ?? 0) + (e.COST_RESTORE_AMT ?? 0) + (e.COST_SURVEIL_AMT ?? 0);

                if (insInd)
                    ctx.T_OD_CLEANUP_PROJECT.Add(e);

                ctx.SaveChanges();

                return e.CLEANUP_PROJECT_IDX;
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }

        }

        public T_OD_CLEANUP_PROJECT getT_OD_CLEANUP_PROJECT_by_IDX(Guid? cLEANUP_PROJECT_IDX)
        {
            try
            {
                return (from a in ctx.T_OD_CLEANUP_PROJECT
                        where a.CLEANUP_PROJECT_IDX == cLEANUP_PROJECT_IDX
                        select a).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }

        public bool getT_OD_CLEANUP_PROJECT_Estimate_by_Assessment(Guid? aSSESSMENT_IDX)
        {
            try
            {
                int xxx = (from a in ctx.T_OD_CLEANUP_PROJECT
                        where a.ASSESSMENT_IDX == aSSESSMENT_IDX
                        && a.PROJECT_TYPE == "Estimate"
                        select a).Count();

                return xxx > 0;
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return false;
            }
        }

        public List<T_OD_CLEANUP_PROJECT> getT_OD_CLEANUP_PROJECT_Estimate_by_Assessment_List(Guid? aSSESSMENT_IDX)
        {
            try
            {
                return (from a in ctx.T_OD_CLEANUP_PROJECT
                           where a.ASSESSMENT_IDX == aSSESSMENT_IDX
                           && a.PROJECT_TYPE == "Estimate"
                           select a).ToList();
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }


        public List<CleanupProjectsDisplayType> getT_OD_CLEANUP_PROJECT_by_User(string UserID)
        {
            try
            {
                return (from z in ctx.T_OD_CLEANUP_PROJECT 
                        join a in ctx.T_OD_ASSESSMENTS on z.ASSESSMENT_IDX equals a.ASSESSMENT_IDX
                        join b in ctx.T_PRT_SITES on a.SITE_IDX equals b.SITE_IDX
                        join c in ctx.T_PRT_ORG_USERS on b.ORG_ID equals c.ORG_ID
                        where c.Id == UserID
                        orderby z.CREATE_DT descending
                        select new CleanupProjectsDisplayType
                        {
                            T_OD_CLEANUP_PROJECT = z,
                            ORG_NAME = c.ORG_ID,
                            SITE_NAME = b.SITE_NAME,
                            ASSESSMENT_DT = a.ASSESSMENT_DT
                        }).ToList();
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }

        public int deleteT_OD_CLEANUP_PROJECT(Guid cLEANUP_PROJECT_IDX)
        {
            try
            {
                T_OD_CLEANUP_PROJECT tda = new T_OD_CLEANUP_PROJECT { CLEANUP_PROJECT_IDX = cLEANUP_PROJECT_IDX };
                ctx.Entry(tda).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                ctx.SaveChanges();

                return 1;
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return 0;
            }
        }


        //************** T_OD_CLEANUP_DOCUMENTS **********************************
        public Guid? InsertUpdateT_OD_CLEANUP_DOCS(Guid? dOC_IDX, Guid cLEANUP_PROJECT_IDX)
        {

            try
            {
                Boolean insInd = false;

                T_OD_CLEANUP_DOCS e = (from c in ctx.T_OD_CLEANUP_DOCS
                                          where c.DOC_IDX == dOC_IDX
                                          select c).FirstOrDefault();

                //insert case
                if (e == null)
                {
                    insInd = true;
                    e = new T_OD_CLEANUP_DOCS();

                }

                if (dOC_IDX != null) e.DOC_IDX = (Guid)dOC_IDX;
                if (cLEANUP_PROJECT_IDX != null) e.CLEANUP_PROJECT_IDX = cLEANUP_PROJECT_IDX;

                if (insInd)
                    ctx.T_OD_CLEANUP_DOCS.Add(e);

                ctx.SaveChanges();
                return e.DOC_IDX;
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }

        }

        public List<T_PRT_DOCUMENTS> GetT_PRT_DOCUMENTS_ByCleanupProjectIDX(Guid cLEANUP_PROJECT_IDX, string docType)
        {
            try
            {
                return (from a in ctx.T_OD_CLEANUP_DOCS
                        join b in ctx.T_PRT_DOCUMENTS on a.DOC_IDX equals b.DOC_IDX
                        where a.CLEANUP_PROJECT_IDX == cLEANUP_PROJECT_IDX
                        && b.DOC_TYPE == docType
                        orderby b.CREATE_DT
                        select b).ToList();
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }

        }

        //****************T_OD_DUMP_CLEANUP_CLEANUP_DTL ***********************************
        public Guid? InsertUpdateT_OD_CLEANUP_CLEANUP_DTL(Guid? cLEANUP_CLEANUP_DTL_IDX, Guid? cLEANUP_PROJECT_IDX, string rEF_WASTE_TYPE_CAT, string rEF_ASSET_NAME, decimal? cLEANUP_COST)
        {

            try
            {
                Boolean insInd = false;

                //first grab from PK
                T_OD_CLEANUP_CLEANUP_DTL e = (from c in ctx.T_OD_CLEANUP_CLEANUP_DTL
                                              where c.CLEANUP_CLEANUP_DTL_IDX == cLEANUP_CLEANUP_DTL_IDX
                                              select c).FirstOrDefault();

                //else grab from composite key
                if (e == null)
                    e = (from c in ctx.T_OD_CLEANUP_CLEANUP_DTL
                         where c.CLEANUP_PROJECT_IDX == cLEANUP_PROJECT_IDX
                         && c.REF_WASTE_TYPE_CAT == rEF_WASTE_TYPE_CAT
                         && c.REF_ASSET_NAME == rEF_ASSET_NAME
                         select c).FirstOrDefault();

                //insert case
                if (e == null)
                {
                    insInd = true;
                    e = new T_OD_CLEANUP_CLEANUP_DTL();
                    e.CLEANUP_CLEANUP_DTL_IDX = Guid.NewGuid();
                }

                if (cLEANUP_PROJECT_IDX != null) e.CLEANUP_PROJECT_IDX = (Guid)cLEANUP_PROJECT_IDX;
                if (rEF_WASTE_TYPE_CAT != null) e.REF_WASTE_TYPE_CAT = rEF_WASTE_TYPE_CAT;
                if (rEF_ASSET_NAME != null) e.REF_ASSET_NAME = rEF_ASSET_NAME;
                if (cLEANUP_COST != null) e.CLEANUP_COST = cLEANUP_COST;

                if (insInd)
                    ctx.T_OD_CLEANUP_CLEANUP_DTL.Add(e);

                ctx.SaveChanges();
                return e.CLEANUP_CLEANUP_DTL_IDX;
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }

        }

        public List<AssessmentCleanupDisplayType> getT_OD_CLEANUP_CLEANUP_DTL_by_ProjectIDX(Guid? cLEANUP_PROJECT_IDX)
        {
            try
            {
                return (from a in ctx.T_OD_CLEANUP_CLEANUP_DTL
                           join b in ctx.T_OD_REF_WASTE_TYPE_CAT_CLEANUP on new { a.REF_WASTE_TYPE_CAT, a.REF_ASSET_NAME } equals new { b.REF_WASTE_TYPE_CAT, b.REF_ASSET_NAME }
                           join c in ctx.T_OD_CLEANUP_PROJECT on a.CLEANUP_PROJECT_IDX equals c.CLEANUP_PROJECT_IDX
                        where a.CLEANUP_PROJECT_IDX == cLEANUP_PROJECT_IDX
                        orderby a.REF_WASTE_TYPE_CAT ascending, a.REF_ASSET_NAME descending
                           select new AssessmentCleanupDisplayType {
                               CLEANUP_CLEANUP_DTL_IDX = a.CLEANUP_CLEANUP_DTL_IDX,
                               CLEANUP_PROJECT_IDX = a.CLEANUP_PROJECT_IDX,
                               ASSESSMENT_IDX = c.ASSESSMENT_IDX,
                               REF_WASTE_TYPE_CAT = a.REF_WASTE_TYPE_CAT,
                               REF_ASSET_NAME = a.REF_ASSET_NAME,
                               CLEANUP_COST = a.CLEANUP_COST,
                               REF_WASTE_TYPE_CAT_CLEANUP_IDX = b.REF_WASTE_TYPE_CAT_CLEANUP_IDX,
                               PROCESS_RATE_PER_HR = b.PROCESS_RATE_PER_HR,
                               PROCESS_RATE_UNIT = b.PROCESS_RATE_UNIT,
                               ASSET_HOURLY_RATE = b.ASSET_HOURLY_RATE,
                               ASSET_COUNT = b.ASSET_COUNT,
                               PER_UNIT_IND = b.PER_UNIT_IND,

                               sumCat = (from aa in ctx.T_OD_ASSESSMENT_CONTENT
                                         join bb in ctx.T_OD_REF_WASTE_TYPE on aa.REF_WASTE_TYPE_IDX equals bb.REF_WASTE_TYPE_IDX
                                         where aa.ASSESSMENT_IDX == c.ASSESSMENT_IDX
                                         && bb.REF_WASTE_TYPE_CAT == b.REF_WASTE_TYPE_CAT
                                         select aa.WASTE_AMT).Sum()

                               //                                         group aa by new { bb.REF_WASTE_TYPE_CAT } into grp
                               //                                       select grp.Sum(a => a.WASTE_AMT))
                               //this.getT_OD_DUMP_ASSESSMENT_CONTENT_DistinctCatSums(a.DUMP_ASSESSMENTS_IDX, b.REF_WASTE_TYPE_CAT).FirstOrDefault().Amount 
                           }).ToList();
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }

        public decimal? getT_OD_CLEANUP_CLEANUP_DTL_Sum_by_ProjectIDX(Guid? cLEANUP_PROJECT_IDX)
        {
            try
            {
                var xxx= (from a in ctx.T_OD_CLEANUP_CLEANUP_DTL
                          where a.CLEANUP_PROJECT_IDX == cLEANUP_PROJECT_IDX
                          select a).ToList();

                return xxx.Select(c => c.CLEANUP_COST).Sum();
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return 0;
            }
        }

        public int deleteT_OD_CLEANUP_CLEANUP_DTL(Guid cLEANUP_CLEANUP_DTL_IDX)
        {
            try
            {
                T_OD_CLEANUP_CLEANUP_DTL tda = new T_OD_CLEANUP_CLEANUP_DTL { CLEANUP_CLEANUP_DTL_IDX = cLEANUP_CLEANUP_DTL_IDX };
                ctx.Entry(tda).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                ctx.SaveChanges();

                return 1;
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return 0;
            }
        }


        //************** T_OD_CLEANUP_TRANSPORT_DTL **********************************
        public Guid? InsertUpdateT_OD_CLEANUP_TRANSPORT_DTL(Guid? cLEANUP_TRANSPORT_DTL_IDX, Guid? cLEANUP_PROJECT_IDX, Guid? rEF_WASTE_TYPE_IDX, int? nUM_LOADS, decimal? hOURS_LOAD, 
            decimal? hOURLY_RATE, decimal? tRANSPORT_COST)
        {

            try
            {
                Boolean insInd = false;

                //first grab from PK
                T_OD_CLEANUP_TRANSPORT_DTL e = (from c in ctx.T_OD_CLEANUP_TRANSPORT_DTL
                                                where c.CLEANUP_TRANSPORT_DTL_IDX == cLEANUP_TRANSPORT_DTL_IDX
                                                select c).FirstOrDefault();

                //else grab from composite key
                if (e == null)
                    e = (from c in ctx.T_OD_CLEANUP_TRANSPORT_DTL
                         where c.CLEANUP_PROJECT_IDX == cLEANUP_PROJECT_IDX
                         && c.REF_WASTE_TYPE_IDX == rEF_WASTE_TYPE_IDX
                         select c).FirstOrDefault();

                //insert case
                if (e == null)
                {
                    insInd = true;
                    e = new T_OD_CLEANUP_TRANSPORT_DTL();
                    e.CLEANUP_TRANSPORT_DTL_IDX = Guid.NewGuid();
                }

                if (cLEANUP_PROJECT_IDX != null) e.CLEANUP_PROJECT_IDX = (Guid)cLEANUP_PROJECT_IDX;
                if (rEF_WASTE_TYPE_IDX != null) e.REF_WASTE_TYPE_IDX = (Guid)rEF_WASTE_TYPE_IDX;
                if (nUM_LOADS != null) e.NUM_LOADS = nUM_LOADS;
                if (hOURS_LOAD != null) e.HOURS_LOAD = hOURS_LOAD;
                if (hOURLY_RATE != null) e.HOURLY_RATE = hOURLY_RATE;
                if (tRANSPORT_COST != null) e.TRANSPORT_COST = tRANSPORT_COST;

                if (insInd)
                    ctx.T_OD_CLEANUP_TRANSPORT_DTL.Add(e);

                ctx.SaveChanges();
                return e.CLEANUP_TRANSPORT_DTL_IDX;
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }

        }

        public decimal? getT_OD_CLEANUP_TRANSPORT_DTL_Sum_by_ProjectIDX(Guid? cLEANUP_PROJECT_IDX)
        {
            try
            {
                var xxx = (from a in ctx.T_OD_CLEANUP_TRANSPORT_DTL
                           where a.CLEANUP_PROJECT_IDX == cLEANUP_PROJECT_IDX
                           select a).ToList();

                return xxx.Select(c => c.TRANSPORT_COST).Sum();
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return 0;
            }
        }



        public List<CleanupTransportDetailsType> getT_OD_CLEANUP_TRANSPORT_DTL_by_ProjectIDX(Guid? cLEANUP_PROJECT_IDX)
        {
            try
            {
                return (from a in ctx.T_OD_CLEANUP_TRANSPORT_DTL
                        join b in ctx.T_OD_REF_WASTE_TYPE on a.REF_WASTE_TYPE_IDX equals b.REF_WASTE_TYPE_IDX
                        where a.CLEANUP_PROJECT_IDX == cLEANUP_PROJECT_IDX
                        orderby b.REF_WASTE_TYPE_NAME ascending
                        select new CleanupTransportDetailsType
                        {
                            T_OD_CLEANUP_TRANSPORT_DTL = a,
                            REF_WASTE_TYPE_NAME = b.REF_WASTE_TYPE_NAME
                        }).ToList();
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }

        //************** T_OD_CLEANUP_DISPOSAL_DTL **********************************
        public Guid? InsertUpdateT_OD_CLEANUP_DISPOSAL_DTL(Guid? cLEANUP_DISPOSAL_DTL_IDX, Guid? cLEANUP_PROJECT_IDX, Guid? rEF_DISPOSAL_IDX, decimal? dISPOSAL_WEIGHT_LBS, decimal? dISPOSAL_COST, decimal? pRICE_PER_TON)
        {

            try
            {
                Boolean insInd = false;

                //first grab from PK
                T_OD_CLEANUP_DISPOSAL_DTL e = (from c in ctx.T_OD_CLEANUP_DISPOSAL_DTL
                                               where c.CLEANUP_DISPOSAL_DTL_IDX == cLEANUP_DISPOSAL_DTL_IDX
                                               select c).FirstOrDefault();

                //else grab from composite key
                if (e == null)
                    e = (from c in ctx.T_OD_CLEANUP_DISPOSAL_DTL
                         where c.CLEANUP_PROJECT_IDX == cLEANUP_PROJECT_IDX
                         && c.REF_DISPOSAL_IDX == rEF_DISPOSAL_IDX
                         select c).FirstOrDefault();

                //insert case
                if (e == null)
                {
                    insInd = true;
                    e = new T_OD_CLEANUP_DISPOSAL_DTL();
                    e.CLEANUP_DISPOSAL_DTL_IDX = Guid.NewGuid();
                }

                if (cLEANUP_PROJECT_IDX != null) e.CLEANUP_PROJECT_IDX = (Guid)cLEANUP_PROJECT_IDX;
                if (rEF_DISPOSAL_IDX != null) e.REF_DISPOSAL_IDX = (Guid)rEF_DISPOSAL_IDX;
                if (dISPOSAL_WEIGHT_LBS != null) e.DISPOSAL_WEIGHT_LBS = dISPOSAL_WEIGHT_LBS;
                if (dISPOSAL_COST != null) e.DISPOSAL_COST = dISPOSAL_COST;
                if (pRICE_PER_TON != null) e.PRICE_PER_TON = pRICE_PER_TON;

                if (insInd)
                    ctx.T_OD_CLEANUP_DISPOSAL_DTL.Add(e);

                ctx.SaveChanges();
                return e.CLEANUP_DISPOSAL_DTL_IDX;
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }

        }

        public List<CleanupDisposalDetailsType> getT_OD_CLEANUP_DISPOSAL_DTL_by_ProjectIDX(Guid? cLEANUP_PROJECT_IDX)
        {
            try
            {
                return (from a in ctx.T_OD_CLEANUP_DISPOSAL_DTL
                        join b in ctx.T_OD_REF_DISPOSAL on a.REF_DISPOSAL_IDX equals b.REF_DISPOSAL_IDX
                        where a.CLEANUP_PROJECT_IDX == cLEANUP_PROJECT_IDX
                        orderby a.REF_DISPOSAL_IDX ascending
                        select new CleanupDisposalDetailsType {
                            T_OD_CLEANUP_DISPOSAL_DTL = a,
                            DISPOSAL_NAME = b.DISPOSAL_NAME
                        }).ToList();
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }

        public decimal? getT_OD_CLEANUP_DISPOSAL_DTL_Sum_by_ProjectIDX(Guid? cLEANUP_PROJECT_IDX)
        {
            try
            {
                var xxx = (from a in ctx.T_OD_CLEANUP_DISPOSAL_DTL
                           where a.CLEANUP_PROJECT_IDX == cLEANUP_PROJECT_IDX
                           select a).ToList();

                return xxx.Select(c => c.DISPOSAL_COST).Sum();
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return 0;
            }
        }



        //****************T_OD_CLEANUP_ACTIVITIES ***********************************
        public T_OD_CLEANUP_ACTIVITIES getT_OD_CLEANUP_ACTIVITIES_by_IDX(Guid? cLEANUP_ACTIVITY_IDX)
        {
            try
            {
                return (from a in ctx.T_OD_CLEANUP_ACTIVITIES
                        where a.CLEANUP_ACTIVITY_IDX == cLEANUP_ACTIVITY_IDX
                        select a).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }

        public List<T_OD_CLEANUP_ACTIVITIES> getT_OD_CLEANUP_ACTIVITIES_by_Project_and_Cat(Guid? cLEANUP_PROJECT_IDX, string cLEANUP_CAT)
        {
            try
            {
                return (from a in ctx.T_OD_CLEANUP_ACTIVITIES
                        where a.CLEANUP_PROJECT_IDX == cLEANUP_PROJECT_IDX
                        && a.CLEANUP_CAT == cLEANUP_CAT
                        orderby a.CLEANUP_ACTIVITY
                        select a).ToList();
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }

        public decimal? getT_OD_CLEANUP_ACTIVITIES_Sum_by_Project_and_Cat(Guid? cLEANUP_PROJECT_IDX, string cLEANUP_CAT)
        {
            try
            {
                var xxx = (from a in ctx.T_OD_CLEANUP_ACTIVITIES
                           where a.CLEANUP_PROJECT_IDX == cLEANUP_PROJECT_IDX
                           && a.CLEANUP_CAT == cLEANUP_CAT
                           select a).ToList();

                return xxx.Select(c => c.CLEANUP_COST).Sum();
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return 0;
            }
        }

        public Guid? InsertUpdateT_OD_CLEANUP_ACTIVITIES(Guid? cLEANUP_ACTIVITY_IDX, Guid? cLEANUP_PROJECT_IDX, string cLEANUP_CAT, string cLEANUP_ACTIVITY, decimal? cLEANUP_COST, 
            string mODIFY_BY, decimal? cLEANUP_UNIT_COST, string qUANTITY, string qUANTITY_UNIT)
        {
            try
            {
                Boolean insInd = false;

                //first grab from PK
                T_OD_CLEANUP_ACTIVITIES e = (from c in ctx.T_OD_CLEANUP_ACTIVITIES
                                             where c.CLEANUP_ACTIVITY_IDX == cLEANUP_ACTIVITY_IDX
                                             select c).FirstOrDefault();

                //insert case
                if (e == null)
                {
                    insInd = true;
                    e = new T_OD_CLEANUP_ACTIVITIES();
                    e.CLEANUP_ACTIVITY_IDX = Guid.NewGuid();
                    e.CREATE_USER_ID = mODIFY_BY;
                    e.CREATE_DT = System.DateTime.Now;
                }
                else
                {
                    e.MODIFY_USER_ID = mODIFY_BY;
                    e.MODIFY_DT = System.DateTime.Now;
                }

                if (cLEANUP_PROJECT_IDX != null) e.CLEANUP_PROJECT_IDX = (Guid)cLEANUP_PROJECT_IDX;
                if (cLEANUP_CAT != null) e.CLEANUP_CAT = cLEANUP_CAT;
                if (cLEANUP_ACTIVITY != null) e.CLEANUP_ACTIVITY = cLEANUP_ACTIVITY;
                if (cLEANUP_COST != null) e.CLEANUP_COST = (decimal)cLEANUP_COST;
                if (cLEANUP_UNIT_COST != null) e.CLEANUP_UNIT_COST = (decimal)cLEANUP_UNIT_COST;
                if (qUANTITY != null) e.QUANTITY = qUANTITY;
                if (qUANTITY_UNIT != null) e.QUANTITY_UNIT = qUANTITY_UNIT;

                if (insInd)
                    ctx.T_OD_CLEANUP_ACTIVITIES.Add(e);

                ctx.SaveChanges();
                return e.CLEANUP_ACTIVITY_IDX;
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }

        }

        public int DeleteT_OD_CLEANUP_ACTIVITIES(Guid cLEANUP_ACTIVITY_IDX)
        {
            try
            {
                var xxx = new T_OD_CLEANUP_ACTIVITIES { CLEANUP_ACTIVITY_IDX = cLEANUP_ACTIVITY_IDX };
                ctx.Entry(xxx).State = EntityState.Deleted;
                ctx.SaveChanges();

                return 1;
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return 0;
            }
        }


        //************** T_OD_REF_DATA **********************************
        public IEnumerable<SelectListItem> get_ddl_T_OD_REF_DATA_by_category(string cat_name, string org_id)
        {
            try
            {
                return (from a in ctx.T_OD_REF_DATA
                           where a.REF_DATA_CAT_NAME == cat_name
                           && (org_id == null ? a.ORG_ID == null : a.ORG_ID == org_id)
                           orderby a.REF_DATA_VAL
                           select new SelectListItem
                           {
                               Value = a.REF_DATA_IDX.ToString(),
                               Text = a.REF_DATA_VAL
                           }).ToList();
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }

        public List<T_OD_REF_DATA> get_T_OD_REF_DATA_by_category(string cat_name)
        {
            try
            {
                return (from a in ctx.T_OD_REF_DATA
                        where a.REF_DATA_CAT_NAME == cat_name
                        orderby a.REF_DATA_VAL
                        select a).ToList();
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }

        public List<SelectListItem> get_ddl_T_OD_REF_DATA_CATEGORIES()
        {
            try
            {
                return (from a in ctx.T_OD_REF_DATA_CATEGORIES
                        orderby a.REF_DATA_CAT_NAME
                        select new SelectListItem
                        {
                            Value = a.REF_DATA_CAT_NAME,
                            Text = a.REF_DATA_CAT_NAME
                        }).ToList();
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }


        //************** T_OD_REF_THREAT_FACTORS **********************************
        public List<T_OD_REF_THREAT_FACTORS> getT_OD_REF_THREAT_FACTORS()
        {
            try
            {
                return (from a in ctx.T_OD_REF_THREAT_FACTORS
                        orderby a.THREAT_FACTOR_SCORE
                        select a).ToList();
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }

        public IEnumerable<SelectListItem> get_ddl_OD_REF_THREAT_FACTORS_by_type(string factor_type)
        {
            try
            {
                var xxx = (from a in ctx.T_OD_REF_THREAT_FACTORS
                           where a.THREAT_FACTOR_TYPE == factor_type
                           orderby a.THREAT_FACTOR_SCORE
                           select new SelectListItem
                           {
                               Value = a.THREAT_FACTOR_IDX.ToString(),
                               Text = a.THREAT_FACTOR_NAME
                           }).ToList();
                xxx.Insert(0, new SelectListItem { Text = "", Value = "" });
                return xxx;
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }

        public T_OD_REF_THREAT_FACTORS getT_OD_REF_THREAT_FACTOR_ByID(Guid threatFactorIDX)
        {
            try
            {
                return (from a in ctx.T_OD_REF_THREAT_FACTORS
                        where a.THREAT_FACTOR_IDX == threatFactorIDX
                        select a).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }


        //************** T_OD_REF_WASTE_TYPE **********************************
        public T_OD_REF_WASTE_TYPE get_T_OD_REF_WASTE_TYPE_by_WasteType(Guid rEF_WASTE_TYPE_IDX)
        {
            try
            {
                return (from a in ctx.T_OD_REF_WASTE_TYPE
                           where a.REF_WASTE_TYPE_IDX == rEF_WASTE_TYPE_IDX
                           select a).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }

        //************** T_OD_REF_WASTE_TYPE_UNITS **********************************
        public IEnumerable<SelectListItem> get_ddl_T_OD_REF_WASTE_TYPE_UNITS_by_WasteType(Guid rEF_WASTE_TYPE_IDX)
        {
            try
            {
                var xxx = (from a in ctx.T_OD_REF_WASTE_TYPE_UNITS
                        join b in ctx.T_PRT_REF_UNITS on a.UNIT_MSR_IDX equals b.UNIT_MSR_IDX
                        where a.REF_WASTE_TYPE_IDX == rEF_WASTE_TYPE_IDX
                        select new SelectListItem
                        {
                            Value = a.UNIT_MSR_IDX.ToString(),
                            Text = b.UNIT_MSR_CD
                        }).ToList();
                return xxx;
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }


        //************** T_OD_REF_DISPOSAL **********************************
        public IEnumerable<SelectListItem> get_ddl_ref_disposal()
        {
            try
            {
                return (from a in ctx.T_OD_REF_DISPOSAL
                        select new SelectListItem
                        {
                            Value = a.REF_DISPOSAL_IDX.ToString(),
                            Text = a.DISPOSAL_NAME
                        }).ToList();
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }

        public T_OD_REF_DISPOSAL getT_OD_REF_DISPOSAL_byID(Guid rEF_DISPOSAL_IDX)
        {
            try
            {
                return (from a in ctx.T_OD_REF_DISPOSAL
                        where a.REF_DISPOSAL_IDX == rEF_DISPOSAL_IDX
                        select a).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }



        //************** REF_CLEANUP_TYPE **********************************
        public IEnumerable<SelectListItem> get_ddl_CLEANUP_PROJECT_TYPE()
        {
            List<SelectListItem> ddl = new List<SelectListItem>();
            ddl.Add(new SelectListItem() { Value = "Estimate", Text = "Estimate" });
            ddl.Add(new SelectListItem() { Value = "Actual", Text = "Actual" });
            return ddl;
        }

        //*************** CALC CLEANUP COSTS
        public int CalcCleanupEstimate(Guid CleanupProjectIDX, bool CalcCleanupCleanupDtl, bool CalcDisposalDtl, bool CalcTransportDtl)
        {
            try
            {
                T_OD_CLEANUP_PROJECT _project = getT_OD_CLEANUP_PROJECT_by_IDX(CleanupProjectIDX);
                if (_project != null) {

                    //CLEANUP_CLEANUP_DTL calculation
                    if (CalcCleanupCleanupDtl)
                    {
                        List<CatSums> DistinctCats = getT_OD_ASSESSMENT_CONTENT_DistinctCatSums(_project.ASSESSMENT_IDX, null);
                        foreach (CatSums Cat in DistinctCats)
                        {
                            //get sum for cat
                            var CategoryResources = (from a in ctx.T_OD_REF_WASTE_TYPE_CAT_CLEANUP
                                                     where a.REF_WASTE_TYPE_CAT == Cat.Category
                                                     select a).ToList();

                            foreach (var refResource in CategoryResources)
                            {
                                decimal? cost = (refResource.PER_UNIT_IND != true ? (Cat.Amount / refResource.PROCESS_RATE_PER_HR * refResource.ASSET_HOURLY_RATE * refResource.ASSET_COUNT) : (Math.Ceiling(Cat.Amount.ConvertOrDefault<decimal>() / refResource.PROCESS_RATE_PER_HR.ConvertOrDefault<decimal>()) * refResource.ASSET_HOURLY_RATE * refResource.ASSET_COUNT));

                                InsertUpdateT_OD_CLEANUP_CLEANUP_DTL(null, CleanupProjectIDX, Cat.Category, refResource.REF_ASSET_NAME, cost);
                            }
                        }
                    }

                    //CLEANUP_DISPOSAL_DTL calculation
                    if (CalcDisposalDtl)
                    {
                        List<DispSums> DistinctDispMethods = getT_OD_ASSESSMENT_CONTENT_DistinctDisposalSums(_project.ASSESSMENT_IDX);
                        foreach (DispSums _disp in DistinctDispMethods)
                        {
                            T_OD_REF_DISPOSAL _ref = getT_OD_REF_DISPOSAL_byID((Guid)_disp.DisposalType);
                            if (_ref != null)
                            {
                                decimal? cost = _disp.AmountWt / 2000 * _ref.PRICE_PER_TON;
                                InsertUpdateT_OD_CLEANUP_DISPOSAL_DTL(null, CleanupProjectIDX, _disp.DisposalType, _disp.AmountWt, cost, _ref.PRICE_PER_TON);
                            }
                        }
                    }

                    //CLEANUP_TRANSPORT_DTL calculation
                    if (CalcTransportDtl)
                    {
                        List<AssessmentContentTypeDisplay> _items = getT_OD_ASSESSMENT_CONTENT_ByDumpAssessmentIDX_TransportDetails(_project.ASSESSMENT_IDX);
                        foreach (AssessmentContentTypeDisplay _item in _items)
                        {
                            decimal _wasteAmt = _item.WASTE_AMT ?? 0;
                            decimal _amt_per_load = _item.TRANSPORT_AMT_PER_LOAD ?? 1;
                            decimal numLoads = Math.Ceiling(_wasteAmt / _amt_per_load);
                            decimal _wasteCost = numLoads * _item.WASTE_DISPOSAL_DIST.ConvertOrDefault<decimal>() * (decimal)81.16;
                            InsertUpdateT_OD_CLEANUP_TRANSPORT_DTL(null, CleanupProjectIDX, _item.REF_WASTE_TYPE_IDX, numLoads.ConvertOrDefault<int>(), _item.WASTE_DISPOSAL_DIST.ConvertOrDefault<decimal>(), (decimal)81.16, _wasteCost);
                        }
                    }


                    //then recalculate and save total cleanup costs
                    decimal? cleanTotCost = getT_OD_CLEANUP_CLEANUP_DTL_Sum_by_ProjectIDX(CleanupProjectIDX);
                    decimal? disposalTotCost = getT_OD_CLEANUP_DISPOSAL_DTL_Sum_by_ProjectIDX(CleanupProjectIDX);
                    decimal? transportTotCost = getT_OD_CLEANUP_TRANSPORT_DTL_Sum_by_ProjectIDX(CleanupProjectIDX);

                    decimal? TotCost = cleanTotCost 
                        + (_project.COST_TRANSPORT_AMT ?? 0)
                        + (disposalTotCost ?? 0)
                        + (transportTotCost ?? 0) 
                        + (_project.COST_RESTORE_AMT ?? 0) 
                        + (_project.COST_SURVEIL_AMT ?? 0);

                    InsertUpdateT_OD_CLEANUP_PROJECT(CleanupProjectIDX, null, null, null, null, null, cleanTotCost, transportTotCost, disposalTotCost, null, null, TotCost, null, null, null);

                    return 1;
                }

                return 0;
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return 0;
            }
        }



        //***************************site local (temp when importing)****************************************
        /// <summary>
        /// Creates a new local SITE record and validates it according to the validation rules contained in XML file
        /// </summary>
        /// <param name="UserIDX"></param>
        /// <param name="colVals">Name value pair for the different fields to import into the project record</param>
        /// <returns></returns>
        public SiteImportType InsertOrUpdate_T_OD_SITE_local(string UserIDX, Dictionary<string, string> colVals, string path)
        {

            try
            {
                SiteImportType e = new SiteImportType();

                //determine if new Site record or updating existing one
                Boolean insInd = true;

                //try to get existing project based on SITE_IDX
                Guid siteIDX;
                string siteIDXStr = Utils.GetValueOrDefault(colVals, "SITE_IDX");
                if (Guid.TryParse(siteIDXStr, out siteIDX))
                {
                    T_OD_SITES p = getT_OD_SITES_BySITEIDX(siteIDX);
                    if (p != null)
                    {
                        insInd = false;
                        e.T_PRT_SITES.SITE_IDX = p.SITE_IDX;
                        e.T_OD_SITES.SITE_IDX = p.SITE_IDX;
                    }
                }

                if (insInd)
                {
                    e.T_PRT_SITES.SITE_IDX = Guid.NewGuid();
                    e.T_PRT_SITES.CREATE_DT = System.DateTime.Now;
                    e.T_PRT_SITES.CREATE_USER_ID = UserIDX;
                    e.T_OD_SITES.SITE_IDX = e.T_PRT_SITES.SITE_IDX;
                }
                else
                {
                    e.T_PRT_SITES.MODIFY_DT = System.DateTime.Now;
                    e.T_PRT_SITES.MODIFY_USER_ID = UserIDX;
                }
                


                //get import config rules
                List<ConfigInfoType> _allRules = Utils.GetAllColumnInfo("S", path);

                //explicitly validate mandatory fields
                foreach (string entry in Utils.GetMandatoryImportFieldList("T_PRT_SITES", path))
                    TableFields_validate(ref e, _allRules, colVals, entry, "T_PRT_SITES");

                //then only validate optional fields if supplied (performance)
                foreach (string entry in Utils.GetOptionalImportFieldList("T_PRT_SITES", path))
                    TableFields_validate(ref e, _allRules, colVals, entry, "T_PRT_SITES");

                //then only validate optional fields if supplied (performance)
                foreach (string entry in Utils.GetOptionalImportFieldList("T_OD_SITES", path))
                    TableFields_validate(ref e, _allRules, colVals, entry, "T_OD_SITES");

                //then only validate optional fields if supplied (performance)

                foreach (string entry in Utils.GetOptionalImportFieldList("T_OD_ASSESSMENTS", path))
                    TableFields_validate(ref e, _allRules, colVals, entry, "T_OD_ASSESSMENTS");

                //********************** CUSTOM POST VALIDATION ********************************************
                //SECURITY CHECK FOR ABILIITY TO IMPORT ORG_ID
                //e.ORG_NAME = Utils.GetValueOrDefault(colVals, "ORG_NAME");

                //T_OE_ORGANIZATION oo = db_Ref.GetT_OE_ORGANIZATION_ByName(e.ORG_NAME);
                //if (oo != null)
                //    e.T_OE_PROJECT.ORG_IDX = oo.ORG_IDX;
                //else
                //{
                //    e.VALIDATE_CD = false;
                //    e.VALIDATE_MSG += "Unable to import for this organization.";
                //}


                ////MEDIA
                //e.MEDIA_NAME = Utils.GetValueOrDefault(colVals, "MEDIA_TAG");
                //if (!string.IsNullOrEmpty(e.MEDIA_NAME))
                //{
                //    T_OE_REF_TAGS media1 = db_Ref.GetT_OE_REF_TAGS_ByCategoryAndName("Project Media", e.MEDIA_NAME);
                //    if (media1 != null)
                //        e.T_OE_PROJECT.MEDIA_TAG = media1.TAG_IDX;
                //    else
                //    {
                //        e.VALIDATE_CD = false;
                //        e.VALIDATE_MSG += "Invalid Media name.";
                //    }
                //}

                //e.PROGRAM_AREAS = Utils.GetValueOrDefault(colVals, "PROGRAM_AREAS");
                //e.FEATURES = Utils.GetValueOrDefault(colVals, "FEATURES");
                //********************** CUSTOM POST VALIDATION END ********************************************

                return e;
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }

        public static void TableFields_validate(ref SiteImportType a, List<ConfigInfoType> t, Dictionary<string, string> colVals, string f, string tableName)
        {
            var _rules = t.Find(item => item._name == f);   //import validation rules for this field
            if (_rules == null)
                return;

            string _value = Utils.GetValueOrDefault(colVals, f); //supplied value for this field

            if (!string.IsNullOrEmpty(_value)) //if value is supplied
            {
                _value = _value.Trim();

                //strings: field length validation and substring 
                if (_rules._datatype == "" && _rules._length != null)
                {
                    if (_value.Length > _rules._length)
                    {
                        a.VALIDATE_CD = false;
                        a.VALIDATE_MSG = (a.VALIDATE_MSG + f + " length (" + _rules._length + ") exceeded. ");
                        _value = _value.SubStringPlus(0, (int)_rules._length);
                    }
                }

                //integers: check type
                if (_rules._datatype == "int")
                {
                    int n;
                    if (int.TryParse(_value, out n) == false)
                    {
                        a.VALIDATE_CD = false;
                        a.VALIDATE_MSG = (a.VALIDATE_MSG + f + " not numeric. ");
                    }
                }

            }
            else
            {
                //required check
                if (_rules._req == "Y")
                {
                    if (_rules._datatype == "")
                        _value = "-";
                    a.VALIDATE_CD = false;
                    a.VALIDATE_MSG = (a.VALIDATE_MSG + "Required field " + f + " missing. ");
                }
            }

            //finally set the value before returning
            try
            {
                if (tableName == "T_PRT_SITES")
                {
                    if (_rules._datatype == "")
                        typeof(T_PRT_SITES).GetProperty(f).SetValue(a.T_PRT_SITES, _value);
                    else if (_rules._datatype == "int")
                        typeof(T_PRT_SITES).GetProperty(f).SetValue(a.T_PRT_SITES, _value.ConvertOrDefault<int?>());
                    else if (_rules._datatype == "dec")
                        typeof(T_PRT_SITES).GetProperty(f).SetValue(a.T_PRT_SITES, _value.ConvertOrDefault<decimal?>());
                }
                else if (tableName == "T_OD_SITES")
                {
                    if (_rules._datatype == "")
                        typeof(T_OD_SITES).GetProperty(f).SetValue(a.T_OD_SITES, _value);
                    else if (_rules._datatype == "int")
                        typeof(T_OD_SITES).GetProperty(f).SetValue(a.T_OD_SITES, _value.ConvertOrDefault<int?>());
                }
                else if (tableName == "T_OD_ASSESSMENTS")
                {
                    if (a.T_OD_ASSESSMENTS == null)
                    {
                        a.T_OD_ASSESSMENTS = new T_OD_ASSESSMENTS
                        {
                            ASSESSMENT_IDX = Guid.NewGuid()
                        };
                    }

                    if (_rules._datatype == "")
                        typeof(T_OD_ASSESSMENTS).GetProperty(f).SetValue(a.T_OD_ASSESSMENTS, _value);
                    else if (_rules._datatype == "int")
                        typeof(T_OD_ASSESSMENTS).GetProperty(f).SetValue(a.T_OD_ASSESSMENTS, _value.ConvertOrDefault<int?>());
                    else if (_rules._datatype == "date")
                        typeof(T_OD_ASSESSMENTS).GetProperty(f).SetValue(a.T_OD_ASSESSMENTS, _value.ConvertOrDefault<DateTime?>());
                }
            }
            catch
            {
                //let fail silently
            }
        }
    }

}
