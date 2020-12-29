using System;
using System.Collections.Generic;
using System.Linq;
using TribalSvcPortal.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TribalSvcPortal.AppLogic.BusinessLogicLayer;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.RegularExpressions;

namespace TribalSvcPortal.AppLogic.DataAccessLayer
{
    public class UserOrgDisplayType
    {
        public int? ORG_USER_IDX { get; set; }
        public string ORG_ID { get; set; }
        public string USER_ID { get; set; }
        public string ACCESS_LEVEL { get; set; }
        public string STATUS_IND { get; set; }
        public string ORG_NAME { get; set; }
        public string USER_NAME { get; set; }
        public List<OrgUserClientShortDisplayType> OrgUserClientDisplay { get; set; }
    }

    public class OrgUserClientDisplayType
    {
        public int ORG_USER_CLIENT_IDX { get; set; }
        public int? ORG_USER_IDX { get; set; }
        public string CLIENT_ID { get; set; }
        public bool ADMIN_IND { get; set; }
        public string STATUS_IND { get; set; }
        public string ORG_ID { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string ORG_CLIENT_ALIAS { get; set; }
    }

    public class OrgUserClientShortDisplayType
    {
        public string CLIENT_ID { get; set; }
        public int? ORG_USER_CLIENT_IDX { get; set; }
        public int? ORG_USER_IDX { get; set; }
        public bool? ADMIN_IND { get; set; }
        public string STATUS_IND { get; set; }
        public string ORG_CLIENT_ALIAS { get; set; }
    }


    public interface IDbPortal
    {
        //*****************APP SETTINGS**********************************
        string GetT_PRT_APP_SETTING(string settingName);
        List<T_PRT_APP_SETTINGS> GetT_PRT_APP_SETTING_List();
        int InsertUpdateT_PRT_APP_SETTING(int sETTING_IDX, string sETTING_NAME, string sETTING_VALUE, bool? eNCRYPT_IND, string sETTING_VALUE_SALT, string cREATE_USER);

        //*****************APP SETTINGS CUSTOM**********************************
        T_PRT_APP_SETTINGS_CUSTOM GetT_PRT_APP_SETTINGS_CUSTOM();
        int InsertUpdateT_PRT_APP_SETTING_CUSTOM(string tERMS_AND_CONDITIONS, string aNNOUNCEMENTS);

        //**************************** T_PRT_CLIENTS ***********************************************
        List<T_PRT_CLIENTS> GetT_PRT_CLIENTS();
        T_PRT_CLIENTS GetT_PRT_CLIENTS_ByClientID(string id);
        string InsertUpdateT_PRT_CLIENTS(string cLIENT_ID, string cLIENT_NAME, string cLIENT_GRANT_TYPE, string cLIENT_REDIRECT_URI, string cLIENT_POST_LOGOUT_URI, string cLIENT_URL);

        //******************************T_PRT_ORGANIZATIONS***********************************************
        List<T_PRT_ORGANIZATIONS> GetT_PRT_ORGANIZATIONS();
        T_PRT_ORGANIZATIONS GetT_PRT_ORGANIZATIONS_ByOrgID(string OrgID);
        List<T_PRT_ORGANIZATIONS> GetT_PRT_ORGANIZATIONS_UserIsAdmin(string UserID);
        List<T_PRT_ORGANIZATIONS> GetT_PRT_ORGANIZATIONS_HaveWordPress();
        string InsertUpdateT_PRT_ORGANIZATIONS(string oRG_ID, string oRG_NAME);
        List<T_PRT_ORGANIZATIONS> GetT_PRT_ORGANIZATIONS_ByEmail(string Email);

        //******************************T_PRT_ORG_EMAIL_RULE***********************************************
        List<T_PRT_ORG_EMAIL_RULE> GetT_PRT_ORG_EMAIL_RULE_ByOrgID(string OrgID);
        int InsertT_PRT_ORG_EMAIL_RULE(string oRG_ID, string eMAIL_STRING, string cREATE_USER);
        int DeleteT_OE_ORGANIZATION_EMAIL_RULE(string OrgID, string email);

        //******************************T_PRT_ORG_USERS***********************************************
        T_PRT_ORG_USERS GetT_PRT_ORG_USERS_ByOrgUserID(int id);
        List<UserOrgDisplayType> GetT_PRT_ORG_USERS_ByUserID(string id);
        List<UserOrgDisplayType> GetT_PRT_ORG_USERS_ByUserID_WithClientList_WithAlias(string UserId, string ClientId);
        List<UserOrgDisplayType> GetT_PRT_ORG_USERS_ByUserID_WithClientList(string UserId);
        List<UserOrgDisplayType> GetT_PRT_ORG_USERS_ByOrgID(string OrgID);
        int InsertUpdateT_PRT_ORG_USERS(int? oRG_USER_IDX, string oRG_ID, string _Id, string aCCESS_LEVEL, string sTATUS_IND, string cREATE_USER);
        int DeleteT_PRT_ORG_USERS(int id);
        int DeleteT_PRT_ORG_USERS(T_PRT_ORG_USERS entity);
        bool IsUserAnOrgAdmin(string UserID, string OrgID);
        bool IsUserAnyOrgAdmin(string UserID);
        T_PRT_ORG_USERS GetUserOrg(string UserID, string OrgID);
        int GetOrgUsersCount(string UserID);

        //******************************T_PRT_ORG_USERS_CLIENT***********************************************
        T_PRT_ORG_USER_CLIENT GetT_PRT_ORG_USERS_CLIENT_ByID(int _OrgUserClientIDX);
        List<OrgUserClientDisplayType> GetT_PRT_ORG_USERS_CLIENT_ByOrgUserID(int _OrgUserIDX);
        List<OrgUserClientDisplayType> GetT_PRT_ORG_USERS_CLIENT_ByUserID(string _UserIDX);
        List<OrgUserClientDisplayType> GetT_PRT_ORG_USERS_CLIENT_AdminByUserID(string _UserIDX);
        List<OrgUserClientDisplayType> GetT_PRT_ORG_USERS_CLIENT_ByOrgIDandClientID(string _orgID, string _clientID, bool AdminOnlyInd);
        IEnumerable<T_PRT_CLIENTS> GetT_PRT_ORG_USERS_CLIENT_DistinctClientByUserID(string UserID);
        IEnumerable<SelectListItem> get_ddl_T_PRT_ORG_USERS_CLIENT_ByUserIDandClient(string UserIDX, string ClientID);
        int InsertUpdateT_PRT_ORG_USERS_CLIENT(int? oRG_USER_CLIENT_IDX, int? oRG_USER_IDX, string cLIENT_ID, bool? aDMIN_IND, string sTATUS_IND, string cREATE_USER);
        int DeleteT_PRT_ORG_USER_CLIENT(int id);
        bool IsUserAnOrgClientAdmin(string UserID);

        //**************************** T_PRT_ROLES ***********************************************
        IEnumerable<IdentityRole> GetT_PRT_ROLES_BelongingToUser(string UserID);
        IEnumerable<ApplicationUser> GetT_PRT_USERS_BelongingToRole(string RoleID);

        //**************************** T_PRT_USERS ***********************************************
        int UpdateT_PRT_USERS_LoginDate(ApplicationUser user);
        int UpdateT_PRT_USERS_PasswordEncrypt(ApplicationUser user, string Password);
        int UpdateT_PRT_USERS_WordPressUserId(ApplicationUser user, int WordPressUserId);

        //*****************SYS_LOG**********************************
        List<T_PRT_SYS_LOG> GetT_PRT_SYS_LOG();

        //*****************SYS_EMAIL_LOG**********************************
        List<T_PRT_SYS_EMAIL_LOG> GetT_PRT_SYS_EMAIL_LOG();

        //**************************** T_PRT_SITES ***********************************************
        Guid? InsertUpdateT_PRT_SITES(Guid? sITE_IDX, string oRG_ID, string sITE_NAME, string ePA_ID, decimal? lATITUDE, decimal? lONGITUDE, string sITE_ADDRESS, string UserIDX, string lAND_STATUS, string tWP, string rANGE, int? sECTION, string cOUNTY);
        T_PRT_SITES GetT_PRT_SITES_BySITEIDX(Guid Siteidx);
        int DeleteT_PRT_SITES(Guid sITE_IDX);
        IEnumerable<SelectListItem> get_ddl_T_PRT_LAND_STATUS();
        List<SelectListItem> getT_PRT_SITES_UniqueCounties();

        //**************************** T_PRT_DOCUMENTS ***********************************************
        Guid? InsertUpdateT_PRT_DOCUMENTS(Guid? dOC_IDX, string oRG_ID, byte[] dOC_CONTENT, string dOC_NAME, string dOC_TYPE, string dOC_FILE_TYPE, int? dOC_SIZE, string dOC_COMMENT,
        string dOC_AUTHOR, string sHARE_TYPE, string dOC_STATUS_TYPE, string UserID);
        T_PRT_DOCUMENTS GetT_PRT_DOCUMENTS_ByID(Guid DocIDX);
        int DeleteT_PRT_DOCUMENTS(Guid DocIDX);

        //**************************** T_PRT_REF_EMAIL_TEMPLATE ***********************************************
        List<SelectListItem> get_ddl_T_PRT_REF_EMAIL_TEMPLATE();
        T_PRT_REF_EMAIL_TEMPLATE GetT_PRT_REF_EMAIL_TEMPLATE_ByID(int id);
        T_PRT_REF_EMAIL_TEMPLATE GetT_PRT_REF_EMAIL_TEMPLATE_ByName(string name);
        int InsertUpdateT_PRT_REF_EMAIL_TEMPLATE(int? eMAIL_TEMPLATE_ID, string sUBJ, string mSG, string UserID);

        //**************************** T_PRT_REF_UNITS ***********************************************
        T_PRT_REF_UNITS get_T_PRT_REF_UNITS_ByID(Guid? id);


        //**************************** DATA APIS ***********************************************
        List<SelectListItem> get_ddl_APIS();
        List<SelectListItem> get_ddl_APIformat();
    }

    public class DbPortal : IDbPortal
    {
        private readonly ApplicationDbContext ctx;
        private readonly Ilog _log;
        public DbPortal(ApplicationDbContext _context, Ilog log)
        {
            ctx = _context;
            _log = log ?? throw new ArgumentNullException(nameof(log));
        }

        //*****************APP_SETTINGS**********************************
        public string GetT_PRT_APP_SETTING(string settingName)
        {
            try
            {
                return (from a in ctx.T_PRT_APP_SETTINGS
                        where a.SETTING_NAME == settingName
                        select a).FirstOrDefault().SETTING_VALUE;
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return "";
            }
        }

        public List<T_PRT_APP_SETTINGS> GetT_PRT_APP_SETTING_List()
        {
                try
                {
                    return (from a in ctx.T_PRT_APP_SETTINGS
                            orderby a.SETTING_IDX
                            select a).ToList();
                }
                catch (Exception ex)
                {
                _log.LogEFException(ex);
                    return null;
                }
        }

        public int InsertUpdateT_PRT_APP_SETTING(int sETTING_IDX, string sETTING_NAME, string sETTING_VALUE, bool? eNCRYPT_IND, string sETTING_VALUE_SALT, string cREATE_USER)
        {
            try
            {
                Boolean insInd = false;

                T_PRT_APP_SETTINGS e = (from c in ctx.T_PRT_APP_SETTINGS
                                        where c.SETTING_IDX == sETTING_IDX
                                    select c).FirstOrDefault();

                if (e == null)
                {
                    insInd = true;
                    e = new T_PRT_APP_SETTINGS();
                }

                if (sETTING_NAME != null) e.SETTING_NAME = sETTING_NAME;
                if (sETTING_VALUE != null) e.SETTING_VALUE = sETTING_VALUE;

                e.MODIFY_DT = System.DateTime.Now;
                e.MODIFY_USER_ID = cREATE_USER;

                if (insInd)
                    ctx.T_PRT_APP_SETTINGS.Add(e);

                ctx.SaveChanges();
                return e.SETTING_IDX;
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return 0;
            }
        }


        //*****************APP_SETTINGS_CUSTOM**********************************
        public T_PRT_APP_SETTINGS_CUSTOM GetT_PRT_APP_SETTINGS_CUSTOM()
        {
            try
            {
                return (from a in ctx.T_PRT_APP_SETTINGS_CUSTOM
                        select a).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }

        }

        public int InsertUpdateT_PRT_APP_SETTING_CUSTOM(string tERMS_AND_CONDITIONS, string aNNOUNCEMENTS)
        {
                try
                {
                    Boolean insInd = false;

                    T_PRT_APP_SETTINGS_CUSTOM e = (from c in ctx.T_PRT_APP_SETTINGS_CUSTOM
                                                  select c).FirstOrDefault();

                    if (e == null)
                    {
                        insInd = true;
                        e = new T_PRT_APP_SETTINGS_CUSTOM();
                    }

                    if (tERMS_AND_CONDITIONS != null) e.TERMS_AND_CONDITIONS = Utils.GetSafeHtml(tERMS_AND_CONDITIONS);
                    if (aNNOUNCEMENTS != null) e.ANNOUNCEMENTS = Utils.GetSafeHtml(aNNOUNCEMENTS);

                    if (insInd)
                        ctx.T_PRT_APP_SETTINGS_CUSTOM.Add(e);

                    ctx.SaveChanges();
                    return e.SETTING_CUSTOM_IDX;
                }
                catch (Exception ex)
                {
                    _log.LogEFException(ex);
                    return 0;
                }            
        }


        //**************************** CLIENTS ***********************************************
        public List<T_PRT_CLIENTS> GetT_PRT_CLIENTS()
        {
            try
            {
                return ctx.T_PRT_CLIENTS.ToList();
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }

        public T_PRT_CLIENTS GetT_PRT_CLIENTS_ByClientID(string id)
        {
            try
            {
                return (from a in ctx.T_PRT_CLIENTS
                        where a.CLIENT_ID == id
                        select a).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
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
                     where c.CLIENT_ID == cLIENT_ID
                        select c).FirstOrDefault();

                if (e == null)
                {
                    insInd = true;
                    e = new T_PRT_CLIENTS();
                }

                if (cLIENT_ID != null) e.CLIENT_ID = cLIENT_ID;
                if (cLIENT_NAME != null) e.CLIENT_NAME = cLIENT_NAME;
                if (cLIENT_GRANT_TYPE != null) e.CLIENT_GRANT_TYPE = cLIENT_GRANT_TYPE;
                if (cLIENT_REDIRECT_URI != null) e.CLIENT_REDIRECT_URI = cLIENT_REDIRECT_URI;
                if (cLIENT_POST_LOGOUT_URI != null) e.CLIENT_POST_LOGOUT_URI = cLIENT_POST_LOGOUT_URI;
                if (cLIENT_URL != null) e.CLIENT_URL = cLIENT_URL;

                if (insInd)
                    ctx.T_PRT_CLIENTS.Add(e);

                ctx.SaveChanges();
                return e.CLIENT_ID;
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }



        //******************************ORGANIZATIONS***********************************************
        public List<T_PRT_ORGANIZATIONS> GetT_PRT_ORGANIZATIONS()
        {
            try
            {
                return ctx.T_PRT_ORGANIZATIONS.ToList();
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }

        public T_PRT_ORGANIZATIONS GetT_PRT_ORGANIZATIONS_ByOrgID(string OrgID)
        {
            try
            {
                return ( from a in ctx.T_PRT_ORGANIZATIONS
                         where a.ORG_ID == OrgID
                         select a).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }

        public List<T_PRT_ORGANIZATIONS> GetT_PRT_ORGANIZATIONS_UserIsAdmin(string UserID)
        {
            try
            {
                return (from a in ctx.T_PRT_ORGANIZATIONS 
                        join b in ctx.T_PRT_ORG_USERS on a.ORG_ID equals b.ORG_ID
                        where b.Id == UserID
                        && b.ACCESS_LEVEL == "A"
                        && b.STATUS_IND == "A"
                        select a).ToList();
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }

        public List<T_PRT_ORGANIZATIONS> GetT_PRT_ORGANIZATIONS_HaveWordPress()
        {
            try
            {
                return (from a in ctx.T_PRT_ORGANIZATIONS
                        where a.WORDPRESS_URL != null
                        select a).ToList();
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }
        

        public string InsertUpdateT_PRT_ORGANIZATIONS(string oRG_ID, string oRG_NAME)
        {
            try
            {
                Boolean insInd = false;

                T_PRT_ORGANIZATIONS e = null;

                e = (from c in ctx.T_PRT_ORGANIZATIONS
                     where c.ORG_ID == oRG_ID
                     select c).FirstOrDefault();

                if (e == null)
                {
                    insInd = true;
                    e = new T_PRT_ORGANIZATIONS();
                    e.ORG_ID = oRG_ID;
                }

                if (oRG_NAME != null) e.ORG_NAME = oRG_NAME;

                if (insInd)
                    ctx.T_PRT_ORGANIZATIONS.Add(e);

                ctx.SaveChanges();
                return e.ORG_ID;
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }

        public List<T_PRT_ORGANIZATIONS> GetT_PRT_ORGANIZATIONS_ByEmail(string email)
        {
            try
            {
                var domain = Regex.Match(email, "@(.*)").Groups[1].Value;

                return (from a in ctx.T_PRT_ORGANIZATIONS
                        join b in ctx.T_PRT_ORG_EMAIL_RULE on a.ORG_ID equals b.ORG_ID
                        where b.EMAIL_STRING.ToUpper() == domain.ToUpper()
                        select a).ToList();
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                throw ex;
            }
        }



        //******************************ORG_EMAIL_RULE***********************************************
        public List<T_PRT_ORG_EMAIL_RULE> GetT_PRT_ORG_EMAIL_RULE_ByOrgID(string OrgID)
        {
            try
            {
                return (from a in ctx.T_PRT_ORG_EMAIL_RULE
                        where a.ORG_ID == OrgID
                        select a).ToList();
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }

        public int InsertT_PRT_ORG_EMAIL_RULE(string oRG_ID, string eMAIL_STRING, string cREATE_USER)
        {
            try
            {
                T_PRT_ORG_EMAIL_RULE e = new T_PRT_ORG_EMAIL_RULE();
                e.ORG_ID = oRG_ID;
                e.EMAIL_STRING = eMAIL_STRING;
                e.MODIFY_DT = System.DateTime.Now;
                e.MODIFY_USERIDX = 0;

                ctx.T_PRT_ORG_EMAIL_RULE.Add(e);
                ctx.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return 0;
            }
        }

        public int DeleteT_OE_ORGANIZATION_EMAIL_RULE(string OrgID, string email)
        {
            try
            {
                T_PRT_ORG_EMAIL_RULE rec = new T_PRT_ORG_EMAIL_RULE { ORG_ID = OrgID, EMAIL_STRING = email };
                ctx.Entry(rec).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                ctx.SaveChanges();

                return 1;
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return 0;
            }
        }



        //******************************ORG_USERS***********************************************
        public T_PRT_ORG_USERS GetT_PRT_ORG_USERS_ByOrgUserID(int id)
        {
            try
            {
                return (from a in ctx.T_PRT_ORG_USERS
                           where a.ORG_USER_IDX == id
                           select a).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }

        public List<UserOrgDisplayType> GetT_PRT_ORG_USERS_ByUserID(string UserId)
        {
            try
            {
                var xxx = (from a in ctx.T_PRT_ORG_USERS
                           join b in ctx.T_PRT_ORGANIZATIONS on a.ORG_ID equals b.ORG_ID
                           where a.Id == UserId
                           orderby b.ORG_NAME
                           select new UserOrgDisplayType
                           {
                               ORG_USER_IDX = a.ORG_USER_IDX,
                               ORG_ID = b.ORG_ID,
                               ACCESS_LEVEL = a.ACCESS_LEVEL,
                               STATUS_IND = a.STATUS_IND,
                               ORG_NAME = b.ORG_NAME
                           }).ToList();

                return xxx;
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }

        public List<UserOrgDisplayType> GetT_PRT_ORG_USERS_ByOrgID(string OrgID)
        {
            try
            {
                var xxx = (from a in ctx.T_PRT_ORG_USERS
                           join b in ctx.Users on a.Id equals b.Id
                           where a.ORG_ID == OrgID
                           orderby b.LAST_NAME
                           select new UserOrgDisplayType
                           {
                               ORG_USER_IDX = a.ORG_USER_IDX,
                               ORG_ID = a.ORG_ID,
                               ACCESS_LEVEL = a.ACCESS_LEVEL,
                               STATUS_IND = a.STATUS_IND,
                               ORG_NAME = null,
                               USER_ID = a.Id,
                               USER_NAME = b.FIRST_NAME + " " + b.LAST_NAME                               
                           }).ToList();

                return xxx;
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }

        public List<UserOrgDisplayType> GetT_PRT_ORG_USERS_ByUserID_WithClientList_WithAlias(string UserId, string ClientId)
        {
            try
            {
                var xxx = (from a in ctx.T_PRT_ORG_USERS
                           join b in ctx.T_PRT_ORGANIZATIONS on a.ORG_ID equals b.ORG_ID
                           where a.Id == UserId
                           orderby b.ORG_NAME
                           select new UserOrgDisplayType
                           {
                               ORG_USER_IDX = a.ORG_USER_IDX,
                               ORG_ID = b.ORG_ID,
                               ACCESS_LEVEL = a.ACCESS_LEVEL,
                               STATUS_IND = a.STATUS_IND,
                               ORG_NAME = b.ORG_NAME//,
                               //OrgUserClientDisplay = (
                               //from aa in ctx.T_PRT_CLIENTS 
                               //join bb in ctx.T_PRT_ORG_USER_CLIENT.Where(o => o.ORG_USER_IDX  == a.ORG_USER_IDX) on aa.CLIENT_ID equals bb.CLIENT_ID
                               ////join bb in ctx.T_PRT_ORG_USER_CLIENT.Where(o => o.ORG_USER_IDX == (a.ORG_USER_IDX == null ? 0 : 1)) on new { aa.CLIENT_ID } equals new { bb.CLIENT_ID }
                               //into sr from x in sr.DefaultIfEmpty()  //left join
                               // select new OrgUserClientShortDisplayType
                               // {
                               //     CLIENT_ID = aa.CLIENT_ID,
                               //     ORG_USER_CLIENT_IDX = x.ORG_USER_CLIENT_IDX,
                               //     ORG_USER_IDX = x.ORG_USER_IDX,
                               //     ADMIN_IND = x.ADMIN_IND,
                               //     STATUS_IND = x.STATUS_IND
                               // }).ToList()


                           }).ToList();

                for (int i = 0; i < xxx.Count; i++)
                {
                    var entryToProcess = xxx[i];

                    int? OrgUserIDX = entryToProcess.ORG_USER_IDX;

                    entryToProcess.OrgUserClientDisplay = (from aa in ctx.T_PRT_CLIENTS
                                                           join bb in ctx.T_PRT_ORG_USER_CLIENT.Where(o => o.ORG_USER_IDX == OrgUserIDX) on aa.CLIENT_ID equals bb.CLIENT_ID
                                                           into sr
                                                           from x in sr.DefaultIfEmpty()  //left join
                                                           join cc in ctx.T_PRT_ORG_USERS on x.ORG_USER_IDX equals cc.ORG_USER_IDX
                                                           join dd in ctx.T_PRT_ORG_CLIENT_ALIAS.Where(a => a.CLIENT_ID == ClientId) on cc.ORG_ID equals dd.ORG_ID
                                                           select new OrgUserClientShortDisplayType
                                                           {
                                                               CLIENT_ID = aa.CLIENT_ID,
                                                               ORG_USER_CLIENT_IDX = x.ORG_USER_CLIENT_IDX == null ? 0 : x.ORG_USER_CLIENT_IDX,
                                                               ORG_USER_IDX = x.ORG_USER_IDX == null ? 0 : x.ORG_USER_IDX,
                                                               ADMIN_IND = x.ADMIN_IND == null ? false : x.ADMIN_IND,
                                                               STATUS_IND = x.STATUS_IND == null ? "" : x.STATUS_IND,
                                                               ORG_CLIENT_ALIAS = dd.ORG_CLIENT_ALIAS == null ? "" : dd.ORG_CLIENT_ALIAS,
                                                           }).ToList();

                    xxx[i].OrgUserClientDisplay = entryToProcess.OrgUserClientDisplay;
                }


                return xxx;
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }

        public List<UserOrgDisplayType> GetT_PRT_ORG_USERS_ByUserID_WithClientList(string UserId)
        {
            try
            {
                var xxx = (from a in ctx.T_PRT_ORG_USERS
                           join b in ctx.T_PRT_ORGANIZATIONS on a.ORG_ID equals b.ORG_ID
                           where a.Id == UserId
                           orderby b.ORG_NAME
                           select new UserOrgDisplayType
                           {
                               ORG_USER_IDX = a.ORG_USER_IDX,
                               ORG_ID = b.ORG_ID,
                               ACCESS_LEVEL = a.ACCESS_LEVEL,
                               STATUS_IND = a.STATUS_IND,
                               ORG_NAME = b.ORG_NAME//,
                               //OrgUserClientDisplay = (
                               //from aa in ctx.T_PRT_CLIENTS 
                               //join bb in ctx.T_PRT_ORG_USER_CLIENT.Where(o => o.ORG_USER_IDX  == a.ORG_USER_IDX) on aa.CLIENT_ID equals bb.CLIENT_ID
                               ////join bb in ctx.T_PRT_ORG_USER_CLIENT.Where(o => o.ORG_USER_IDX == (a.ORG_USER_IDX == null ? 0 : 1)) on new { aa.CLIENT_ID } equals new { bb.CLIENT_ID }
                               //into sr from x in sr.DefaultIfEmpty()  //left join
                               // select new OrgUserClientShortDisplayType
                               // {
                               //     CLIENT_ID = aa.CLIENT_ID,
                               //     ORG_USER_CLIENT_IDX = x.ORG_USER_CLIENT_IDX,
                               //     ORG_USER_IDX = x.ORG_USER_IDX,
                               //     ADMIN_IND = x.ADMIN_IND,
                               //     STATUS_IND = x.STATUS_IND
                               // }).ToList()


                           }).ToList();

                for (int i = 0; i < xxx.Count; i++)
                {
                    var entryToProcess = xxx[i];

                    int? OrgUserIDX = entryToProcess.ORG_USER_IDX;

                    entryToProcess.OrgUserClientDisplay = (from aa in ctx.T_PRT_CLIENTS
                                                           join bb in ctx.T_PRT_ORG_USER_CLIENT.Where(o => o.ORG_USER_IDX == OrgUserIDX) on aa.CLIENT_ID equals bb.CLIENT_ID
                                                           into sr
                                                           from x in sr.DefaultIfEmpty()  //left join
                                                           select new OrgUserClientShortDisplayType
                                                           {
                                                               CLIENT_ID = aa.CLIENT_ID,
                                                               ORG_USER_CLIENT_IDX = x.ORG_USER_CLIENT_IDX == null ? 0 : x.ORG_USER_CLIENT_IDX,
                                                               ORG_USER_IDX = x.ORG_USER_IDX == null ? 0 : x.ORG_USER_IDX,
                                                               ADMIN_IND = x.ADMIN_IND == null ? false : x.ADMIN_IND,
                                                               STATUS_IND = x.STATUS_IND == null ? "" : x.STATUS_IND, 
                                                           }).ToList();

                    xxx[i].OrgUserClientDisplay = entryToProcess.OrgUserClientDisplay;
                }


                return xxx;
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }

        public int InsertUpdateT_PRT_ORG_USERS(int? oRG_USER_IDX, string oRG_ID, string _Id, string aCCESS_LEVEL, string sTATUS_IND, string cREATE_USER)
        {

            try
            {
                Boolean insInd = false;

                T_PRT_ORG_USERS e = null;

                e = (from c in ctx.T_PRT_ORG_USERS
                     where c.ORG_USER_IDX == oRG_USER_IDX
                     select c).FirstOrDefault();

                //now try to grab from user and org id
                if (e == null)
                {
                    e = (from c in ctx.T_PRT_ORG_USERS
                         where c.ORG_ID.ToUpper() == oRG_ID.ToUpper()
                         && c.Id == _Id
                         select c).FirstOrDefault();
                }

                if (e == null)
                {
                    insInd = true;
                    e = new T_PRT_ORG_USERS
                    {
                        CREATE_DT = System.DateTime.Now,
                        CREATE_USER_ID = cREATE_USER
                    };
                }
                else
                {
                    e.MODIFY_DT = System.DateTime.Now;
                    e.MODIFY_USER_ID = cREATE_USER;
                }

                if (oRG_ID != null) e.ORG_ID = oRG_ID;
                if (_Id != null) e.Id = _Id;
                if (aCCESS_LEVEL != null) e.ACCESS_LEVEL = aCCESS_LEVEL;
                if (sTATUS_IND != null) e.STATUS_IND = sTATUS_IND;

                if (insInd)
                    ctx.T_PRT_ORG_USERS.Add(e);

                ctx.SaveChanges();
                return e.ORG_USER_IDX;
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return 0;
            }

        }

        public int DeleteT_PRT_ORG_USERS(int id)
        {
            try
            {
                T_PRT_ORG_USERS rec = new T_PRT_ORG_USERS { ORG_USER_IDX = id };
                ctx.Entry(rec).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                ctx.SaveChanges();

                return 1;
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return 0;
            }
        }

        public int DeleteT_PRT_ORG_USERS(T_PRT_ORG_USERS entity)
        {
            try
            {
                ctx.T_PRT_ORG_USERS.Remove(entity);
                ctx.SaveChanges();

                return 1;
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return 0;
            }
        }

        public bool IsUserAnOrgAdmin(string UserID, string OrgID)
        {
            try
            {
                var xxx = (from a in ctx.T_PRT_ORG_USERS
                           where a.Id == UserID
                           && a.ORG_ID == OrgID
                           select a).FirstOrDefault();

                return (xxx != null && xxx.ACCESS_LEVEL == "A");
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return false;
            }
        }

        /// <summary>
        /// Is the user an overall admin of any organization
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public bool IsUserAnyOrgAdmin(string UserID)
        {
            try
            {
                return (from a in ctx.T_PRT_ORG_USERS
                           where a.Id == UserID
                           && a.ACCESS_LEVEL == "A"
                           && a.STATUS_IND == "A"
                           select a).Any();
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return false;
            }
        }

        public T_PRT_ORG_USERS GetUserOrg(string UserID, string OrgID)
        {
            try
            {
                var xxx = (from a in ctx.T_PRT_ORG_USERS
                           where a.Id == UserID
                           && a.ORG_ID == OrgID
                           select a).FirstOrDefault();

                return xxx;
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }

        public int GetOrgUsersCount(string UserID)
        {
            try
            {
                return ctx.T_PRT_ORG_USERS.Where(x => x.Id == UserID).Count();
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return 0;
            }
        }



        //******************************ORG_USER_CLIENT***********************************************
        public T_PRT_ORG_USER_CLIENT GetT_PRT_ORG_USERS_CLIENT_ByID(int _OrgUserClientIDX)
        {
            try
            {
                return (from a in ctx.T_PRT_ORG_USER_CLIENT
                        where a.ORG_USER_CLIENT_IDX == _OrgUserClientIDX
                        select a).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }

        }

        public List<OrgUserClientDisplayType> GetT_PRT_ORG_USERS_CLIENT_ByOrgUserID(int _OrgUserIDX)
        {
            try
            {
                var xxx = (from a in ctx.T_PRT_ORG_USER_CLIENT
                           join b in ctx.T_PRT_ORG_USERS on a.ORG_USER_IDX equals b.ORG_USER_IDX
                           join c in ctx.Users on b.Id equals c.Id
                           where a.ORG_USER_IDX == _OrgUserIDX
                           orderby a.CLIENT_ID
                            select new OrgUserClientDisplayType
                            {
                                ORG_USER_CLIENT_IDX = a.ORG_USER_CLIENT_IDX,
                                ORG_USER_IDX = a.ORG_USER_IDX,
                                CLIENT_ID = a.CLIENT_ID,
                                ADMIN_IND = a.ADMIN_IND,
                                STATUS_IND = a.STATUS_IND,
                                ORG_ID = b.ORG_ID,
                                UserID = b.Id,
                                UserName = c.FIRST_NAME + " " + c.LAST_NAME
                            }).ToList();

                return xxx;
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }

        }

        public List<OrgUserClientDisplayType> GetT_PRT_ORG_USERS_CLIENT_ByUserID(string _UserIDX)
        {
            try
            {
                var xxx = (from a in ctx.T_PRT_ORG_USER_CLIENT
                           join b in ctx.T_PRT_ORG_USERS on a.ORG_USER_IDX equals b.ORG_USER_IDX
                           join c in ctx.T_PRT_ORG_CLIENT_ALIAS on new { a.CLIENT_ID, b.ORG_ID } equals new { c.CLIENT_ID, c.ORG_ID }
                           join d in ctx.Users on b.Id equals d.Id
                           where b.Id == _UserIDX
                           select new OrgUserClientDisplayType
                           {
                                ORG_USER_CLIENT_IDX = a.ORG_USER_CLIENT_IDX,
                                ORG_USER_IDX = a.ORG_USER_IDX,
                                CLIENT_ID = a.CLIENT_ID,
                                ADMIN_IND = a.ADMIN_IND,
                                STATUS_IND = a.STATUS_IND,
                                ORG_ID = b.ORG_ID,
                                UserID = b.Id,
                                ORG_CLIENT_ALIAS = c.ORG_CLIENT_ALIAS,
                               UserName = d.FIRST_NAME
                           }).ToList();

                return xxx;
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }

        public List<OrgUserClientDisplayType> GetT_PRT_ORG_USERS_CLIENT_AdminByUserID(string _UserIDX)
        {
            try
            {
                var xxx = (from a in ctx.T_PRT_ORG_USER_CLIENT
                           join b in ctx.T_PRT_ORG_USERS on a.ORG_USER_IDX equals b.ORG_USER_IDX
                           join c in ctx.T_PRT_ORGANIZATIONS on b.ORG_ID equals c.ORG_ID
                           where b.Id == _UserIDX
                           && a.ADMIN_IND == true
                           select new OrgUserClientDisplayType
                           {
                               ORG_USER_CLIENT_IDX = a.ORG_USER_CLIENT_IDX,
                               ORG_USER_IDX = a.ORG_USER_IDX,
                               CLIENT_ID = a.CLIENT_ID,
                               ADMIN_IND = a.ADMIN_IND,
                               STATUS_IND = a.STATUS_IND,
                               ORG_ID = b.ORG_ID,
                               UserID = b.Id,
                               ORG_CLIENT_ALIAS = c.ORG_NAME
                           }).ToList();

                return xxx;
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }

        public List<OrgUserClientDisplayType> GetT_PRT_ORG_USERS_CLIENT_ByOrgIDandClientID(string _orgID, string _clientID, bool AdminOnlyInd)
        {
            try
            {
                var xxx = (from a in ctx.T_PRT_ORG_USER_CLIENT
                           join b in ctx.T_PRT_ORG_USERS on a.ORG_USER_IDX equals b.ORG_USER_IDX
                           join d in ctx.Users on b.Id equals d.Id
                           where a.CLIENT_ID == _clientID
                           && b.ORG_ID == _orgID
                           && (AdminOnlyInd == true ? a.ADMIN_IND == true : true)
                           select new OrgUserClientDisplayType
                           {
                               ORG_USER_CLIENT_IDX = a.ORG_USER_CLIENT_IDX,
                               ORG_USER_IDX = a.ORG_USER_IDX,
                               CLIENT_ID = a.CLIENT_ID,
                               ADMIN_IND = a.ADMIN_IND,
                               STATUS_IND = a.STATUS_IND,
                               ORG_ID = b.ORG_ID,
                               UserID = b.Id,
                               UserName = d.FIRST_NAME + " " + d.LAST_NAME
                           }).ToList();

                return xxx;
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }

        //Retrieve Clients the User has access to (filter out currently PENDING or REJECTED)
        public IEnumerable<T_PRT_CLIENTS> GetT_PRT_ORG_USERS_CLIENT_DistinctClientByUserID(string UserID)
        {
            try
            {
                var xxx = (from a in ctx.T_PRT_ORG_USER_CLIENT
                           join b in ctx.T_PRT_ORG_USERS on a.ORG_USER_IDX equals b.ORG_USER_IDX
                           join c in ctx.T_PRT_CLIENTS on a.CLIENT_ID equals c.CLIENT_ID
                           where b.Id == UserID
                           && b.STATUS_IND == "A"
                           && a.STATUS_IND == "A"
                           orderby a.CLIENT_ID
                           select c).ToList().Distinct();

                return xxx;
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }

        public IEnumerable<SelectListItem> get_ddl_T_PRT_ORG_USERS_CLIENT_ByUserIDandClient(string UserIDX, string ClientID)
        {
            try
            {
                var xxx = (from a in ctx.T_PRT_ORG_USERS
                           join b in ctx.T_PRT_ORGANIZATIONS on a.ORG_ID equals b.ORG_ID
                           join c in ctx.T_PRT_ORG_USER_CLIENT on a.ORG_USER_IDX equals c.ORG_USER_IDX
                           where a.Id == UserIDX
                           && c.CLIENT_ID == ClientID
                           orderby b.ORG_NAME
                           select new SelectListItem
                           {
                               Value = a.ORG_ID.ToString(),
                               Text = b.ORG_NAME
                           }).ToList();

                return xxx;
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
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
                     where c.ORG_USER_CLIENT_IDX == oRG_USER_CLIENT_IDX
                        select c).FirstOrDefault();

                //now try to grab from user and org id
                if (e == null)
                {
                    e = (from c in ctx.T_PRT_ORG_USER_CLIENT
                         where c.CLIENT_ID.ToUpper() == cLIENT_ID.ToUpper()
                            && c.ORG_USER_IDX == oRG_USER_IDX
                            select c).FirstOrDefault();
                }

                if (e == null)
                {
                    insInd = true;
                    e = new T_PRT_ORG_USER_CLIENT
                    {
                        CREATE_DT = System.DateTime.Now,
                        CREATE_USER_ID = cREATE_USER
                    };
                }
                else
                {
                    e.MODIFY_DT = System.DateTime.Now;
                    e.MODIFY_USER_ID = cREATE_USER;
                }

                if (oRG_USER_IDX != null) e.ORG_USER_IDX = oRG_USER_IDX ?? 0;
                if (cLIENT_ID != null) e.CLIENT_ID = cLIENT_ID;
                if (aDMIN_IND != null) e.ADMIN_IND = aDMIN_IND ?? false;
                if (sTATUS_IND != null) e.STATUS_IND = sTATUS_IND;

                if (insInd)
                    ctx.T_PRT_ORG_USER_CLIENT.Add(e);

                ctx.SaveChanges();
                return e.ORG_USER_IDX;
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex); 
                return 0;
            }

        }

        public int DeleteT_PRT_ORG_USER_CLIENT(int id)
        {
            try
            {
                T_PRT_ORG_USER_CLIENT rec = new T_PRT_ORG_USER_CLIENT { ORG_USER_CLIENT_IDX = id };
                ctx.Entry(rec).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                ctx.SaveChanges();

                return 1;
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return 0;
            }

        }

        /// <summary>
        /// Is user an organization client admin for any organization clients
        /// </summary>
        public bool IsUserAnOrgClientAdmin(string UserID)
        {
            try
            {
                var xxx = (from a in ctx.T_PRT_ORG_USER_CLIENT
                           join b in ctx.T_PRT_ORG_USERS on a.ORG_USER_IDX equals b.ORG_USER_IDX
                           where a.STATUS_IND == "A"
                           && b.Id == UserID
                           && a.ADMIN_IND == true
                           select a).Count();

                return xxx > 0;
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return false;
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
                _log.LogEFException(ex);
                return null;
            }
        }

        public IEnumerable<ApplicationUser> GetT_PRT_USERS_BelongingToRole(string RoleID)
        {
            try
            {
                return (from u in ctx.Users
                        join ur in ctx.UserRoles on u.Id equals ur.UserId
                        where ur.RoleId == RoleID
                        select u).ToList();
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }

        //*****************USERS**********************************
        public int UpdateT_PRT_USERS_LoginDate(ApplicationUser user)
        {
            try
            {
                user.LAST_LOGIN_DT = System.DateTime.Now;
                ctx.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return 0;
            }
        }

        public int UpdateT_PRT_USERS_PasswordEncrypt(ApplicationUser user, string Password)
        {
            try
            {
                user.PasswordEncrypt = Utils.Encrypt(Password);
                ctx.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return 0;
            }
        }
        
        public int UpdateT_PRT_USERS_WordPressUserId(ApplicationUser user, int WordPressUserId)
        {
            try
            {
                user.WordPressUserId = WordPressUserId;
                ctx.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return 0;
            }
        }
        //*****************SYS_LOG**********************************
        public List<T_PRT_SYS_LOG> GetT_PRT_SYS_LOG()
        {
            try
            {
                return (from a in ctx.T_PRT_SYS_LOG
                        orderby a.LOG_DT descending
                        select a).Take(1000).ToList();
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }

        //*****************SYS_EMAIL_LOG**********************************
        public List<T_PRT_SYS_EMAIL_LOG> GetT_PRT_SYS_EMAIL_LOG()
        {
            try
            {
                return (from a in ctx.T_PRT_SYS_EMAIL_LOG
                        orderby a.LOG_DT descending
                        select a).Take(1000).ToList();
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }


        //*****************SITES *************************************
        public Guid? InsertUpdateT_PRT_SITES(Guid? sITE_IDX, string oRG_ID, string sITE_NAME, string ePA_ID, decimal? lATITUDE, decimal? lONGITUDE, string sITE_ADDRESS, string UserIDX, string lAND_STATUS,
            string tWP, string rANGE, int? sECTION, string cOUNTY)
        {
            try
            {
                Boolean insInd = false;

                T_PRT_SITES e = (from c in ctx.T_PRT_SITES
                                 where c.SITE_IDX == sITE_IDX
                                 select c).FirstOrDefault();

                //insert case
                if (e == null)
                {
                    insInd = true;
                    e = new T_PRT_SITES();
                    e.SITE_IDX = Guid.NewGuid();
                    e.CREATE_DT = System.DateTime.UtcNow;
                    e.CREATE_USER_ID = UserIDX;
                }
                else
                {
                    e.MODIFY_DT = System.DateTime.UtcNow;
                    e.MODIFY_USER_ID = UserIDX;
                }

                if (oRG_ID != null) e.ORG_ID = oRG_ID;
                if (sITE_NAME != null) e.SITE_NAME = sITE_NAME;
                if (ePA_ID != null) e.EPA_ID = ePA_ID;
                if (lATITUDE != null) e.LATITUDE = lATITUDE;
                if (lONGITUDE != null) e.LONGITUDE = lONGITUDE;
                if (sITE_ADDRESS != null) e.SITE_ADDRESS = sITE_ADDRESS;
                if (lAND_STATUS != null) e.LAND_STATUS = lAND_STATUS;
                if (tWP != null) e.TWP = tWP;
                if (rANGE != null) e.RANGE = rANGE;
                if (sECTION != null) e.SECTION = sECTION;
                if (cOUNTY != null) e.COUNTY = cOUNTY;

                if (insInd)
                    ctx.T_PRT_SITES.Add(e);

                ctx.SaveChanges();
                return e.SITE_IDX;
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }

        public T_PRT_SITES GetT_PRT_SITES_BySITEIDX(Guid Siteidx)
        {
            try
            {
                return (from a in ctx.T_PRT_SITES
                        where a.SITE_IDX == Siteidx
                        select a).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }

        public int DeleteT_PRT_SITES(Guid sITE_IDX)
        {
            try
            {
                T_PRT_SITES tps = new T_PRT_SITES { SITE_IDX = sITE_IDX };
                ctx.Entry(tps).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                ctx.SaveChanges();

                return 1;
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return 0;
            }
        }

        public IEnumerable<SelectListItem> get_ddl_T_PRT_LAND_STATUS()
        {
            List<SelectListItem> ddl_LandStatus = new List<SelectListItem>();

            ddl_LandStatus.Add(new SelectListItem() { Value = "Private", Text = "Private" });
            ddl_LandStatus.Add(new SelectListItem() { Value = "Trust", Text = "Trust" });
            ddl_LandStatus.Add(new SelectListItem() { Value = "Tribal Trust", Text = "Tribal Trust" });
            ddl_LandStatus.Add(new SelectListItem() { Value = "Allotted", Text = "Allotted" });
            ddl_LandStatus.Add(new SelectListItem() { Value = "Fee", Text = "Fee" });

            return ddl_LandStatus;
        }

        public List<SelectListItem> getT_PRT_SITES_UniqueCounties()
        {
            try
            {
                return (from a in ctx.T_PRT_SITES
                        orderby a.COUNTY
                        where a.COUNTY != null
                        select new SelectListItem
                        {
                            Value = a.COUNTY,
                            Text = a.COUNTY
                        }).Distinct().ToList();
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }


        //*******************************DOCUMENTS *********************************************
        public Guid? InsertUpdateT_PRT_DOCUMENTS(Guid? dOC_IDX, string oRG_ID, byte[] dOC_CONTENT, string dOC_NAME, string dOC_TYPE, string dOC_FILE_TYPE, int? dOC_SIZE, string dOC_COMMENT,
            string dOC_AUTHOR, string sHARE_TYPE, string dOC_STATUS_TYPE, string UserID)
        {

            try
            {
                Boolean insInd = false;

                T_PRT_DOCUMENTS e = (from c in ctx.T_PRT_DOCUMENTS
                                    where c.DOC_IDX == dOC_IDX
                                    select c).FirstOrDefault();

                //insert case
                if (e == null)
                {
                    insInd = true;
                    e = new T_PRT_DOCUMENTS();
                    e.DOC_IDX = Guid.NewGuid();
                    e.CREATE_DT = System.DateTime.UtcNow;
                    e.CREATE_USER_ID = UserID;
                }
                else
                {
                    e.MODIFY_DT = System.DateTime.UtcNow;
                    e.MODIFY_USER_ID = UserID;
                }

                if (dOC_CONTENT != null) e.DOC_CONTENT = dOC_CONTENT;
                if (dOC_NAME != null) e.DOC_NAME = dOC_NAME;
                if (dOC_TYPE != null) e.DOC_TYPE = dOC_TYPE;
                if (dOC_FILE_TYPE != null) e.DOC_FILE_TYPE = dOC_FILE_TYPE;
                if (dOC_SIZE != null) e.DOC_SIZE = dOC_SIZE;
                if (dOC_COMMENT != null) e.DOC_COMMENT = dOC_COMMENT;
                if (dOC_AUTHOR != null) e.DOC_AUTHOR = dOC_AUTHOR;
                if (oRG_ID != null) e.ORG_ID = oRG_ID;
                if (dOC_STATUS_TYPE != null) e.DOC_STATUS_TYPE = dOC_STATUS_TYPE;
                if (sHARE_TYPE != null) e.SHARE_TYPE = sHARE_TYPE;
                
                if (insInd)
                    ctx.T_PRT_DOCUMENTS.Add(e);

                ctx.SaveChanges();
                return e.DOC_IDX;
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }

        }

        public T_PRT_DOCUMENTS GetT_PRT_DOCUMENTS_ByID(Guid DocIDX)
        {
            try
            {
                return (from a in ctx.T_PRT_DOCUMENTS
                        where a.DOC_IDX == DocIDX
                        select a).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }

        }

        public int DeleteT_PRT_DOCUMENTS(Guid DocIDX)
        {
            try
            {
                ctx.Database.ExecuteSqlCommand("DELETE FROM T_OD_ASSESSMENT_DOCS where DOC_IDX = {0}", DocIDX);
                ctx.Database.ExecuteSqlCommand("DELETE FROM T_OD_CLEANUP_DOCS where DOC_IDX = {0}", DocIDX);
                ctx.Database.ExecuteSqlCommand("DELETE FROM T_PRT_DOCUMENTS where DOC_IDX = {0}", DocIDX);

                return 1;
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return 0;
            }

        }

        //*******************************EMAIL TEMPLATE *********************************************
        public List<T_PRT_REF_EMAIL_TEMPLATE> GetT_PRT_REF_EMAIL_TEMPLATE()
        {
            try
            {
                return (from a in ctx.T_PRT_REF_EMAIL_TEMPLATE
                        select a).ToList();
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }

        public T_PRT_REF_EMAIL_TEMPLATE GetT_PRT_REF_EMAIL_TEMPLATE_ByID(int id)
        {
            try
            {
                return (from a in ctx.T_PRT_REF_EMAIL_TEMPLATE
                        where a.EMAIL_TEMPLATE_ID == id
                        select a).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }

        public T_PRT_REF_EMAIL_TEMPLATE GetT_PRT_REF_EMAIL_TEMPLATE_ByName(string name)
        {
            try
            {
                return (from a in ctx.T_PRT_REF_EMAIL_TEMPLATE
                        where a.EMAIL_TEMPLATE_NAME == name
                        select a).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }

        public List<SelectListItem> get_ddl_T_PRT_REF_EMAIL_TEMPLATE()
        {
            try
            {
                return (from a in ctx.T_PRT_REF_EMAIL_TEMPLATE
                        orderby a.EMAIL_TEMPLATE_NAME
                        select new SelectListItem
                        {
                            Value = a.EMAIL_TEMPLATE_ID.ToString(),
                            Text = a.EMAIL_TEMPLATE_NAME
                        }).ToList();
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }

        public int InsertUpdateT_PRT_REF_EMAIL_TEMPLATE(int? eMAIL_TEMPLATE_ID, string sUBJ, string mSG, string UserID)
        {

            try
            {
                Boolean insInd = false;

                T_PRT_REF_EMAIL_TEMPLATE e = (from c in ctx.T_PRT_REF_EMAIL_TEMPLATE
                                              where c.EMAIL_TEMPLATE_ID == eMAIL_TEMPLATE_ID
                                              select c).FirstOrDefault();

                //insert case
                if (e == null)
                {
                    insInd = true;
                    e = new T_PRT_REF_EMAIL_TEMPLATE();
                }

                e.MODIFY_DT = System.DateTime.UtcNow;
                e.MODIFY_USER_ID = UserID;

                if (sUBJ != null) e.SUBJ = sUBJ;
                if (mSG != null) e.MSG = mSG;

                if (insInd)
                    ctx.T_PRT_REF_EMAIL_TEMPLATE.Add(e);

                ctx.SaveChanges();
                return e.EMAIL_TEMPLATE_ID;
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return 0;
            }

        }


        //**************************** T_PRT_REF_UNITS ***********************************************
        public T_PRT_REF_UNITS get_T_PRT_REF_UNITS_ByID(Guid? id)
        {
            try
            {
                return (from a in ctx.T_PRT_REF_UNITS
                        where a.UNIT_MSR_IDX == id
                        select a).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }



        //**************************** DATA APIS ***********************************************
        public List<SelectListItem> get_ddl_APIS()
        {
            List<SelectListItem> _list = new List<SelectListItem>();
            _list.Add(new SelectListItem() { Value = "OD_Sites", Text = "Open Dump Sites" });
            _list.Add(new SelectListItem() { Value = "OD_Assess", Text = "Open Dump Assessments" });
            return _list;
        }

        public List<SelectListItem> get_ddl_APIformat()
        {
            List<SelectListItem> _list = new List<SelectListItem>();
            _list.Add(new SelectListItem() { Value = "J", Text = "Json" });
            _list.Add(new SelectListItem() { Value = "X", Text = "XML" });
            return _list;
        }

    }
}
