﻿using System.Collections.Generic;
using System.Security.Claims;

namespace Shrooms.Authentification.ExternalLoginInfrastructure
{
    public class ExternalLoginData
    {
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public IList<Claim> GetClaims()
        {
            IList<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, ProviderKey, null, LoginProvider, ClaimTypes.Email));

            if (UserName != null)
            {
                claims.Add(new Claim(ClaimTypes.Name, UserName, null, LoginProvider, ClaimTypes.Email));
            }

            return claims;
        }

        public static ExternalLoginData FromIdentity(ClaimsIdentity identity)
        {
            if (identity == null)
            {
                return null;
            }

            var providerKeyClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

            if (providerKeyClaim == null || string.IsNullOrEmpty(providerKeyClaim.Issuer)
                || string.IsNullOrEmpty(providerKeyClaim.Value))
            {
                return null;
            }

            if (providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer)
            {
                return null;
            }

            // Could be wrong
            return new ExternalLoginData
            {
                LoginProvider = providerKeyClaim.Issuer,
                ProviderKey = providerKeyClaim.Value,
                UserName = identity.FindFirst(ClaimTypes.Name).Value,
                Email = identity.FindFirst(ClaimTypes.Email).Value
            };
        }
    }
}