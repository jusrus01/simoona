using Shrooms.Contracts.Constants;
using Shrooms.Contracts.Exceptions;
using Shrooms.DataLayer.EntityModels.Models;

namespace Shrooms.Domain.ServiceValidators.Validators.Organizations
{
    public class OrganizationValidator : IOrganizationValidator
    {
        public void CheckIfFoundOrganizationMatchesRequestHeader(Organization organization, string requestTenantName)
        {
            if (requestTenantName.ToLowerInvariant() != organization.ShortName.ToLowerInvariant())
            {
                throw new ValidationException(ErrorCodes.UserNotFound, "User not found");
            }
        }
    }
}
