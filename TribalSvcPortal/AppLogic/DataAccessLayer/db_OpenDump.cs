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


    }
}
