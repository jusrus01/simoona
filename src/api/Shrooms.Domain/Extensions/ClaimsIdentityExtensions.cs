using Shrooms.Contracts.Constants;
using System.Security.Claims;

namespace Shrooms.Domain.Extensions
{
    public static class ClaimsIdentityExtensions
    {
        public static string GetEmail(this ClaimsIdentity claimsIdentity)
        {
            return claimsIdentity.FindFirstValue(ClaimTypes.Email);
        }

        public static string GetFirstName(this ClaimsIdentity claimsIdentity)
        {
            return claimsIdentity.FindFirstValue(ClaimTypes.GivenName);
        }

        public static string GetLastName(this ClaimsIdentity claimsIdentity)
        {
            return claimsIdentity.FindFirstValue(ClaimTypes.Surname);
        }

        public static string GetGooglePictureUrl(this ClaimsIdentity claimsIdentity)
        {
            return claimsIdentity.FindFirstValue(WebApiConstants.ClaimPicture);
        }

        private static string FindFirstValue(this ClaimsIdentity claimsIdentity, string claimType)
        {
            return claimsIdentity.FindFirst(claimType)?.Value;
        }
    }
}
