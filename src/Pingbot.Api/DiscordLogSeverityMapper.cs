using System.Runtime.CompilerServices;
using Discord;
using Microsoft.Extensions.Logging;

namespace Pingbot.Api
{
    public static class DiscordLogSeverityMapper
    {
        public static LogLevel Map(LogSeverity sev)
        {
            return sev switch
            {
                LogSeverity.Debug => LogLevel.Debug,
                LogSeverity.Verbose => LogLevel.Debug,
                LogSeverity.Info => LogLevel.Information,
                LogSeverity.Warning => LogLevel.Warning,
                LogSeverity.Error => LogLevel.Error,
                LogSeverity.Critical => LogLevel.Critical,
                _ => throw new SwitchExpressionException("Invalid log severity")
            };
        }
    }
}