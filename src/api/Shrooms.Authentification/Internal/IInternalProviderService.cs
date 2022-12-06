using Shrooms.Contracts.DataTransferObjects.Models.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shrooms.Authentication.Internal
{
    public interface IInternalProviderService
    {
        Task RegisterAsync(RegisterDto registerDto);

        Task VerifyAsync(VerifyEmailDto verifyDto);

        Task ResetPasswordAsync(ResetPasswordDto resetDto);

        Task SendPasswordResetEmailAsync(string email);

        Task<IEnumerable<ExternalLoginDto>> GetLoginsAsync();

        Task CookieSignInAsync();

        Task CookieSignOutAsync();
    }
}
