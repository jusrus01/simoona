using Shrooms.DataLayer.EntityModels.Models;
using Shrooms.Domain.Extensions;
using System.Security.Claims;

namespace Shrooms.Authentication.External
{
    public static class ExternalProviderUserCreator
    {
        public static ApplicationUser CreateApplicationUser(ClaimsIdentity claimsIdentity, string pictureId, int organizationId)
        {
            var userFirstName = claimsIdentity.GetFirstName();
            var userLastName = claimsIdentity.GetLastName();
            var email = claimsIdentity.GetEmail();

            return new ApplicationUser
            {
                FirstName = userFirstName,
                LastName = userLastName,
                Email = email,
                UserName = email,
                EmailConfirmed = true,
                OrganizationId = organizationId,
                PictureId = pictureId
            };
        }
    }
}
