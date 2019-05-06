using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Phone.Data.DTOs.User;
using Phone.Data.Entities.User;
using Phone.Exceptions;
using Phone.Services.User.Interfaces;
using System.Threading.Tasks;


namespace Phone.Controllers.User
{
    /// <remarks>
    /// This class-controller added for authorization user.
    /// </remarks>
    public class AuthController : ControllerBase
    {
        private IJwtService jwtService;
        private IUserService userService;

        public AuthController(IJwtService jwt, IUserService service)
        {
            jwtService = jwt;
            userService = service;
        }

        /// <summary>
        /// Authorization, authentification user
        /// <summary>
        /// <response code="200">Success</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [HttpPost]
        [Route("api/auth/login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync([FromBody]AuthLoginDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ApplicationUser user = null;
            bool userNotFound = false;
            try
            {
                user = await userService.FindUserByEmailAsync(dto.Email);
            }
            catch (CurrentEntryNotFoundException)
            {
                userNotFound = true;
            }

            if (userNotFound || !await userService.CheckPasswordAsync(user, dto.Password))
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

        /// <summary>
        /// Method for build jwt token
        /// <summary>
        private async Task<AuthTokensDto> GetBuildToken(string accessToken, string refreshToken)
        {
            return new AuthTokensDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpireOn = jwtService.ExpirationTime
            };
        }

        [HttpGet]
        [Route("api/auth/res")]
        [Authorize(Policy = "SellerShop")]
        public IActionResult RegisterAsync()
        {
            return Ok("Success");
        }

    }
}
