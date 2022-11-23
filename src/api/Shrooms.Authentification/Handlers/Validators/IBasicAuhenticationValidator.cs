using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;

namespace Shrooms.Authentication.Handlers.Validators
{
    public interface IBasicAuhenticationValidator
    {
        void CheckIfAllCredentialsAreGiven((string, string) credentials);

        void CheckIfCredentialsAreValid((string, string) credentials);

        void CheckIfAuthorizationHeaderIsValid(AuthenticationHeaderValue header);

        void CheckIfEndpointIsAuthorized(Endpoint endpoint);

        void CheckIfHeaderContainsCredentials(AuthenticationHeaderValue header);

        void CheckIfRequestContainsAuthorizationHeader(HttpRequest request);

        void CheckIfSchemeIsBasic(AuthenticationHeaderValue header);

        void CheckIfRequiredOrganizationExists(bool exists);
    }
}
