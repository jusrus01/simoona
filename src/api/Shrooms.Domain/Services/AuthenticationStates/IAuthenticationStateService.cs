namespace Shrooms.Domain.Services.AuthenticationStates
{
    public interface IAuthenticationStateService
    {
        string GenerateExternalAuthenticationState();
    }
}
