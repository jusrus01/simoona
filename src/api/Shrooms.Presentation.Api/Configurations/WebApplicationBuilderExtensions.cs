//using Microsoft.ApplicationInsights.Extensibility;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.Extensions.Configuration;
//using Shrooms.Presentation.Api.GeneralCode;

//namespace Shrooms.Presentation.Api.Configurations
//{
//    public static class WebApplicationBuilderExtensions
//    {
//        public static void AddTelemetry(this WebApplicationBuilder builder)
//        {
//            var configuration = builder.Configuration;

//            if (configuration.GetSection("EnableAITelemetry").Get<bool>())
//            {
//                TelemetryConfiguration.Active.InstrumentationKey = configuration.GetSection("AIInstrumentationKey").Get<string>();

//                var chainBuilder = TelemetryConfiguration.Active.TelemetryProcessorChainBuilder;
                
//                chainBuilder.Use(next => new UnwantedTelemetryFilter(next));
//                chainBuilder.Build();
//            }
//            else
//            {
//                TelemetryConfiguration.Active.DisableTelemetry = true;
//            }
//        }
//    }
//}
