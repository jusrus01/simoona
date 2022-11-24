using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;

namespace Shrooms.Presentation.Api.Configurations
{
    public static class CorsOptionsExtensions
    {
        public static void ConfigureDefinedOrigins(this CorsOptions options, IConfiguration configuration)
        {
            var origins = configuration.GetSection("AllowedCorsOrigins").Get<string[]>();
            var policy = GetPolicy(origins);
            options.AddDefaultPolicy(policy);
        }

        private static CorsPolicy GetPolicy(string[] origins)
        {
            var builder = new CorsPolicyBuilder(origins);

            builder.AllowCredentials()
                .AllowAnyHeader()
                .AllowAnyMethod();

            if (!HasOrigins(origins))
            {
                builder.SetIsOriginAllowed(isAllowed => true);
            }

            return builder.SetPreflightMaxAge(TimeSpan.FromSeconds(short.MaxValue))
                .Build();
        }

        private static bool HasOrigins(string[] origins) => origins.Length != 0;
    }
}
