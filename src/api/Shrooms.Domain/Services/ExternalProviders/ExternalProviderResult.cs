using Microsoft.AspNetCore.Authentication;
using Shrooms.Contracts.Constants;
using Shrooms.Contracts.Enums;
using System;

namespace Shrooms.Domain.Services.ExternalProviders
{
    public class ExternalProviderResult//TODO: transform to generic?
    {
        public ExternalProviderResult()
        {
            RedirectType = ExternalProviderRedirectType.None;
        }

        public ExternalProviderResult(string tokenRedirectUrl)
        {
            RedirectUrl = tokenRedirectUrl;
            RedirectType = ExternalProviderRedirectType.RedirectToTokenConsumer;
        }

        public ExternalProviderResult(
            AuthenticationProperties properties,
            string provider)
        {
            Properties = properties;
            AuthenticationScheme = GetAuthenticationSchemeByProvider(provider);
            RedirectType = ExternalProviderRedirectType.RedirectToProvider;
        }

        public AuthenticationProperties Properties { get; }

        public string AuthenticationScheme { get; }

        public string RedirectUrl { get; }

        public ExternalProviderRedirectType RedirectType { get; }

        private static string GetAuthenticationSchemeByProvider(string provider)
        {
            if (provider == null || provider == string.Empty)
            {
                throw new InvalidOperationException();
            }

            return provider switch
            {
                AuthenticationConstants.GoogleLoginProvider => "Google", // TODO: Change after full implementation (make decision)
                AuthenticationConstants.InternalLoginProvider => throw new InvalidOperationException(),
                _ => throw new InvalidOperationException()
            };
        }
    }
}