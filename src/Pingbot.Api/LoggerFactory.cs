using System;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;

namespace Pingbot.Api
{
    public static class LoggerFactory
    {
        public static ILogger CreateLogger() 
            => CreateLoggerConfiguration().CreateLogger();

        private static LoggerConfiguration CreateLoggerConfiguration()
        {
            var config = new LoggerConfiguration()
                .MinimumLevel.Is(GetLogLevel())
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .Enrich.With<DefaultLogSourceEnricher>();
            SetLogFormat(config);
            return config;
        }

        private static void SetLogFormat(LoggerConfiguration logConfig)
        {
            if (Environment.GetEnvironmentVariable("LOG_FORMAT")?.ToLower() == "text")
            {
                logConfig.WriteTo.Console(
                    outputTemplate: "[{Timestamp:O} {Level:u3}] - [{Source}] - {Message:lj}{NewLine}{Exception}");
            }
            else
            {
                logConfig.WriteTo.Console(new RenderedCompactJsonFormatter());
            }
        }

        private static LogEventLevel GetLogLevel()
        {
            return Environment.GetEnvironmentVariable("LOG_LEVEL")?.ToLower() switch
            {
                "verbose" => LogEventLevel.Verbose,
                "debug" => LogEventLevel.Debug,
                "info" => LogEventLevel.Information,
                "warn" => LogEventLevel.Warning,
                "err" => LogEventLevel.Error,
                "ftl" => LogEventLevel.Fatal,
                _ => LogEventLevel.Information
            };
        }
    }
}