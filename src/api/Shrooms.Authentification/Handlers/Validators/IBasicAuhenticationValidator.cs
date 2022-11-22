using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;

namespace Shrooms.Authentication.Handlers.Validators
{
    public interface IBasicAuhenticationValidator
    {
        bool CheckIfAllCredentialsAreGiven((string, string) credentials);
        
        bool CheckIfCredentialsAreValid((string, string) credentials);

        bool CheckIfAuthorizationHeaderIsValid(AuthenticationHeaderValue header);

        bool CheckIfEndpointIsAnonymous(Endpoint endpoint);

        bool CheckIfHeaderContainsCredentials(AuthenticationHeaderValue header);
        
        bool CheckIfRequestContainsAuthorizationHeader(HttpRequest request);

        bool CheckIfSchemeIsBasic(AuthenticationHeaderValue header);
    }
}
