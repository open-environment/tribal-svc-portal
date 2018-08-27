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

    public interface IDbOpenDump
    {

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
                var xxx = (from a in ctx.T_PRT_SITES
                           join b in ctx.T_OD_SITES on a.SiteIdx equals b.SiteIdx
                           where a.OrgId == orgID
                           && (a.SiteName.ToUpper().Contains(searchStr.ToUpper())
                           || a.SiteAddress.ToUpper().Contains(searchStr.ToUpper()))
                           orderby a.SiteName
                           select new OpenDumpListDisplay
                           {
                               //ORG_USER_IDX = a.OrgUserIdx,
                               //ORG_ID = b.OrgId,
                               //ORG_ADMIN_IND = a.OrgAdminInd,
                               //STATUS_IND = a.StatusInd,
                               //ORG_NAME = b.OrgName
                           }).ToList();

                return null;// xxx;
            }
            catch (Exception ex)
            {
                //LogEFException(ex);
                return null;
            }
        }




    }
}
