using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Shrooms.Contracts.Options;
using System.Net;
using System.Net.Http.Headers;

namespace Shrooms.Authentication.Handlers.Validators//Q: Would it have been a better idea to create multiple Exceptions and then return result in try/catch block?
{
    public class BasicAuthenticationValidator : IBasicAuhenticationValidator
    {
        private readonly BasicAuthenticationOptions _basicOptions;

        public BasicAuthenticationValidator(IOptions<BasicAuthenticationOptions> basicOptions)
        {
            _basicOptions = basicOptions.Value;
        }

        public bool CheckIfEndpointIsAnonymous(Endpoint endpoint)
        {
            return endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null;
        }

        public bool CheckIfRequestContainsAuthorizationHeader(HttpRequest request)
        {
            return request.Headers.ContainsKey("Authorization");
        }

        public bool CheckIfAuthorizationHeaderIsValid(AuthenticationHeaderValue header)
        {
            return header != null;
        }

        public bool CheckIfSchemeIsBasic(AuthenticationHeaderValue header)
        {
            var basicSchemeName = AuthenticationSchemes.Basic.ToString();

            return header.Scheme == basicSchemeName;
        }

        public bool CheckIfHeaderContainsCredentials(AuthenticationHeaderValue header)
        {
            return header.Parameter != null;
        }

        public bool CheckIfAllCredentialsAreGiven((string, string) credentials)
        {
            var username = credentials.Item1;
            var password = credentials.Item2;

            return username != null && password != null;
        }

        public bool CheckIfCredentialsAreValid((string, string) credentials)
        {
            var username = credentials.Item1;
            var password = credentials.Item2;

            return username == _basicOptions.UserName && password == _basicOptions.Password;
        }
    }
}
