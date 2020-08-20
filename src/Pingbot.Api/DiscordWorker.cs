using System;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog.Context;

namespace Pingbot.Api
{
    public class DiscordWorker : BackgroundService
    {
        private readonly ILogger<DiscordWorker> _logger;
        private readonly DiscordShardedClient _client;

        public DiscordWorker(ILogger<DiscordWorker> logger, DiscordShardedClient client)
        {
            _logger = logger;
            _client = client;
        }
        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Discord worker started...");
            stoppingToken.Register(() => _logger.LogInformation("Discord worker TERMINATED"));
            _client.ShardReady += ReadyAsync;
            _client.Log += LogAsync;
            _client.MessageReceived += MessageReceived;
            var botToken = Environment.GetEnvironmentVariable("BOT_TOKEN");
            if (string.IsNullOrEmpty(botToken)) throw new ArgumentNullException("BOT_TOKEN");
            await _client.LoginAsync(TokenType.Bot, botToken);
            await _client.StartAsync();
            await Task.Delay(Timeout.Infinite, stoppingToken);
        }

        private async Task MessageReceived(SocketMessage msg)
        {
            using (LogContext.PushProperty("Source", "Bot"))
            {
                if (msg.Content == "!ping")
                {
                    _logger.LogInformation("Received ping message!");
                    await msg.Channel.SendMessageAsync("Pong!");   
                }
            }
        }

        private Task LogAsync(LogMessage msg)
        {
            var level = DiscordLogSeverityMapper.Map(msg.Severity);
            using (LogContext.PushProperty("Source", msg.Source))
            {
                _logger.Log(level, msg.Exception, msg.Message);
            }
            return Task.CompletedTask;
        }

        private Task ReadyAsync(DiscordSocketClient shard)
        {
            _logger.LogInformation("Shard {ShardNumber} is connected and ready...", shard.ShardId);
            return Task.CompletedTask;
        }
    }
}