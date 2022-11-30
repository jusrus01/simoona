using JetBrains.Annotations;
using Shrooms.Contracts.DataTransferObjects.Models.Controllers;
using Shrooms.Contracts.DataTransferObjects.Models.Users;

namespace Shrooms.Contracts.DataTransferObjects.Models.ExternalProviders
{
    // We cannot be sure which properties will be initialized because the service for which this is used
    // does not conform to the single responsibility principle
    public class ExternalProviderStrategyParametersDto
    {
        public ExternalProviderStrategyParametersDto()
        {
        }
        
        public ExternalProviderStrategyParametersDto(ExternalLoginRequestDto request)
        {
            Request = request;
        }

        public ExternalProviderStrategyParametersDto(
            ExternalLoginRequestDto request,
            int organizationId)
            :
            this(request)
        {
            OrganizationId = organizationId;
        }

        public ExternalProviderStrategyParametersDto(ExternalLoginRequestDto request, ControllerRouteDto route)
            :
            this(request)
        {
            Route = route;
        }

        public ExternalProviderStrategyParametersDto(ExternalProviderStrategyParametersDto parameters, bool restoreUser)
        {
            Request = parameters.Request;
            OrganizationId = parameters.OrganizationId;
            RestoreUser = restoreUser;
            Route = parameters.Route;
        }

        [CanBeNull]
        public ExternalLoginRequestDto Request { get; }

        [CanBeNull]
        public int? OrganizationId { get; }

        [CanBeNull]
        public bool? RestoreUser { get; }

        [CanBeNull]
        public ControllerRouteDto Route { get; }
    }
}
