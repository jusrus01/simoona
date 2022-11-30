using Microsoft.AspNetCore.Identity;
using Shrooms.Contracts.DataTransferObjects.Models.ExternalProviders;
using System;
using System.Threading.Tasks;

namespace Shrooms.Domain.Services.ExternalProviders.Strategies
{
    public abstract class ExternalProviderStrategyBase : IExternalProviderStrategy
    {
        public Task<ExternalProviderResult> ExecuteStrategyAsync(ExternalProviderStrategyParametersDto parameters, ExternalLoginInfo loginInfo = null)
        {
            EnsureValidParameters(parameters, loginInfo);
            return ExecuteAsync(parameters, loginInfo);
        }

        public abstract Task<ExternalProviderResult> ExecuteAsync(ExternalProviderStrategyParametersDto parameters, ExternalLoginInfo loginInfo = null);

        public abstract void EnsureValidParameters(ExternalProviderStrategyParametersDto parameters, ExternalLoginInfo loginInfo = null);

        protected static void EnsureParametersAreSet(params object[] parameters)
        {
            foreach (var parameter in parameters)
            {
                if (parameter == null)
                {
                    throw new ArgumentException($"Parameter {parameter.GetType().Name} cannot be null");
                }
            }
        }
    }
}
