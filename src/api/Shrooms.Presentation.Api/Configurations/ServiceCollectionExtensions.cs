using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shrooms.Contracts.Options;

namespace Shrooms.Presentation.Api.Configurations
{
    public static class ServiceCollectionExtensions
    {
        public static ApplicationOptions AddOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ApplicationOptions>(configuration);
            services.Configure<ApplicationOptions>(options =>
            {
                options.ClientUrl = configuration["Kestrel:Endpoints:Urls:Url"];
            });

            var applicationOptions = configuration.Get<ApplicationOptions>();

            services.Configure<BasicOptions>(options => options = applicationOptions.Authentication.Basic);
            services.Configure<GoogleOptions>(options => options = applicationOptions.Authentication.Google);
            services.Configure<JwtOptions>(options => options = applicationOptions.Authentication.Jwt);
            services.Configure<ShroomsAuthenticationOptions>(options => options = applicationOptions.Authentication);

            return applicationOptions;
        }
    }
}
