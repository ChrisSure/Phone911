using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Phone.Data.DTOs.Catalog;
using Phone.Data.Entities.Catalog;
using Phone.Services.Catalog.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phone.Controllers.Catalog
{
    public class ProductController : MainController
    {
        private IProductService productService;
        private readonly IMapper dtoMapper;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
            dtoMapper = new Mapper(new MapperConfiguration(mapper =>
            {
                mapper.CreateMap<Product, ProductViewDto>().ReverseMap();
                mapper.CreateMap<Product, ProductListDto>().ReverseMap();
                mapper.CreateMap<Product, ProductCreateDto>().ReverseMap();
            }
            ));
        }

        [HttpGet]
        [Route("api/products/{categoryId}/category")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> ListByCategoryId([FromRoute] int categoryId)
        {
            var products = dtoMapper.Map<IList<Product>, IList<ProductListDto>>(await productService.ListProductsByCategoryIdAll(categoryId));
            return Ok(products);
        }

        [HttpGet]
        [Route("api/products/{orderId}/order")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> ListByOrderId([FromRoute] int orderId)
        {
            var products = await productService.ListProductsByOrderIdAll(orderId);
            return Ok(products);
        }

        [HttpGet]
        [Route("api/products/{productId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> SingleAll([FromRoute] int productId)
        {
            var product = dtoMapper.Map<Product, ProductViewDto>(await productService.SingleProduct(productId));
            return Ok(product);
        }

        [HttpPost]
        [Route("api/products")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Create([FromBody] ProductCreateDto productCreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var itemModel = dtoMapper.Map<ProductCreateDto, Product>(productCreateDto);
            await productService.CreateProduct(itemModel);
            return Ok("Product has created");
        }

        [HttpPut]
        [Route("api/products/{productId}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Update([FromBody] ProductCreateDto productCreateDto, [FromRoute] int productId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var itemModel = dtoMapper.Map<ProductCreateDto, Product>(productCreateDto);
            await productService.UpdateProduct(productId, itemModel);
            return Ok("Product has updated");
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [Route("api/products/{productId}")]
        public async Task<IActionResult> Delete([FromRoute] int productId)
        {
            await productService.DeleteProduct(productId);
            return Ok("Product deleted.");
        }


    }
}
