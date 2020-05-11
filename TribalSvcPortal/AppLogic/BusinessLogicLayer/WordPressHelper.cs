using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TribalSvcPortal.AppLogic.DataAccessLayer;
using TribalSvcPortal.Data.Models;
using WordPressPCL;
using WordPressPCL.Models;
using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;

namespace TribalSvcPortal.AppLogic.BusinessLogicLayer
{
    public class WordPressHelper
    {
        private static UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IDbPortal _DbPortal;
        private readonly IConfiguration _config;
        private WordPressClient _clientAuth;
        private readonly Ilog _log;

        string WordPressUri, UserName, Password = "";
        public WordPressHelper(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IDbPortal DbPortal,
            IConfiguration config,
            Ilog log)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _DbPortal = DbPortal;
            _config = config ?? throw new System.ArgumentNullException(nameof(config));
            _log = log ?? throw new ArgumentNullException(nameof(log));

            WordPressUri = _config.GetSection("WordPress").GetValue<string>("WordPressUri");
            UserName = _config.GetSection("WordPress").GetValue<string>("UserName");
            Password = _config.GetSection("WordPress").GetValue<string>("Password");
        }
        public async Task<int> SetupWordPressAccess(string uidx, string org_id, string AccessLevel, string StatusInd)
        {
            _log.InsertT_PRT_SYS_LOG("Info", "SetupWordPressAccess called.");
            int actResult = 0;
            try
            {
                ApplicationUser user = await _userManager.FindByIdAsync(uidx);
                if (user != null)
                {
                    _log.InsertT_PRT_SYS_LOG("Info", "we have a vlaid user.");
                    SetWordPressUri(org_id);
                    _log.InsertT_PRT_SYS_LOG("Info", WordPressUri);

                    WordPressClient wordPressClient = await GetAuthenticatedWordPressClient(WordPressUri, UserName, Password);
                    var isTokenValid = await wordPressClient.IsValidJWToken();
                    if (isTokenValid)
                    {
                        _log.InsertT_PRT_SYS_LOG("Info", "Token is valid.");
                        if (AccessLevel == "A" && StatusInd == "A")
                        {
                            _log.InsertT_PRT_SYS_LOG("Info", "AccessLevel/Status is A");
                            int wpuid = 0;
                            Int32.TryParse(user.WordPressUserId.ToString(), out wpuid);
                            if (wpuid > 0)
                            {
                                _log.InsertT_PRT_SYS_LOG("Info", "User already exist, update as administrator.");
                                await UpdateWordPressUser(user, wordPressClient, wpuid, "administrator");
                            }
                            else
                            {
                                _log.InsertT_PRT_SYS_LOG("Info", "User does not exis, add new user as administrator.");
                                User createdUser = await CreateWordPressUser(user, wordPressClient);
                                _DbPortal.UpdateT_PRT_USERS_WordPressUserId(user, createdUser.Id);
                            }

                            actResult = 1;
                        }
                        else
                        {
                            _log.InsertT_PRT_SYS_LOG("Info", "AccessLevel/Status is NOT A");
                            int wuid = 0;
                            if (Int32.TryParse(user.WordPressUserId.ToString(), out wuid) && wuid > 0)
                            {
                                _log.InsertT_PRT_SYS_LOG("Info", "Set user as inactive");
                                await UpdateWordPressUser(user, wordPressClient, wuid, "inactive");
                            }
                            else
                            {
                                _log.InsertT_PRT_SYS_LOG("Error", "Issue with wordpress user id.");
                            }
                        }
                    }
                    else
                    {
                        _log.InsertT_PRT_SYS_LOG("Error", "JWT token is not valid.");
                    }
                }
                else
                {
                    _log.InsertT_PRT_SYS_LOG("Error", "user is null.");
                }
            }
            catch (Exception ex)
            {
                _log.InsertT_PRT_SYS_LOG("ERROR", ex.Message + " : " + ex.StackTrace);
                //Log errors
            }

            return actResult;

            string GetWordPressUserName(ApplicationUser user)
            {
                string uname = user.UserName;
                if (string.IsNullOrEmpty(uname) || uname.Length <= 4)
                {
                    uname = GenerateName(59).ToLower();
                }
                else
                {
                    if (uname.Length > 60) uname = uname.Take(59).ToString();
                    Regex rgx = new Regex("[^a-z0-9 -]");
                    uname = rgx.Replace(uname, "");
                }
                return uname;
            }

            async Task<bool> UpdateWordPressUser(ApplicationUser user, WordPressClient wordPressClient, int wpuid, string role)
            {
                try
                {
                    string userstring = string.Format("Id:{0},FirstName:{1},LastName{2},Email:{3},UserName:{4},Password:{5}", wpuid, user.FIRST_NAME, user.LAST_NAME, user.Email, GetWordPressUserName(user), Utils.Decrypt(user.PasswordEncrypt));
                    _log.InsertT_PRT_SYS_LOG("Info", "Inside UpdateWordPressUser");
                    _log.InsertT_PRT_SYS_LOG("Info", userstring);
                    var updateUser = new User
                    {
                        Id = wpuid,
                        FirstName = user.FIRST_NAME,
                        LastName = user.LAST_NAME,
                        Email = user.Email,
                        //UserName = user.UserName,
                        //In wordpress email can't be stored as username
                        //also, installed plugin to use email as login in wordpress
                        //hence, UserName is not important here
                        //or validate username for wordpress and set it here
                        UserName = GetWordPressUserName(user),
                        Password = Utils.Decrypt(user.PasswordEncrypt),
                        Roles = new List<string> { role }
                    };
                    var updatedUser = await wordPressClient.Users.Update(updateUser);
                    _log.InsertT_PRT_SYS_LOG("Info", "User updation successful.");
                    return true;

                }
                catch (Exception ex)
                {
                    _log.InsertT_PRT_SYS_LOG("Error", "UpdateWordPressUser:" + ex.Message + ex.StackTrace);
                    return false;
                }
            }

            async Task<User> CreateWordPressUser(ApplicationUser user, WordPressClient wordPressClient)
            {
                try
                {
                    _log.InsertT_PRT_SYS_LOG("Info", "Inside CreateWordPressUser");
                    string userstring = string.Format("FirstName:{0},LastName{1},Email:{2},UserName:{3},Password:{4}", user.FIRST_NAME, user.LAST_NAME, user.Email, GetWordPressUserName(user), Utils.Decrypt(user.PasswordEncrypt));
                    _log.InsertT_PRT_SYS_LOG("Info", userstring);
                    var newUser = new User
                    {
                        FirstName = user.FIRST_NAME,
                        LastName = user.LAST_NAME,
                        Email = user.Email,
                        //UserName = user.UserName,
                        //In wordpress email can't be stored as username
                        //also, installed plugin to use email as login in wordpress
                        //hence, UserName is not important here
                        //or validate username for wordpress and set it here
                        UserName = GetWordPressUserName(user),
                        Password = Utils.Decrypt(user.PasswordEncrypt),
                        Roles = new List<string> { "administrator" }
                    };
                     
                    var createdUser = await wordPressClient.Users.Create(newUser);
                    _log.InsertT_PRT_SYS_LOG("Info", "User creation successful.");
                    return createdUser;
                }
                catch (Exception ex)
                {
                    _log.InsertT_PRT_SYS_LOG("Error", "CreateWordPressUser:" + ex.Message + ex.StackTrace);
                    return null;
                }
            }
        }

        private void SetWordPressUri(string org_id)
        {
            if (org_id.Equals("MCNCREEK"))
            {
                WordPressUri = string.Format("{0}/mcn/wp-json", WordPressUri);
            }
            else if (org_id.Equals("KICKAPOO"))
            {
                WordPressUri = string.Format("{0}/kickapootribe/wp-json", WordPressUri);
            }
            else if (org_id.Equals("SFNOES"))
            {
                WordPressUri = string.Format("{0}/sfnoes/wp-json", WordPressUri);
            }
            else if (org_id.Equals("ABSHAWNEE"))
            {
                WordPressUri = string.Format("{0}/abshawnee/wp-json", WordPressUri);
            }
            else
            {
                _log.InsertT_PRT_SYS_LOG("Error", "Organization ID Issue:" + org_id);
            }
        }

        private async Task<WordPressClient> GetAuthenticatedWordPressClient(String WordPressUri,
                                                                                  String UserName,
                                                                                  String Password,
                                                                                  AuthMethod method = AuthMethod.JWT)
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
        public static async Task<WordPressClient> GetCustomWordPressClient(String WordPressUri)
        {
            //https://github.com/wp-net/WordPressPCL/wiki/CustomRequest
            //https://gitmemory.com/issue/wp-net/WordPressPCL/186/569330352
            WordPressClient client = new WordPressClient(WordPressUri, @"mcn/wp-json/wl/v1/");
            return client;
        }
        private string GenerateName(int len)
        {
            Random r = new Random();
            string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "l", "n", "p", "q", "r", "s", "sh", "zh", "t", "v", "w", "x" };
            string[] vowels = { "a", "e", "i", "o", "u", "ae", "y" };
            string Name = "";
            Name += consonants[r.Next(consonants.Length)].ToUpper();
            Name += vowels[r.Next(vowels.Length)];
            int b = 2; //b tells how many times a new letter has been added. It's 2 right now because the first two letters are already in the name.
            while (b < len)
            {
                Name += consonants[r.Next(consonants.Length)];
                b++;
                Name += vowels[r.Next(vowels.Length)];
                b++;
            }

            return Name;


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
                string msg = ex.Message;
                return null;
            }
           
        }
        public static void SetUserManager(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

    }
}
