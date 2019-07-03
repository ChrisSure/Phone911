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
using ProfileNamespace = Phone.Data.Entities.User;


namespace Phone.Controllers.User
{
    /// <remarks>
    /// This class-controller added for authorization user.
    /// </remarks>
    //[Authorize(Roles = RoleTypes.SuperAdmin)]
    public class UserController : MainController
    {
        private IUserService userService;
        private IProfileService profileService;
        private readonly IMapper dtoMapper;

        public UserController(IUserService userService, IProfileService profileService)
        {
            this.userService = userService;
            this.profileService = profileService;
            dtoMapper = new Mapper(new MapperConfiguration(mapper =>
                {
                    mapper.CreateMap<ApplicationUser, UserCreateDto>().ReverseMap()
                        .ForMember(user => user.PasswordHash, opt => opt.MapFrom(src => src.Password));
                    mapper.CreateMap<ProfileNamespace.Profile, ProfileInfoDto>();
                    mapper.CreateMap<ProfileNamespace.Profile, ProfileCreatedDto>().ReverseMap();
                    mapper.CreateMap<ApplicationUser, UserViewDto>();
                }
            ));
        }

        [HttpGet]
        [Route("api/user/{userId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Single([FromRoute] string userId)
        {
            var admin = await userService.GetUserByIdAsync(userId);
            var profile = dtoMapper.Map<ProfileInfoDto>(await profileService.GetProfileByUserId(userId));
            var role = await userService.GetRoleByUserId(admin);
            return Ok(new UserSingleDto { UserInfo = dtoMapper.Map<UserViewDto>(admin), ProfileInfo = profile, RoleInfo = role });
        }

        [HttpPost]
        [Route("api/user")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Create([FromBody] UserCreateDto userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var itemModel = dtoMapper.Map<UserCreateDto, ApplicationUser>(userDto);
            await userService.CreateUserAsync(itemModel, userDto.Role);

            return Created(
                this.BaseApiUrl + "/" + itemModel.Id,
                new { UserId = itemModel.Id }
            );
        }

        [HttpDelete("api/user/{userId}")]
        public async Task<IActionResult> Delete([FromRoute] string userId)
        {
            await userService.DeleteUser(userId);
            return Ok("User deleted.");
        }

        [HttpPut]
        [Route("api/user/{userId}/change-password")]
        public async Task<IActionResult> ChangePassword([FromBody]UserPasswordChangeDto passwordDto, [FromRoute]string userId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await userService.GetUserByIdAsync(userId);
            if (!await userService.CheckPassword(user, passwordDto.CurrentPassword))
            {
                return BadRequest("You have entered wrong your password!");
            }
            await userService.ChangePassword(user, passwordDto.CurrentPassword, passwordDto.NewPassword);
            return Ok("Password changed");
        }

        [HttpPut]
        [Route("api/user/{userId}/change-email")]
        public async Task<IActionResult> ChangeEmail([FromBody]UserBaseDto emailDto, [FromRoute]string userId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await userService.ChangeEmail(emailDto.Email, userId);
            return Ok("Email changed");
        }

        [HttpPut]
        [Route("api/user/{userId}/change-role")]
        public async Task<IActionResult> ChangeRole([FromBody]UserRoleDto emailDto, [FromRoute]string userId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await userService.ChangeRole(emailDto.Role, userId);
            return Ok("Role changed");
        }


        [HttpGet]
        [Route("api/profile/{userId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> SingleProfile([FromRoute] string userId)
        {
            var profile = dtoMapper.Map<ProfileInfoDto>(await profileService.GetProfileByUserId(userId));
            return Ok(profile);
        }

        [HttpPost]
        [Route("api/profile")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateProfile([FromBody] ProfileCreatedDto profileDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var itemModel = dtoMapper.Map<ProfileCreatedDto, ProfileNamespace.Profile>(profileDto);
            await profileService.CreateProfileAsync(itemModel);

            return Created(
                this.BaseApiUrl + "/" + itemModel.Id,
                new { UserId = itemModel.Id }
            );
        }

        [HttpPut]
        [Route("api/profile/{profileId}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateProfile([FromBody] ProfileCreatedDto profileDto, [FromRoute] int profileId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var itemModel = dtoMapper.Map<ProfileCreatedDto, ProfileNamespace.Profile>(profileDto);

            await profileService.UpdateProfileAsync(itemModel, profileId);
            return Ok("User Profile updated");
        }

    }
}
