using Shrooms.Contracts.Constants;
using System;
using System.Security.Claims;

namespace Shrooms.Domain.Extensions
{
    public static class ClaimsIdentityExtensions
    {
        public static int GetOrganizationId(this ClaimsIdentity claimsIdentity)
        {
            return Convert.ToInt32(claimsIdentity.FindFirst(WebApiConstants.ClaimOrganizationId));
        }

        public static string GetOrganizationName(this ClaimsIdentity claimsIdentity)
        {
            return claimsIdentity.FindFirstValue(WebApiConstants.ClaimOrganizationName);
        }

        public static string GetUserId(this ClaimsIdentity claimsIdentity)
        {
            return claimsIdentity.FindFirstValue(ClaimTypes.NameIdentifier);
        }

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
