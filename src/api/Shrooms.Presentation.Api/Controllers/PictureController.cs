using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shrooms.Authentication.Attributes;
using Shrooms.Contracts.Constants;
using Shrooms.Domain.Services.Picture;
using System.Drawing.Imaging;
using System.Threading.Tasks;

namespace Shrooms.Presentation.Api.Controllers
{
    [Authorize]
    [Route("Picture")]
    public class PictureController : ApplicationControllerBase
    {
        private readonly IPictureService _pictureService;

        public PictureController(IPictureService pictureService)
        {
            _pictureService = pictureService;
        }

        [PermissionAuthorize(BasicPermissions.Picture)]
        [HttpPost("Upload")]
        public async Task<IActionResult> Upload([FromForm(Name = "file0")] IFormFile picture)
        {
            //FileUpload
            //if (!Request.ContentType.IsMimeMultipartContent())
            //{
            //    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            //}

            //var provider = new MultipartMemoryStreamProvider();
            //await Request.Content.ReadAsMultipartAsync(provider);
            //var imageContent = provider.Contents[0];

            //if (imageContent.Headers.ContentLength >= WebApiConstants.MaximumPictureSizeInBytes)
            //{
            //    return BadRequest("File is too large");
            //}

            //Image image;
            //var imageStream = await provider.Contents[0].ReadAsStreamAsync();

            //try
            //{
            //    image = Image.FromStream(imageStream);
            //}
            //catch (ArgumentException)
            //{
            //    return UnsupportedMediaType();
            //}

            //if (image.RawFormat.Guid != ImageFormat.Png.Guid && image.RawFormat.Guid != ImageFormat.Gif.Guid
            //        && image.RawFormat.Guid != ImageFormat.Jpeg.Guid && image.RawFormat.Guid != ImageFormat.Bmp.Guid)
            //{
            //    return UnsupportedMediaType();
            //}

            //imageStream.Position = 0;

            //var pictureName = await _pictureService.UploadFromImageAsync(image,
            //        imageContent.Headers.ContentType.ToString(),
            //        imageContent.Headers.ContentDisposition.FileName.Replace("\"", string.Empty),
            //        GetUserAndOrganization().OrganizationId);

            //return Ok(pictureName);
            return Ok();
        }
    }
}