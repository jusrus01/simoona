using System.Security.Claims;
using System.Threading.Tasks;

namespace Shrooms.Domain.Services.ExternalProviders
{
    public interface IExternalPictureService
    {
        Task<string> UploadProviderImageAsync(ClaimsIdentity claimsIdentity);
    }
}
