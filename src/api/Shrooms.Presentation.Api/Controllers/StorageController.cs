using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shrooms.Contracts.Constants;

namespace Shrooms.Presentation.Api.Controllers
{
    [Authorize(Policy = PolicyConstants.StoragePolicy)]
    [Route("Storage")]
    public class StorageController : ShroomsControllerBase
    {
        public IActionResult test()
        {
            return Ok();
        }
    }
}
