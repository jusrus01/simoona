using Shrooms.Contracts.Constants;
using Shrooms.Contracts.Exceptions;

namespace Shrooms.Domain.ServiceValidators.Validators.Pictures
{
    public class PictureValidator : IPictureValidator
    {
        public void CheckIfPictureNameHasExtension(string extension)
        {
            if (extension == string.Empty)
            {
                throw new ValidationException(ErrorCodes.PictureFileWithoutExtension, "Could not find extension");
            }
        }
    }
}
