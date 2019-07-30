using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Phone.Data.DTOs.User;
using Phone.Data.Entities.User;
using Phone.Helpers.User;
using Phone.Services.User.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileNamespace = Phone.Data.Entities.User;


namespace Phone.Controllers.User
{
    public class CustomerController : MainController
    {
        private ICustomerService customerService;
        private readonly IMapper dtoMapper;

        public CustomerController(ICustomerService customerService)
        {
            this.customerService = customerService;
            dtoMapper = new Mapper(new MapperConfiguration(mapper =>
            {
                mapper.CreateMap<ProfileNamespace.Profile, CustomerCreateDto>().ReverseMap();
                mapper.CreateMap<ApplicationUser, UserViewDto>();
                mapper.CreateMap<ApplicationUser, UserCreateDto>().ReverseMap()
                        .ForMember(user => user.PasswordHash, opt => opt.MapFrom(src => src.Password));
            }
            ));
        }

        [HttpGet]
        [Route("api/customers")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> ListCustomers()
        {
            IList<UserViewDto> customers = dtoMapper.Map<IList<ApplicationUser>, IList<UserViewDto>>(await customerService.ListCustomersAsync());
            return Ok(customers);
        }

        [HttpPost]
        [Route("api/customers")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerCreateDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userDto = new UserCreateDto { Email = customerDto.Email, UserName = customerDto.Name + customerDto.LastName + customerDto.Phone, Password = "123", ConfirmPassword = "123", Role = RoleTypes.Customer };
            var itemUser = dtoMapper.Map<UserCreateDto, ApplicationUser>(userDto);
            var itemProfile = dtoMapper.Map<CustomerCreateDto, ProfileNamespace.Profile>(customerDto);

            await customerService.CreateCustomerAsync(itemProfile, itemUser);

            return Created(
                this.BaseApiUrl + "/" + itemUser.Id,
                new { UserId = itemUser.Id }
            );
        }
    }
}
