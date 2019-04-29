using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Phone.Data.DTOs.User;
using Phone.Data.Entities.User;
using Phone.Exceptions;
using Phone.Services.User.Interfaces;
using System.Threading.Tasks;

namespace Phone.Controllers.User
{

    public class AuthController : ControllerBase
    {
        private UserManager<ApplicationUser> userManager;
        private IJwtService jwtService;

        public AuthController(UserManager<ApplicationUser> user, IJwtService jwt)
        {
            userManager = user;
            jwtService = jwt;
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

            ApplicationUser user = null;
            bool userNotFound = false;
            try
            {
                user = await userManager.FindByEmailAsync(dto.Email);
            }
            catch (CurrentEntryNotFoundException)
            {
                userNotFound = true;
            }

            if (userNotFound || !await userManager.CheckPasswordAsync(user, dto.Password))
            {
                ModelState.AddModelError("loginFailure", "Invalid email or password");
                return BadRequest(ModelState);
            }

            if (user.IsBlocked ?? false)
            {
                ModelState.AddModelError("loginFailure", "Account has been blocked");
                return BadRequest(ModelState);
            }

            var userClaims = await jwtService.GetClaimsAsync(user);
            var accessToken = jwtService.GenerateJwtAccessToken(userClaims);

            var tokens = new AuthTokensDto
            {
                AccessToken = accessToken,
                ExpireOn = jwtService.ExpirationTime
            };

            return Ok(tokens);
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
