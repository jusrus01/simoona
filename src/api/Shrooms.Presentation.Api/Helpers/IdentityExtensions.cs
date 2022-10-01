using System.Security.Principal;
using Shrooms.Contracts.DataTransferObjects;

namespace Shrooms.Presentation.Api.Helpers
{
    public static class IdentityExtensions
    {
        public static UserAndOrganizationDto GetUserAndOrganization(this IIdentity identity)
        {
            //return new UserAndOrganizationDto
            //{
            //    OrganizationId = identity.GetOrganizationId(),
            //    UserId = identity.GetUserId()
            //};
            return new UserAndOrganizationDto();
        }
    }
}