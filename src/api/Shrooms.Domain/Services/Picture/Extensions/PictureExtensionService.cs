using Shrooms.Contracts.Constants;
using Shrooms.Contracts.Exceptions;
using Shrooms.Infrastructure.Models;
using System.Collections.Generic;

namespace Shrooms.Domain.Services.Picture
{
    public class PictureExtensionService : IPictureExtensionService
    {
        private readonly ConstantConcurrentDictionary<string, string> _validExtensions;

        public PictureExtensionService()//Q: Feedback
        {
            _validExtensions = new ConstantConcurrentDictionary<string, string>(new Dictionary<string, string>
            {
                { ".gif", "image/gif" },
                { ".jpg", "image/jpeg" },
                { ".jpeg", "image/jpeg" },
                { ".png", "image/png" }
            });
        }

        public string GetContentTypeFromExtension(string pictureExtension)
        {
            if (!_validExtensions.TryGetValue(pictureExtension, out var contentType))
            {
                throw new ValidationException(ErrorCodes.PictureInvalidExtensionProvided, $"{pictureExtension} does not have an assigned content type");
            }

            return contentType;
        }

        public bool IsPictureExtensionSupported(string pictureExtension)
        {
            return _validExtensions.ContainsKey(pictureExtension);
        }
    }
}
