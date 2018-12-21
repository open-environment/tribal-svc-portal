using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using TribalSvcPortal.Data.Models;
using TribalSvcPortal.AppLogic.BusinessLogicLayer;

namespace TribalSvcPortal.AppLogic.DataAccessLayer
{
    public class OpenDumpSiteListDisplay
    {
        public string OrgName { get; set; }
        public Guid Site_Idx { get; set; }
        public string SiteName { get; set; }
        public string SiteAddress { get; set; }
        public string ReportedBy { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? ReportedOn { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]

        public T_OD_DUMP_ASSESSMENTS LastAssessment { get; set; }
        public int? HEALTH_THREAT_SCORE { get; set; }
        public decimal? EST_CLEANUP_COSTS { get; set; }
    }

    public class RefThreatFactor
    {
        public Guid THREAT_FACTOR_IDX { get; set; }
        public string THREAT_FACTOR_TYPE { get; set; }
        public string THREAT_FACTOR_NAME { get; set; }
        public int? THREAT_FACTOR_SCORE { get; set; }

    }

    public class SelectedWasteTypeDisplay {
        public T_OD_REF_WASTE_TYPE T_OD_REF_WASTE_TYPE { get; set; }
        public bool IsChecked { get; set; }
    }

    public class CatSums
    {
        public string Category { get; set; }
        public decimal? Amount { get; set; }
    }

    public class AssessmentContentTypeDisplay
    {
        public Guid DUMP_ASSESSMENTS_CONTENT_IDX { get; set; }
        public Guid DUMP_ASSESSMENTS_IDX { get; set; }
        public Guid REF_WASTE_TYPE_IDX { get; set; }
        public string REF_WASTE_TYPE_NAME { get; set; }
        public string REF_WASTE_TYPE_CAT { get; set; }

        public decimal? WASTE_AMT { get; set; }
        public Guid? UNIT_MSR_IDX { get; set; }
        public Guid? WASTE_DISPOSAL_METHOD { get; set; }
        public string WASTE_DISPOSAL_DIST { get; set; }
        public IEnumerable<SelectListItem> ddl_Unit { get; set; }
    }

    public class AssessmentCleanupDisplayType {
        public Guid DUMP_ASSESSMENT_CLEANUP_IDX { get; set; }
        public Guid DUMP_ASSESSMENTS_IDX { get; set; }
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
        public Guid DUMP_ASSESSMENTS_IDX { get; set; }
        public DateTime ASSESSMENT_DT { get; set; }
        public string ASSESSED_BY { get; set; }
        public string ASSESSMENT_NOTES { get; set; }
        public int? HEALTH_THREAT_SCORE { get; set; }
        public decimal? COST_TOTAL_AMT { get; set; }
        public string ORG_NAME { get; set; }
        public string SITE_NAME { get; set; }

    }

    public interface IDbOpenDump
    {
        //************** T_OD_SITES **********************************
        T_OD_SITES getT_OD_SITES_BySITEIDX(Guid Siteidx);
        List<OpenDumpSiteListDisplay> getT_OD_SITES_ListBySearch(string id, string searchStr, string selOrg);
        Guid? InsertUpdateT_OD_SITES(Guid sITE_IDX, string rEPORTED_BY, DateTime? rEPORTED_ON, Guid? cOMMUNITY_IDX, Guid? sITE_SETTING_IDX, Guid? pF_AQUIFER_VERT_DIST, Guid? pF_SURF_WATER_HORIZ_DIST, Guid? pF_HOMES_DIST);

        //************** T_OD_DUMP_ASSESSMENTS **********************************
        T_OD_DUMP_ASSESSMENTS getT_OD_DUMP_ASSESSMENTS_ByDumpAssessmentIDX(Guid DumpAssessmentIDX);
        List<AssessmentSummaryDisplayType> getT_OD_DUMP_ASSESSMENTS_BySITEIDX(Guid Siteidx);
        List<AssessmentSummaryDisplayType> getT_OD_DUMP_ASSESSMENTS_ByUser(string UserID);
        IEnumerable<SelectListItem> get_ddl_T_OD_DUMP_ASSESSMENTS_by_BySITEIDX(Guid? Siteidx);
        int deleteT_OD_DumpAssessment(Guid DumpAssessmentIDX);
        Guid? InsertUpdateT_OD_DumpAssessment(Guid dUMPASSESSMENTS_IDX, Guid? sITE_IDX, DateTime? aSSESSMENT_DT, string aSSESSED_BY, Guid? ASSESSMENT_TYPE_IDX, bool? ACTIVE_SITE_IND, string SITE_DESCRIPTION,
                                                        string ASSESSMENT_NOTES, decimal? aREA_ACRES, decimal? vOLUMN_CU_YD, Guid? hF_RAINFALL, Guid? hF_DRAINAGE, Guid? hF_FLOODING, Guid? hF_BURNING, Guid? hF_FENCING,
                                                        Guid? hF_ACCESS_CONTROL, Guid? hF_PUBLIC_CONCERN, int? hEALTH_THREAT_SCORE, decimal? cOST_CLEANUP_AMT, decimal? cOST_TRANSPORT_AMT, decimal? cOST_DISPOSAL_AMT,
                                                        decimal? cOST_RESTORE_AMT, decimal? cOST_SURVEIL_AMT, decimal? cOST_TOTAL_AMT);

        //************** T_OD_DUMP_ASSESSMENT_CONTENT **********************************
        List<SelectedWasteTypeDisplay> getT_OD_DUMP_ASSESSMENT_CONTENT_by_AssessIDX(Guid? AssessmentIdx);
        List<AssessmentContentTypeDisplay> getT_OD_DUMP_ASSESSMENT_CONTENT_ByDumpAssessmentIDX(Guid DumpAssessmentIDX);
        Guid? InsertUpdateT_OD_DumpAssessment_Content(Guid? dUMP_ASSESSMENTS_CONTENT_IDX, Guid? dUMP_ASSESSMENTS_IDX, Guid? rEF_WASTE_TYPE_IDX, decimal? wASTE_AMT, Guid? wASTE_UNIT_MSR, Guid? wASTE_DISPOSAL_METHOD, string wASTE_DISPOSAL_DIST, bool IS_CHECKED);
        List<CatSums> getT_OD_DUMP_ASSESSMENT_CONTENT_DistinctCatSums(Guid dUMPASSESSMENTS_IDX, string Cat);

        //************** T_OD_DUMP_ASSESSMENT_DOCUMENTS *********************************
        Guid? InsertUpdateT_OD_DUMP_ASSESSMENT_DOCUMENTS(Guid? dOC_IDX, Guid dUMPASSESSMENTS_IDX);
        List<T_PRT_DOCUMENTS> GetT_PRT_DOCUMENTS_ByDumpAssessmentsIDx(Guid dUMPASSESSMENTS_IDX);
        List<T_PRT_DOCUMENTS> GetT_PRT_DOCUMENTS_Photos_ByDumpAssessmentsIDx(Guid dUMPASSESSMENTS_IDX);


        //************** T_OD_DUMP_ASSESSMENT_CLEANUP **********************************
        Guid? InsertUpdateT_OD_DUMP_ASSESSMENT_CLEANUP(Guid? dUMP_ASSESSMENT_CLEANUP_IDX, Guid? dUMP_ASSESSMENTS_IDX, string rEF_WASTE_TYPE_CAT, string rEF_ASSET_NAME, decimal? cLEANUP_COST);
        List<AssessmentCleanupDisplayType> getT_OD_DUMP_ASSESSMENT_CLEANUP_by_AssessIDX(Guid? dUMPASSESSMENTS_IDX);
        decimal? getT_OD_DUMP_ASSESSMENT_CLEANUP_Sum_by_AssessIDX(Guid? dUMPASSESSMENTS_IDX);

        //****************T_OD_DUMP_ASSESSMENT_RESTORE **********************************
        T_OD_DUMP_ASSESSMENT_RESTORE getT_OD_DUMP_ASSESSMENT_RESTORE_by_IDX(Guid? dUMP_ASSESSMENT_RESTORE_IDX);
        List<T_OD_DUMP_ASSESSMENT_RESTORE> getT_OD_DUMP_ASSESSMENT_RESTORE_by_DumpAssessIDXandCat(Guid? dUMPASSESSMENTS_IDX, string rESTORE_CAT);
        decimal? getT_OD_DUMP_ASSESSMENT_RESTORE_Sum_by_AssessIDX(Guid? dUMPASSESSMENTS_IDX, string rESTORE_CAT);
        Guid? InsertUpdateT_OD_DUMP_ASSESSMENT_RESTORE(Guid? dUMP_ASSESSMENT_RESTORE_IDX, Guid? dUMP_ASSESSMENTS_IDX, string rESTORE_CAT, string rESTORE_ACTIVITY, decimal? rESTORE_COST, string mODIFY_BY);
        int DeleteT_OD_DUMP_ASSESSMENT_RESTORE(Guid dUMP_ASSESSMENT_RESTORE_IDX);

        //************** T_OD_REF_DATA **************************************************
        IEnumerable<SelectListItem> get_ddl_T_OD_REF_DATA_by_category(string cat_name, string org_id);
        List<T_OD_REF_DATA> get_T_OD_REF_DATA_by_category(string cat_name);
        List<SelectListItem> get_ddl_T_OD_REF_DATA_CATEGORIES();


        //************** T_OD_REF_THREAT_FACTORS ****************************************
        List<T_OD_REF_THREAT_FACTORS> getT_OD_REF_THREAT_FACTORS();
        IEnumerable<SelectListItem> get_ddl_OD_REF_THREAT_FACTORS_by_type(string factor_type);
        T_OD_REF_THREAT_FACTORS getT_OD_REF_THREAT_FACTOR_ByID(Guid threatFactorIDX);

        //************** T_OD_REF_WASTE_TYPE_UNITS **************************************
        IEnumerable<SelectListItem> get_ddl_T_OD_REF_WASTE_TYPE_UNITS_by_WasteType(Guid rEF_WASTE_TYPE_IDX);

        //************** T_OD_REF_DISPOSAL **********************************************
        IEnumerable<SelectListItem> get_ddl_ref_disposal();

        //util
        int CalcCleanup(Guid DumpAssessmentIDX);

    }

    public class DbOpenDump : IDbOpenDump
    {
        private readonly ApplicationDbContext ctx;
        public DbOpenDump(ApplicationDbContext _context)
        {
            ctx = _context;
        }


        //************** T_OD_SITES **********************************
        public T_OD_SITES getT_OD_SITES_BySITEIDX(Guid Siteidx)
        {
            try
            {
                return (from a in ctx.T_OD_SITES
                        where a.SITE_IDX == Siteidx
                        select a).FirstOrDefault();
            }
            catch (Exception ex)
            {
                log.LogEFException(ex);
                return null;
            }
        }

        public List<OpenDumpSiteListDisplay> getT_OD_SITES_ListBySearch(string id, string searchStr, string selOrg)
        {
            try
            {
                return (from a in ctx.T_PRT_ORG_USERS
                        join b in ctx.T_PRT_ORGANIZATIONS on a.ORG_ID equals b.ORG_ID
                        join d in ctx.T_PRT_SITES on a.ORG_ID equals d.ORG_ID
                        join e in ctx.T_OD_SITES on d.SITE_IDX equals e.SITE_IDX
                        where a.Id == id
                        && (selOrg == null || a.ORG_ID == selOrg)
                        && (searchStr == null || (d.SITE_NAME.ToUpper().Contains(searchStr.ToUpper())
                           || d.SITE_ADDRESS.ToUpper().Contains(searchStr.ToUpper()) ))
                        orderby b.ORG_NAME, d.SITE_NAME
                        select new OpenDumpSiteListDisplay
                        {
                            OrgName = b.ORG_NAME,
                            Site_Idx = d.SITE_IDX,
                            SiteName = d.SITE_NAME,
                            SiteAddress = d.SITE_ADDRESS,
                            ReportedBy = e.REPORTED_BY,
                            ReportedOn = e.REPORTED_ON,
                            Latitude = d.LATITUDE,
                            Longitude = d.LONGITUDE,
                            LastAssessment = (from v1 in ctx.T_OD_DUMP_ASSESSMENTS
                                             where v1.SITE_IDX == d.SITE_IDX
                                             orderby v1.ASSESSMENT_DT descending
                                             select v1).FirstOrDefault(),
                            HEALTH_THREAT_SCORE = (from v1 in ctx.T_OD_DUMP_ASSESSMENTS
                                                   where v1.SITE_IDX == d.SITE_IDX
                                                   && v1.HEALTH_THREAT_SCORE != null
                                                   orderby v1.ASSESSMENT_DT descending
                                                   select v1.HEALTH_THREAT_SCORE).FirstOrDefault(),
                            EST_CLEANUP_COSTS = (from v1 in ctx.T_OD_DUMP_ASSESSMENTS
                                                   where v1.SITE_IDX == d.SITE_IDX
                                                   && v1.COST_TOTAL_AMT != null
                                                   orderby v1.ASSESSMENT_DT descending
                                                   select v1.COST_TOTAL_AMT).FirstOrDefault()
                        }).ToList();
            }
            catch (Exception ex)
            {
                log.LogEFException(ex);
                return null;
            }
        }

        public Guid? InsertUpdateT_OD_SITES(Guid sITE_IDX, string rEPORTED_BY, DateTime? rEPORTED_ON, Guid? cOMMUNITY_IDX, Guid? sITE_SETTING_IDX, Guid? pF_AQUIFER_VERT_DIST,
    Guid? pF_SURF_WATER_HORIZ_DIST, Guid? pF_HOMES_DIST)
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

                if (insInd)
                    ctx.T_OD_SITES.Add(e);
                ctx.SaveChanges();
                return e.SITE_IDX;
            }
            catch (Exception ex)
            {
                log.LogEFException(ex);
                return null;
            }
        }


        //************** T_OD_DUMP_ASSESSMENTS **********************************
        public T_OD_DUMP_ASSESSMENTS getT_OD_DUMP_ASSESSMENTS_ByDumpAssessmentIDX(Guid DumpAssessmentIDX)
        {
            try
            {
                return (from a in ctx.T_OD_DUMP_ASSESSMENTS
                        where a.DUMP_ASSESSMENTS_IDX == DumpAssessmentIDX
                        select a).FirstOrDefault();
            }
            catch (Exception ex)
            {
                log.LogEFException(ex);
                return null;
            }
        }

        public List<AssessmentSummaryDisplayType> getT_OD_DUMP_ASSESSMENTS_BySITEIDX(Guid Siteidx)
        {
            try
            {
                return (from a in ctx.T_OD_DUMP_ASSESSMENTS
                        where a.SITE_IDX == Siteidx
                        orderby a.ASSESSMENT_DT descending
                        select new AssessmentSummaryDisplayType
                        {
                            DUMP_ASSESSMENTS_IDX = a.DUMP_ASSESSMENTS_IDX,
                            ASSESSMENT_DT = a.ASSESSMENT_DT,
                            ASSESSED_BY = a.ASSESSED_BY,
                            ASSESSMENT_NOTES = a.ASSESSMENT_NOTES,
                            HEALTH_THREAT_SCORE = a.HEALTH_THREAT_SCORE,
                            COST_TOTAL_AMT = a.COST_TOTAL_AMT,
                            ORG_NAME = null,
                            SITE_NAME = null
                        }).ToList();
            }
            catch (Exception ex)
            {
                log.LogEFException(ex);
                return null;
            }
        }

        public List<AssessmentSummaryDisplayType> getT_OD_DUMP_ASSESSMENTS_ByUser(string UserID)
        {
            try
            {
                return (from a in ctx.T_OD_DUMP_ASSESSMENTS
                        join b in ctx.T_PRT_SITES on a.SITE_IDX equals b.SITE_IDX
                        join c in ctx.T_PRT_ORG_USERS on b.ORG_ID equals c.ORG_ID
                        where c.Id == UserID
                        orderby a.ASSESSMENT_DT descending
                        select new AssessmentSummaryDisplayType {
                            DUMP_ASSESSMENTS_IDX= a.DUMP_ASSESSMENTS_IDX,
                            ASSESSMENT_DT = a.ASSESSMENT_DT,
                            ASSESSED_BY = a.ASSESSED_BY,
                            ASSESSMENT_NOTES = a.ASSESSMENT_NOTES,
                            HEALTH_THREAT_SCORE = a.HEALTH_THREAT_SCORE,
                            COST_TOTAL_AMT = a.COST_TOTAL_AMT,
                            ORG_NAME = c.ORG_ID,
                            SITE_NAME = b.SITE_NAME
                        }).ToList();
            }
            catch (Exception ex)
            {
                log.LogEFException(ex);
                return null;
            }
        }



        public IEnumerable<SelectListItem> get_ddl_T_OD_DUMP_ASSESSMENTS_by_BySITEIDX(Guid? Siteidx)
        {
            try
            {

                return (from a in ctx.T_OD_DUMP_ASSESSMENTS
                        where a.SITE_IDX == Siteidx
                        orderby a.ASSESSMENT_DT descending
                        select new SelectListItem
                        {
                            Value = a.DUMP_ASSESSMENTS_IDX.ToString(),
                            Text = a.ASSESSMENT_DT.ToString("MM/dd/yyyy")
                        }).ToList();
            }
            catch (Exception ex)
            {
                log.LogEFException(ex);
                return null;
            }
        }

        public int deleteT_OD_DumpAssessment(Guid DumpAssessmentIDX)
        {
            try
            {
                T_OD_DUMP_ASSESSMENTS tda = new T_OD_DUMP_ASSESSMENTS { DUMP_ASSESSMENTS_IDX = DumpAssessmentIDX };
                ctx.Entry(tda).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                ctx.SaveChanges();

                return 1;
            }
            catch (Exception ex)
            {
                log.LogEFException(ex);
                return 0;
            }
        }

        public Guid? InsertUpdateT_OD_DumpAssessment(Guid dUMPASSESSMENTS_IDX, Guid? sITE_IDX, DateTime? aSSESSMENT_DT, string aSSESSED_BY, Guid? ASSESSMENT_TYPE_IDX, bool? ACTIVE_SITE_IND, string SITE_DESCRIPTION,
                                                        string ASSESSMENT_NOTES, decimal? aREA_ACRES, decimal? vOLUMN_CU_YD, Guid? hF_RAINFALL, Guid? hF_DRAINAGE, Guid? hF_FLOODING, Guid? hF_BURNING, Guid? hF_FENCING, 
                                                        Guid? hF_ACCESS_CONTROL, Guid? hF_PUBLIC_CONCERN, int? hEALTH_THREAT_SCORE, decimal? cOST_CLEANUP_AMT, decimal? cOST_TRANSPORT_AMT, decimal? cOST_DISPOSAL_AMT, 
                                                        decimal? cOST_RESTORE_AMT, decimal? cOST_SURVEIL_AMT, decimal? cOST_TOTAL_AMT)
        {
            try
            {
                Boolean insInd = false;

                T_OD_DUMP_ASSESSMENTS e = (from c in ctx.T_OD_DUMP_ASSESSMENTS
                                           where c.DUMP_ASSESSMENTS_IDX == dUMPASSESSMENTS_IDX
                                           select c).FirstOrDefault();

                //insert case
                if (e == null)
                {
                    insInd = true;
                    e = new T_OD_DUMP_ASSESSMENTS();
                    e.DUMP_ASSESSMENTS_IDX = Guid.NewGuid();
                }

                if (sITE_IDX != null) e.SITE_IDX = (Guid)sITE_IDX;
                if (aSSESSMENT_DT != null) e.ASSESSMENT_DT = (DateTime)aSSESSMENT_DT;
                if (aSSESSED_BY != null) e.ASSESSED_BY = aSSESSED_BY;
                if (ASSESSMENT_TYPE_IDX != null) e.ASSESSMENT_TYPE_IDX = ASSESSMENT_TYPE_IDX;
                if (ACTIVE_SITE_IND != null) e.ACTIVE_SITE_IND = (bool)ACTIVE_SITE_IND;
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
                if (cOST_CLEANUP_AMT != null) e.COST_CLEANUP_AMT = cOST_CLEANUP_AMT;
                if (cOST_TRANSPORT_AMT != null) e.COST_TRANSPORT_AMT = cOST_TRANSPORT_AMT;
                if (cOST_DISPOSAL_AMT != null) e.COST_DISPOSAL_AMT = cOST_DISPOSAL_AMT;
                if (cOST_RESTORE_AMT != null) e.COST_RESTORE_AMT = cOST_RESTORE_AMT;
                if (cOST_SURVEIL_AMT != null) e.COST_SURVEIL_AMT = cOST_SURVEIL_AMT;
                if (cOST_TOTAL_AMT != null)
                    e.COST_TOTAL_AMT = cOST_TOTAL_AMT;
                else if (cOST_CLEANUP_AMT != null || cOST_TRANSPORT_AMT != null || cOST_DISPOSAL_AMT != null || cOST_RESTORE_AMT != null || cOST_SURVEIL_AMT != null)
                    e.COST_TOTAL_AMT = (e.COST_CLEANUP_AMT??0) + (e.COST_TRANSPORT_AMT??0) + (e.COST_DISPOSAL_AMT??0) + (e.COST_RESTORE_AMT??0) + (e.COST_SURVEIL_AMT??0);

                if (insInd)
                    ctx.T_OD_DUMP_ASSESSMENTS.Add(e);

                ctx.SaveChanges();
                return e.DUMP_ASSESSMENTS_IDX;
            }
            catch (Exception ex)
            {
                log.LogEFException(ex);
                return null;
            }

        }



        //************** T_OD_DUMP_ASSESSMENT_CONTENT **********************************
        public List<SelectedWasteTypeDisplay> getT_OD_DUMP_ASSESSMENT_CONTENT_by_AssessIDX(Guid? AssessmentIdx)
        {
            try
            {
                return (from a in ctx.T_OD_REF_WASTE_TYPE
                        join b in ctx.T_OD_DUMP_ASSESSMENT_CONTENT.Where(o => o.DUMP_ASSESSMENTS_IDX == AssessmentIdx) on a.REF_WASTE_TYPE_IDX equals b.REF_WASTE_TYPE_IDX
                        into sr
                        from x in sr.DefaultIfEmpty()  //left join
                        orderby a.REF_WASTE_TYPE_NAME
                        select new SelectedWasteTypeDisplay
                        {
                            T_OD_REF_WASTE_TYPE = a,
                            IsChecked = (x.DUMP_ASSESSMENTS_CONTENT_IDX != null)
                        }).ToList();
            }
            catch (Exception ex)
            {
                log.LogEFException(ex);
                return null;
            }
        }

        public List<AssessmentContentTypeDisplay> getT_OD_DUMP_ASSESSMENT_CONTENT_ByDumpAssessmentIDX(Guid DumpAssessmentIDX)
        {
            try
            {
                return (from a in ctx.T_OD_DUMP_ASSESSMENT_CONTENT
                        join b in ctx.T_OD_REF_WASTE_TYPE on a.REF_WASTE_TYPE_IDX equals b.REF_WASTE_TYPE_IDX
                        where a.DUMP_ASSESSMENTS_IDX == DumpAssessmentIDX
                        orderby b.REF_WASTE_TYPE_CAT, b.REF_WASTE_TYPE_NAME
                        select new AssessmentContentTypeDisplay {
                            DUMP_ASSESSMENTS_CONTENT_IDX = a.DUMP_ASSESSMENTS_CONTENT_IDX,
                            DUMP_ASSESSMENTS_IDX = a.DUMP_ASSESSMENTS_IDX,
                            REF_WASTE_TYPE_IDX = a.REF_WASTE_TYPE_IDX,
                            REF_WASTE_TYPE_NAME = b.REF_WASTE_TYPE_NAME,
                            REF_WASTE_TYPE_CAT = b.REF_WASTE_TYPE_CAT,
                            WASTE_AMT = a.WASTE_AMT,
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
                log.LogEFException(ex);
                return null;
            }
        }

        public Guid? InsertUpdateT_OD_DumpAssessment_Content(Guid? dUMP_ASSESSMENTS_CONTENT_IDX, Guid? dUMP_ASSESSMENTS_IDX, Guid? rEF_WASTE_TYPE_IDX, decimal? wASTE_AMT, Guid? wASTE_UNIT_MSR, Guid? wASTE_DISPOSAL_METHOD, string wASTE_DISPOSAL_DIST, bool IS_CHECKED)
        {
            try
            {
                Boolean insInd = false;

                //first try grabbing from PK
                T_OD_DUMP_ASSESSMENT_CONTENT e = (from c in ctx.T_OD_DUMP_ASSESSMENT_CONTENT
                                                  where c.DUMP_ASSESSMENTS_CONTENT_IDX == dUMP_ASSESSMENTS_CONTENT_IDX
                                                  select c).FirstOrDefault();

                //then try grabbing from composite key
                if (e == null)
                    e = (from c in ctx.T_OD_DUMP_ASSESSMENT_CONTENT
                         where c.DUMP_ASSESSMENTS_IDX == dUMP_ASSESSMENTS_IDX 
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
                        e = new T_OD_DUMP_ASSESSMENT_CONTENT();
                        e.DUMP_ASSESSMENTS_CONTENT_IDX = Guid.NewGuid();
                        e.DUMP_ASSESSMENTS_IDX = (Guid)dUMP_ASSESSMENTS_IDX;
                        e.REF_WASTE_TYPE_IDX = (Guid)rEF_WASTE_TYPE_IDX;
                    }
                    if (wASTE_AMT != null) e.WASTE_AMT = wASTE_AMT;
                    if (wASTE_UNIT_MSR != null) e.UNIT_MSR_IDX = wASTE_UNIT_MSR;
                    if (wASTE_DISPOSAL_METHOD != null) e.WASTE_DISPOSAL_METHOD = wASTE_DISPOSAL_METHOD;
                    if (wASTE_DISPOSAL_DIST != null) e.WASTE_DISPOSAL_DIST = wASTE_DISPOSAL_DIST;

                    if (insInd)
                        ctx.T_OD_DUMP_ASSESSMENT_CONTENT.Add(e);
                    ctx.SaveChanges();
                }
                return dUMP_ASSESSMENTS_IDX;
            }
            catch (Exception ex)
            {
                log.LogEFException(ex);
                return null;
            }

        }

        public List<CatSums> getT_OD_DUMP_ASSESSMENT_CONTENT_DistinctCatSums(Guid dUMPASSESSMENTS_IDX, string Cat) {
            try
            {
                return (from a in ctx.T_OD_DUMP_ASSESSMENT_CONTENT
                        join b in ctx.T_OD_REF_WASTE_TYPE on a.REF_WASTE_TYPE_IDX equals b.REF_WASTE_TYPE_IDX
                        where a.DUMP_ASSESSMENTS_IDX == dUMPASSESSMENTS_IDX
                        && (Cat == null ? true : b.REF_WASTE_TYPE_CAT == Cat)
                        group a by new { b.REF_WASTE_TYPE_CAT } into grp
                        select new CatSums
                        {
                            Category = grp.Key.REF_WASTE_TYPE_CAT,
                            Amount = grp.Sum(a => a.WASTE_AMT)
                        }).ToList();
            }
            catch (Exception ex)
            {
                log.LogEFException(ex);
                return null;
            }
        }


        //************** T_OD_DUMP_ASSESSMENT_DOCUMENTS **********************************
        public Guid? InsertUpdateT_OD_DUMP_ASSESSMENT_DOCUMENTS(Guid? dOC_IDX, Guid dUMPASSESSMENTS_IDX)
        {

            try
            {
                Boolean insInd = false;

                T_OD_DUMP_ASSESSMENT_DOCS e = (from c in ctx.T_OD_DUMP_ASSESSMENT_DOCS
                                               where c.DOC_IDX == dOC_IDX
                                               select c).FirstOrDefault();

                //insert case
                if (e == null)
                {
                    insInd = true;
                    e = new T_OD_DUMP_ASSESSMENT_DOCS();

                }

                if (dOC_IDX != null) e.DOC_IDX = (Guid)dOC_IDX;
                if (dUMPASSESSMENTS_IDX != null) e.DUMP_ASSESSMENTS_IDX = dUMPASSESSMENTS_IDX;

                if (insInd)
                    ctx.T_OD_DUMP_ASSESSMENT_DOCS.Add(e);

                ctx.SaveChanges();
                return e.DOC_IDX;
            }
            catch (Exception ex)
            {
                log.LogEFException(ex);
                return null;
            }

        }

        public List<T_PRT_DOCUMENTS> GetT_PRT_DOCUMENTS_ByDumpAssessmentsIDx(Guid dUMPASSESSMENTS_IDX)
        {
            try
            {
                return (from a in ctx.T_OD_DUMP_ASSESSMENT_DOCS
                        join b in ctx.T_PRT_DOCUMENTS on a.DOC_IDX equals b.DOC_IDX
                        where a.DUMP_ASSESSMENTS_IDX == dUMPASSESSMENTS_IDX
                        && !b.DOC_FILE_TYPE.Contains("image")
                        select b).ToList();
            }
            catch (Exception ex)
            {
                log.LogEFException(ex);
                return null;
            }

        }

        public List<T_PRT_DOCUMENTS> GetT_PRT_DOCUMENTS_Photos_ByDumpAssessmentsIDx(Guid dUMPASSESSMENTS_IDX)
        {
            try
            {
                return (from a in ctx.T_OD_DUMP_ASSESSMENT_DOCS
                        join b in ctx.T_PRT_DOCUMENTS on a.DOC_IDX equals b.DOC_IDX
                        where a.DUMP_ASSESSMENTS_IDX == dUMPASSESSMENTS_IDX
                        && b.DOC_FILE_TYPE.Contains("image")
                        select b).ToList();
            }
            catch (Exception ex)
            {
                log.LogEFException(ex);
                return null;
            }
        }



        //****************T_OD_DUMP_ASSESSMENT_CLEANUP ***********************************
        public Guid? InsertUpdateT_OD_DUMP_ASSESSMENT_CLEANUP(Guid? dUMP_ASSESSMENT_CLEANUP_IDX, Guid? dUMP_ASSESSMENTS_IDX, string rEF_WASTE_TYPE_CAT, string rEF_ASSET_NAME, decimal? cLEANUP_COST)
        {

            try
            {
                Boolean insInd = false;
                
                //first grab from PK
                T_OD_DUMP_ASSESSMENT_CLEANUP e = (from c in ctx.T_OD_DUMP_ASSESSMENT_CLEANUP
                                                  where c.DUMP_ASSESSMENT_CLEANUP_IDX == dUMP_ASSESSMENT_CLEANUP_IDX
                                                  select c).FirstOrDefault();

                //else grab from composite key
                if (e == null)
                    e = (from c in ctx.T_OD_DUMP_ASSESSMENT_CLEANUP
                         where c.DUMP_ASSESSMENTS_IDX == dUMP_ASSESSMENTS_IDX
                         && c.REF_WASTE_TYPE_CAT == rEF_WASTE_TYPE_CAT
                         && c.REF_ASSET_NAME == rEF_ASSET_NAME
                         select c).FirstOrDefault();

                //insert case
                if (e == null)
                {
                    insInd = true;
                    e = new T_OD_DUMP_ASSESSMENT_CLEANUP();
                    e.DUMP_ASSESSMENT_CLEANUP_IDX = Guid.NewGuid();
                }

                if (dUMP_ASSESSMENTS_IDX != null) e.DUMP_ASSESSMENTS_IDX = (Guid)dUMP_ASSESSMENTS_IDX;
                if (rEF_WASTE_TYPE_CAT != null) e.REF_WASTE_TYPE_CAT = rEF_WASTE_TYPE_CAT;
                if (rEF_ASSET_NAME != null) e.REF_ASSET_NAME = rEF_ASSET_NAME;
                if (cLEANUP_COST != null) e.CLEANUP_COST = cLEANUP_COST;

                if (insInd)
                    ctx.T_OD_DUMP_ASSESSMENT_CLEANUP.Add(e);

                ctx.SaveChanges();
                return e.DUMP_ASSESSMENT_CLEANUP_IDX;
            }
            catch (Exception ex)
            {
                log.LogEFException(ex);
                return null;
            }

        }

        public List<AssessmentCleanupDisplayType> getT_OD_DUMP_ASSESSMENT_CLEANUP_by_AssessIDX(Guid? dUMPASSESSMENTS_IDX)
        {
            try
            {
                return (from a in ctx.T_OD_DUMP_ASSESSMENT_CLEANUP
                           join b in ctx.T_OD_REF_WASTE_TYPE_CAT_CLEANUP on new { a.REF_WASTE_TYPE_CAT, a.REF_ASSET_NAME } equals new { b.REF_WASTE_TYPE_CAT, b.REF_ASSET_NAME } 
                           where a.DUMP_ASSESSMENTS_IDX == dUMPASSESSMENTS_IDX
                           orderby a.REF_WASTE_TYPE_CAT ascending, a.REF_ASSET_NAME descending
                           select new AssessmentCleanupDisplayType {
                               DUMP_ASSESSMENT_CLEANUP_IDX = a.DUMP_ASSESSMENT_CLEANUP_IDX,
                               DUMP_ASSESSMENTS_IDX = a.DUMP_ASSESSMENTS_IDX,
                               REF_WASTE_TYPE_CAT = a.REF_WASTE_TYPE_CAT,
                               REF_ASSET_NAME = a.REF_ASSET_NAME,
                               CLEANUP_COST = a.CLEANUP_COST,
                               REF_WASTE_TYPE_CAT_CLEANUP_IDX = b.REF_WASTE_TYPE_CAT_CLEANUP_IDX,
                               PROCESS_RATE_PER_HR = b.PROCESS_RATE_PER_HR,
                               PROCESS_RATE_UNIT = b.PROCESS_RATE_UNIT,
                               ASSET_HOURLY_RATE = b.ASSET_HOURLY_RATE,
                               ASSET_COUNT = b.ASSET_COUNT,
                               PER_UNIT_IND = b.PER_UNIT_IND,

                               sumCat = (from aa in ctx.T_OD_DUMP_ASSESSMENT_CONTENT
                                         join bb in ctx.T_OD_REF_WASTE_TYPE on aa.REF_WASTE_TYPE_IDX equals bb.REF_WASTE_TYPE_IDX
                                         where aa.DUMP_ASSESSMENTS_IDX == dUMPASSESSMENTS_IDX
                                         && bb.REF_WASTE_TYPE_CAT == b.REF_WASTE_TYPE_CAT
                                         select aa.WASTE_AMT).Sum()
//                                         group aa by new { bb.REF_WASTE_TYPE_CAT } into grp
  //                                       select grp.Sum(a => a.WASTE_AMT))
                               //this.getT_OD_DUMP_ASSESSMENT_CONTENT_DistinctCatSums(a.DUMP_ASSESSMENTS_IDX, b.REF_WASTE_TYPE_CAT).FirstOrDefault().Amount 
                           }).ToList();
            }
            catch (Exception ex)
            {
                log.LogEFException(ex);
                return null;
            }
        }

        public decimal? getT_OD_DUMP_ASSESSMENT_CLEANUP_Sum_by_AssessIDX(Guid? dUMPASSESSMENTS_IDX)
        {
            try
            {
                var xxx= (from a in ctx.T_OD_DUMP_ASSESSMENT_CLEANUP
                        where a.DUMP_ASSESSMENTS_IDX == dUMPASSESSMENTS_IDX
                        select a).ToList();

                return xxx.Select(c => c.CLEANUP_COST).Sum();
            }
            catch (Exception ex)
            {
                log.LogEFException(ex);
                return 0;
            }
        }



        //****************T_OD_DUMP_ASSESSMENT_RESTORE ***********************************
        public T_OD_DUMP_ASSESSMENT_RESTORE getT_OD_DUMP_ASSESSMENT_RESTORE_by_IDX(Guid? dUMP_ASSESSMENT_RESTORE_IDX)
        {
            try
            {
                return (from a in ctx.T_OD_DUMP_ASSESSMENT_RESTORE
                        where a.DUMP_ASSESSMENT_RESTORE_IDX == dUMP_ASSESSMENT_RESTORE_IDX
                        orderby a.RESTORE_ACTIVITY
                        select a).FirstOrDefault();
            }
            catch (Exception ex)
            {
                log.LogEFException(ex);
                return null;
            }
        }


        public List<T_OD_DUMP_ASSESSMENT_RESTORE> getT_OD_DUMP_ASSESSMENT_RESTORE_by_DumpAssessIDXandCat(Guid? dUMPASSESSMENTS_IDX, string rESTORE_CAT)
        {
            try
            {
                return (from a in ctx.T_OD_DUMP_ASSESSMENT_RESTORE
                        where a.DUMP_ASSESSMENTS_IDX == dUMPASSESSMENTS_IDX
                        && a.RESTORE_CAT == rESTORE_CAT
                        orderby a.RESTORE_ACTIVITY
                        select a).ToList();
            }
            catch (Exception ex)
            {
                log.LogEFException(ex);
                return null;
            }
        }

        public decimal? getT_OD_DUMP_ASSESSMENT_RESTORE_Sum_by_AssessIDX(Guid? dUMPASSESSMENTS_IDX, string rESTORE_CAT)
        {
            try
            {
                var xxx = (from a in ctx.T_OD_DUMP_ASSESSMENT_RESTORE
                           where a.DUMP_ASSESSMENTS_IDX == dUMPASSESSMENTS_IDX
                           && a.RESTORE_CAT == rESTORE_CAT
                           select a).ToList();

                return xxx.Select(c => c.RESTORE_COST).Sum();
            }
            catch (Exception ex)
            {
                log.LogEFException(ex);
                return 0;
            }
        }

        public Guid? InsertUpdateT_OD_DUMP_ASSESSMENT_RESTORE(Guid? dUMP_ASSESSMENT_RESTORE_IDX, Guid? dUMP_ASSESSMENTS_IDX, string rESTORE_CAT, string rESTORE_ACTIVITY, decimal? rESTORE_COST, string mODIFY_BY)
        {

            try
            {
                Boolean insInd = false;

                //first grab from PK
                T_OD_DUMP_ASSESSMENT_RESTORE e = (from c in ctx.T_OD_DUMP_ASSESSMENT_RESTORE
                                                  where c.DUMP_ASSESSMENT_RESTORE_IDX == dUMP_ASSESSMENT_RESTORE_IDX
                                                  select c).FirstOrDefault();

                //else grab from composite key
                if (e == null)
                    e = (from c in ctx.T_OD_DUMP_ASSESSMENT_RESTORE
                         where c.DUMP_ASSESSMENTS_IDX == dUMP_ASSESSMENTS_IDX
                         && c.RESTORE_ACTIVITY == rESTORE_ACTIVITY
                         && c.RESTORE_CAT == rESTORE_CAT
                         select c).FirstOrDefault();

                //insert case
                if (e == null)
                {
                    insInd = true;
                    e = new T_OD_DUMP_ASSESSMENT_RESTORE();
                    e.DUMP_ASSESSMENT_RESTORE_IDX = Guid.NewGuid();
                }

                if (dUMP_ASSESSMENTS_IDX != null) e.DUMP_ASSESSMENTS_IDX = (Guid)dUMP_ASSESSMENTS_IDX;
                if (rESTORE_CAT != null) e.RESTORE_CAT = rESTORE_CAT;
                if (rESTORE_ACTIVITY != null) e.RESTORE_ACTIVITY = rESTORE_ACTIVITY;
                if (rESTORE_COST != null) e.RESTORE_COST = (decimal)rESTORE_COST;

                if (insInd)
                    ctx.T_OD_DUMP_ASSESSMENT_RESTORE.Add(e);

                ctx.SaveChanges();
                return e.DUMP_ASSESSMENT_RESTORE_IDX;
            }
            catch (Exception ex)
            {
                log.LogEFException(ex);
                return null;
            }

        }

        public int DeleteT_OD_DUMP_ASSESSMENT_RESTORE(Guid dUMP_ASSESSMENT_RESTORE_IDX)
        {
            try
            {
                T_OD_DUMP_ASSESSMENT_RESTORE xxx = getT_OD_DUMP_ASSESSMENT_RESTORE_by_IDX(dUMP_ASSESSMENT_RESTORE_IDX);
                ctx.Entry(xxx).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                ctx.SaveChanges();

                return 1;
            }
            catch (Exception ex)
            {
                log.LogEFException(ex);
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
                log.LogEFException(ex);
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
                log.LogEFException(ex);
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
                log.LogEFException(ex);
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
                log.LogEFException(ex);
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
                log.LogEFException(ex);
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
                log.LogEFException(ex);
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
                log.LogEFException(ex);
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
                log.LogEFException(ex);
                return null;
            }
        }



        //*************** CALC CLEANUP COSTS
        public int CalcCleanup(Guid DumpAssessmentIDX)
        {
            try
            {
                List<CatSums> DistinctCats = getT_OD_DUMP_ASSESSMENT_CONTENT_DistinctCatSums(DumpAssessmentIDX,null);

                foreach (CatSums Cat in DistinctCats)
                {
                    //get sum for cat
                    var CategoryResources = (from a in ctx.T_OD_REF_WASTE_TYPE_CAT_CLEANUP
                                              where a.REF_WASTE_TYPE_CAT == Cat.Category
                                              select a).ToList();

                    foreach (var refResource in CategoryResources) {
                        Decimal? cost = (refResource.PER_UNIT_IND != true ? (Cat.Amount / refResource.PROCESS_RATE_PER_HR * refResource.ASSET_HOURLY_RATE * refResource.ASSET_COUNT) : (Math.Ceiling(Cat.Amount.ConvertOrDefault<decimal>() / refResource.PROCESS_RATE_PER_HR.ConvertOrDefault<decimal>()) * refResource.ASSET_HOURLY_RATE * refResource.ASSET_COUNT));
                        InsertUpdateT_OD_DUMP_ASSESSMENT_CLEANUP(null, DumpAssessmentIDX, Cat.Category, refResource.REF_ASSET_NAME, cost);
                    }
                }

                //then add up sums for all cleanup
                decimal? totCost = getT_OD_DUMP_ASSESSMENT_CLEANUP_Sum_by_AssessIDX(DumpAssessmentIDX);

                InsertUpdateT_OD_DumpAssessment(DumpAssessmentIDX, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null,
                    null, null, totCost, null, null, null, null, null);

                return 1;
            }
            catch (Exception ex)
            {
                log.LogEFException(ex);
                return 0;
            }


        }

    }
}
