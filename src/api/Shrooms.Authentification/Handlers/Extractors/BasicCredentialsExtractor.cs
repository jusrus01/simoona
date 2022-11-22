using System;
using System.Net.Http.Headers;
using System.Text;

namespace Shrooms.Authentication.Handlers.Extractors
{
    public static class BasicCredentialsExtractor//Q: should i use DI for these kind of classes as well? (if there were instance members used...)
    {
        private const int CredentialsSplitCount = 2;
        private const char CredentialsSeparator = ':';

        public static (string, string) ExtractCredentials(AuthenticationHeaderValue header)
        {
            var credentialBytes = Convert.FromBase64String(header.Parameter);
            var credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] { CredentialsSeparator }, CredentialsSplitCount);

            var username = credentials[0];
            var password = credentials[1];

            return (username, password);
        }
    }
}
