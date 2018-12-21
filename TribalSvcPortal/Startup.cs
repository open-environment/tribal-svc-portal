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
using System.Text;
using System.Security.Cryptography.X509Certificates;
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

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

            services.AddMvc();

            //cache memory of the left menu
            services.AddMemoryCache();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddScoped<IDbPortal, DbPortal>();
            services.AddScoped<IDbOpenDump, DbOpenDump>();

            //configure identity server with in-memory stores, keys, clients and scopes
            services.AddIdentityServer()

                 //.AddSigningCredential(
                 //    new SigningCredentials(
                 //        new SymmetricSecurityKey(
                 //            Encoding.UTF8.GetBytes(Configuration["SigningSecurityKey"])),
                 //            SecurityAlgorithms.RsaSha256Signature))
                 .AddDeveloperSigningCredential(true)  //adds a demo signing certificate
                //.AddSigningCredential(CreateRsaSecurityKey(Configuration["SigningSecurityKey"]))
                .AddInMemoryIdentityResources(IdentityServerConfig.GetIdentityResources())
                .AddInMemoryClients(IdentityServerConfig.GetClients2())
                .AddAspNetIdentity<ApplicationUser>()
                .AddProfileService<CustomProfileService>();


            services.AddSingleton<IEmailSender, EmailSender>();
            services.Configure<AuthMessageSenderOptions>(Configuration);
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
