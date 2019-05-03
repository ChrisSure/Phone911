using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Phone.Data.DTOs.User;
using Phone.Data.Entities.User;
using Phone.Services.User.Interfaces;
using System.Threading.Tasks;

namespace Phone.Controllers.User
{

    public class AuthController : ControllerBase
    {
        private UserManager<ApplicationUser> userManager;
        private IJwtService jwtService;
        private IUserService userService;

        public AuthController(UserManager<ApplicationUser> user, IJwtService jwt, IUserService service)
        {
            userManager = user;
            jwtService = jwt;
            userService = service;
        }

        [HttpPost]
        [Route("api/auth/login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync([FromBody]AuthLoginDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ApplicationUser user = (ApplicationUser)await userService.FindUserByEmailAsync(dto.Email);
            if (user == null || !await userManager.CheckPasswordAsync(user, dto.Password))
            {
                ModelState.AddModelError("loginFailure", "Invalid email or password");
                return BadRequest(ModelState);
            } else if (user.IsBlocked ?? false)
            {
                ModelState.AddModelError("loginFailure", "Account has been blocked");
                return BadRequest(ModelState);
            }

            var userClaims = await jwtService.GetClaimsAsync(user);
            var accessToken = jwtService.GenerateJwtAccessToken(userClaims);
            var refreshToken = jwtService.GenerateJwtRefreshToken();
            await jwtService.LoginByRefreshTokenAsync(user.Id, refreshToken);
            return Ok(await GetBuildToken(accessToken, refreshToken));
        }


        [HttpGet]
        [Route("api/auth/res")]
        [Authorize(Policy = "SellerShop")]
        public IActionResult RegisterAsync()
        {
            return Ok("Success");
        }


        private async Task<AuthTokensDto> GetBuildToken(string accessToken, string refreshToken)
        {
            return new AuthTokensDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpireOn = jwtService.ExpirationTime
            };
        }

    }
}
