using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TribalSvcPortal.Data.Models;

namespace TribalSvcPortal.AppLogic.DataAccessLayer
{
    public class OpenDumpListDisplay
    {
        public T_OD_SITES _Sites { get; set; }
        public int UserPoints { get; set; }
        public string OrgName { get; set; }
    }
    public class OpenDumpSiteListDisplay
    {
        public Guid Site_Idx { get; set; }
        public string SiteName { get; set; }
        public string SiteAddress { get; set; }
        public string ReportedBy { get; set; }
        public DateTime? ReportedOn { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
    }
    public class RefThreatFactor
    {
        public Guid THREAT_FACTOR_IDX { get; set; }
        public string THREAT_FACTOR_TYPE { get; set; }
        public string THREAT_FACTOR_NAME { get; set; }
        public int? THREAT_FACTOR_SCORE { get; set; }

    }

    public interface IDbOpenDump
    {
        List<UserOrgDisplayType> GetT_OD_SITES_bySearch(string orgID, string searchStr);
        IEnumerable<SelectListItem> get_ddl_od_organizations(string id);
        List<OpenDumpSiteListDisplay> get_OpenDump_Sites_By_Organization_SiteName(string selStr, string selOrg);
        IEnumerable<SelectListItem> get_ddl_refdata_by_category(string cat_name);

        T_OD_SITES GetT_OD_SITES_BySITEIDX(Guid Siteidx);
        Guid? InsertUpdateT_OD_SITES(Guid sITE_IDX, string rEPORTED_BY, DateTime? rEPORTED_ON, Guid? cOMMUNITY_IDX, Guid? sITE_SETTING_IDX, Guid? pF_AQUIFER_VERT_DIST,
        Guid? pF_SURF_WATER_HORIZ_DIST, Guid? pF_HOMES_DIST);
        IEnumerable<SelectListItem> get_ddl_refthreatfactor_by_factortype(string factor_type);
        List<OpenDumpSiteListDisplay> get_AllOpenDump_Sites(string id);
        List<T_OD_DUMP_ASSESSMENTS> GetT_OD_DumpAssessmentList_BySITEIDX(Guid Siteidx);
        IEnumerable<SelectListItem> get_ddl_od_dumpassessment_by_BySITEIDX(Guid? Siteidx);
        T_OD_DUMP_ASSESSMENTS GetT_OD_DumpAssessment_ByDumpAssessmentIDX(Guid DumpAssessmentIDX);
        int DeleteT_OD_DumpAssessment(Guid DumpAssessmentIDX);
        Guid? InsertUpdateT_OD_DumpAssessment(Guid dUMPASSESSMENTS_IDX, Guid sITE_IDX, DateTime aSSESSMENT_DT, string aSSESSED_BY, Guid? ASSESSMENT_TYPE_IDX, bool ACTIVE_SITE_IND, string SITE_DESCRIPTION, string ASSESSMENT_NOTES);
        Guid? InsertUpdateT_OD_DUMP_ASSESSMENT_DOCUMENTS(Guid? dOC_IDX, Guid dUMPASSESSMENTS_IDX);
        List<T_OD_REF_WASTE_TYPE> get_checkbox_refwastetype_by_wastetypecat(string waste_type_cat, Guid? AssessmentIdx);
        List<RefThreatFactor> get_ddl_refthreatfactor();
    }

    public class DbOpenDump : IDbOpenDump
    {
        private readonly ApplicationDbContext ctx;
        public DbOpenDump(ApplicationDbContext _context)
        {
            ctx = _context;
        }


        public List<UserOrgDisplayType> GetT_OD_SITES_bySearch(string orgID, string searchStr)
        {
            try
            {
                //var xxx = (from a in ctx.T_PRT_SITES
                //           join b in ctx.T_OD_SITES on a.SiteIdx equals b.SiteIdx
                //           where a.OrgId == orgID
                //           && (a.SiteName.ToUpper().Contains(searchStr.ToUpper())
                //           || a.SiteAddress.ToUpper().Contains(searchStr.ToUpper()))
                //           orderby a.SiteName
                //           select new OpenDumpListDisplay
                //           {
                //               //ORG_USER_IDX = a.OrgUserIdx,
                //               //ORG_ID = b.OrgId,
                //               //ORG_ADMIN_IND = a.OrgAdminInd,
                //               //STATUS_IND = a.StatusInd,
                //               //ORG_NAME = b.OrgName
                //           }).ToList();

                return null;// xxx;
            }
            catch (Exception ex)
            {
                log.LogEFException(ex);
                return null;
            }
        }

        public IEnumerable<SelectListItem> get_ddl_refdata_by_category(string cat_name)
        {
            try
            {
                var xxx = (from a in ctx.T_OD_REF_DATA
                           where a.REF_DATA_CAT_NAME == cat_name
                           orderby a.REF_DATA_VAL
                           select new SelectListItem
                           {
                               Value = a.REF_DATA_IDX.ToString(),
                               Text = a.REF_DATA_VAL
                           }).ToList();

                return xxx;
            }
            catch (Exception ex)
            {
                log.LogEFException(ex);
                return null;
            }
        }
        public IEnumerable<SelectListItem> get_ddl_refthreatfactor_by_factortype(string factor_type)
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

                return xxx;
            }
            catch (Exception ex)
            {
                log.LogEFException(ex);
                return null;
            }
        }
        public List<T_OD_REF_WASTE_TYPE> get_checkbox_refwastetype_by_wastetypecat(string waste_type_cat, Guid? AssessmentIdx)
        {
            try
            {
                List<T_OD_REF_WASTE_TYPE> xxx = new List<T_OD_REF_WASTE_TYPE>();
                if (AssessmentIdx == null)
                {
                   xxx = (from a in ctx.T_OD_REF_WASTE_TYPE
                           where a.REF_WASTE_TYPE_CAT == waste_type_cat
                           orderby a.REF_WASTE_TYPE_NAME
                           select a).ToList();
                }
                else
                {
                    xxx = (from a in ctx.T_OD_REF_WASTE_TYPE
                           where a.REF_WASTE_TYPE_CAT == waste_type_cat
                           orderby a.REF_WASTE_TYPE_NAME
                           select a).ToList();

                    List<T_OD_DUMP_ASSESSMENT_CONTENT> todac = (from a in ctx.T_OD_DUMP_ASSESSMENT_CONTENT
                                                                where a.DUMP_ASSESSMENTS_IDX == AssessmentIdx                                                               
                                                                select a).ToList();
                    foreach (T_OD_REF_WASTE_TYPE oWasteType in xxx)
                    {
                        foreach (T_OD_DUMP_ASSESSMENT_CONTENT oOneNew in todac)
                        {
                            if (oWasteType.REF_WASTE_TYPE_IDX == oOneNew.REF_WASTE_TYPE_IDX)
                            {
                                oWasteType.IS_CHECKED = true;
                                break;
                            }                           
                        }
                    }
                }

                return xxx;
            }
            catch (Exception ex)
            {
                log.LogEFException(ex);
                return null;
            }
        }
        public T_OD_SITES GetT_OD_SITES_BySITEIDX(Guid Siteidx)
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

        public IEnumerable<SelectListItem> get_ddl_od_organizations(string id)
        {
            try
            {
                var xxx = (from a in ctx.T_PRT_ORG_USERS
                           join b in ctx.T_PRT_ORGANIZATIONS on a.OrgId equals b.OrgId
                           join c in ctx.T_PRT_ORG_USER_CLIENT on a.OrgUserIdx equals c.OrgUserIdx
                           where a.Id == id && c.ClientId == "open_dump"
                           orderby b.OrgName
                           select new SelectListItem
                           {
                               Value = a.OrgId.ToString(),
                               Text = b.OrgName
                           }).ToList();

                return xxx;
            }
            catch (Exception ex)
            {
                log.LogEFException(ex);
                return null;
            }

        }

        public List<OpenDumpSiteListDisplay> get_OpenDump_Sites_By_Organization_SiteName(string selStr, string selOrg)
        {
            try
            {
                List<OpenDumpSiteListDisplay> xxx = new List<OpenDumpSiteListDisplay>();
                if (selStr != null && selOrg != null)
                {
                    xxx = (from a in ctx.T_PRT_SITES
                           join b in ctx.T_OD_SITES on a.SiteIdx equals b.SITE_IDX
                           where (selOrg != null && a.OrgId == selOrg)
                           && (selStr != null && (a.SiteName.ToUpper().Contains(selStr.ToUpper())
                           || (a.SiteAddress != null && a.SiteAddress.ToUpper().Contains(selStr.ToUpper()))))

                           orderby a.SiteName
                           select new OpenDumpSiteListDisplay
                           {
                               Site_Idx = a.SiteIdx,
                               SiteName = a.SiteName,
                               SiteAddress = a.SiteAddress,
                               ReportedBy = b.REPORTED_BY,
                               ReportedOn = b.REPORTED_ON,
                               Latitude = a.Latitude,
                               Longitude = a.Longitude

                           }).ToList();
                }
                else if (selStr == null && selOrg != null)
                {
                    xxx = (from a in ctx.T_PRT_SITES
                           join b in ctx.T_OD_SITES on a.SiteIdx equals b.SITE_IDX
                           where (selOrg != null && a.OrgId == selOrg)
                           orderby a.SiteName
                           select new OpenDumpSiteListDisplay
                           {
                               Site_Idx = a.SiteIdx,
                               SiteName = a.SiteName,
                               SiteAddress = a.SiteAddress,
                               ReportedBy = b.REPORTED_BY,
                               ReportedOn = b.REPORTED_ON,
                               Latitude = a.Latitude,
                               Longitude = a.Longitude

                           }).ToList();
                }
                else if (selStr != null && selOrg == null)
                {
                    xxx = (from a in ctx.T_PRT_SITES
                           join b in ctx.T_OD_SITES on a.SiteIdx equals b.SITE_IDX
                           where (selStr != null && (a.SiteName.ToUpper().Contains(selStr.ToUpper())
                           || (a.SiteAddress != null && a.SiteAddress.ToUpper().Contains(selStr.ToUpper()))))

                           orderby a.SiteName
                           select new OpenDumpSiteListDisplay
                           {
                               Site_Idx = a.SiteIdx,
                               SiteName = a.SiteName,
                               SiteAddress = a.SiteAddress,
                               ReportedBy = b.REPORTED_BY,
                               ReportedOn = b.REPORTED_ON,
                               Latitude = a.Latitude,
                               Longitude = a.Longitude

                           }).ToList();
                }

                return xxx;
            }
            catch (Exception ex)
            {
                log.LogEFException(ex);
                return null;
            }

        }

        public List<OpenDumpSiteListDisplay> get_AllOpenDump_Sites(string id)
        {
            try
            {
                List<OpenDumpSiteListDisplay> xxx = new List<OpenDumpSiteListDisplay>();
                OpenDumpSiteListDisplay OpenDumpSite = new OpenDumpSiteListDisplay();
                var odsld = (from a in ctx.T_PRT_ORG_USERS
                           join b in ctx.T_PRT_ORGANIZATIONS on a.OrgId equals b.OrgId
                           join c in ctx.T_PRT_ORG_USER_CLIENT on a.OrgUserIdx equals c.OrgUserIdx
                           where a.Id == id && c.ClientId == "open_dump"
                           orderby b.OrgName
                           select new SelectListItem
                           {
                               Value = a.OrgId.ToString(),
                               Text = b.OrgName
                           }).ToList();

                for (int i=0;i< odsld.Count();i++)
                {
                    OpenDumpSite = (from a in ctx.T_PRT_SITES
                           join b in ctx.T_OD_SITES on a.SiteIdx equals b.SITE_IDX
                           where (odsld[i].Value != null && a.OrgId == odsld[i].Value)
                           orderby a.SiteName
                           select new OpenDumpSiteListDisplay
                           {
                               Site_Idx = a.SiteIdx,
                               SiteName = a.SiteName,
                               SiteAddress = a.SiteAddress,
                               ReportedBy = b.REPORTED_BY,
                               ReportedOn = b.REPORTED_ON,
                               Latitude = a.Latitude,
                               Longitude = a.Longitude

                           }).FirstOrDefault();
                    if (OpenDumpSite != null)
                    {
                        xxx.Add(OpenDumpSite);
                    }
                }                          

                return xxx;
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

        public List<T_OD_DUMP_ASSESSMENTS> GetT_OD_DumpAssessmentList_BySITEIDX(Guid Siteidx)
        {
            try
            {
                return (from a in ctx.T_OD_DUMP_ASSESSMENTS
                        where a.SITE_IDX == Siteidx
                        select a).ToList();
            }
            catch (Exception ex)
            {
                log.LogEFException(ex);
                return null;
            }
        }

        public IEnumerable<SelectListItem> get_ddl_od_dumpassessment_by_BySITEIDX(Guid? Siteidx)
        {
            try
            {
                //if (Siteidx != null)
                //{
                    var xxx = (from a in ctx.T_OD_DUMP_ASSESSMENTS
                               where a.SITE_IDX == Siteidx
                               orderby a.ASSESSMENT_DT
                               select new SelectListItem
                               {
                                   Value = a.DUMP_ASSESSMENTS_IDX.ToString(),
                                   Text = a.ASSESSMENT_DT.ToString("dd-MM-yyyy")
                               }).ToList();
                xxx.Insert(0, new SelectListItem() { Value = "98567684-a5d5-4742-ac6d-1dd5080f76a7", Text = "View All" });              
                    return xxx;               
               
            }
            catch (Exception ex)
            {
                log.LogEFException(ex);
                return null;
            }
        }
      
        public T_OD_DUMP_ASSESSMENTS GetT_OD_DumpAssessment_ByDumpAssessmentIDX(Guid DumpAssessmentIDX)
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
        public int DeleteT_OD_DumpAssessment(Guid DumpAssessmentIDX)
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

        public Guid? InsertUpdateT_OD_DumpAssessment(Guid dUMPASSESSMENTS_IDX, Guid sITE_IDX, DateTime aSSESSMENT_DT, string aSSESSED_BY, Guid? ASSESSMENT_TYPE_IDX, bool ACTIVE_SITE_IND, string SITE_DESCRIPTION, string ASSESSMENT_NOTES)
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
                    e.DUMP_ASSESSMENTS_IDX = dUMPASSESSMENTS_IDX;
                }

                if (sITE_IDX != null) e.SITE_IDX = sITE_IDX;
                if (aSSESSMENT_DT != null) e.ASSESSMENT_DT = aSSESSMENT_DT;
                if (aSSESSED_BY != null) e.ASSESSED_BY = aSSESSED_BY;
                if (ASSESSMENT_TYPE_IDX != null) e.ASSESSMENT_TYPE_IDX = ASSESSMENT_TYPE_IDX;
                 e.ACTIVE_SITE_IND = ACTIVE_SITE_IND;
                if (SITE_DESCRIPTION != null) e.SITE_DESCRIPTION = SITE_DESCRIPTION;
                if (ASSESSMENT_NOTES != null) e.ASSESSMENT_NOTES = ASSESSMENT_NOTES;

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
        public List<RefThreatFactor> get_ddl_refthreatfactor()
        {
            try
            {
                var xxx = (from a in ctx.T_OD_REF_THREAT_FACTORS                          
                           orderby a.THREAT_FACTOR_SCORE
                           select new RefThreatFactor
                           {
                               THREAT_FACTOR_IDX = a.THREAT_FACTOR_IDX,
                               THREAT_FACTOR_NAME = a.THREAT_FACTOR_NAME,
                               THREAT_FACTOR_SCORE =a.THREAT_FACTOR_SCORE,
                               THREAT_FACTOR_TYPE = a.THREAT_FACTOR_TYPE

                           }).ToList();

                return xxx;
            }
            catch (Exception ex)
            {
                log.LogEFException(ex);
                return null;
            }
        }


    }
}
