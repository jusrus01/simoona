using Shrooms.Contracts.Constants;
using Shrooms.Domain.Extensions;
using Shrooms.Domain.Services.Picture;
using System.IO;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Shrooms.Authentication.External
{
    public class ExternalPictureService : IExternalPictureService
    {
        private readonly IPictureService _pictureService;

        public ExternalPictureService(IPictureService pictureService)
        {
            _pictureService = pictureService;
        }

        public async Task<string> UploadProviderImageAsync(ClaimsIdentity claimsIdentity)
        {
            try
            {
                var imageBytes = await DownloadProviderImageAsync(claimsIdentity);
                using var memoryStream = new MemoryStream(imageBytes);
                return await _pictureService.UploadFromStreamAsync(memoryStream, MimeTypeConstants.Jpeg);
            }
            catch
            {
                return null;
            }
        }

        private static async Task<byte[]> DownloadProviderImageAsync(ClaimsIdentity claimsIdentity)
        {
            var imageUrl = claimsIdentity.GetGooglePictureUrl();

            if (imageUrl == null)
            {
                return null;
            }

            return await new WebClient().DownloadDataTaskAsync(imageUrl);
        }
    }
}
