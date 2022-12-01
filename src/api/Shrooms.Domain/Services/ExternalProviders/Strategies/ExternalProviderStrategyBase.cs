using System;
using System.Threading.Tasks;

namespace Shrooms.Domain.Services.ExternalProviders.Strategies
{
    public abstract class ExternalProviderStrategyBase : IExternalProviderStrategy
    {
        public Task<ExternalProviderResult> ExecuteStrategyAsync()
        {
            CheckIfParametersPropertyIsSet();
            CheckIfRequiredParametersAreSet();
            return ExecuteAsync();
        }

        public ExternalProviderStrategyParameters Parameters { get; private set; }

        public abstract Task<ExternalProviderResult> ExecuteAsync();

        public abstract void CheckIfRequiredParametersAreSet();

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

        public void SetParameters(ExternalProviderStrategyParameters parameters)
        {
            Parameters = parameters;
        }

        private void CheckIfParametersPropertyIsSet()
        {
            if (Parameters == null)
            {
                throw new ArgumentException($"Strategy requires parameters to be set");
            }
        }
    }
}
