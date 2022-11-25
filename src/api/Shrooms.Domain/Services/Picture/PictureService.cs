using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shrooms.Contracts.DAL;
using Shrooms.Contracts.DataTransferObjects.Models.Pictures;
using Shrooms.DataLayer.EntityModels.Models;
using Shrooms.Infrastructure.Storage;

namespace Shrooms.Domain.Services.Picture
{
    public class PictureService : IPictureService
    {
        private readonly IPictureContentTypeService _pictureContentTypeService;
        private readonly IStorage _storage;
        private readonly DbSet<Organization> _organizationsDbSet;

        public PictureService(
            IStorage storage,
            IPictureContentTypeService pictureContentTypeService,
            IUnitOfWork2 uow)
        {
            _pictureContentTypeService = pictureContentTypeService;
            _storage = storage;
            _organizationsDbSet = uow.GetDbSet<Organization>();
        }

        public async Task<string> UploadFromImageAsync(Image image, string mimeType, string fileName, int orgId)
        {
            var pictureName = GetNewPictureName(fileName);
            var tenantPicturesContainer = await GetPictureContainerAsync(orgId);

            await _storage.UploadPictureAsync(image, pictureName, mimeType, tenantPicturesContainer);

            return pictureName;
        }

        public async Task<string> UploadFromStreamAsync(Stream stream, string mimeType, string fileName, int orgId)
        {
            var pictureName = GetNewPictureName(fileName);
            var tenantPicturesContainer = await GetPictureContainerAsync(orgId);

            await _storage.UploadPictureAsync(stream, pictureName, mimeType, tenantPicturesContainer);

            return pictureName;
        }

        public async Task RemoveImageAsync(string blobKey, int orgId)
        {
            var tenantPicturesContainer = await GetPictureContainerAsync(orgId);

            await _storage.RemovePictureAsync(blobKey, tenantPicturesContainer);
        }

        public async Task<PictureDto> GetPictureAsync(string organizationName, string pictureName)
        {
            var contentType = _pictureContentTypeService.GetPictureContentType(pictureName);
            var pictureBytes = await _storage.GetPictureAsync(pictureName, organizationName);
            return MapToPictureDto(pictureBytes, contentType);
        }

        private static PictureDto MapToPictureDto(byte[] content, string contentType)
        {
            return new PictureDto
            {
                Content = content,
                ContentType = contentType,
            };
        }

        private static string GetNewPictureName(string fileName)
        {
            var id = Guid.NewGuid().ToString();
            var extension = Path.GetExtension(fileName)?.ToLowerInvariant();

            return $"{id}{extension}";
        }

        private async Task<string> GetPictureContainerAsync(int id)
        {
            var organization = await _organizationsDbSet.FirstAsync(x => x.Id == id);

            return organization.ShortName.ToLowerInvariant();
        }
    }
}