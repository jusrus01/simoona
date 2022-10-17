using Microsoft.AspNetCore.Identity;
using Shrooms.Contracts.DataTransferObjects.Models.Tokens;
using System.Threading.Tasks;

namespace Shrooms.Domain.Services.Tokens
{
    public interface ITokenService
    {
        Task<TokenResponseDto> GetTokenAsync(TokenRequestDto requestDto);
        
        Task<string> GetTokenForExternalAsync(ExternalLoginInfo externalLoginInfo);
    }
}
