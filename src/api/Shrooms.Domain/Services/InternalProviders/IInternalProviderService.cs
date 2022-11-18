using Shrooms.Contracts.DataTransferObjects.Models.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shrooms.Domain.Services.InternalProviders
{
    public interface IInternalProviderService//todo: handle verification
    {
        Task RegisterAsync(RegisterDto registerDto);

        Task<IEnumerable<ExternalLoginDto>> GetLoginsAsync();

        Task SetSignInCookieAsync();
    }
}
