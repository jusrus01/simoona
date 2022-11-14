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
        private const string LocalPictureFolderName = "storage";

        private readonly ApplicationOptions _applicationOptions;

        public FileSystemStorage(IOptions<ApplicationOptions> applicationOptions)
        {
            _applicationOptions = applicationOptions.Value;
        }

        public Task RemovePictureAsync(string blobKey, string tenantPicturesContainer)
        {
            throw new NotImplementedException();
            //var filePath = HostingEnvironment.MapPath($"~/storage/{tenantPicturesContainer}/{blobKey}");
            //var fileInfo = new FileInfo(filePath);

            //if (fileInfo.Exists)
            //{
            //    fileInfo.Delete();
            //}

            //return Task.FromResult<object>(null);
        }

        public Task UploadPictureAsync(Image image, string blobKey, string mimeType, string tenantPicturesContainer)
        {
            throw new NotImplementedException();
            //var filePath = HostingEnvironment.MapPath($"~/storage/{tenantPicturesContainer}/");
            //var fullPath = Path.Combine(filePath, blobKey);
            //Directory.CreateDirectory(filePath);

            //image.Save(fullPath);

            //return Task.CompletedTask;
        }

        public async Task UploadPictureAsync(Stream stream, string blobKey, string mimeType, string tenantPicturesContainer)
        {
            var storagePath = GetStorageFolderPath(tenantPicturesContainer);
            Directory.CreateDirectory(storagePath);

            var destinationStream = File.Create(GetStorageFilePath(storagePath, blobKey));
            
            await stream.CopyToAsync(destinationStream);
        }

        private string GetStorageFolderPath(string tenantName) => $"{_applicationOptions.ContentRootPath}/{LocalPictureFolderName}/{tenantName}";

        private static string GetStorageFilePath(string storagePath, string blobKey) => $"{storagePath}/{blobKey}";
    }
}