using Shrooms.DataLayer.EntityModels.Models;
using System;
using System.Linq;

namespace Shrooms.Domain.Extensions
{
    public static class OrganizationExtensions
    {
        private const string ProviderSeparator = ";";

        public static bool HasProvider(this Organization organization, string provider)
        {
            var lowerCaseProvider = provider.ToLower();

            return organization.AuthenticationProviders.Split(ProviderSeparator, StringSplitOptions.RemoveEmptyEntries)
                .Any(availableProvider => availableProvider.ToLower() == lowerCaseProvider);
        }
    }
}
