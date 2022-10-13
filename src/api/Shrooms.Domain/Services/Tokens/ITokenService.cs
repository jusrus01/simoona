using Shrooms.Contracts.DataTransferObjects.Models.Tokens;
using System.Threading.Tasks;

namespace Shrooms.Domain.Services.Tokens
{
    public interface ITokenService
    {
        Task<TokenResponseDto> GetTokenAsync(TokenRequestDto requestDto, string tenantName);
    }
}
