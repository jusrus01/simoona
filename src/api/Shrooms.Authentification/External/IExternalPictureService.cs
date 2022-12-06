using System.Security.Claims;
using System.Threading.Tasks;

namespace Shrooms.Authentication.External
{
    public interface IExternalPictureService
    {
        Task<string> UploadProviderImageAsync(ClaimsIdentity claimsIdentity);
    }
}
