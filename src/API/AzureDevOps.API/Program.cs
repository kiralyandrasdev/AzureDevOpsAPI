using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;

namespace AzureDevOps.API
{
    public class Program
    {
        /// <summary>
        /// Program entry point.
        /// </summary>        
        public static async Task Main(string[] args)
        {
            await CreateWebHostBuilder(args).Build().RunAsync();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                   .UseUrls("https://localhost:6969")
                   .UseEnvironment("Production")
                   .UseStartup<Startup>();
    }
}
