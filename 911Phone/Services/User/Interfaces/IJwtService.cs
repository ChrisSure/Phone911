using Phone.Data.Entities.User;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Phone.Services.User.Interfaces
{
    public interface IJwtService
    {
        DateTime ExpirationTime { get; }
        string GenerateJwtAccessToken(IEnumerable<Claim> claims);
        Task<Claim[]> GetClaimsAsync(ApplicationUser userInfo);
        string GenerateJwtRefreshToken();
        Task LoginByRefreshTokenAsync(string userId, string refreshToken);
        ClaimsPrincipal GetPrincipalFromExpiredAccessToken(string accessToken);
        Task DeleteRefreshTokenAsync(ClaimsPrincipal userPrincipal);
        Task<string> UpdateRefreshTokenAsync(string oldRefreshTokenPlain, ClaimsPrincipal userPrincipal);
    }
}
