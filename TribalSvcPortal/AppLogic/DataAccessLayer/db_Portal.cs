using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TribalSvcPortal.Data.Models;
using TribalSvcPortal.AppLogic.BusinessLogicLayer;

namespace TribalSvcPortal.AppLogic.DataAccessLayer
{
    public class UserTenantsDisplayType
    {
        public int TENANT_USER_IDX { get; set; }
        public string TENANT_ID { get; set; }
        public bool? TENANT_ADMIN_IND { get; set; }
        public string STATUS_IND { get; set; }
        public string TENANT_NAME { get; set; }
    }

    public class TenantUserClientDisplayType
    {
        public int TENANT_USER_CLIENT_IDX { get; set; }
        public int TENANT_USER_IDX { get; set; }
        public string CLIENT_ID { get; set; }
        public bool ADMIN_IND { get; set; }
        public string STATUS_IND { get; set; }
        public string TENANT_ID { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string TENANT_CLIENT_ALIAS { get; set; }
    }


    public interface IDbPortal
    {
        List<TPrtClients> GetT_PRT_CLIENTS();
        TPrtClients GetT_PRT_CLIENTS_ByClientID(string id);
        string InsertUpdateT_PRT_CLIENTS(string cLIENT_ID, string cLIENT_NAME, string cLIENT_GRANT_TYPE, string cLIENT_REDIRECT_URI, string cLIENT_POST_LOGOUT_URI, string cLIENT_URL);
        List<TPrtTenants> GetT_PRT_TENANTS();
        TPrtTenantUsers GetT_PRT_TENANT_USERS_ByTenantUserID(int id);
        List<UserTenantsDisplayType> GetT_PRT_TENANT_USERS_ByUserID(string id);
        int InsertUpdateT_PRT_TENANT_USERS(int? tENANT_USER_IDX, string tENANT_ID, string _Id, bool? tENANT_ADMIN_IND, string sTATUS_IND, string cREATE_USER);
        int DeleteT_PRT_TENANT_USERS(int id);
        List<TenantUserClientDisplayType> GetT_PRT_TENANT_USERS_CLIENT_ByTenantUserID(int _TenantUserIDX);
        List<TenantUserClientDisplayType> GetT_PRT_TENANT_USERS_CLIENT_ByUserID(string _UserIDX);
        IEnumerable<TPrtClients> GetT_PRT_TENANT_USERS_CLIENT_DistinctClientByUserID(string UserID);
        int InsertUpdateT_PRT_TENANT_USERS_CLIENT(int? tENANT_USER_CLIENT_IDX, int? tENANT_USER_IDX, string cLIENT_ID, bool? aDMIN_IND, string sTATUS_IND, string cREATE_USER);
        int DeleteT_PRT_TENANT_USER_CLIENT(int id);
        int InsertT_OE_SYS_LOG(string logType, string logMsg);

        TOeSysLog GetT_OE_SYS_LOG();
    }

    public class DbPortal : IDbPortal
    {
        private readonly ApplicationDbContext ctx;
        public DbPortal(ApplicationDbContext _context)
        {
            ctx = _context;
        }


        //**************************** T_PRT_CLIENTS ***********************************************
        public List<TPrtClients> GetT_PRT_CLIENTS()
        {
            try
            {
                return ctx.TPrtClients.ToList();
            }
            catch (Exception ex)
            {
                Utils.LogEFException(ex);
                return null;
            }
        }

        public TPrtClients GetT_PRT_CLIENTS_ByClientID(string id)
        {
            try
            {
                return (from a in ctx.TPrtClients
                        where a.ClientId == id
                        select a).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Utils.LogEFException(ex);
                return null;
            }
        }

        public string InsertUpdateT_PRT_CLIENTS(string cLIENT_ID, string cLIENT_NAME, string cLIENT_GRANT_TYPE, string cLIENT_REDIRECT_URI, string cLIENT_POST_LOGOUT_URI, string cLIENT_URL)
        {
            try
            {
                Boolean insInd = false;

                TPrtClients e = null;

                e = (from c in ctx.TPrtClients
                        where c.ClientId == cLIENT_ID
                        select c).FirstOrDefault();

                if (e == null)
                {
                    insInd = true;
                    e = new TPrtClients();
                }

                if (cLIENT_ID != null) e.ClientId = cLIENT_ID;
                if (cLIENT_NAME != null) e.ClientName = cLIENT_NAME;
                if (cLIENT_GRANT_TYPE != null) e.ClientGrantType = cLIENT_GRANT_TYPE;
                if (cLIENT_REDIRECT_URI != null) e.ClientRedirectUri = cLIENT_REDIRECT_URI;
                if (cLIENT_POST_LOGOUT_URI != null) e.ClientPostLogoutUri = cLIENT_POST_LOGOUT_URI;
                if (cLIENT_URL != null) e.ClientUrl = cLIENT_URL;

                if (insInd)
                    ctx.TPrtClients.Add(e);

                ctx.SaveChanges();
                return e.ClientId;
            }
            catch (Exception ex)
            {
                Utils.LogEFException(ex);
                return null;
            }
        }



        //******************************T_PRT_TENANTS***********************************************
        public List<TPrtTenants> GetT_PRT_TENANTS()
        {
            try
            {
                return ctx.TPrtTenants.ToList();
            }
            catch (Exception ex)
            {
                Utils.LogEFException(ex);
                return null;
            }
        }



        //******************************T_PRT_TENANT_USERS***********************************************
        public TPrtTenantUsers GetT_PRT_TENANT_USERS_ByTenantUserID(int id)
        {
            try
            {
                return (from a in ctx.TPrtTenantUsers
                           where a.TenantUserIdx == id
                           select a).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Utils.LogEFException(ex);
                return null;
            }
        }

        public List<UserTenantsDisplayType> GetT_PRT_TENANT_USERS_ByUserID(string UserId)
        {
            try
            {
                var xxx = (from a in ctx.TPrtTenantUsers
                            join b in ctx.TPrtTenants on a.TenantId equals b.TenantId
                            where a.Id == UserId
                            orderby b.TenantName
                            select new UserTenantsDisplayType
                            {
                                TENANT_USER_IDX = a.TenantUserIdx,
                                TENANT_ID = b.TenantId,
                                TENANT_ADMIN_IND = a.TenantAdminInd,
                                STATUS_IND = a.StatusInd,
                                TENANT_NAME = b.TenantName
                            }).ToList();

                return xxx;
            }
            catch (Exception ex)
            {
                Utils.LogEFException(ex);
                return null;
            }
        }

        public int InsertUpdateT_PRT_TENANT_USERS(int? tENANT_USER_IDX, string tENANT_ID, string _Id, bool? tENANT_ADMIN_IND, string sTATUS_IND, string cREATE_USER)
        {

            try
            {
                Boolean insInd = false;

                TPrtTenantUsers e = null;

                e = (from c in ctx.TPrtTenantUsers
                        where c.TenantUserIdx == tENANT_USER_IDX
                        select c).FirstOrDefault();

                //now try to grab from user and tenant id
                if (e == null)
                {
                    e = (from c in ctx.TPrtTenantUsers
                            where c.TenantId.ToUpper() == tENANT_ID.ToUpper()
                            && c.Id == _Id
                            select c).FirstOrDefault();
                }

                if (e == null)
                {
                    insInd = true;
                    e = new TPrtTenantUsers
                    {
                        CreateDt = System.DateTime.Now,
                        CreateUserId = cREATE_USER
                    };
                }
                else
                {
                    e.ModifyDt = System.DateTime.Now;
                    e.ModifyUserId = cREATE_USER;
                }

                if (tENANT_ID != null) e.TenantId = tENANT_ID;
                if (_Id != null) e.Id = _Id;
                if (tENANT_ADMIN_IND != null) e.TenantAdminInd = tENANT_ADMIN_IND ?? false;
                if (sTATUS_IND != null) e.StatusInd = sTATUS_IND;

                if (insInd)
                    ctx.TPrtTenantUsers.Add(e);

                ctx.SaveChanges();
                return e.TenantUserIdx;
            }
            catch (Exception ex)
            {
                Utils.LogEFException(ex);
                return 0;
            }

        }

        public int DeleteT_PRT_TENANT_USERS(int id)
        {
            try
            {
                TPrtTenantUsers rec = new TPrtTenantUsers { TenantUserIdx = id };
                ctx.Entry(rec).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                ctx.SaveChanges();

                return 1;
            }
            catch (Exception ex)
            {
                Utils.LogEFException(ex);
                return 0;
            }
        }



        //******************************T_PRT_TENANT_USER_CLIENT***********************************************
        public List<TenantUserClientDisplayType> GetT_PRT_TENANT_USERS_CLIENT_ByTenantUserID(int _TenantUserIDX)
        {
            try
            {
                var xxx = (from a in ctx.TPrtTenantUserClient
                            join b in ctx.TPrtTenantUsers on a.TenantUserIdx equals b.TenantUserIdx
                            where a.TenantUserIdx == _TenantUserIDX
                            orderby a.ClientId
                            select new TenantUserClientDisplayType
                            {
                                TENANT_USER_CLIENT_IDX = a.TenantUserClientIdx,
                                TENANT_USER_IDX = a.TenantUserIdx,
                                CLIENT_ID = a.ClientId,
                                ADMIN_IND = a.AdminInd,
                                STATUS_IND = a.StatusInd,
                                TENANT_ID = b.TenantId,
                                UserID = b.Id,
                                UserName = b.Id
                            }).ToList();

                return xxx;
            }
            catch (Exception ex)
            {
                Utils.LogEFException(ex);
                return null;
            }

        }

        public List<TenantUserClientDisplayType> GetT_PRT_TENANT_USERS_CLIENT_ByUserID(string _UserIDX)
        {
            try
            {
                var xxx = (from a in ctx.TPrtTenantUserClient
                            join b in ctx.TPrtTenantUsers on a.TenantUserIdx equals b.TenantUserIdx
                            join c in ctx.TPrtTenantClientAlias on new { a.ClientId, b.TenantId } equals new { c.ClientId, c.TenantId }
                            where b.Id == _UserIDX
                            select new TenantUserClientDisplayType
                            {
                                TENANT_USER_CLIENT_IDX = a.TenantUserClientIdx,
                                TENANT_USER_IDX = a.TenantUserIdx,
                                CLIENT_ID = a.ClientId,
                                ADMIN_IND = a.AdminInd,
                                STATUS_IND = a.StatusInd,
                                TENANT_ID = b.TenantId,
                                UserID = b.Id,
                                UserName = b.Id,
                                TENANT_CLIENT_ALIAS = c.TenantClientAlias
                            }).ToList();

                return xxx;
            }
            catch (Exception ex)
            {
                Utils.LogEFException(ex);
                return null;
            }
        }

        public IEnumerable<TPrtClients> GetT_PRT_TENANT_USERS_CLIENT_DistinctClientByUserID(string UserID)
        {
            try
            {
                var xxx = (from a in ctx.TPrtTenantUserClient
                            join b in ctx.TPrtTenantUsers on a.TenantUserIdx equals b.TenantUserIdx
                            join c in ctx.TPrtClients on a.ClientId equals c.ClientId
                            where b.Id == UserID
                            orderby a.ClientId
                            select c).ToList().Distinct();

                return xxx;
            }
            catch (Exception ex)
            {
                Utils.LogEFException(ex);
                return null;
            }
        }

        public int InsertUpdateT_PRT_TENANT_USERS_CLIENT(int? tENANT_USER_CLIENT_IDX, int? tENANT_USER_IDX, string cLIENT_ID, bool? aDMIN_IND, string sTATUS_IND, string cREATE_USER)
        {
            try
            {
                Boolean insInd = false;

                TPrtTenantUserClient e = null;

                e = (from c in ctx.TPrtTenantUserClient
                        where c.TenantUserClientIdx == tENANT_USER_CLIENT_IDX
                        select c).FirstOrDefault();

                //now try to grab from user and tenant id
                if (e == null)
                {
                    e = (from c in ctx.TPrtTenantUserClient
                            where c.ClientId.ToUpper() == cLIENT_ID.ToUpper()
                            && c.TenantUserIdx == tENANT_USER_IDX
                            select c).FirstOrDefault();
                }

                if (e == null)
                {
                    insInd = true;
                    e = new TPrtTenantUserClient
                    {
                        CreateDt = System.DateTime.Now,
                        CreateUserId = cREATE_USER
                    };
                }
                else
                {
                    e.ModifyDt = System.DateTime.Now;
                    e.ModifyUserId = cREATE_USER;
                }

                if (tENANT_USER_IDX != null) e.TenantUserIdx = tENANT_USER_IDX ?? 0;
                if (cLIENT_ID != null) e.ClientId = cLIENT_ID;
                if (aDMIN_IND != null) e.AdminInd = aDMIN_IND ?? false;
                if (sTATUS_IND != null) e.StatusInd = sTATUS_IND;

                if (insInd)
                    ctx.TPrtTenantUserClient.Add(e);

                ctx.SaveChanges();
                return e.TenantUserIdx;
            }
            catch (Exception ex)
            {
                Utils.LogEFException(ex);
                return 0;
            }

        }

        public int DeleteT_PRT_TENANT_USER_CLIENT(int id)
        {
            try
            {
                TPrtTenantUserClient rec = new TPrtTenantUserClient { TenantUserClientIdx = id };
                ctx.Entry(rec).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                ctx.SaveChanges();

                return 1;
            }
            catch (Exception ex)
            {
                Utils.LogEFException(ex);
                return 0;
            }

        }



        //*****************SYS_LOG**********************************
        public int InsertT_OE_SYS_LOG(string logType, string logMsg)
        {
            try
            {
                TOeSysLog e = new TOeSysLog
                {
                    LogType = logType,
                    LogMsg = logMsg,
                    LogDt = System.DateTime.Now
                };
                ctx.TOeSysLog.Add(e);
                ctx.SaveChanges();
                return e.SysLogId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TOeSysLog GetT_OE_SYS_LOG()
        {
            return null;
        }
    }
}
