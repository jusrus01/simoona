using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Shrooms.Domain.Services.Users
{
    public interface IApplicationSignInManager
    {
        Task ExternalLoginSignInAsync(ExternalLoginInfo externalLoginInfo);

        AuthenticationProperties ConfigureExternalAuthenticationProperties(string provider, string redirectUrl);

        Task<ExternalLoginInfo> GetExternalLoginInfoAsync();
    }
}
