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

                // Creating case-insensitive dictionary
                options.ConnectionStrings = new ConcurrentDictionary<string, string>(options.ConnectionStrings, StringComparer.InvariantCultureIgnoreCase);
            });

            services.Configure<BasicAuthenticationOptions>(options => options = applicationOptions.Authentication.Basic);
            services.Configure<GoogleAuthenticationOptions>(options => options = applicationOptions.Authentication.Google);
            services.Configure<JwtAuthenticationOptions>(options => options = applicationOptions.Authentication.Jwt);
            services.Configure<ShroomsAuthenticationOptions>(options => options = applicationOptions.Authentication);
            services.Configure<MailOptions>(options => options = applicationOptions.MailSettings);

            return applicationOptions;
        }
    }
}
