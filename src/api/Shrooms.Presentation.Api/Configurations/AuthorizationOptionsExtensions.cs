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
            var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(
                JwtBearerDefaults.AuthenticationScheme,
                "Bearer");

            defaultAuthorizationPolicyBuilder = defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();

            options.DefaultPolicy = defaultAuthorizationPolicyBuilder
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .Build();
        }

        public static void AddBasicPolicy(this AuthorizationOptions options)
        {
            var basicAuthenticationPolicyBuilder = new AuthorizationPolicyBuilder(AuthenticationConstants.BasicScheme);

            options.AddPolicy(PolicyConstants.BasicPolicy, basicAuthenticationPolicyBuilder
                .RequireClaim("role")
                .Build());
        }
    }
}
