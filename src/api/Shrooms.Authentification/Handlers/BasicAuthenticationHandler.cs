using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Shrooms.Contracts.Constants;
using Shrooms.Contracts.Options;
using Shrooms.Domain.Services.Organizations;
using Shrooms.Infrastructure.FireAndForget;
using System;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Shrooms.Authentification.Handlers
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly BasicAuthenticationOptions _basicOptions;
        private readonly IOrganizationService _organizationService;
        private readonly ITenantNameContainer _tenantNameContainer;

        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IOptions<BasicAuthenticationOptions> basicOptions,
            IOrganizationService organizationService,
            ITenantNameContainer tenantNameContainer)
            :
            base(options, logger, encoder, clock)
        {
            _basicOptions = basicOptions.Value;
            _organizationService = organizationService;
            _tenantNameContainer = tenantNameContainer;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var endpoint = Context.GetEndpoint();

            if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
            {
                return AuthenticateResult.NoResult();
            }

            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.Fail("No authentication was attempted");
            }

            if (!AuthenticationHeaderValue.TryParse(Request.Headers["Authorization"], out var authorizationHeader))
            {
                return AuthenticateResult.Fail("Invalid Authorization Header");
            }

            var basicSchemeName = AuthenticationSchemes.Basic.ToString();

            if (authorizationHeader.Scheme != basicSchemeName)
            {
                return AuthenticateResult.Fail("No authentication was attempted");
            }

            if (authorizationHeader.Parameter == null)
            {
                return AuthenticateResult.Fail("No authentication was attempted");
            }

            var credentials = ExtractCredentials(authorizationHeader);

            var username = credentials.Item1;
            var password = credentials.Item2;

            if (username == null || password == null)
            {
                return AuthenticateResult.Fail("No authentication was attempted");
            }

            if (!AreCredentialsValid(username, password) ||
                !await _organizationService.HasOrganizationAsync(_tenantNameContainer.TenantName))
            {
                return AuthenticateResult.Fail("Invalid credentials");
            }

            return AuthenticateResult.Success(CreateTicket(basicSchemeName));
        }

        private AuthenticationTicket CreateTicket(string schemeName)
        {
            var claims = new[]
            {
                new Claim("name", "app"),
                new Claim("role", "scheduler-webhook"),
                new Claim(WebApiConstants.ClaimOrganizationName, _tenantNameContainer.TenantName)
            };

            var identity = new ClaimsIdentity(claims, "Basic", "name", "role");
            var principal = new ClaimsPrincipal(identity);
            
            return new AuthenticationTicket(principal, schemeName);
        }

        private static (string, string) ExtractCredentials(AuthenticationHeaderValue header)
        {
            var credentialBytes = Convert.FromBase64String(header.Parameter);
            var credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 2);

            var username = credentials[0];
            var password = credentials[1];

            return (username, password);
        }

        private bool AreCredentialsValid(string username, string password)
        {
            return username == _basicOptions.UserName && password == _basicOptions.Password;
        }
    }
}
