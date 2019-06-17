using System.Threading.Tasks;
using AspNetCoreRateLimit;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreRateLimitDemo
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await (await CreateWebHostBuilder(args)).RunAsync();
        }

        public static async Task<IWebHost> CreateWebHostBuilder(string[] args)
        {
            var webHost = WebHost.CreateDefaultBuilder(args)
                    .UseStartup<Startup>()
                    .Build();
            using (var scope = webHost.Services.CreateScope())
            {
                // get the IpPolicyStore instance
                var ipPolicyStore = scope.ServiceProvider.GetRequiredService<IIpPolicyStore>();

                // seed IP data from appsettings
                await ipPolicyStore.SeedAsync();
            }

            return webHost;
        }
    }
}
