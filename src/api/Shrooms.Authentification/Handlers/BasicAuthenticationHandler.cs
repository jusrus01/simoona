using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Shrooms.Authentication.Constants;
using Shrooms.Contracts.Constants;
using Shrooms.Domain.Services.Organizations;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Shrooms.Authentication.Handlers.Validators;
using Shrooms.Authentication.Handlers.Extractors;
using Shrooms.Contracts.Exceptions;
using Shrooms.Contracts.Infrastructure;

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
            Microsoft.AspNetCore.Authentication.ISystemClock clock,
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
            try
            {
                _validator.CheckIfRequestIsValid(Context);
                
                var authorizationHeader = ExtractAuthorizationHeaderValue();
                _validator.CheckIfAuthorizationHeaderIsValid(authorizationHeader);
                
                var credentials = BasicCredentialsExtractor.ExtractCredentials(authorizationHeader);
                _validator.CheckIfCredentialsAreValid(credentials);
                
                await ValidateRequiredOrganizationAsync();

                return SuccessResult(authorizationHeader);
            }
            catch (ValidationException ex)
            {
                return GetValidationErrorResult(ex);
            }
        }

        private async Task ValidateRequiredOrganizationAsync()
        {
            var exists = await _organizationService.HasOrganizationAsync(_tenantNameContainer.TenantName);
            _validator.CheckIfRequiredOrganizationExists(exists);
        }

        private static AuthenticateResult GetValidationErrorResult(ValidationException ex)
        {
            return ex.ErrorCode switch
            {
                ErrorCodes.BasicInvalidCredentials => InvalidCredentialsResult(),
                ErrorCodes.BasicNoResult => NoAuthenticationWasMadeResult(),
                ErrorCodes.BasicInvalidHeader => InvalidAuthorizationHeaderResult(),
                ErrorCodes.BasicNotAttemped => NoAuthenticationAttemptedResult(),
                _ => NoAuthenticationAttemptedResult()
            };
        }

        private AuthenticateResult SuccessResult(AuthenticationHeaderValue header)
        {
            return AuthenticateResult.Success(CreateTicket(header.Scheme));
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
