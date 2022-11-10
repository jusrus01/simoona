using Microsoft.AspNetCore.Mvc;
using Shrooms.Contracts.Enums;
using Shrooms.Domain.Services.ExternalProviders;
using System;

namespace Shrooms.Presentation.Api.ShroomsExtensions
{
    public static class ExternalProviderResultExtensions
    {
        public static IActionResult ToActionResult(this ExternalProviderResult providerResult, ControllerBase controller)
        {
            return providerResult.RedirectType switch
            {
                ExternalProviderRedirectType.RedirectToTokenConsumer => controller.Redirect(providerResult.RedirectUrl),
                ExternalProviderRedirectType.RedirectToProvider => new ChallengeResult(providerResult.AuthenticationScheme, providerResult.Properties),
                ExternalProviderRedirectType.None => controller.Ok(),
                _ => throw new NotSupportedException()
            };
        }
    }
}
