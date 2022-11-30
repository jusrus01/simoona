using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Shrooms.Contracts.Options;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Shrooms.Domain.Services.Cookies
{
    public class CookieService : ICookieService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly JwtAuthenticationOptions _jwtOptions; // TODO: Create cookie

        public CookieService(IOptions<JwtAuthenticationOptions> jwtOptions, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _jwtOptions = jwtOptions.Value;
        }

        public async Task RemoveExternalCookieAsync()
        {
            await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public async Task SetExternalCookieAsync()
        {
            var claimsIdentity = new ClaimsIdentity(
              new List<Claim>(),
              CookieAuthenticationDefaults.AuthenticationScheme);

            var timestamp = DateTime.UtcNow;
            var properties = new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = timestamp.AddDays(_jwtOptions.DurationInDays),
                IsPersistent = true,
                IssuedUtc = timestamp
            };

            await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);
        }
    }
}
