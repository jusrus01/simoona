using Microsoft.AspNetCore.Identity;
using Shrooms.Contracts.DataTransferObjects.Models.Controllers;
using Shrooms.Contracts.DataTransferObjects.Models.Users;
using Shrooms.DataLayer.EntityModels.Models;
using Shrooms.Domain.Services.ExternalProviders.Strategies;

namespace Shrooms.Domain.Services.ExternalProviders
{
    public class ExternalProviderStrategyFactory : IExternalProviderStrategyFactory
    {
        private readonly ExternalRegisterRedirectToProviderStrategy _registerRedirectStrategy;
        private readonly ExternalProviderLinkAccountStrategy _linkAccountStrategy;
        private readonly ExternalLoginRedirectToProviderStrategy _loginRedirectStrategy;
        private readonly ExternalRegisterStrategy _registerStrategy;
        private readonly ExternalLoginStrategy _loginStrategy;

        public ExternalProviderStrategyFactory(
            ExternalRegisterRedirectToProviderStrategy registerRedirectStrategy,
            ExternalProviderLinkAccountStrategy linkAccountStrategy,
            ExternalLoginRedirectToProviderStrategy loginRedirectStrategy,
            ExternalRegisterStrategy registerStrategy,
            ExternalLoginStrategy loginStrategy)
        {
            _registerRedirectStrategy = registerRedirectStrategy;
            _linkAccountStrategy = linkAccountStrategy;
            _loginRedirectStrategy = loginRedirectStrategy;
            _registerStrategy = registerStrategy;
            _loginStrategy = loginStrategy;
        }

        public IExternalProviderStrategy GetStrategy(
            ExternalLoginInfo externalLoginInfo,
            ExternalLoginRequestDto requestDto,
            ControllerRouteDto routeDto,
            Organization organization)
        {
            if (CanLinkAccount(externalLoginInfo, requestDto))
            {
                return LinkStrategy(requestDto, externalLoginInfo);
            }

            if (HasCookieFromExternalProvider(externalLoginInfo))
            {
                if (requestDto.IsRegistration)
                {
                    return RegisterStrategy(requestDto, organization.Id, externalLoginInfo);
                }

                return LoginStrategy(externalLoginInfo);
            }

            if (requestDto.IsRegistration)
            {
                return RegisterRedirectStrategy(requestDto, routeDto);
            }

            return LoginRedirectStrategy(requestDto, routeDto);
        }

        private IExternalProviderStrategy LinkStrategy(ExternalLoginRequestDto requestDto, ExternalLoginInfo loginInfo)
        {
            _linkAccountStrategy.SetParameters(new ExternalProviderStrategyParameters(requestDto, loginInfo));
            return _linkAccountStrategy;
        }

        private IExternalProviderStrategy RegisterStrategy(ExternalLoginRequestDto requestDto, int organizationId, ExternalLoginInfo loginInfo)
        {
            _registerStrategy.SetParameters(new ExternalProviderStrategyParameters(requestDto, organizationId, loginInfo));
            return _registerStrategy;
        }

        private IExternalProviderStrategy LoginStrategy(ExternalLoginInfo loginInfo)
        {
            _loginStrategy.SetParameters(new ExternalProviderStrategyParameters(loginInfo));
            return _loginStrategy;
        }

        private IExternalProviderStrategy RegisterRedirectStrategy(ExternalLoginRequestDto requestDto, ControllerRouteDto routeDto)
        {
            _registerRedirectStrategy.SetParameters(new ExternalProviderStrategyParameters(requestDto, routeDto));
            return _registerRedirectStrategy;
        }

        private IExternalProviderStrategy LoginRedirectStrategy(ExternalLoginRequestDto requestDto, ControllerRouteDto routeDto)
        {
            _loginRedirectStrategy.SetParameters(new ExternalProviderStrategyParameters(requestDto, routeDto));
            return _loginRedirectStrategy;
        }

        private static bool HasCookieFromExternalProvider(ExternalLoginInfo externalLoginInfo)
        {
            return externalLoginInfo != null;
        }

        private static bool CanLinkAccount(ExternalLoginInfo externalLoginInfo, ExternalLoginRequestDto requestDto)
        {
            return externalLoginInfo != null && requestDto.UserId != null;
        }
    }
}
