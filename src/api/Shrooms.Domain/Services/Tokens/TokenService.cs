using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shrooms.Contracts.Constants;
using Shrooms.Contracts.DataTransferObjects.Models.Tokens;
using Shrooms.Contracts.Options;
using Shrooms.DataLayer.EntityModels.Models;
using Shrooms.Domain.Services.Organizations;
using Shrooms.Domain.Services.Permissions;
using Shrooms.Domain.Services.Users;
using Shrooms.Domain.Extensions;
using Shrooms.Domain.ServiceValidators.Validators.Organizations;
using Shrooms.Infrastructure.BackgroundJobs;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Shrooms.Contracts.Infrastructure;

namespace Shrooms.Domain.Services.Tokens
{
    public class TokenService : ITokenService
    {
        private const string TokenType = "Bearer";
        private const string AccessTokenQueryParameter = "access_token";//TODO: add specific auth schemes
        private const string AuthTypeParameterName = "authType";

        private readonly ApplicationOptions _applicationOptions;

        private readonly IApplicationUserManager _userManager;
        private readonly IApplicationSignInManager _signInManager;

        private readonly IPermissionService _permissionService;
        private readonly IOrganizationService _organizationService;
        private readonly IOrganizationValidator _organizationValidator;
        private readonly ITenantNameContainer _tenantNameContainer;

        public TokenService(
            IOptions<ApplicationOptions> applicationOptions,
            IApplicationUserManager userManager,
            IApplicationSignInManager signInManager,
            IPermissionService permissionService,
            IOrganizationService organizationService,
            ITenantNameContainer tenantNameContainer,
            IOrganizationValidator organizationValidator)
        {
            _applicationOptions = applicationOptions.Value;

            _userManager = userManager;
            _permissionService = permissionService;
            _signInManager = signInManager;
            _organizationService = organizationService;
            _tenantNameContainer = tenantNameContainer;
            _organizationValidator = organizationValidator;
        }

        public async Task<string> GetTokenRedirectUrlForExternalAsync(ExternalLoginInfo externalLoginInfo)
        {
            var token = await GetTokenForExternalAsync(externalLoginInfo);
            var uri = _applicationOptions.GetClientLoginUrl(_tenantNameContainer.TenantName);

            return $"{QueryHelpers.AddQueryString(uri, AuthTypeParameterName, externalLoginInfo.ProviderDisplayName)}#{AccessTokenQueryParameter}={token}";
        }

        public async Task<TokenResponseDto> GetTokenAsync(TokenRequestDto requestDto)
        {
            var user = await _userManager.FindByNameAsync(requestDto.Username);
            await _userManager.CheckPasswordAsync(user, requestDto.Password);
            return await CreateTokenAsync(user);
        }

        private async Task<string> GetTokenForExternalAsync(ExternalLoginInfo externalLoginInfo)
        {
            await _signInManager.ExternalLoginSignInAsync(externalLoginInfo);

            var claimsIdentity = externalLoginInfo.Principal.Identity as ClaimsIdentity;
            var email = claimsIdentity.GetEmail();
            var user = await _userManager.FindByEmailAsync(email);
            return (await CreateTokenAsync(user)).AccessToken;
        }

        private async Task<TokenResponseDto> CreateTokenAsync(ApplicationUser user)
        {
            var organization = await _organizationService.GetUserOrganizationAsync(user);

            _organizationValidator.CheckIfFoundOrganizationMatchesRequestHeader(organization, _tenantNameContainer.TenantName);

            var claims = await CreateRequiredTokenClaimsAsync(user, organization);
            var expirationSpan = TimeSpan.FromDays(_applicationOptions.Authentication.Jwt.DurationInDays);
            var expirationDate = DateTime.UtcNow.Add(expirationSpan);
            var accessToken = GenerateAccessToken(claims, expirationDate);

            return new TokenResponseDto
            {
                Expires = expirationDate,
                Issued = DateTime.UtcNow,
                ClientId = _applicationOptions.ClientId,
                ExpiresIn = Convert.ToInt32(expirationSpan.TotalSeconds),
                TokenType = TokenType,
                UserIndentifier = user.Id,
                Persistent = string.Empty,
                AccessToken = accessToken
            };
        }

        private string GenerateAccessToken(IEnumerable<Claim> claims, DateTime expirationDate)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_applicationOptions.Authentication.Jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _applicationOptions.ApiUrl,
                audience: _applicationOptions.ApiUrl,
                claims: claims,
                expires: expirationDate,
                signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }

        private async Task<IEnumerable<Claim>> CreateRequiredTokenClaimsAsync(ApplicationUser user, Organization organization)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            var permissions = await _permissionService.GetUserPermissionsAsync(user.Id);

            var roleClaims = MapToClaims(ClaimTypes.Role, userRoles);
            var permissionClaims = MapToClaims(WebApiConstants.ClaimPermission, permissions.ToList());

            return new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.GivenName, user.FullName),
                new Claim(WebApiConstants.ClaimOrganizationName, organization.Name),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(WebApiConstants.ClaimOrganizationId, organization.Id.ToString())
            }
            .Union(userClaims)
            .Union(roleClaims)
            .Union(permissionClaims);
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
