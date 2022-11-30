using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Shrooms.Contracts.Constants;
using Shrooms.Contracts.Infrastructure;
using Shrooms.Contracts.Options;
using Shrooms.Infrastructure.BackgroundJobs;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Shrooms.Domain.Services.Cookies
{
    public class CookieService : ICookieService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITenantNameContainer _tenantNameContainer;
        private readonly JwtAuthenticationOptions _jwtOptions; // Only used to specify the same lifetime

        public CookieService(
            IHttpContextAccessor httpContextAccessor,
            IOptions<JwtAuthenticationOptions> jwtOptions,
            ITenantNameContainer tenantNameContainer)
        {
            _httpContextAccessor = httpContextAccessor;
            _tenantNameContainer = tenantNameContainer;
            _jwtOptions = jwtOptions.Value;
        }

        public async Task RemoveExternalCookieAsync()
        {
            await ClearExternalCookieAsync();
        }

        public async Task SetExternalCookieAsync()
        {
            await ClearExternalCookieAsync();
            var claimsIdentity = CreateClaimsIdentity();
            var properties = CreateAuthenticationProperties();
            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);
        }

        private Task ClearExternalCookieAsync()
        {
            return _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        private ClaimsIdentity CreateClaimsIdentity()
        {
            var claims = new List<Claim>
            {
                new Claim(WebApiConstants.ClaimOrganizationName, _tenantNameContainer.TenantName)
            };

            return new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        }

        private AuthenticationProperties CreateAuthenticationProperties()
        {
            var timestamp = DateTime.UtcNow;
            return new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = timestamp.AddDays(_jwtOptions.DurationInDays),
                IsPersistent = true,
                IssuedUtc = timestamp
            };
        }
    }
}
