using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Configuration;
using Shrooms.Presentation.Api.GeneralCode;

namespace Shrooms.Presentation.Api.Configurations
{
    public static class Telemetry
    {
        public static void Configure(IConfiguration configuration)
        {
            var isEnabled = configuration.GetEnableAITelemtry().Get<bool>();
            TelemetryConfiguration.Active.DisableTelemetry = !isEnabled;

            if (!isEnabled)
            {
                return;
            }

            TelemetryConfiguration.Active.InstrumentationKey = configuration.GetAIInstrumentationKey().Get<string>();

            var builder = TelemetryConfiguration.Active.TelemetryProcessorChainBuilder;

            builder.Use(next => new UnwantedTelemetryFilter(next));
            builder.Build();
        }
    }
}
