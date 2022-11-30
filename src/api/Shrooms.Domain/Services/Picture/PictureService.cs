using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Shrooms.Contracts.DataTransferObjects.Models.Pictures;
using Shrooms.Infrastructure.FireAndForget;
using Shrooms.Infrastructure.Storage;
using Shrooms.Contracts.Exceptions;
using Shrooms.Contracts.Constants;

namespace Shrooms.Domain.Services.Picture
{
    public class PictureService : IPictureService
    {
        private readonly ITenantNameContainer _tenantNameContainer;
        private readonly IStorage _storage;

        public PictureService(ITenantNameContainer tenantNameContainer, IStorage storage)
        {
            _storage = storage;
            _tenantNameContainer = tenantNameContainer;
        }

        public async Task RemoveImageAsync(string fileName)
        {
            await _storage.RemovePictureAsync(fileName, _tenantNameContainer.TenantName);
        }

        public async Task<PictureDto> GetPictureAsync(string fileName)
        {
            var bytes = await _storage.GetPictureAsync(fileName, _tenantNameContainer.TenantName);
            return MapToPictureDto(fileName, bytes);
        }

        public async Task<string> UploadFromFormFileAsync(IFormFile picture)
        {
            throw new NotImplementedException();
        }

        public async Task<string> UploadFromImageAsync(Image image, string mimeType)
        {
            var pictureName = GenerateUniquePictureName(mimeType);
            await _storage.UploadPictureAsync(image, pictureName, mimeType, _tenantNameContainer.TenantName);
            return pictureName;
        }

        public async Task<string> UploadFromStreamAsync(Stream stream, string mimeType)
        {
            var pictureName = GenerateUniquePictureName(mimeType);
            await _storage.UploadPictureAsync(stream, pictureName, mimeType, _tenantNameContainer.TenantName);
            return pictureName;
        }

        private static PictureDto MapToPictureDto(string fileName, byte[] bytes)
        {
            return new PictureDto
            {
                Content = bytes,
                ContentType = GetContentType(fileName)
            };
        }

        private static string GenerateUniquePictureName(string contentType)
        {
            return $"{Guid.NewGuid()}{GetPictureExtension(contentType)}";
        }

        private static string GetPictureExtension(string mimeType)
        {
            return mimeType switch
            {
                MimeTypeConstants.Jpeg => FileExtensionConstants.Jpg,
                MimeTypeConstants.Png => FileExtensionConstants.Png,
                MimeTypeConstants.Gif => FileExtensionConstants.Gif,
                _ => throw new ValidationException(ErrorCodes.PictureInvalidContentType, $"Mime type {mimeType} is not supported")
            };
        }

        private static string GetContentType(string fileName)
        {
            var extension = Path.GetExtension(fileName);
            return extension switch
            {
                FileExtensionConstants.Jpg => MimeTypeConstants.Jpeg,
                FileExtensionConstants.Jpeg => MimeTypeConstants.Jpeg,
                FileExtensionConstants.Png => MimeTypeConstants.Png,
                FileExtensionConstants.Gif => MimeTypeConstants.Gif,
                _ => throw new ValidationException(ErrorCodes.PictureCorruptName, $"Could not retrieve content type from {fileName} file")
            };
        }
    }
}