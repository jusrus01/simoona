using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Shrooms.Presentation.Api.Controllers
{
    [AllowAnonymous]
    [Route("Token")]
    public class TokenController : ControllerBase
    {
        // This is where asp net cookies might need to be set
        [HttpPost("Token")]
        public async Task<IActionResult> GetToken()
        {

        }
    }
}
