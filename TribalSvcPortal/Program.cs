using System.IO;
using System.Reflection;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace TribalSvcPortal
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(
                    (hostingContext, config) => {
                        var hostingEnvironment = hostingContext.HostingEnvironment;

                        config.SetBasePath(Directory.GetCurrentDirectory());

                        config.AddJsonFile("appsettings.json", true, true)
                            .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true);

                        if (hostingEnvironment.IsDevelopment())
                        {
                            var assembly = Assembly.Load(new AssemblyName(hostingEnvironment.ApplicationName));
                            if (assembly != null)
                            {
                                config.AddUserSecrets(assembly, true);
                            }
                        }

                        config.AddEnvironmentVariables();
                        if (args == null)
                            return;
                        config.AddCommandLine(args);
                    })
                .UseStartup<Startup>()
                .Build();
    }
}
