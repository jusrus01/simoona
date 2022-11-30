using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shrooms.Contracts.Constants;
using Shrooms.Domain.Services.Picture;
using System.Threading.Tasks;

namespace Shrooms.Presentation.Api.Controllers
{
    [Authorize(Policy = PolicyConstants.StoragePolicy)]
    [Route("Storage")]
    public class StorageController : ControllerBase
    {
        private readonly IPictureService _pictureService;

        public StorageController(IPictureService pictureService)
        {
            _pictureService = pictureService;
        }

        [HttpGet("{organizationName}/{pictureName}")]
        public async Task<IActionResult> GetPicture(string pictureName) // TODO: Add width, height and mode parameters when image processing library is decided
        {
            var pictureDto = await _pictureService.GetPictureAsync(pictureName);
            return File(pictureDto.Content, pictureDto.ContentType);
        }
    }
}
