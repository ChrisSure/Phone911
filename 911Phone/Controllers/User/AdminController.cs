using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Phone.Helpers.User;
using Phone.Services.User.Interfaces;
using System.Threading.Tasks;
using AutoMapper;
using Phone.Data.DTOs.User;
using System;
using Phone.Data.Entities.User;
using System.Collections.Generic;

namespace Phone.Controllers.User
{
    //[Authorize(Roles = RoleTypes.SuperAdmin)]
    /// <remarks>
    /// This class-controller added for authorization user.
    /// </remarks>
    public class AdminController : ControllerBase
    {
        private IUserAdminService userService;
        private IProfileService profileService;
        private readonly IMapper dtoMapper;

        public AdminController(IUserAdminService userService, IProfileService profileService)
        {
            this.userService = userService;
            this.profileService = profileService;
            dtoMapper = new Mapper(new MapperConfiguration(mapper =>
                {
                    mapper.CreateMap<ApplicationUser, UserCreateDto>().ReverseMap().ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password));
                    mapper.CreateMap<Data.Entities.User.Profile, ProfileInfoDto>();
                    mapper.CreateMap<ApplicationUser, UserViewDto>();
                }
            ));
        }


        [HttpGet]
        [Route("api/admin")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> List()
        {
            IList<UserViewDto> users = dtoMapper.Map<IList<ApplicationUser>, IList<UserViewDto>>(await userService.ListAdminsAsync());
            return Ok(users);
        }

        [HttpGet]
        [Route("api/admin/{userId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Single([FromRoute] string userId)
        {
            var admin = await userService.GetAdminAsync(userId);
            var profile = dtoMapper.Map<ProfileInfoDto>(await profileService.GetProfileByUserId(userId));
            var role = await userService.GetRoleByUserId(admin);
            return Ok(new { UserInfo = dtoMapper.Map<UserViewDto>(admin), ProfileInfo = profile, RoleInfo = role });
        }

        [HttpPost]
        [Route("api/admin")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Create([FromBody] UserCreateDto userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var itemModel = dtoMapper.Map<UserCreateDto, ApplicationUser>(userDto);
            await userService.CreateUserAsync(itemModel);

            return Created(
                this.BaseApiUrl + "/" + itemModel.Id,
                new { UserId = itemModel.Id }
            );
        }



        public virtual string BaseApiUrl
        {
            get
            {
                var request = ControllerContext.HttpContext.Request;
                return request.Scheme + "://" + request.Host + request.Path;
            }
        }
    }
}
