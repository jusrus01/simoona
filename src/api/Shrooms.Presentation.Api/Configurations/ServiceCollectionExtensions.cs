using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shrooms.Contracts.Constants;
using Shrooms.Contracts.Options;
using System;
using System.Collections.Concurrent;

namespace Shrooms.Presentation.Api.Configurations
{
    public static class ServiceCollectionExtensions
    {
        public static ApplicationOptions AddOptions(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            var applicationOptions = configuration.Get<ApplicationOptions>();
            applicationOptions.ApiUrl = configuration[WebApiConstants.ConfigurationApiUrlKey];

            services.Configure<ApplicationOptions>(configuration);
            services.Configure<ApplicationOptions>(options =>
            {
                options.ApiUrl = applicationOptions.ApiUrl;
                options.ContentRootPath = environment.ContentRootPath;
                options.ConnectionStrings = new ConcurrentDictionary<string, string>(options.ConnectionStrings, StringComparer.InvariantCultureIgnoreCase);
            });
            services.Configure<BasicAuthenticationOptions>(configuration.GetBasicAuthentication());
            services.Configure<GoogleAuthenticationOptions>(configuration.GetGoogleAuthentication());
            services.Configure<JwtAuthenticationOptions>(configuration.GetJwtAuthentication());
            services.Configure<ApplicationAuthenticationOptions>(configuration.GetAuthentication());
            services.Configure<MailOptions>(configuration.GetMailOptions());

            return applicationOptions;
        }
    }
}
