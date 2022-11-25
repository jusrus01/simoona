using Shrooms.Domain.ServiceValidators.Validators.Pictures;
using Shrooms.Infrastructure.Models;
using System.Collections.Generic;
using System.IO;

namespace Shrooms.Domain.Services.Picture
{
    public class PictureContentTypeService : IPictureContentTypeService
    {
        private readonly ConstantConcurrentDictionary<string, string> _validExtensions;
        private readonly IPictureValidator _validator;

        public PictureContentTypeService(IPictureValidator validator)//Q: Feedback
        {
            _validExtensions = new ConstantConcurrentDictionary<string, string>(new Dictionary<string, string>
            {
                { ".gif", "image/gif" },
                { ".jpg", "image/jpeg" },
                { ".jpeg", "image/jpeg" },
                { ".png", "image/png" }
            });

            _validator = validator;
        }

        public string GetPictureContentType(string pictureName)
        {
            var pictureExtension = ExtractPictureExtension(pictureName);
            var found = _validExtensions.TryGetValue(pictureExtension, out var contentType);
            _validator.CheckIfExtensionIsSupported(found, pictureExtension);
            return contentType;
        }

        private string ExtractPictureExtension(string pictureName)
        {
            var extension = Path.GetExtension(pictureName);
            _validator.CheckIfPictureNameHasExtension(extension);
            return extension;
        }
    }
}
