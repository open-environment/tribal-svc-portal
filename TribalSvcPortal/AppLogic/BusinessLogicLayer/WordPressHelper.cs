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

namespace TribalSvcPortal.AppLogic.BusinessLogicLayer
{
    public class WordPressHelper
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IDbPortal _DbPortal;
        private readonly IConfiguration _config;
        private WordPressClient _clientAuth;

        string WordPressUri, UserName, Password = "";
        public WordPressHelper(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IDbPortal DbPortal,
            IConfiguration config)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _DbPortal = DbPortal;
            _config = config ?? throw new System.ArgumentNullException(nameof(config));

            WordPressUri = _config.GetSection("WordPress").GetValue<string>("WordPressUri");
            UserName = _config.GetSection("WordPress").GetValue<string>("UserName");
            Password = _config.GetSection("WordPress").GetValue<string>("Password");
        }
        public async Task<int> SetupWordPressAccess(string uidx, string org_id, string AccessLevel, string StatusInd)
        {
            int actResult = 0;
            try
            {
                ApplicationUser user = await _userManager.FindByIdAsync(uidx);
                if (user != null)
                {
                    if (org_id.Equals("MCNCREEK"))
                    {
                        WordPressUri = string.Format("{0}/mcn/wp-json", WordPressUri);
                    }else if (org_id.Equals("KICKAPOO"))
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

                    WordPressClient wordPressClient = await GetAuthenticatedWordPressClient(WordPressUri, UserName, Password);
                    var isTokenValid = await wordPressClient.IsValidJWToken();
                    if (isTokenValid)
                    {
                        if (AccessLevel == "A" && StatusInd == "A")
                        {
                            int wpuid = 0;
                            Int32.TryParse(user.WordPressUserId.ToString(), out wpuid);
                            if (wpuid > 0)
                            {
                                await UpdateWordPressUser(user, wordPressClient, wpuid, "administrator");
                            }
                            else
                            {
                                User createdUser = await CreateWordPressUser(user, wordPressClient);
                                _DbPortal.UpdateT_PRT_USERS_WordPressUserId(user, createdUser.Id);
                            }

                            actResult = 1;
                        }
                        else
                        {
                            int wuid = 0;
                            if (Int32.TryParse(user.WordPressUserId.ToString(), out wuid) && wuid > 0)
                            {
                                await UpdateWordPressUser(user, wordPressClient, wuid, "inactive");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Log errors
            }

            return actResult;

            string GetUserName(ApplicationUser user)
            {
                string uname = user.FIRST_NAME + user.LAST_NAME;
                string onlyletters = new string(uname.Where(Char.IsLetter).ToArray());
                string onlynumbers = new string(uname.Where(Char.IsNumber).ToArray());
                uname = onlyletters + onlynumbers;
                return uname;
            }

            async Task UpdateWordPressUser(ApplicationUser user, WordPressClient wordPressClient, int wpuid, string role)
            {
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
                    UserName = GetUserName(user),
                    Password = Utils.Decrypt(user.PasswordEncrypt),
                    Roles = new List<string> { role }
                };
                var isUpdated = await wordPressClient.Users.Update(updateUser);
            }

            async Task<User> CreateWordPressUser(ApplicationUser user, WordPressClient wordPressClient)
            {
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
                    UserName = GetUserName(user),
                    Password = Utils.Decrypt(user.PasswordEncrypt),
                    Roles = new List<string> { "administrator" }
                };
                //check for id, store in db, 
                var createdUser = await wordPressClient.Users.Create(newUser);
                return createdUser;
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
    }
}
