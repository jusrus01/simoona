using Microsoft.AspNetCore.Identity;
using Shrooms.Contracts.DataTransferObjects.Models.Users;
using System.Threading.Tasks;

namespace Shrooms.Domain.Services.ExternalProviders.Strategies
{
    public interface IExternalProviderStrategy<TArgument> : IExternalProviderStrategy where TArgument : class
    {
        void SetArguments(TArgument arguments);
    }

    public interface IExternalProviderStrategy
    {
        bool CanBeUsed(ExternalLoginInfo loginInfo, ExternalLoginRequestDto requestDto);

        void SetArguments(params object[] arguments);

        Task<ExternalProviderPartialResult> ExecuteAsync();
    }
}