//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using TribalSvcPortal.Data.Models;

//namespace TribalSvcPortal.AppLogic.DataAccessLayer
//{
//    public class db_PortalStatic
//    {
//        private ApplicationDbContext ctx;
//        public db_PortalStatic(ApplicationDbContext _context)
//        {
//            ctx = _context;
//        }

//        public static async List<T_PRT_CLIENTS> GetT_PRT_CLIENTS()
//        {

//                try
//                {
//                    return await  ctx.T_PRT_CLIENTS.ToList();
//                }
//                catch (Exception ex)
//                {
//                    //LogEFException(ex);
//                    return null;
//                }

//        }


//        public static List<OrgUserClientDisplayType> GetT_PRT_ORG_USERS_CLIENT_ByUserID(string _UserIDX)
//        {
//            using (ApplicationDbContextTemp ctx = new ApplicationDbContextTemp())
//            {
//                try
//                {
//                    var xxx = (from a in ctx.T_PRT_ORG_USER_CLIENT
//                               join b in ctx.T_PRT_ORG_USERS on a.OrgUserIdx equals b.OrgUserIdx
//                               join c in ctx.T_PRT_ORG_CLIENT_ALIAS on new { a.ClientId, b.OrgId } equals new { c.ClientId, c.OrgId }
//                               where b.Id == _UserIDX
//                               select new OrgUserClientDisplayType
//                               {
//                                   ORG_USER_CLIENT_IDX = a.OrgUserClientIdx,
//                                   ORG_USER_IDX = a.OrgUserIdx,
//                                   CLIENT_ID = a.ClientId,
//                                   ADMIN_IND = a.AdminInd,
//                                   STATUS_IND = a.StatusInd,
//                                   ORG_ID = b.OrgId,
//                                   UserID = b.Id,
//                                   UserName = b.Id,
//                                   ORG_CLIENT_ALIAS = c.OrgClientAlias
//                               }).ToList();

//                    return xxx;
//                }
//                catch (Exception ex)
//                {
//                    //LogEFException(ex);
//                    return null;
//                }
//            }
//        }

//    }
//}
