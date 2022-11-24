using Microsoft.Extensions.Configuration;

namespace Shrooms.Presentation.Api.Configurations
{
    public static class ConfigurationExtensions
    {
        public static IConfigurationSection GetAIInstrumentationKey(this IConfiguration configuration) => configuration.GetSection("AIInstrumentationKey");

        public static IConfigurationSection GetEnableAITelemtry(this IConfiguration configuration) => configuration.GetSection("EnableAITelemetry");

        public static IConfigurationSection GetAllowedOrigins(this IConfiguration configuration) => configuration.GetSection("AllowedCorsOrigins");

        public static IConfigurationSection GetBasicAuthentication(this IConfiguration configuration) => configuration.GetSection("Authentication:Basic");

        public static IConfigurationSection GetGoogleAuthentication(this IConfiguration configuration) => configuration.GetSection("Authentication:Google");

        public static IConfigurationSection GetJwtAuthentication(this IConfiguration configuration) => configuration.GetSection("Authentication:Jwt");

        public static IConfigurationSection GetAuthentication(this IConfiguration configuration) => configuration.GetSection("Authentication");

        public static IConfigurationSection GetMailOptions(this IConfiguration configuration) => configuration.GetSection("MailOptions");
    }
}
