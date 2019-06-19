using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace IdSrv4Demo
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Console.Title = "IdSrv4Demo";

            await CreateWebHostBuilder(args).Build().RunAsync();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                    .UseStartup<Startup>()
                    .UseSerilog((context, configuration) =>
                    {
                        configuration
                            .MinimumLevel.Debug()
                            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                            .MinimumLevel.Override("System", LogEventLevel.Warning)
                            .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
                            .Enrich.FromLogContext()
                            .WriteTo.File(@"IsdSrv4Demo.log")
                            .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}", theme: AnsiConsoleTheme.Literate);
                    });
        }
    }
}
