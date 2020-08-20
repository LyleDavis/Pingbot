using System;
using Discord.WebSocket;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Pingbot.Api
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks();
            AddDiscordClient(services);
            services.AddHostedService<DiscordWorker>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSerilogRequestLogging();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/service/health");
            });
        }
        
        private void AddDiscordClient(IServiceCollection services)
        {
            var shards = Environment.GetEnvironmentVariable("DISCORD_SHARDS");
            if (string.IsNullOrEmpty(shards)) throw new ArgumentNullException("DISCORD_SHARDS");
            var socketConfig = new DiscordSocketConfig
            {
                TotalShards = int.Parse(shards)
            };
            services.AddSingleton(_ => new DiscordShardedClient(socketConfig));
        }
    }
}
