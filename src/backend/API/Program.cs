using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Core.Helpers;
using Data.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Models.Interfaces;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using Data.Seed;

namespace API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var appUnitOfWork = services.GetService<IAppUnitOfWork>();
                    var currencyConverter = services.GetService<ICurrencyConverter>();

                    await DataSeeder.SeedProductData(appUnitOfWork, currencyConverter);

                    if (services is IDisposable disposable) disposable.Dispose();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occured during migration");
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseSerilog((hostingContext, loggingConfiguration) => loggingConfiguration
                    .Enrich.FromLogContext()
                    .Enrich.WithProperty("Application", "discovery")
                    .Enrich.WithProperty("MachineName", Environment.MachineName)
                    .Enrich.WithProperty("CurrentManagedThreadId", Environment.CurrentManagedThreadId)
                    .Enrich.WithProperty("OSVersion", Environment.OSVersion)
                    .Enrich.WithProperty("Version", Environment.Version)
                    .Enrich.WithProperty("UserName", Environment.UserName)
                    .Enrich.WithProperty("ProcessId", Environment.ProcessId)
                    .Enrich.WithProperty("ProcessName", Process.GetCurrentProcess().ProcessName)
                    .WriteTo.Console(theme: AnsiConsoleTheme.Code)
                    .WriteTo.File(
                        formatter: new LogTextFormatter(),
                        path: Path.Combine(
                            hostingContext.HostingEnvironment.ContentRootPath +
                            $"{Path.DirectorySeparatorChar}Logs{Path.DirectorySeparatorChar}",
                            $"discovery_log_{DateTime.Now:yyyyMMdd}.txt"
                        )
                    ).ReadFrom.Configuration(hostingContext.Configuration)
                );

                webBuilder.UseStartup<Startup>();
            });
    }
}
