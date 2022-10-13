using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shrooms.Contracts.DataTransferObjects.Models.Tokens;
using Shrooms.Contracts.Exceptions;
using Shrooms.Domain.Services.Tokens;
using Shrooms.Presentation.WebViewModels.Models.Tokens;
using System.Threading.Tasks;

namespace Shrooms.Presentation.Api.Controllers
{
    [Route("Token")]
    public class TokenController : ShroomsControllerBase
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
        public async Task<IActionResult> GetToken([FromBody] TokenRequestViewModel requestViewModel) // Unable to map underscores
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var requestDto = _mapper.Map<TokenRequestDto>(requestViewModel);
                var reponseDto = await _tokenService.GetTokenAsync(requestDto, Request.Headers["Organization"]);
                var responseViewModel = _mapper.Map<TokenResponseViewModel>(reponseDto);

                return Ok(responseViewModel);
            }
            catch (ValidationException e)
            {
                return BadRequestWithError(e);
            }
        }
        
        [Authorize]
        [HttpPost("Test")]
        public IActionResult Test()
        {
            return Ok();
        }
    }
}
