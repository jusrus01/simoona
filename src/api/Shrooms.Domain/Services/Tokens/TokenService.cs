using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shrooms.Contracts.DataTransferObjects.Models.Tokens;
using Shrooms.DataLayer.EntityModels.Models;
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
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;

        public TokenService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<TokenResponseDto> GetTokenAsync(TokenRequestDto requestDto)
        {
            // TODO: recreate
            var user = await _userManager.FindByNameAsync(requestDto.Username);

            if (user == null)
            {
                throw new Exception();
            }

            var isValidPassword = await _userManager.CheckPasswordAsync(user, requestDto.Password);

            if (!isValidPassword)
            {
                throw new Exception();
            }

            var bearerToken = await CreateBearerTokenAsync(user);

            return new TokenResponseDto
            {
                AccessToken = bearerToken,
                UserIndentifier = user.Id
            };
        }

        private async Task<string> CreateBearerTokenAsync(ApplicationUser user)
        {
            // TODO: recreate
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(item: new Claim("roles", roles[i]));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: System.DateTime.UtcNow.AddMinutes(60),
                signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }
    }
}
