using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shrooms.Contracts.DataTransferObjects.Models.Tokens;
using Shrooms.Domain.Services.Tokens;
using Shrooms.Presentation.WebViewModels.Models.Tokens;
using System.Threading.Tasks;

namespace Shrooms.Presentation.Api.Controllers
{
    [Route("Token")]
    public class TokenController : ApplicationControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public TokenController(IMapper mapper, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IActionResult> CreateToken([FromForm] TokenRequestViewModel requestViewModel)
        {
            var requestDto = _mapper.Map<TokenRequestDto>(requestViewModel);
            var reponseDto = await _tokenService.CreateTokenAsync(requestDto);
            var responseViewModel = _mapper.Map<TokenResponseViewModel>(reponseDto);

            return Ok(responseViewModel);
        }
    }
}
