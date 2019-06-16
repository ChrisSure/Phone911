using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Phone.Data.DTOs.Shop;
using Phone.Data.Entities.Shop;
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
                mapper.CreateMap<ShopCategory, AddCategoryToShopDto>().ReverseMap();
                mapper.CreateMap<ShopSeller, AddSellerToShopDto>().ReverseMap();
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

        [HttpPost]
        [Route("api/shops-add-category")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> AddCategoryToShop([FromBody] AddCategoryToShopDto addcategoryToShopDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var itemModel = dtoMapper.Map<AddCategoryToShopDto, ShopCategory>(addcategoryToShopDto);
            await shopService.AddCategoryToShop(itemModel);
            return Ok("Category has added to shop");
        }

        [HttpPost]
        [Route("api/shops-add-seller")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> AddSellerToShop([FromBody] AddSellerToShopDto addsellerToShopDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var itemModel = dtoMapper.Map<AddSellerToShopDto, ShopSeller>(addsellerToShopDto);
            await shopService.AddSellerToShop(itemModel);
            return Ok("Seller has added to shop");
        }

        [HttpPut]
        [Route("api/shops-remove-seller")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> RemoveSellerFromShop([FromBody] AddSellerToShopDto removeSellerFromShopDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var itemModel = dtoMapper.Map<AddSellerToShopDto, ShopSeller>(removeSellerFromShopDto);
            await shopService.RemoveSellerFromShop(itemModel);
            return Ok("Seller has removed from shop");
        }




    }
}
