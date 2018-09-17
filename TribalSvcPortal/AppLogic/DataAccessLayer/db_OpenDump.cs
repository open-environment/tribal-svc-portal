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
    }

    public interface IDbOpenDump
    {
        List<UserOrgDisplayType> GetT_OD_SITES_bySearch(string orgID, string searchStr);
        IEnumerable<SelectListItem> get_ddl_organizations(string id);
        List<OpenDumpSiteListDisplay> get_OpenDump_Sites_By_Organization_SiteName(string selStr, string selOrg);
        IEnumerable<SelectListItem> get_ddl_refdata_by_category(string cat_name);
        T_PRT_SITES GetT_PRT_SITES_BySITEIDX(Guid Siteidx);
        T_OD_SITES GetT_OD_SITES_BySITEIDX(Guid Siteidx);
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
                                   ReportedOn = b.ReportedOn

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
                               ReportedOn = b.ReportedOn

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
                               ReportedOn = b.ReportedOn

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
    }
}
