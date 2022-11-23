namespace Shrooms.Domain.Services.Picture
{
    public interface IPictureExtensionService
    {
        bool IsPictureExtensionSupported(string pictureExtension);

        string GetContentTypeFromExtension(string pictureExtension);
    }
}
