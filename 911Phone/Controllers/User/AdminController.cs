using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Phone.Helpers.User;
using Phone.Services.User.Interfaces;
using System.Threading.Tasks;

namespace Phone.Controllers.User
{
    [Authorize(Roles = RoleTypes.SuperAdmin)]
    /// <remarks>
    /// This class-controller added for authorization user.
    /// </remarks>
    public class AdminController : ControllerBase
    {
        private IUserAdminService userService;

        public AdminController(IUserAdminService service)
        {
            userService = service;
        }


        [HttpGet]
        [Route("api/admin")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> List()
        {
            var admins = await userService.ListAdminsAsync();
            return Ok(admins);
        }
    }
}
