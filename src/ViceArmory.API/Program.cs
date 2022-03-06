using Common.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace ViceArmory.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).ConfigureLogging(logBuilder =>
             {
                 logBuilder.ClearProviders(); // removes all providers from LoggerFactory
                 logBuilder.AddConsole();
                 logBuilder.AddTraceSource("Information, ActivityTracing"); // Add Trace listener provider
             })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls("http://143.110.244.193:5002");
                });
    }
}
