using System.Threading.Tasks;

namespace Shrooms.Domain.Services.ExternalProviders
{
    public interface IExternalProviderStrategy
    {
        Task<ExternalProviderResult> ExecuteStrategyAsync();
    }
}
