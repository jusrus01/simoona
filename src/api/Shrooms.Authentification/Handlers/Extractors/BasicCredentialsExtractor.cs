using System;
using System.Net.Http.Headers;
using System.Text;

namespace Shrooms.Authentication.Handlers.Extractors
{
    public static class BasicCredentialsExtractor
    {
        private const int CredentialsCount = 2;
        private const char CredentialsSeparator = ':';

        public static (string, string) ExtractCredentials(AuthenticationHeaderValue header)
        {
            var credentialBytes = Convert.FromBase64String(header.Parameter);
            var credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] { CredentialsSeparator }, CredentialsCount);

            var username = credentials[0];
            var password = credentials[1];

            return (username, password);
        }
    }
}
