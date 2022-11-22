using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Shrooms.Authentication.Constants;
using Shrooms.Contracts.Constants;
using System.Security.Claims;

namespace Shrooms.Presentation.Api.Configurations
{
    public static class AuthorizationOptionsExtensions
    {
        public static void AddBearerAsDefault(this AuthorizationOptions options)
        {
            var policyBuilder = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme);
            policyBuilder = policyBuilder.RequireAuthenticatedUser();

            options.DefaultPolicy = policyBuilder.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .Build();
        }

        public static void AddBasicPolicy(this AuthorizationOptions options)
        {
            var policyBuilder = new AuthorizationPolicyBuilder(AuthenticationConstants.BasicScheme);
            policyBuilder.RequireClaim(ClaimTypes.Role, AuthenticationClaims.DefaultBasicAuthenticationRoleValue);

            options.AddPolicy(PolicyConstants.BasicPolicy, policyBuilder.Build());
        }

        public static void AddStoragePolicy(this AuthorizationOptions options)
        {
            var policyBuilder = new AuthorizationPolicyBuilder();
            policyBuilder.AddAuthenticationSchemes(
                JwtBearerDefaults.AuthenticationScheme,
                CookieAuthenticationDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser();

            options.AddPolicy(PolicyConstants.StoragePolicy, policyBuilder.Build());
        }
    }
}
