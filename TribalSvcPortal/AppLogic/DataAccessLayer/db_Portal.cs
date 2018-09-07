using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TribalSvcPortal.Data.Models;
using TribalSvcPortal.AppLogic.BusinessLogicLayer;
using Microsoft.AspNetCore.Identity;
using System.Data.Entity.Validation;

namespace TribalSvcPortal.AppLogic.DataAccessLayer
{
    public class UserOrgDisplayType
    {
        public int ORG_USER_IDX { get; set; }
        public string ORG_ID { get; set; }
        public bool? ORG_ADMIN_IND { get; set; }
        public string STATUS_IND { get; set; }
        public string ORG_NAME { get; set; }
    }

    public class OrgUserClientDisplayType
    {
        public int ORG_USER_CLIENT_IDX { get; set; }
        public int ORG_USER_IDX { get; set; }
        public string CLIENT_ID { get; set; }
        public bool ADMIN_IND { get; set; }
        public string STATUS_IND { get; set; }
        public string ORG_ID { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string ORG_CLIENT_ALIAS { get; set; }
    }

    public interface IDbPortal
    {
        string GetT_PRT_APP_SETTING(string settingName);
        List<T_PRT_APP_SETTINGS> GetT_PRT_APP_SETTING_List();
        int InsertUpdateT_PRT_APP_SETTING(int sETTING_IDX, string sETTING_NAME, string sETTING_VALUE, bool? eNCRYPT_IND, string sETTING_VALUE_SALT, string cREATE_USER);
        List<T_PRT_CLIENTS> GetT_PRT_CLIENTS();
        T_PRT_CLIENTS GetT_PRT_CLIENTS_ByClientID(string id);
        string InsertUpdateT_PRT_CLIENTS(string cLIENT_ID, string cLIENT_NAME, string cLIENT_GRANT_TYPE, string cLIENT_REDIRECT_URI, string cLIENT_POST_LOGOUT_URI, string cLIENT_URL);
        List<T_PRT_ORGANIZATIONS> GetT_PRT_ORGANIZATIONS();
        T_PRT_ORG_USERS GetT_PRT_ORG_USERS_ByOrgUserID(int id);
        List<UserOrgDisplayType> GetT_PRT_ORG_USERS_ByUserID(string id);
        int InsertUpdateT_PRT_ORG_USERS(int? oRG_USER_IDX, string oRG_ID, string _Id, bool? oRG_ADMIN_IND, string sTATUS_IND, string cREATE_USER);
        int DeleteT_PRT_ORG_USERS(int id);
        List<OrgUserClientDisplayType> GetT_PRT_ORG_USERS_CLIENT_ByOrgUserID(int _OrgUserIDX);
        List<OrgUserClientDisplayType> GetT_PRT_ORG_USERS_CLIENT_ByUserID(string _UserIDX);        
        IEnumerable<T_PRT_CLIENTS> GetT_PRT_ORG_USERS_CLIENT_DistinctClientByUserID(string UserID);
        int InsertUpdateT_PRT_ORG_USERS_CLIENT(int? oRG_USER_CLIENT_IDX, int? oRG_USER_IDX, string cLIENT_ID, bool? aDMIN_IND, string sTATUS_IND, string cREATE_USER);
        int DeleteT_PRT_ORG_USER_CLIENT(int id);
       // int InsertT_PRT_SYS_LOG(string logType, string logMsg);
        IEnumerable<IdentityRole> GetT_PRT_ROLES_BelongingToUser(string UserID);
        T_PRT_SYS_LOG GetT_PRT_SYS_LOG();
       // string GetT_PRT_USER(string Email);
        IEnumerable<T_PRT_CLIENTS> GetDistinct_USERS_CLIENT_ByUserID(string _UserIDX);
        // void LogEFException(Exception ex);
    }

    public class DbPortal : IDbPortal
    {
        private readonly ApplicationDbContext ctx;
        public DbPortal(ApplicationDbContext _context)
        {
            ctx = _context;
        }


        //*****************APP SETTINGS**********************************
        public string GetT_PRT_APP_SETTING(string settingName)
        {
            try
            {
                return (from a in ctx.T_PRT_APP_SETTINGS
                        where a.SettingName == settingName
                        select a).FirstOrDefault().SettingValue;
            }
            catch (Exception ex)
            {
                log.LogEFException(ex);
                return "";
            }
        }

        public List<T_PRT_APP_SETTINGS> GetT_PRT_APP_SETTING_List()
        {
                try
                {
                    return (from a in ctx.T_PRT_APP_SETTINGS
                            orderby a.SettingIdx
                            select a).ToList();
                }
                catch (Exception ex)
                {
                log.LogEFException(ex);
                    return null;
                }
        }

        public int InsertUpdateT_PRT_APP_SETTING(int sETTING_IDX, string sETTING_NAME, string sETTING_VALUE, bool? eNCRYPT_IND, string sETTING_VALUE_SALT, string cREATE_USER)
        {
            try
            {
                Boolean insInd = false;

                T_PRT_APP_SETTINGS e = (from c in ctx.T_PRT_APP_SETTINGS
                                        where c.SettingIdx == sETTING_IDX
                                    select c).FirstOrDefault();

                if (e == null)
                {
                    insInd = true;
                    e = new T_PRT_APP_SETTINGS();
                }

                if (sETTING_NAME != null) e.SettingName = sETTING_NAME;
                if (sETTING_VALUE != null) e.SettingValue = sETTING_VALUE;

                e.ModifyDt = System.DateTime.Now;
                e.ModifyUserId = cREATE_USER;

                if (insInd)
                    ctx.T_PRT_APP_SETTINGS.Add(e);

                ctx.SaveChanges();
                return e.SettingIdx;
            }
            catch (Exception ex)
            {
                log.LogEFException(ex);
                return 0;
            }
        }


        //*****************APP SETTINGS CUSTOM**********************************



        //**************************** T_PRT_CLIENTS ***********************************************
        public List<T_PRT_CLIENTS> GetT_PRT_CLIENTS()
        {
            try
            {
                return ctx.T_PRT_CLIENTS.ToList();
            }
            catch (Exception ex)
            {
                log.LogEFException(ex);
                return null;
            }
        }

        public T_PRT_CLIENTS GetT_PRT_CLIENTS_ByClientID(string id)
        {
            try
            {
                return (from a in ctx.T_PRT_CLIENTS
                        where a.ClientId == id
                        select a).FirstOrDefault();
            }
            catch (Exception ex)
            {
                log.LogEFException(ex);
                return null;
            }
        }

        public string InsertUpdateT_PRT_CLIENTS(string cLIENT_ID, string cLIENT_NAME, string cLIENT_GRANT_TYPE, string cLIENT_REDIRECT_URI, string cLIENT_POST_LOGOUT_URI, string cLIENT_URL)
        {
            try
            {
                Boolean insInd = false;

                T_PRT_CLIENTS e = null;

                e = (from c in ctx.T_PRT_CLIENTS
                     where c.ClientId == cLIENT_ID
                        select c).FirstOrDefault();

                if (e == null)
                {
                    insInd = true;
                    e = new T_PRT_CLIENTS();
                }

                if (cLIENT_ID != null) e.ClientId = cLIENT_ID;
                if (cLIENT_NAME != null) e.ClientName = cLIENT_NAME;
                if (cLIENT_GRANT_TYPE != null) e.ClientGrantType = cLIENT_GRANT_TYPE;
                if (cLIENT_REDIRECT_URI != null) e.ClientRedirectUri = cLIENT_REDIRECT_URI;
                if (cLIENT_POST_LOGOUT_URI != null) e.ClientPostLogoutUri = cLIENT_POST_LOGOUT_URI;
                if (cLIENT_URL != null) e.ClientUrl = cLIENT_URL;

                if (insInd)
                    ctx.T_PRT_CLIENTS.Add(e);

                ctx.SaveChanges();
                return e.ClientId;
            }
            catch (Exception ex)
            {
                log.LogEFException(ex);
                return null;
            }
        }



        //******************************T_PRT_ORGANIZATIONS***********************************************
        public List<T_PRT_ORGANIZATIONS> GetT_PRT_ORGANIZATIONS()
        {
            try
            {
                return ctx.T_PRT_ORGANIZATIONS.ToList();
            }
            catch (Exception ex)
            {
                log.LogEFException(ex);
                return null;
            }
        }



        //******************************T_PRT_ORG_USERS***********************************************
        public T_PRT_ORG_USERS GetT_PRT_ORG_USERS_ByOrgUserID(int id)
        {
            try
            {
                return (from a in ctx.T_PRT_ORG_USERS
                           where a.OrgUserIdx == id
                           select a).FirstOrDefault();
            }
            catch (Exception ex)
            {
                log.LogEFException(ex);
                return null;
            }
        }

        public List<UserOrgDisplayType> GetT_PRT_ORG_USERS_ByUserID(string UserId)
        {
            try
            {
                var xxx = (from a in ctx.T_PRT_ORG_USERS
                           join b in ctx.T_PRT_ORGANIZATIONS on a.OrgId equals b.OrgId
                           where a.Id == UserId
                           orderby b.OrgName
                           select new UserOrgDisplayType
                           {
                               ORG_USER_IDX = a.OrgUserIdx,
                               ORG_ID = b.OrgId,
                               ORG_ADMIN_IND = a.OrgAdminInd,
                               STATUS_IND = a.StatusInd,
                               ORG_NAME = b.OrgName
                           }).ToList();

                return xxx;
            }
            catch (Exception ex)
            {
                log.LogEFException(ex);
                return null;
            }
        }

        public int InsertUpdateT_PRT_ORG_USERS(int? oRG_USER_IDX, string oRG_ID, string _Id, bool? oRG_ADMIN_IND, string sTATUS_IND, string cREATE_USER)
        {

            try
            {
                Boolean insInd = false;

                T_PRT_ORG_USERS e = null;

                e = (from c in ctx.T_PRT_ORG_USERS
                     where c.OrgUserIdx == oRG_USER_IDX
                     select c).FirstOrDefault();

                //now try to grab from user and org id
                if (e == null)
                {
                    e = (from c in ctx.T_PRT_ORG_USERS
                         where c.OrgId.ToUpper() == oRG_ID.ToUpper()
                         && c.Id == _Id
                         select c).FirstOrDefault();
                }

                if (e == null)
                {
                    insInd = true;
                    e = new T_PRT_ORG_USERS
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

                if (oRG_ID != null) e.OrgId = oRG_ID;
                if (_Id != null) e.Id = _Id;
                if (oRG_ADMIN_IND != null) e.OrgAdminInd = oRG_ADMIN_IND ?? false;
                if (sTATUS_IND != null) e.StatusInd = sTATUS_IND;

                if (insInd)
                    ctx.T_PRT_ORG_USERS.Add(e);

                ctx.SaveChanges();
                return e.OrgUserIdx;
            }
            catch (Exception ex)
            {
                log.LogEFException(ex);
                return 0;
            }

        }

        public int DeleteT_PRT_ORG_USERS(int id)
        {
            try
            {
                T_PRT_ORG_USERS rec = new T_PRT_ORG_USERS { OrgUserIdx = id };
                ctx.Entry(rec).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                ctx.SaveChanges();

                return 1;
            }
            catch (Exception ex)
            {
                log.LogEFException(ex);
                return 0;
            }
        }



        //******************************T_PRT_ORG_USER_CLIENT***********************************************
        public List<OrgUserClientDisplayType> GetT_PRT_ORG_USERS_CLIENT_ByOrgUserID(int _OrgUserIDX)
        {
            try
            {
                var xxx = (from a in ctx.T_PRT_ORG_USER_CLIENT
                           join b in ctx.T_PRT_ORG_USERS on a.OrgUserIdx equals b.OrgUserIdx
                           where a.OrgUserIdx == _OrgUserIDX
                           orderby a.ClientId
                            select new OrgUserClientDisplayType
                            {
                                ORG_USER_CLIENT_IDX = a.OrgUserClientIdx,
                                ORG_USER_IDX = a.OrgUserIdx,
                                CLIENT_ID = a.ClientId,
                                ADMIN_IND = a.AdminInd,
                                STATUS_IND = a.StatusInd,
                                ORG_ID = b.OrgId,
                                UserID = b.Id,
                                UserName = b.Id
                            }).ToList();

                return xxx;
            }
            catch (Exception ex)
            {
                log.LogEFException(ex);
                return null;
            }

        }

        public List<OrgUserClientDisplayType> GetT_PRT_ORG_USERS_CLIENT_ByUserID(string _UserIDX)
        {
            try
            {
                var xxx = (from a in ctx.T_PRT_ORG_USER_CLIENT
                            join b in ctx.T_PRT_ORG_USERS on a.OrgUserIdx equals b.OrgUserIdx
                           join c in ctx.T_PRT_ORG_CLIENT_ALIAS on new { a.ClientId, b.OrgId } equals new { c.ClientId, c.OrgId }
                            where b.Id == _UserIDX
                            select new OrgUserClientDisplayType
                            {
                                ORG_USER_CLIENT_IDX = a.OrgUserClientIdx,
                                ORG_USER_IDX = a.OrgUserIdx,
                                CLIENT_ID = a.ClientId,
                                ADMIN_IND = a.AdminInd,
                                STATUS_IND = a.StatusInd,
                                ORG_ID = b.OrgId,
                                UserID = b.Id,
                                UserName = b.Id,
                                ORG_CLIENT_ALIAS = c.OrgClientAlias
                            }).ToList();

                return xxx;
            }
            catch (Exception ex)
            {
                log.LogEFException(ex);
                return null;
            }
        }

        public IEnumerable<T_PRT_CLIENTS> GetT_PRT_ORG_USERS_CLIENT_DistinctClientByUserID(string UserID)
        {
            try
            {
                var xxx = (from a in ctx.T_PRT_ORG_USER_CLIENT
                           join b in ctx.T_PRT_ORG_USERS on a.OrgUserIdx equals b.OrgUserIdx
                           join c in ctx.T_PRT_CLIENTS on a.ClientId equals c.ClientId
                            where b.Id == UserID
                            orderby a.ClientId
                            select c).ToList().Distinct();

                return xxx;
            }
            catch (Exception ex)
            {
                log.LogEFException(ex);
                return null;
            }
        }
        public IEnumerable<T_PRT_CLIENTS> GetDistinct_USERS_CLIENT_ByUserID(string _UserIDX)
        {
            try
            {
                var xxx = (from a in ctx.T_PRT_ORG_USER_CLIENT
                           join b in ctx.T_PRT_ORG_USERS on a.OrgUserIdx equals b.OrgUserIdx
                           join c in ctx.T_PRT_ORG_CLIENT_ALIAS on new { a.ClientId, b.OrgId } equals new { c.ClientId, c.OrgId }
                           join d in ctx.T_PRT_CLIENTS on a.ClientId equals d.ClientId
                           where b.Id == _UserIDX
                           select d).ToList().Distinct();

                return xxx;
            }
            catch (Exception ex)
            {
                log.LogEFException(ex);
                return null;
            }
        }
        public int InsertUpdateT_PRT_ORG_USERS_CLIENT(int? oRG_USER_CLIENT_IDX, int? oRG_USER_IDX, string cLIENT_ID, bool? aDMIN_IND, string sTATUS_IND, string cREATE_USER)
        {
            try
            {
                Boolean insInd = false;

                T_PRT_ORG_USER_CLIENT e = null;

                e = (from c in ctx.T_PRT_ORG_USER_CLIENT
                     where c.OrgUserClientIdx == oRG_USER_CLIENT_IDX
                        select c).FirstOrDefault();

                //now try to grab from user and org id
                if (e == null)
                {
                    e = (from c in ctx.T_PRT_ORG_USER_CLIENT
                         where c.ClientId.ToUpper() == cLIENT_ID.ToUpper()
                            && c.OrgUserIdx == oRG_USER_IDX
                            select c).FirstOrDefault();
                }

                if (e == null)
                {
                    insInd = true;
                    e = new T_PRT_ORG_USER_CLIENT
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

                if (oRG_USER_IDX != null) e.OrgUserIdx = oRG_USER_IDX ?? 0;
                if (cLIENT_ID != null) e.ClientId = cLIENT_ID;
                if (aDMIN_IND != null) e.AdminInd = aDMIN_IND ?? false;
                if (sTATUS_IND != null) e.StatusInd = sTATUS_IND;

                if (insInd)
                    ctx.T_PRT_ORG_USER_CLIENT.Add(e);

                ctx.SaveChanges();
                return e.OrgUserIdx;
            }
            catch (Exception ex)
            {
                log.LogEFException(ex); 
                return 0;
            }

        }

        public int DeleteT_PRT_ORG_USER_CLIENT(int id)
        {
            try
            {
                T_PRT_ORG_USER_CLIENT rec = new T_PRT_ORG_USER_CLIENT { OrgUserClientIdx = id };
                ctx.Entry(rec).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                ctx.SaveChanges();

                return 1;
            }
            catch (Exception ex)
            {
                log.LogEFException(ex);
                return 0;
            }

        }
              

        //*****************ROLES**********************************
        public IEnumerable<IdentityRole> GetT_PRT_ROLES_BelongingToUser(string UserID)
        {
            try
            {
                return (from r in ctx.Roles
                           join ur in ctx.UserRoles on r.Id equals ur.RoleId
                           where ur.UserId == UserID
                           select r).ToList();
            }
            catch (Exception ex)
            {
                log.LogEFException(ex);
                return null;
            }
        }



        //*****************SYS_LOG**********************************
       

        public T_PRT_SYS_LOG GetT_PRT_SYS_LOG()
        {
            return null;
        }

             

    }
}
