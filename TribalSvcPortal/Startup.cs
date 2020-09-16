using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TribalSvcPortal.Services;
using TribalSvcPortal.Data.Models;
using TribalSvcPortal.AppLogic.DataAccessLayer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace TribalSvcPortal
{
    public class Startup
    {
        public IHostingEnvironment HostingEnvironment { get; }
        public IConfiguration Configuration { get; }
        public Startup(IHostingEnvironment env, IConfiguration config)
        {
            HostingEnvironment = env;
            Configuration = config;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Add database connection
            var dbConnecctionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(dbConnecctionString));


            //Add ASP.NET Identity and configure its settings
            services.AddIdentity<ApplicationUser, IdentityRole>(x =>
            {
                x.Password.RequiredLength = 8;
                x.Password.RequireUppercase = false;
                x.Password.RequireLowercase = false;
                x.Password.RequireNonAlphanumeric = false;
                x.SignIn.RequireConfirmedEmail = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            services.AddCors();
            //configure the web application to use MVC
            services.AddMvc();

            //cache memory of the left menu
            services.AddMemoryCache();


            // Add application services
            services.AddScoped<IDbPortal, DbPortal>();
            services.AddScoped<IDbOpenDump, DbOpenDump>();
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<Ilog, log>();


            //configure identity server with in-memory stores, keys, clients and scopes
            services.AddIdentityServer()
                .AddSigningCredential(CreateRsaSecurityKey(Configuration["SigningSecurityKey"]))
                .AddInMemoryIdentityResources(IdentityServerConfig.GetIdentityResources())
                .AddInMemoryClients(IdentityServerConfig.GetClients2(Configuration, new log(Configuration)))
                .AddAspNetIdentity<ApplicationUser>()
                .AddProfileService<CustomProfileService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseCors(builder =>
                builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials());
            app.UseStaticFiles();

            app.UseIdentityServer();

            //Identity is enabled:
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }


        public static RsaSecurityKey CreateRsaSecurityKey(string key)
        {
            var rSA = RSA.Create();
            RsaSecurityKey rsaSecurityKey;
            if (rSA is RSACryptoServiceProvider)
            {
                rSA.Dispose();
                rsaSecurityKey = new RsaSecurityKey(new RSACng(2048).ExportParameters(includePrivateParameters: true));
            }
            else
            {
                rSA.KeySize = 2048;
                rsaSecurityKey = new RsaSecurityKey(rSA);
            }
            rsaSecurityKey.KeyId = key;
            return rsaSecurityKey;
        }

    }
}
