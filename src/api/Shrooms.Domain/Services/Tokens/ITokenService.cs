using Microsoft.AspNetCore.Identity;
using Shrooms.Contracts.DataTransferObjects.Models.Tokens;
using System.Threading.Tasks;

namespace Shrooms.Domain.Services.Tokens
{
    public interface ITokenService
    {
        Task<TokenResponseDto> CreateTokenAsync(TokenRequestDto requestDto);

        Task<string> GetTokenRedirectUrlForExternalAsync(ExternalLoginInfo externalLoginInfo);
    }
}
