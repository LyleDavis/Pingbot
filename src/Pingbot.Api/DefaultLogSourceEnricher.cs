using Serilog.Core;
using Serilog.Events;

namespace Pingbot.Api
{
    public class DefaultLogSourceEnricher : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            logEvent.AddPropertyIfAbsent(
                propertyFactory.CreateProperty("Source", "Application")
            );
        }
    }
}