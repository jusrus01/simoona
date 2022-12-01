using Microsoft.AspNetCore.Identity;
using Shrooms.Contracts.DataTransferObjects.Models.Controllers;
using Shrooms.Contracts.DataTransferObjects.Models.Users;

namespace Shrooms.Domain.Services.ExternalProviders.Strategies
{
    // We cannot be sure which properties will be initialized because the service for which this is used
    // does not conform to the single responsibility principle
    public class ExternalProviderStrategyParameters
    {
        public ExternalProviderStrategyParameters(ExternalLoginRequestDto request, ExternalLoginInfo loginInfo, bool restoreUser = false)
        {
            Request = request;
            LoginInfo = loginInfo;
            RestoreUser = restoreUser;
        }

        public ExternalProviderStrategyParameters(ExternalLoginInfo loginInfo)
        {
            LoginInfo = loginInfo;
        }

        public ExternalProviderStrategyParameters(
            ExternalLoginRequestDto request,
            int organizationId,
            ExternalLoginInfo loginInfo)
            :
            this(request, loginInfo)
        {
            OrganizationId = organizationId;
        }

        public ExternalProviderStrategyParameters(ExternalLoginRequestDto request, ControllerRouteDto route)
        {
            Route = route;
            Request = request;
        }

        public ExternalProviderStrategyParameters(ExternalProviderStrategyParameters parameters, bool restoreUser)
        {
            Request = parameters.Request;
            OrganizationId = parameters.OrganizationId;
            RestoreUser = restoreUser;
            Route = parameters.Route;
            LoginInfo = parameters.LoginInfo;
        }

        public ExternalLoginRequestDto Request { get; }

        public int? OrganizationId { get; }

        public bool? RestoreUser { get; }

        public ControllerRouteDto Route { get; }

        public ExternalLoginInfo LoginInfo { get; }
    }
}
