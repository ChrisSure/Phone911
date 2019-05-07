using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Phone.Helpers.User;
using Phone.Services.User.Interfaces;
using System.Threading.Tasks;
using AutoMapper;
using Phone.Data.DTOs.User;
using Phone.Data.Entities.User;
using System;

namespace Phone.Controllers.User
{
    [Authorize(Roles = RoleTypes.SuperAdmin)]
    /// <remarks>
    /// This class-controller added for authorization user.
    /// </remarks>
    public class AdminController : ControllerBase
    {
        private IUserAdminService userService;
        private readonly IMapper dtoMapper;

        public AdminController(IUserAdminService service)
        {
            userService = service;
            dtoMapper = new Mapper(new MapperConfiguration(mapper =>
                {
                    mapper.CreateMap<ApplicationUser, UserProfileDto>()
                        .ForMember(d => d.Name, a => a.MapFrom(s => s.Profile.Name))
                        .ForMember(d => d.LastName, a => a.MapFrom(s => s.Profile.LastName))
                        .ForMember(d => d.SurName, a => a.MapFrom(s => s.Profile.SurName))
                        .ForMember(d => d.Sex, a => a.MapFrom(s => s.Profile.Sex))
                        .ForMember(d => d.Age, a => a.MapFrom(s => s.Profile.Age))
                        .ForMember(d => d.Description, a => a.MapFrom(s => s.Profile.Description))
                        .ForMember(d => d.Experience, a => a.MapFrom(s => s.Profile.Experience))
                        .ForMember(d => d.Phone, a => a.MapFrom(s => s.Profile.Phone))
                        .ForMember(d => d.Position, a => a.MapFrom(s => s.Profile.Position))
                        .ForMember(d => d.Salary, a => a.MapFrom(s => s.Profile.Salary));
                }
            ));
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

        [HttpGet]
        [Route("api/admin/{userId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Single([FromRoute] string userId)
        {
            var admin = await userService.GetAdminAsync(userId);
            return Ok(dtoMapper.Map<UserProfileDto>(admin));
        }
    }
}
