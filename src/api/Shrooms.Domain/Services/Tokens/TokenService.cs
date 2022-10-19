using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shrooms.Contracts.Constants;
using Shrooms.Contracts.DataTransferObjects.Models.Tokens;
using Shrooms.Contracts.Exceptions;
using Shrooms.Contracts.Options;
using Shrooms.DataLayer.EntityModels.Models;
using Shrooms.Domain.Services.Organizations;
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
        private const int SecondsInADay = 86400;

        private readonly ApplicationOptions _applicationOptions;

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        private readonly IPermissionService _permissionService;
        private readonly IOrganizationService _organizationService;
        private readonly ITenantNameContainer _tenantNameContainer;

        public TokenService(
            IOptions<ApplicationOptions> applicationOptions,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IPermissionService permissionService,
            IOrganizationService organizationService,
            ITenantNameContainer tenantNameContainer)
        {
            _applicationOptions = applicationOptions.Value;

            _userManager = userManager;
            _permissionService = permissionService;
            _signInManager = signInManager;
            _organizationService = organizationService;
            _tenantNameContainer = tenantNameContainer;
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

            return (await CreateTokenAsync(user)).AccessToken;
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

            return await CreateTokenAsync(user);
        }

        private async Task<TokenResponseDto> CreateTokenAsync(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            var permissions = await _permissionService.GetUserPermissionsAsync(user.Id);

            var roleClaims = MapToClaims(ClaimTypes.Role, userRoles);
            var permissionClaims = MapToClaims(WebApiConstants.ClaimPermission, permissions.ToList());

            var organization = await _organizationService.GetUserOrganizationAsync(user);

            if (_tenantNameContainer.TenantName.ToLowerInvariant() != organization.ShortName.ToLowerInvariant())
            {
                throw new ValidationException(ErrorCodes.UserNotFound, "User not found");
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(WebApiConstants.ClaimOrganizationName, organization.Name),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(WebApiConstants.ClaimOrganizationId, organization.Id.ToString())
            }
            .Union(userClaims)
            .Union(roleClaims)
            .Union(permissionClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_applicationOptions.Authentication.Jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var expirationDate = DateTime.UtcNow.AddDays(_applicationOptions.Authentication.Jwt.DurationInDays);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _applicationOptions.ApiUrl,
                audience: _applicationOptions.ApiUrl,
                claims: claims,
                expires: expirationDate,
                signingCredentials: signingCredentials);

            return new TokenResponseDto
            {
                Expires = expirationDate,
                Issued = DateTime.UtcNow,
                ClientId = _applicationOptions.ClientId,
                ExpiresIn = _applicationOptions.Authentication.Jwt.DurationInDays * SecondsInADay,
                TokenType = "Bearer",
                UserIndentifier = user.Id,
                Persistent = "",
                AccessToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken)
            };
        }

        private static List<Claim> MapToClaims(string type, IList<string> claimNames)
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
