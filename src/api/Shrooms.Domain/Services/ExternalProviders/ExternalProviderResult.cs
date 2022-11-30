using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Shrooms.Contracts.Constants;
using Shrooms.Contracts.Enums;
using System;

namespace Shrooms.Domain.Services.ExternalProviders
{
    public class ExternalProviderResult
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
            if (string.IsNullOrEmpty(provider))
            {
                throw new InvalidOperationException();
            }
            
            return provider switch
            {
                AuthenticationConstants.GoogleLoginProvider =>  GoogleDefaults.AuthenticationScheme, // Q: installed package, just for one constant...
                AuthenticationConstants.InternalLoginProvider => throw new InvalidOperationException(),
                _ => throw new InvalidOperationException()
            };
        }
    }
}