namespace Shrooms.Domain.ServiceValidators.Validators.Pictures
{
    public interface IPictureValidator
    {
        void CheckIfPictureNameHasExtension(string extension);
    }
}
