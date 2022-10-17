using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shrooms.Contracts.Constants;
using Shrooms.Contracts.DataTransferObjects.Models.Tokens;
using Shrooms.Contracts.Exceptions;
using Shrooms.Contracts.Infrastructure;
using Shrooms.Contracts.Options;
using Shrooms.DataLayer.EntityModels.Models;
using Shrooms.Domain.Services.Permissions;
using Shrooms.Infrastructure.FireAndForget;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Shrooms.Domain.Services.Tokens
{
    public class TokenService : ITokenService
    {
        private const int SecondsMultiplier = 86400;

        private readonly IPermissionService _permissionService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITenantNameContainer _tenantNameContainer;
        private readonly ApplicationOptions _applicationOptions;

        public TokenService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IPermissionService permissionService,
            ITenantNameContainer tenantNameContainer,
            IOptions<ApplicationOptions> applicationOptions)
        {
            _userManager = userManager;
            _permissionService = permissionService;
            _signInManager = signInManager;
            _tenantNameContainer = tenantNameContainer;
            _applicationOptions = applicationOptions.Value;
        }

        public async Task<string> GetTokenForExternalAsync(ExternalLoginInfo externalLoginInfo)
        {
            var result = await _signInManager.ExternalLoginSignInAsync(externalLoginInfo.LoginProvider, externalLoginInfo.ProviderKey, false);

            if (!result.Succeeded)
            {
                throw new ValidationException(ErrorCodes.InvalidCredentials, "Invalid credentials");
            }

            var claimsIdentity = externalLoginInfo.Principal.Identity as ClaimsIdentity;
            var email = claimsIdentity.Claims.First(claim => claim.Type == ClaimTypes.Email).Value;
            var user = await _userManager.FindByEmailAsync(email);

            await _userManager.AddLoginAsync(user, externalLoginInfo);

            return (await CreateTokenAsync(user, _tenantNameContainer.TenantName)).AccessToken;
        }

        public async Task<TokenResponseDto> GetTokenAsync(TokenRequestDto requestDto)
        {
            var user = await _userManager.FindByNameAsync(requestDto.Username);

            if (user == null)
            {
                throw new ValidationException(ErrorCodes.UserNotFound, "User not found");
            }

            var isValidPassword = await _userManager.CheckPasswordAsync(user, requestDto.Password);

            if (!isValidPassword)
            {
                throw new ValidationException(ErrorCodes.InvalidCredentials, "Invalid credentials");
            }

            return await CreateTokenAsync(user, _tenantNameContainer.TenantName);
        }

        private async Task<TokenResponseDto> CreateTokenAsync(ApplicationUser user, string tenantName)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            var permissions = await _permissionService.GetUserPermissionsAsync(user.Id);

            var roleClaims = MapToClaims(ClaimTypes.Role, userRoles);
            var permissionClaims = MapToClaims(WebApiConstants.ClaimPermissions, permissions.ToList());

            // TODO: Add org id
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(WebApiConstants.ClaimOrganizationName, tenantName),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims)
            .Union(permissionClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_applicationOptions.Authentication.Jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var expirationDate = DateTime.UtcNow.AddDays(_applicationOptions.Authentication.Jwt.DurationInDays);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _applicationOptions.ClientUrl,
                audience: _applicationOptions.ClientUrl,
                claims: claims,
                expires: expirationDate,
                signingCredentials: signingCredentials);

            return new TokenResponseDto
            {
                Expires = expirationDate,
                Issued = DateTime.UtcNow,
                ClientId = _applicationOptions.ClientId,
                ExpiresIn = _applicationOptions.Authentication.Jwt.DurationInDays * SecondsMultiplier,
                TokenType = "Bearer",
                UserIndentifier = user.Id,
                Persistent = "", // TODO: Figure this out
                AccessToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken)
            };
        }

        private List<Claim> MapToClaims(string type, IList<string> claimNames)
        {
            var claims = new List<Claim>();

            foreach (var name in claimNames)
            {
                claims.Add(new Claim(type, name));
            }

            return claims;
        }
    }
}
