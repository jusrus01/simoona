using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shrooms.Contracts.DataTransferObjects.Models.Tokens;
using Shrooms.Domain.Services.Tokens;
using Shrooms.Presentation.WebViewModels.Models.Tokens;
using System;
using System.Threading.Tasks;

// TEMPORARY IMPLEMENTATION
namespace Shrooms.Presentation.Api.Controllers
{
    [Route("Token")]// SignIn function returns asp.net. cookie and some other things...
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public TokenController(IMapper mapper, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _mapper = mapper;
        }

        // This is where asp net cookies might need to be set ???
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> GetToken([FromForm] TokenRequestViewModel requestViewModel) // TODO: map to dto
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model invalid");
            }

            try
            {
                var requestDto = _mapper.Map<TokenRequestDto>(requestViewModel);
                var reponseDto = await _tokenService.GetTokenAsync(requestDto);
                var responseViewModel = _mapper.Map<TokenResponseViewModel>(reponseDto);

                return Ok(responseViewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
