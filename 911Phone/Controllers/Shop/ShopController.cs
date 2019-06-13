using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Phone.Data.DTOs.Shop;
using Phone.Services.Shop.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShopEntity = Phone.Data.Entities.Shop.Shop;

namespace Phone.Controllers.Shop
{
    public class ShopController : MainController
    {
        private IShopService shopService;
        private readonly IMapper dtoMapper;

        public ShopController(IShopService shopService)
        {
            this.shopService = shopService;
            dtoMapper = new Mapper(new MapperConfiguration(mapper =>
            {
                mapper.CreateMap<ShopEntity, ShopListDto>();
                mapper.CreateMap<ShopEntity, ShopViewDto>().ReverseMap();
                mapper.CreateMap<ShopEntity, ShopCreateDto>().ReverseMap();
            }
            ));
        }

        [HttpGet]
        [Route("api/shops/{shopId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Single([FromRoute] int shopId)
        {
            var order = dtoMapper.Map<ShopEntity, ShopViewDto>(await shopService.SingleShop(shopId));
            return Ok(order);
        }

        [HttpGet]
        [Route("api/shops")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> List()
        {
            var orders = dtoMapper.Map<IList<ShopEntity>, IList<ShopListDto>>(await shopService.ListShops());
            return Ok(orders);
        }

        [HttpGet]
        [Route("api/shops-seller/{sellerId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> ListBySellerId([FromRoute] string sellerId)
        {
            var orders = dtoMapper.Map<IList<ShopEntity>, IList<ShopListDto>>(await shopService.ListShopsBySellerId(sellerId));
            return Ok(orders);
        }

        [HttpPost]
        [Route("api/shops")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Create([FromBody] ShopCreateDto shopCreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var itemModel = dtoMapper.Map<ShopCreateDto, ShopEntity>(shopCreateDto);
            await shopService.CreateShop(itemModel);
            return Ok("Shop has created");
        }

        [HttpPut]
        [Route("api/shops/{shopId}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Update([FromBody] ShopCreateDto shopCreateDto, [FromRoute] int shopId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var itemModel = dtoMapper.Map<ShopCreateDto, ShopEntity>(shopCreateDto);
            await shopService.UpdateShop(shopId, itemModel);
            return Ok("Shop has updated");
        }



    }
}
