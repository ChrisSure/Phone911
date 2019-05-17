using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Phone.Data.DTOs.User;
using Phone.Data.Entities.User;
using Phone.Services.User.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phone.Controllers.User
{
    public class AdminController : MainController
    {
        private IAdminService adminService;
        private readonly IMapper dtoMapper;

        public AdminController(IAdminService adminService)
        {
            this.adminService = adminService;
            dtoMapper = new Mapper(new MapperConfiguration(mapper =>
                {
                    mapper.CreateMap<ApplicationUser, UserViewDto>();
                }
            ));
        }

        [HttpGet]
        [Route("api/admins")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> ListAdmins()
        {
            IList<UserViewDto> admins = dtoMapper.Map<IList<ApplicationUser>, IList<UserViewDto>>(await adminService.ListAdminsAsync());
            return Ok(admins);
        }
    }
}
