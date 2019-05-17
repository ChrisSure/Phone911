using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Phone.Data.DTOs.User;
using Phone.Data.Entities.User;
using Phone.Services.User.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

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
                mapper.CreateMap<ApplicationUser, UserViewDto>();
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
    }
}
