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
        private readonly BasicAuthenticationOptions _basicOptions;

        public BasicAuthenticationValidator(IOptions<BasicAuthenticationOptions> basicOptions)
        {
            _basicOptions = basicOptions.Value;
        }

        public void CheckIfEndpointIsAuthorized(Endpoint endpoint)
        {
            if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
            {
                throw new ValidationException(ErrorCodes.BasicNoResult);
            }
        }

        public void CheckIfRequestContainsAuthorizationHeader(HttpRequest request)
        {
            if (!request.Headers.ContainsKey("Authorization"))
            {
                throw new ValidationException(ErrorCodes.BasicNotAttemped);
            }
        }

        public void CheckIfAuthorizationHeaderIsValid(AuthenticationHeaderValue header)
        {
            if (header == null)
            {
                throw new ValidationException(ErrorCodes.BasicInvalidHeader);
            }
        }

        public void CheckIfSchemeIsBasic(AuthenticationHeaderValue header)
        {
            var basicSchemeName = AuthenticationSchemes.Basic.ToString();

            if (header.Scheme != basicSchemeName)
            {
                throw new ValidationException(ErrorCodes.BasicNotAttemped);
            }
        }

        public void CheckIfHeaderContainsCredentials(AuthenticationHeaderValue header)
        {
            if (header.Parameter == null)
            {
                throw new ValidationException(ErrorCodes.BasicNotAttemped);
            }
        }

        public void CheckIfAllCredentialsAreGiven((string, string) credentials)
        {
            if (credentials.Item1 == null || credentials.Item2 == null)
            {
                throw new ValidationException(ErrorCodes.BasicNotAttemped);
            }
        }

        public void CheckIfCredentialsAreValid((string, string) credentials)
        {
            if (credentials.Item1 != _basicOptions.UserName || credentials.Item2 != _basicOptions.Password)
            {
                throw new ValidationException(ErrorCodes.BasicInvalidCredentials);
            }
        }

        public void CheckIfRequiredOrganizationExists(bool exists)
        {
            if (!exists)
            {
                throw new ValidationException(ErrorCodes.BasicInvalidCredentials);
            }
        }
    }
}
