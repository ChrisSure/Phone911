using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Phone.Data.DTOs.User;
using Phone.Data.Entities.User;
using System.Threading.Tasks;

namespace Phone.Controllers.User
{

    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private UserManager<ApplicationUser> userManager;

        public AuthController(UserManager<ApplicationUser> user)
        {
            userManager = user;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody]AuthLoginDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ApplicationUser user = await userManager.FindByEmailAsync(dto.Email);

            if (await userManager.CheckPasswordAsync(user, dto.Password))
            {
                return Ok("Success");
            }

            return BadRequest("Error");
        }

    }
}
