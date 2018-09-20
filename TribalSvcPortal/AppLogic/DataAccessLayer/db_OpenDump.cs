﻿using Microsoft.AspNetCore.Mvc.Rendering;
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
        IEnumerable<SelectListItem> get_ddl_organizations(string id);
        List<OpenDumpSiteListDisplay> get_OpenDump_Sites_By_Organization_SiteName(string selStr, string selOrg);
        IEnumerable<SelectListItem> get_ddl_refdata_by_category(string cat_name);
        T_PRT_SITES GetT_PRT_SITES_BySITEIDX(Guid Siteidx);
        T_OD_SITES GetT_OD_SITES_BySITEIDX(Guid Siteidx);
        Guid? InsertUpdateT_PRT_SITES(Guid? sITE_IDX, string oRG_ID, string sITE_NAME, string ePA_ID, decimal? lATITUDE, decimal? lONGITUDE, string sITE_ADDRESS,
           string UserIDX);
        Guid? InsertUpdateT_OD_SITES(Guid sITE_IDX, Guid cOMMUNITY_IDX, Guid sITE_SETTING_IDX, string rEPORTED_BY, DateTime? rEPORTED_ON, string rESPONSE_ACTION);
        int DeleteT_PRT_SITES(Guid sITE_IDX);
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
                           where a.RefDataCatName == cat_name
                           orderby a.RefDataName
                           select new SelectListItem
                           {
                               Value = a.RefDataIdx.ToString(),
                               Text = a.RefDataName
                           }).ToList();

                return xxx;
            }
            catch (Exception ex)
            {
                log.LogEFException(ex);
                return null;
            }
        }
        public T_PRT_SITES GetT_PRT_SITES_BySITEIDX(Guid Siteidx)
        {
            try
            {
                return (from a in ctx.T_PRT_SITES
                        where a.SiteIdx == Siteidx
                        select a).FirstOrDefault();
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
                        where a.SiteIdx == Siteidx
                        select a).FirstOrDefault();
            }
            catch (Exception ex)
            {
                log.LogEFException(ex);
                return null;
            }
        }
        public IEnumerable<SelectListItem> get_ddl_organizations(string id)
        {
            try
            {
                var xxx = (from a in ctx.T_PRT_ORG_USERS
                           join b in ctx.T_PRT_ORGANIZATIONS on a.OrgId equals b.OrgId
                           where a.Id == id
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
                           join b in ctx.T_OD_SITES on a.SiteIdx equals b.SiteIdx
                           where (selOrg != null && a.OrgId == selOrg)
                           && (selStr != null && (a.SiteName.ToUpper().Contains(selStr.ToUpper())
                           || (a.SiteAddress != null && a.SiteAddress.ToUpper().Contains(selStr.ToUpper()))))

                           orderby a.SiteName
                           select new OpenDumpSiteListDisplay
                           {
                               Site_Idx = a.SiteIdx,
                               SiteName = a.SiteName,
                               SiteAddress = a.SiteAddress,
                               ReportedBy = b.ReportedBy,
                               ReportedOn = b.ReportedOn,
                               Latitude = a.Latitude,
                               Longitude = a.Longitude

                           }).ToList();
                }
                else if (selStr == null && selOrg != null)
                {
                    xxx = (from a in ctx.T_PRT_SITES
                           join b in ctx.T_OD_SITES on a.SiteIdx equals b.SiteIdx
                           where (selOrg != null && a.OrgId == selOrg)
                           orderby a.SiteName
                           select new OpenDumpSiteListDisplay
                           {
                               Site_Idx = a.SiteIdx,
                               SiteName = a.SiteName,
                               SiteAddress = a.SiteAddress,
                               ReportedBy = b.ReportedBy,
                               ReportedOn = b.ReportedOn,
                               Latitude = a.Latitude,
                               Longitude = a.Longitude

                           }).ToList();
                }
                else if (selStr != null && selOrg == null)
                {
                    xxx = (from a in ctx.T_PRT_SITES
                           join b in ctx.T_OD_SITES on a.SiteIdx equals b.SiteIdx
                           where (selStr != null && (a.SiteName.ToUpper().Contains(selStr.ToUpper())
                           || (a.SiteAddress != null && a.SiteAddress.ToUpper().Contains(selStr.ToUpper()))))

                           orderby a.SiteName
                           select new OpenDumpSiteListDisplay
                           {
                               Site_Idx = a.SiteIdx,
                               SiteName = a.SiteName,
                               SiteAddress = a.SiteAddress,
                               ReportedBy = b.ReportedBy,
                               ReportedOn = b.ReportedOn,
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

        public Guid? InsertUpdateT_PRT_SITES(Guid? sITE_IDX, string oRG_ID, string sITE_NAME, string ePA_ID, decimal? lATITUDE, decimal? lONGITUDE, string sITE_ADDRESS,
            string UserIDX)
        {
            try
            {
                Boolean insInd = false;

                T_PRT_SITES e = (from c in ctx.T_PRT_SITES
                                 where c.SiteIdx == sITE_IDX
                                 select c).FirstOrDefault();

                //insert case
                if (e == null)
                {
                    insInd = true;
                    e = new T_PRT_SITES();
                    e.SiteIdx = Guid.NewGuid();
                    e.CreateDt = System.DateTime.UtcNow;
                    e.CreateUserId = UserIDX;
                }
                else
                {
                    e.ModifyDt = System.DateTime.UtcNow;
                    e.ModifyUserId = UserIDX;
                }

                if (oRG_ID != null) e.OrgId = oRG_ID;
                if (sITE_NAME != null) e.SiteName = sITE_NAME;
                if (ePA_ID != null) e.EpaId = ePA_ID;
                if (lATITUDE != null) e.Latitude = lATITUDE;
                if (lONGITUDE != null) e.Longitude = lONGITUDE;
                if (sITE_ADDRESS != null) e.SiteAddress = sITE_ADDRESS;


                if (insInd)
                    ctx.T_PRT_SITES.Add(e);

                ctx.SaveChanges();
                return e.SiteIdx;
            }
            catch (Exception ex)
            {
                log.LogEFException(ex);
                return null;
            }
        }
        public Guid? InsertUpdateT_OD_SITES(Guid sITE_IDX, Guid cOMMUNITY_IDX, Guid sITE_SETTING_IDX, string rEPORTED_BY, DateTime? rEPORTED_ON,string rESPONSE_ACTION)
        {
            try
            {
                Boolean insInd = false;

                T_OD_SITES e = (from c in ctx.T_OD_SITES
                                where c.SiteIdx == sITE_IDX
                                 select c).FirstOrDefault();

                //insert case
                if (e == null)
                {
                    insInd = true;
                    e = new T_OD_SITES();
                    e.SiteIdx = sITE_IDX;                  
                }              

                if (cOMMUNITY_IDX != null) e.CommunityIdx = cOMMUNITY_IDX;
                if (sITE_SETTING_IDX != null) e.SiteSettingIdx = sITE_SETTING_IDX;
                if (rEPORTED_BY != null) e.ReportedBy = rEPORTED_BY;
                if (rEPORTED_ON != null) e.ReportedOn = rEPORTED_ON;
                if (rESPONSE_ACTION != null) e.ResponseAction = rESPONSE_ACTION;
               
                if (insInd)
                    ctx.T_OD_SITES.Add(e);
                ctx.SaveChanges();
                return e.SiteIdx;
            }
            catch (Exception ex)
            {
                log.LogEFException(ex);
                return null;
            }

        }
        public int DeleteT_PRT_SITES(Guid sITE_IDX)
        {
            try
            {
                T_OD_SITES tos = new T_OD_SITES { SiteIdx = sITE_IDX };
                ctx.Entry(tos).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                ctx.SaveChanges();

                T_PRT_SITES tps = new T_PRT_SITES { SiteIdx = sITE_IDX };
                ctx.Entry(tps).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                ctx.SaveChanges();

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
