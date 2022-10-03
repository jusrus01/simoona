using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shrooms.Presentation.Api.Filters;

namespace Shrooms.Presentation.Api.Controllers
{
    [AllowAnonymous]
    [SkipOrganizationValidationFilter]
    [Route("Default")]
    public class DefaultController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("API is up and running");
        }
    }
}