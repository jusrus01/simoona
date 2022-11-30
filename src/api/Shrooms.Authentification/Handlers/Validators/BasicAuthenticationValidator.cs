using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Shrooms.Contracts.Constants;
using Shrooms.Contracts.Exceptions;
using Shrooms.Contracts.Options;
using System.Net;
using System.Net.Http.Headers;

namespace Shrooms.Authentication.Handlers.Validators
{
    public class BasicAuthenticationValidator : IBasicAuhenticationValidator
    {
        private const string AuthorizationHeader = "Authorization";

        private readonly BasicAuthenticationOptions _basicOptions;

        public BasicAuthenticationValidator(IOptions<BasicAuthenticationOptions> basicOptions)
        {
            _basicOptions = basicOptions.Value;
        }

        public void CheckIfRequestIsValid(HttpContext httpContext)
        {
            CheckIfEndpointIsAuthorized(httpContext.GetEndpoint());
            CheckIfRequestContainsAuthorizationHeader(httpContext.Request);
        }

        public void CheckIfAuthorizationHeaderIsValid(AuthenticationHeaderValue header)
        {
            CheckIfAuthorizationHeaderIsPresent(header);
            CheckIfSchemeIsBasic(header);
            CheckIfHeaderContainsCredentials(header);
        }

        public void CheckIfRequiredOrganizationExists(bool exists)
        {
            if (!exists)
            {
                throw new ValidationException(ErrorCodes.BasicInvalidCredentials);
            }
        }

        public void CheckIfCredentialsAreValid((string, string) credentials)
        {
            CheckIfAllCredentialsAreGiven(credentials);
            CheckIfCredentialsAreCorrect(credentials);
        }

        private static void CheckIfAllCredentialsAreGiven((string, string) credentials)
        {
            if (credentials.Item1 == null || credentials.Item2 == null)
            {
                throw new ValidationException(ErrorCodes.BasicNotAttemped);
            }
        }

        private void CheckIfCredentialsAreCorrect((string, string) credentials)
        {
            if (credentials.Item1 != _basicOptions.UserName || credentials.Item2 != _basicOptions.Password)
            {
                throw new ValidationException(ErrorCodes.BasicInvalidCredentials);
            }
        }

        private static void CheckIfEndpointIsAuthorized(Endpoint endpoint)
        {
            if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
            {
                throw new ValidationException(ErrorCodes.BasicNoResult);
            }
        }

        private static void CheckIfRequestContainsAuthorizationHeader(HttpRequest request)
        {
            if (!request.Headers.ContainsKey(AuthorizationHeader))
            {
                throw new ValidationException(ErrorCodes.BasicNotAttemped);
            }
        }

        private static void CheckIfAuthorizationHeaderIsPresent(AuthenticationHeaderValue header)
        {
            if (header == null)
            {
                throw new ValidationException(ErrorCodes.BasicInvalidHeader);
            }
        }

        private static void CheckIfSchemeIsBasic(AuthenticationHeaderValue header)
        {
            var basicSchemeName = AuthenticationSchemes.Basic.ToString();

            if (header.Scheme != basicSchemeName)
            {
                throw new ValidationException(ErrorCodes.BasicNotAttemped);
            }
        }

        private static void CheckIfHeaderContainsCredentials(AuthenticationHeaderValue header)
        {
            if (header.Parameter == null)
            {
                throw new ValidationException(ErrorCodes.BasicNotAttemped);
            }
        }
    }
}
