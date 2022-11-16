using Shrooms.DataLayer.EntityModels.Models;

namespace Shrooms.Domain.ServiceValidators.Validators.Organizations
{
    public interface IOrganizationValidator
    {
        void CheckIfFoundOrganizationMatchesRequestHeader(Organization organization, string requestTenantName);
    }
}
