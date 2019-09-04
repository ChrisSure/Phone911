using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Phone.Data.DTOs.User;
using Phone.Data.Entities.User;
using Phone.Services.User.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phone.Controllers.User
{
    public class SellerController : MainController
    {
        private ISellerService sellerService;
        private readonly IMapper dtoMapper;

        public SellerController(ISellerService sellerService)
        {
            this.sellerService = sellerService;
            dtoMapper = new Mapper(new MapperConfiguration(mapper =>
            {
                mapper.CreateMap<ApplicationUser, UserViewDto>();
            }
            ));
        }

        [HttpGet]
        [Route("api/sellers")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> ListSellers()
        {
            IList<UserViewDto> sellers = dtoMapper.Map<IList<ApplicationUser>, IList<UserViewDto>>(await sellerService.ListSellersAsync());
            return Ok(sellers);
        }

        [HttpGet]
        [Route("api/sellers/{shopId}/shop")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> ListSellersByShopId([FromRoute] int shopId)
        {
            IList<SellerShopDto> sellers = await sellerService.ListSellersByShopId(shopId);
            return Ok(sellers);
        }

    }
}
