using Microsoft.Extensions.Options;
using Shrooms.Contracts.Options;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace Shrooms.Infrastructure.Storage.FileSystem
{
    public class FileSystemStorage : IStorage
    {
        private const string PictureContainerName = "storage";

        private readonly ApplicationOptions _applicationOptions;

        public FileSystemStorage(IOptions<ApplicationOptions> applicationOptions)
        {
            _applicationOptions = applicationOptions.Value;
        }

        public Task RemovePictureAsync(string blobKey, string tenantPicturesContainer)
        {
            var filePath = GetStorageFilePathFromKey(blobKey, tenantPicturesContainer);
            
            if (!File.Exists(filePath))
            {
                return Task.CompletedTask;
            }
            File.Delete(filePath);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Should not be used for production. Does not perform signature validation or any kind of validation.
        /// </summary>
        public Task UploadPictureAsync(Image image, string blobKey, string mimeType, string tenantPicturesContainer)
        {
            throw new NotImplementedException();
            //var filePath = HostingEnvironment.MapPath($"~/storage/{tenantPicturesContainer}/");
            //var fullPath = Path.Combine(filePath, blobKey);
            //Directory.CreateDirectory(filePath);

            //image.Save(fullPath);

            //return Task.CompletedTask;
        }

        public async Task<byte[]> GetPictureAsync(string fileName, string tenantPicturesContainer)
        {
            var filePath = GetStorageFilePathFromKey(fileName, tenantPicturesContainer);
            return await File.ReadAllBytesAsync(filePath);
        }

        /// <summary>
        /// Should not be used for production. Does not perform signature validation or any kind of validation.
        /// </summary>
        public async Task UploadPictureAsync(Stream stream, string blobKey, string mimeType, string tenantPicturesContainer)
        {
            var storagePath = GetStorageFolderPath(tenantPicturesContainer);
            Directory.CreateDirectory(storagePath);

            var filePath = GetStorageFilePath(storagePath, blobKey);

            using var destinationStream = File.Create(filePath);

            stream.Seek(0, SeekOrigin.Begin);
            
            await stream.CopyToAsync(destinationStream);
        }

        private string GetStorageFolderPath(string tenantPictureContainer) => $"{_applicationOptions.ContentRootPath}/{PictureContainerName}/{tenantPictureContainer}";
        
        private string GetStorageFilePathFromKey(string blobKey, string tenantName) => GetStorageFilePath(GetStorageFolderPath(tenantName), blobKey);

        private static string GetStorageFilePath(string storagePath, string blobKey) => $"{storagePath}/{blobKey}";
    }
}