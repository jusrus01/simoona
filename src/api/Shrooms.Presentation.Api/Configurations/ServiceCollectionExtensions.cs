using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shrooms.Contracts.Constants;
using Shrooms.Contracts.Options;

namespace Shrooms.Presentation.Api.Configurations
{
    public static class ServiceCollectionExtensions
    {
        public static ApplicationOptions AddOptions(this IServiceCollection services, IConfiguration configuration)
        {
            var applicationOptions = configuration.Get<ApplicationOptions>();

            applicationOptions.ApiUrl = configuration[WebApiConstants.ConfigurationApiUrlKey];

            services.Configure<ApplicationOptions>(configuration);
            services.Configure<ApplicationOptions>(options =>
            {
                options.ApiUrl = applicationOptions.ApiUrl;
            });

            services.Configure<BasicAuthenticationOptions>(options => options = applicationOptions.Authentication.Basic);
            services.Configure<GoogleAuthenticationOptions>(options => options = applicationOptions.Authentication.Google);
            services.Configure<JwtAuthenticationOptions>(options => options = applicationOptions.Authentication.Jwt);
            services.Configure<ShroomsAuthenticationOptions>(options => options = applicationOptions.Authentication);

            return applicationOptions;
        }
    }
}
