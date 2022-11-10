using System.Threading.Tasks;

namespace Shrooms.Domain.Services.Cookies
{
    public interface ICookieService
    {
        Task SetExternalCookieAsync();
    }
}
