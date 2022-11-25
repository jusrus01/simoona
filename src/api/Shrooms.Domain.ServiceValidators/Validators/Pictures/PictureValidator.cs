using Shrooms.Contracts.Constants;
using Shrooms.Contracts.Exceptions;

namespace Shrooms.Domain.ServiceValidators.Validators.Pictures
{
    public class PictureValidator : IPictureValidator
    {
        public void CheckIfExtensionIsSupported(bool found, string extension)
        {
            if (!found)
            {
                throw new ValidationException(ErrorCodes.PictureInvalidExtensionProvided, $"{extension} does not have an assigned content type");
            }
        }

        public void CheckIfPictureNameHasExtension(string extension)
        {
            if (extension == string.Empty)
            {
                throw new ValidationException(ErrorCodes.PictureFileWithoutExtension, $"Could not find {extension} extension");
            }
        }
    }
}
