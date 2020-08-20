using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Pingbot.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = LoggerFactory.CreateLogger();
            Boot(args);
        }

        private static void Boot(string[] args)
        {
            try
            {
                Log.Information("Booting in {Environment}...", 
                    Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Terminated unexpectedly - big oops");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
