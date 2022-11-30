using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;

namespace Shrooms.Authentication.Handlers.Validators
{
    public interface IBasicAuhenticationValidator
    {
        void CheckIfRequestIsValid(HttpContext httpContext);

        void CheckIfAuthorizationHeaderIsValid(AuthenticationHeaderValue header);

        void CheckIfCredentialsAreValid((string, string) credentials);

        void CheckIfRequiredOrganizationExists(bool exists);
    }
}
