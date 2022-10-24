using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Shrooms.Contracts.Constants;

namespace Shrooms.Presentation.Api.Configurations
{
    public static class AuthorizationOptionsExtensions
    {
        public static void AddBearerAsDefault(this AuthorizationOptions options)
        {
            var policyBuilder = new AuthorizationPolicyBuilder(
                JwtBearerDefaults.AuthenticationScheme,
                "Bearer");

            policyBuilder = policyBuilder.RequireAuthenticatedUser();

            options.DefaultPolicy = policyBuilder
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .Build();
        }

        public static void AddBasicPolicy(this AuthorizationOptions options)
        {
            var policyBuilder = new AuthorizationPolicyBuilder(AuthenticationConstants.BasicScheme);

            options.AddPolicy(PolicyConstants.BasicPolicy, policyBuilder
                .RequireClaim("role")
                .Build());
        }

        public static void AddStoragePolicy(this AuthorizationOptions options)
        {
            var policyBuilder = new AuthorizationPolicyBuilder();

            options.AddPolicy(PolicyConstants.StoragePolicy, policyBuilder
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme,
                    CookieAuthenticationDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build());
        }
    }
}
