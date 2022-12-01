using System.Threading.Tasks;

namespace Shrooms.Domain.Services.ExternalProviders.Strategies
{
    public interface IExternalProviderStrategy
    {
        void SetParameters(ExternalProviderStrategyParameters parameters);

        Task<ExternalProviderResult> ExecuteStrategyAsync();
    }
}