using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Shrooms.Authentication.Constants;
using Shrooms.Contracts.Constants;
using Shrooms.Domain.Services.Organizations;
using Shrooms.Infrastructure.FireAndForget;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Shrooms.Authentication.Handlers.Validators;
using Shrooms.Authentication.Handlers.Extractors;

namespace Shrooms.Authentification.Handlers
{
    public sealed class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IOrganizationService _organizationService;
        private readonly ITenantNameContainer _tenantNameContainer;
        private readonly IBasicAuhenticationValidator _validator;

        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IOrganizationService organizationService,
            ITenantNameContainer tenantNameContainer,
            IBasicAuhenticationValidator validator)
            :
            base(options, logger, encoder, clock)
        {
            _organizationService = organizationService;
            _tenantNameContainer = tenantNameContainer;
            _validator = validator;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var endpoint = Context.GetEndpoint();

            if (_validator.CheckIfEndpointIsAnonymous(endpoint))
            {
                return NoAuthenticationWasMadeResult();
            }

            if (!_validator.CheckIfRequestContainsAuthorizationHeader(Request))
            {
                return NoAuthenticationAttemptedResult();
            }

            var authorizationHeader = ExtractAuthorizationHeaderValue();

            if (!_validator.CheckIfAuthorizationHeaderIsValid(authorizationHeader))
            {
                return InvalidAuthorizationHeaderResult();
            }

            if (!_validator.CheckIfSchemeIsBasic(authorizationHeader))
            {
                return NoAuthenticationAttemptedResult();
            }

            if (!_validator.CheckIfHeaderContainsCredentials(authorizationHeader))
            {
                return NoAuthenticationAttemptedResult();
            }

            var credentials = BasicCredentialsExtractor.ExtractCredentials(authorizationHeader);

            if (!_validator.CheckIfAllCredentialsAreGiven(credentials))
            {
                return NoAuthenticationAttemptedResult();
            }

            if (!_validator.CheckIfCredentialsAreValid(credentials) || !await RequiredOrganizationExists())
            {
                return InvalidCredentialsResult();
            }

            return SuccessResult(authorizationHeader);
        }

        private AuthenticateResult SuccessResult(AuthenticationHeaderValue header)
        {
            return AuthenticateResult.Success(CreateTicket(header.Scheme));
        }

        private Task<bool> RequiredOrganizationExists()
        {
            return _organizationService.HasOrganizationAsync(_tenantNameContainer.TenantName);
        }

        private static AuthenticateResult InvalidCredentialsResult()
        {
            return AuthenticateResult.Fail("Invalid credentials");
        }

        private static AuthenticateResult InvalidAuthorizationHeaderResult()
        {
            return AuthenticateResult.Fail("Invalid Authorization Header");
        }

        private static AuthenticateResult NoAuthenticationWasMadeResult()
        {
            return AuthenticateResult.NoResult();
        }

        private static AuthenticateResult NoAuthenticationAttemptedResult()
        {
            return AuthenticateResult.Fail("No authentication was attempted");
        }

        private AuthenticationHeaderValue ExtractAuthorizationHeaderValue()
        {
            if (AuthenticationHeaderValue.TryParse(Request.Headers["Authorization"], out var authorizationHeader))
            {
                return authorizationHeader;
            }
         
            return null;
        }

        private AuthenticationTicket CreateTicket(string schemeName)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Role, AuthenticationClaims.DefaultBasicAuthenticationRoleValue),
                new Claim(WebApiConstants.ClaimOrganizationName, _tenantNameContainer.TenantName)
            };

            var identity = new ClaimsIdentity(claims, AuthenticationConstants.BasicScheme);
            var principal = new ClaimsPrincipal(identity);
            
            return new AuthenticationTicket(principal, schemeName);
        }
    }
}
