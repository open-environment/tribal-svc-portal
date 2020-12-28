using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TribalSvcPortal.AppLogic.DataAccessLayer;
using TribalSvcPortal.Data.Models;
using TribalSvcPortal.Services;
using WordPressPCL;
using WordPressPCL.Models;

namespace TribalSvcPortal.AppLogic.BusinessLogicLayer
{
    public class WordPressHelper
    {
        private static UserManager<ApplicationUser> _userManager;
        private readonly IDbPortal _DbPortal;
        private WordPressClient _clientAuth;
        private readonly Ilog _log;
        private readonly IEmailSender _emailSender;

        string WordPressBaseUri, WordPressUri, UserName, Password, FromEmail = "";
        public WordPressHelper(UserManager<ApplicationUser> userManager,
            IDbPortal DbPortal,
            Ilog log,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _DbPortal = DbPortal;
            _log = log ?? throw new ArgumentNullException(nameof(log));
            _emailSender = emailSender;

            WordPressBaseUri = _DbPortal.GetT_PRT_APP_SETTING("WORDPRESS_URI");
            UserName = _DbPortal.GetT_PRT_APP_SETTING("WORDPRESS_USERNAME");
            Password = Utils.Decrypt(_DbPortal.GetT_PRT_APP_SETTING("WORDPRESS_PWD"));
        }

        public async Task<int> SetupWordPressAccess(string uidx, string orgId, string accessLevel, string statusInd)
        {
            _log.InsertT_PRT_SYS_LOG("Info", "SetupWordPressAccess called.");
            int actResult = 1;
            try
            {
                ApplicationUser user = await _userManager.FindByIdAsync(uidx);
                if (user != null)
                {
                    //_log.InsertT_PRT_SYS_LOG("Info", "we have a valid user.");
                    int orgUserCount = _DbPortal.GetOrgUsersCount(uidx);
                    SetWordPressUri(orgId);
                    //_log.InsertT_PRT_SYS_LOG("Info", WordPressUri);

                    WordPressClient wordPressClient = await GetAuthenticatedWordPressClient(WordPressUri, UserName, Password);
                    var isTokenValid = await wordPressClient.IsValidJWToken();
                    if (isTokenValid)
                    {
                        //_log.InsertT_PRT_SYS_LOG("Info", "Token is valid.");
                        if (accessLevel == "A" && statusInd == "A")
                        {
                            //_log.InsertT_PRT_SYS_LOG("Info", "AccessLevel/Status is A");
                            int.TryParse(user.WordPressUserId.ToString(), out var wpuid);
                            if (orgUserCount > 0)
                            {
                                T_PRT_ORG_USERS orgUser = _DbPortal.GetUserOrg(uidx, orgId);
                                if(orgUser == null)
                                {
                                    //This situation is unlikely to occur, since we do an upsert before reaching here
                                    actResult = 0;
                                    _log.InsertT_PRT_SYS_LOG("ERROR", "Org-User not found.");
                                }
                                else
                                {
                                    if(wpuid > 0)
                                    {
                                        //_log.InsertT_PRT_SYS_LOG("Info", "User already exists, update as administrator.");
                                        await UpdateWordPressUser(user, wordPressClient, wpuid, "administrator");
                                        
                                        AddRemoveUserSite(wpuid, orgId, 1);
                                    }
                                    else
                                    {
                                        //_log.InsertT_PRT_SYS_LOG("Info", "User does not exist, add new user as administrator.");
                                        User createdUser = await CreateWordPressUser(user, wordPressClient, orgId);
                                        if(createdUser != null)
                                        {
                                            _DbPortal.UpdateT_PRT_USERS_WordPressUserId(user, createdUser.Id);
                                        }
                                        else
                                        {
                                            actResult = 0;
                                            _log.InsertT_PRT_SYS_LOG("ERROR", "New user could not be added.");
                                        }
                                        
                                    }
                                }
                            }
                            else
                            {
                                if(wpuid > 0)
                                {
                                    //_log.InsertT_PRT_SYS_LOG("Info", "User already exist, update as administrator.");
                                    await UpdateWordPressUser(user, wordPressClient, wpuid, "administrator");
                                    AddRemoveUserSite(wpuid, orgId, 1);
                                }
                                else
                                {
                                    //_log.InsertT_PRT_SYS_LOG("Info", "User does not exist, add new user as administrator.");
                                    User createdUser = await CreateWordPressUser(user, wordPressClient, orgId);
                                    _DbPortal.UpdateT_PRT_USERS_WordPressUserId(user, createdUser.Id);
                                }
                            }

                            
                        }
                        else
                        {
                            //_log.InsertT_PRT_SYS_LOG("Info", "AccessLevel/Status is NOT A");

                            if (Int32.TryParse(user.WordPressUserId.ToString(), out var wuid) && wuid > 0)
                            {
                                if(orgUserCount > 1)
                                    AddRemoveUserSite(wuid, orgId, 0);
                                else
                                    await UpdateWordPressUser(user, wordPressClient, wuid, "inactive");
                            }
                            else
                            {
                                actResult = 0;
                                _log.InsertT_PRT_SYS_LOG("ERROR", "Issue with WordPress user id.");
                            }
                        }
                    }
                    else
                    {
                        actResult = 0;
                        _log.InsertT_PRT_SYS_LOG("ERROR", "JWT token is not valid.");
                    }
                }
                else
                {
                    actResult = 0;
                    _log.InsertT_PRT_SYS_LOG("ERROR", "user is null.");
                }
            }
            catch (Exception ex)
            {
                actResult = 0;
                _log.InsertT_PRT_SYS_LOG("ERROR", ex.Message + " : " + ex.StackTrace);
                //Log errors
            }

            return actResult;


        }

        public async Task<bool> UpdateWordPressUser(ApplicationUser user, WordPressClient wordPressClient, int wpuid, string role)
        {
            try
            {
                //string userstring = string.Format("Id:{0},FirstName:{1},LastName{2},Email:{3},UserName:{4},Password:{5}", wpuid, user.FIRST_NAME, user.LAST_NAME, user.Email, GetWordPressUserName(user), Utils.Decrypt(user.PasswordEncrypt));
                //_log.InsertT_PRT_SYS_LOG("Info", "Inside UpdateWordPressUser");
                //_log.InsertT_PRT_SYS_LOG("Info", userstring);
                var updateUser = new User
                {
                    Id = wpuid,
                    FirstName = user.FIRST_NAME,
                    LastName = user.LAST_NAME,
                    Email = user.Email,
                    //UserName = user.UserName,
                    //In wordpress email can't be stored as username
                    //we have used plugin to use email as login in wordpress
                    //hence, Email is stored as user name
                    //or if you want username, then validate for wordpress with following method
                    //UserName = GetWordPressUserName(user),
                    UserName = user.Email,
                    Password = Utils.Decrypt(user.PasswordEncrypt),
                    Roles = new List<string> { role }
                };
                var updatedUser = await wordPressClient.Users.Update(updateUser);
                //_log.InsertT_PRT_SYS_LOG("Info", "User update successful.");
                return true;

            }
            catch (Exception ex)
            {
                _log.InsertT_PRT_SYS_LOG("ERROR", "UpdateWordPressUser:" + ex.Message + ex.StackTrace);
                return false;
            }
        }

        async Task<User> CreateWordPressUser(ApplicationUser user, WordPressClient wordPressClient, string org_id)
        {
            try
            {
                //_log.InsertT_PRT_SYS_LOG("Info", "Inside CreateWordPressUser");
                //string userstring = string.Format("FirstName:{0},LastName{1},Email:{2},UserName:{3},Password:{4}", user.FIRST_NAME, user.LAST_NAME, user.Email, GetWordPressUserName(user), Utils.Decrypt(user.PasswordEncrypt));
                //_log.InsertT_PRT_SYS_LOG("Info", userstring);
                var newUser = new User
                {
                    FirstName = user.FIRST_NAME,
                    LastName = user.LAST_NAME,
                    Email = user.Email,
                    //UserName = user.UserName,
                    //In WordPress email can't be stored as username
                    //we have used plugin to use email as login in WordPress
                    //hence, Email is stored as user name
                    //or if you want username, then validate for WordPress with following method
                    //UserName = GetWordPressUserName(user),
                    UserName = user.Email,
                    Password = Utils.Decrypt(user.PasswordEncrypt),
                    Roles = new List<string> { "administrator" }
                };

                var createdUser = await wordPressClient.Users.Create(newUser);
                //_log.InsertT_PRT_SYS_LOG("Info", "User creation successful.");
                string link;
                if (org_id.Equals("MCNCREEK"))
                    link = $"{WordPressBaseUri}/mcn/wp-admin";
                else if (org_id.Equals("KICKAPOO"))
                    link = $"{WordPressBaseUri}/kickapootribe/wp-admin";
                else if (org_id.Equals("SFNOES"))
                    link = $"{WordPressBaseUri}/sfnoes/wp-admin";
                else if (org_id.Equals("ABSHAWNEE"))
                    link = $"{WordPressBaseUri}/abshawnee/wp-admin";
                else
                    link = WordPressBaseUri;

                List<emailParam> emailParams = new List<emailParam>
                {
                    new emailParam
                    {
                         PARAM_NAME="Tribe",
                         PARAM_VAL=org_id
                    },
                    new emailParam
                    {
                         PARAM_NAME="link",
                          PARAM_VAL=link
                    },
                    new emailParam
                    {
                         PARAM_NAME="recipient",
                         PARAM_VAL=user.FIRST_NAME + " " + user.LAST_NAME
                    }
                };
                _emailSender.SendEmail(FromEmail, user.Email, null, null, null, null, "WP_ADMIN", emailParams);
                return createdUser;
            }
            catch (Exception ex)
            {
                _log.InsertT_PRT_SYS_LOG("ERROR", "CreateWordPressUser:" + ex.Message + ex.StackTrace);
                return null;
            }
        }

        public bool AddRemoveUserSite(int wpuid, string orgId, int syncunsync)
        {
            bool actResult = false;
            try
            {
                //_log.InsertT_PRT_SYS_LOG("Info", "Inside AddRemoveUserSite");
                string endPoint = $"{WordPressBaseUri}/wp-json"; 
                endPoint = $"{endPoint}/wl/v1/manageusersite";
                var client = new RestClient(endPoint);
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                request.AddQueryParameter("user", wpuid.ToString());
                request.AddQueryParameter("org_id", orgId.ToUpper());
                request.AddQueryParameter("user_sync_unsync", syncunsync.ToString()); //1 = Add user to site
                IRestResponse response = client.Execute(request);
                string res = response.Content;
                if (res.Contains("true"))
                {
                    //_log.InsertT_PRT_SYS_LOG("Info", "Addition/Removal of site was successful.");
                    actResult = true;
                } else
                {
                    _log.InsertT_PRT_SYS_LOG("ERROR", res);
                }
            }
            catch (Exception ex)
            {
                _log.InsertT_PRT_SYS_LOG("ERROR", "AddUserToSite:" + ex.Message + ex.StackTrace);
                actResult = false;
            }
            
            return actResult;
        }

        public string SetWordPressUri(string orgId)
        {
            if (orgId.Equals("MCNCREEK"))
                WordPressUri = $"{WordPressBaseUri}/mcn/wp-json";
            else if (orgId.Equals("KICKAPOO"))
                WordPressUri = $"{WordPressBaseUri}/kickapootribe/wp-json";
            else if (orgId.Equals("SFNOES"))
                WordPressUri = $"{WordPressBaseUri}/sfnoes/wp-json";
            else if (orgId.Equals("ABSHAWNEE"))
                WordPressUri = $"{WordPressBaseUri}/abshawnee/wp-json";
            else
                _log.InsertT_PRT_SYS_LOG("Error", "Organization ID Issue:" + orgId);

            return WordPressUri;
        }
        
        public async Task<WordPressClient> GetAuthenticatedWordPressClient(string orgId)
        {
            SetWordPressUri(orgId);
            return await GetAuthenticatedWordPressClient(WordPressUri, UserName, Password);
        }
        
        private async Task<WordPressClient> GetAuthenticatedWordPressClient(string WordPressUri, string UserName, string Password, AuthMethod method = AuthMethod.JWT)
        {
            if (_clientAuth == null)
            {
                _clientAuth = new WordPressClient(WordPressUri)
                {
                    AuthMethod = AuthMethod.JWT
                };
                await _clientAuth.RequestJWToken(UserName, Password);
            }

            return _clientAuth;
        }
     
        public async static Task<ApplicationUser> GetApplicationUser(string id)
        {
            try
            {
                ApplicationUser user = await _userManager.FindByIdAsync(id);
                return user;
            }
            catch (Exception ex)
            {
                return null;
            }
           
        }
        
        public static void SetUserManager(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

    }
}
