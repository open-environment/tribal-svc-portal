using IdentityServer4;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TribalSvcPortal.AppLogic.DataAccessLayer;
using TribalSvcPortal.Data.Models;

namespace TribalSvcPortal
{
       
    internal class IdentityServerConfig
    {
        public static IEnumerable<Client> GetClients2(IConfiguration config, Ilog log)
        {
            log.InsertT_PRT_SYS_LOG("info", "GetClients2");
            var _DbPortal = new DbPortal(new ApplicationDbContext(new DbContextOptions<ApplicationDbContext>(), config),log);

            List<T_PRT_CLIENTS> dbclients = _DbPortal.GetT_PRT_CLIENTS();

            List<Client> _clients = new List<Client>();
            foreach (T_PRT_CLIENTS dbclient in dbclients)
            {
                Client _client = new Client();
                _client.ClientId = dbclient.CLIENT_ID;
                _client.ClientName = dbclient.CLIENT_NAME;
                log.InsertT_PRT_SYS_LOG("info", dbclient.CLIENT_NAME);
                _client.AllowedGrantTypes = (dbclient.CLIENT_GRANT_TYPE == "HYBRID" ? GrantTypes.Hybrid : GrantTypes.Implicit);
                
                _client.RequireConsent = false;
                _client.AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    "api.read"
                };
                _client.RedirectUris = new List<string> { dbclient.CLIENT_REDIRECT_URI };
                log.InsertT_PRT_SYS_LOG("info", dbclient.CLIENT_REDIRECT_URI);
                _client.PostLogoutRedirectUris = new List<string> { dbclient.CLIENT_POST_LOGOUT_URI };
                log.InsertT_PRT_SYS_LOG("info", dbclient.CLIENT_POST_LOGOUT_URI);
                _client.AllowAccessTokensViaBrowser = true;
                _client.AccessTokenLifetime = 3600;
                _client.AllowedCorsOrigins = new List<string> { "https://openwater2clientdev.azurewebsites.net","http://localhost:4200" };
                _clients.Add(_client);
            }

            return _clients;
        }

        //Returns list of IdentityResources
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource> {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("openwatersapi", "OpenWater2 API Resource")
                {
                    Scopes = {new Scope("api.read")}
                }
            };
        }
    }

    internal class CustomProfileService : IProfileService
    {
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _claimsFactory;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IDbPortal _DbPortal;

        public CustomProfileService(UserManager<ApplicationUser> userManager,
              IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory,
              IDbPortal DbPortal)
        {
            _userManager = userManager;
            _claimsFactory = claimsFactory;
            _DbPortal = DbPortal;
        }


        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);
            var principal = await _claimsFactory.CreateAsync(user);

            var cs = principal.Claims.ToList();
            cs = cs.Where(claim => context.RequestedClaimTypes.Contains(claim.Type)).ToList();

            // Add User Properties
            List<OrgUserClientDisplayType> claims = _DbPortal.GetT_PRT_ORG_USERS_CLIENT_ByUserID(sub.ToString());
            foreach (OrgUserClientDisplayType claim in claims)
            {
                string orgName = _DbPortal.GetT_PRT_ORGANIZATIONS_ByOrgID(claim.ORG_ID).ORG_NAME;
                cs.Add(new Claim(claim.CLIENT_ID, claim.ORG_CLIENT_ALIAS + ";" + claim.ADMIN_IND + ";" + claim.STATUS_IND + ";" + claim.UserName + ";" + orgName));
            }

            context.IssuedClaims = cs;
        }


        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);
            context.IsActive = user != null;
        }
    }
}
