using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Phone.Data.Entities.User;
using Phone.Repositories.User.Interfaces;
using Phone.Services.User.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace Phone.Services.User
{
    public class JwtService : IJwtService
    {
        private readonly IUserRefreshTokenRepository refreshRepository;
        private IUserService userService;
        private readonly IConfiguration configuration;

        public JwtService(IUserRefreshTokenRepository refreshRepository, IUserService userService, IConfiguration configuration)
        {
            this.refreshRepository = refreshRepository;
            this.userService = userService;
            this.configuration = configuration;
        }

        public DateTime ExpirationTime => DateTime.Now.AddMinutes(120);

        /// <summary>
        /// Method set user claims
        /// <summary>
        /// <param name="userInfo"><see cref="ApplicationUser"/></param>
        /// <returns>Claim[]</returns>
        public async Task<Claim[]> GetClaimsAsync(ApplicationUser userInfo)
        {
            var roles = await userService.GetUserRolesAsync(userInfo);

            var claims = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.UserName),
                new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("uid", userInfo.Id)
            };
            claims.Add(new Claim(ClaimTypes.Role, roles[0]));

            return claims.ToArray();
        }

        /// <summary>
        /// Method for generation access token for user
        /// <summary>
        /// <param name="claims">IEnumerable<Claim></param>
        /// <returns>string</returns>
        public string GenerateJwtAccessToken(IEnumerable<Claim> claims)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
              issuer: configuration["Jwt:Issuer"],
              audience: configuration["Jwt:Audience"],
              claims: claims,
              expires: ExpirationTime,
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Method for generation refresh token for user
        /// <summary>
        /// <returns>string</returns>
        public string GenerateJwtRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        /// <summary>
        /// Method for login user's refresh token
        /// <summary>
        /// <param name="userId">string</param>
        /// <param name="refreshToken">string</param>
        /// <returns>void</returns>
        public async Task LoginByRefreshTokenAsync(string userId, string refreshToken)
        {
            var userRefreshToken = await refreshRepository.GetByUserIdAsync(userId);
            if (userRefreshToken != null)
            {
                userRefreshToken.RefreshToken = refreshToken;
                userRefreshToken.ExpireOn = DateTime.Now.AddMonths(3);
                await refreshRepository.UpdateAsync(userRefreshToken);
            }
            else
            {
                userRefreshToken = new UserRefreshToken
                {
                    UserId = userId,
                    RefreshToken = refreshToken,
                    ExpireOn = DateTime.Now.AddMonths(3)
                };
                await refreshRepository.CreateAsync(userRefreshToken);
            }
        }

    }
}
