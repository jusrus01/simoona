using System.Threading.Tasks;

namespace Shrooms.Domain.Services.ExternalProviders.Strategies
{
    public interface IExternalProviderStrategy
    {
        Task<ExternalProviderResult> ExecuteStrategyAsync();
    }
}
