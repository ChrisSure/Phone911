using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Phone.Data.DTOs.User;
using Phone.Data.Entities.User;
using Phone.Exceptions;
using Phone.Services.User.Interfaces;
using System.Linq;
using System.Threading.Tasks;


namespace Phone.Controllers.User
{
    /// <remarks>
    /// This class-controller added for authorization user.
    /// </remarks>
    public class AuthController : ControllerBase
    {
        private IJwtService jwtService;
        private IUserAuthService userService;

        public AuthController(IJwtService jwt, IUserAuthService service)
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
        public async Task<IActionResult> LoginAsync([FromBody]UserAuthDto dto)
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

            if (userNotFound || !await userService.CheckPasswordAsync(user, dto.Password) || await userService.IsCustomer(user))
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

        [HttpPost]
        [Route("api/auth/refresh")]
        [AllowAnonymous]
        public async Task<IActionResult> Refresh([FromBody]AuthTokensDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var principal = jwtService.GetPrincipalFromExpiredAccessToken(dto.AccessToken);
            if (principal == null)
            {
                ModelState.AddModelError("loginFailure", "Failure tokrn");
                return BadRequest(ModelState);
            }

            var user = await userService.GetUserByIdAsync(principal.Claims.Single(claim => claim.Type == "uid").Value);

            if (user.IsBlocked == true)
            {
                ModelState.AddModelError("loginFailure", "Account has been blocked");
                return BadRequest(ModelState);
            }

            var userClaims = await jwtService.GetClaimsAsync(user);

            dto.AccessToken = jwtService.GenerateJwtAccessToken(userClaims);
            dto.RefreshToken = await jwtService.UpdateRefreshTokenAsync(dto.RefreshToken, principal);
            dto.ExpireOn = jwtService.ExpirationTime;

            return Ok(dto);
        }

        [HttpPost]
        [Route("api/auth/logout")]
        [AllowAnonymous]
        public async Task<IActionResult> Logout([FromBody]AuthTokensDto dto)
        {
            var principal = jwtService.GetPrincipalFromExpiredAccessToken(dto.AccessToken);
            await jwtService.DeleteRefreshTokenAsync(principal);
            return Ok();
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
