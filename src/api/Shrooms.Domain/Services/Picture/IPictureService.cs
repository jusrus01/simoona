using Shrooms.Contracts.DataTransferObjects.Models.Pictures;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace Shrooms.Domain.Services.Picture
{
    public interface IPictureService
    {
        Task<string> UploadFromImageAsync(Image image, string mimeType);

        Task<string> UploadFromStreamAsync(Stream stream, string mimeType);

        Task RemoveImageAsync(string fileName);

        Task<PictureDto> GetPictureAsync(string fileName);
    }
}