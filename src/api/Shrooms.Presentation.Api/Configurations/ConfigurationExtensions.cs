using Microsoft.Extensions.Configuration;

namespace Shrooms.Presentation.Api.Configurations
{
    public static class ConfigurationExtensions
    {
        public static bool IsTelemetryEnabled(this IConfiguration configuration) => configuration.GetSection("EnableAITelemetry").Get<bool>();

        public static string[] GetAllowedOrigins(this IConfiguration configuration) => configuration.GetSection("AllowedCorsOrigins").Get<string[]>();

        public static IConfigurationSection GetBasicAuthenticationSection(this IConfiguration configuration) => configuration.GetSection("Authentication:Basic");

        public static IConfigurationSection GetGoogleAuthenticationSection(this IConfiguration configuration) => configuration.GetSection("Authentication:Google");

        public static IConfigurationSection GetJwtAuthenticationSection(this IConfiguration configuration) => configuration.GetSection("Authentication:Jwt");

        public static IConfigurationSection GetAuthenticationSection(this IConfiguration configuration) => configuration.GetSection("Authentication");

        public static IConfigurationSection GetMailOptions(this IConfiguration configuration) => configuration.GetSection("MailOptions");
    }
}
