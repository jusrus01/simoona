using Microsoft.Extensions.Configuration;

namespace Shrooms.Presentation.Api.Configurations
{
    public static class ConfigurationExtensions
    {
        public static bool IsTelemetryEnabled(this IConfiguration configuration)
        {
            return configuration.GetSection("EnableAITelemetry").Get<bool>();
        }
    }
}
