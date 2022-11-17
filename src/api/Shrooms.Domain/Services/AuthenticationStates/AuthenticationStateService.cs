using System;
using System.Security.Cryptography;

namespace Shrooms.Domain.Services.AuthenticationStates
{
    public class AuthenticationStateService : IAuthenticationStateService
    {
        private const int StateStrengthInBits = 256;
        private const int BitsPerByte = 8;

        public string GenerateExternalAuthenticationState()
        {
            using var cryptoProvider = new RNGCryptoServiceProvider();

            if (StateStrengthInBits % BitsPerByte != 0)
            {
                throw new ArgumentException("strengthInBits must be evenly divisible by 8.", "strengthInBits");
            }

            var strengthInBytes = StateStrengthInBits / BitsPerByte;

            var data = new byte[strengthInBytes];

            cryptoProvider.GetBytes(data);

            var base64 = Convert.ToBase64String(data);

            return base64[0..^1];
        }
    }
}
