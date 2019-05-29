using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Phone.Data.Entities.Catalog;
using System.Threading.Tasks;
using Phone.Data.DTOs.Catalog;
using Phone.Services.Catalog.Interfaces;
using System.Collections.Generic;

namespace Phone.Controllers.Catalog
{
    public class CategoryController : MainController
    {

        private ICategoryService categoryService;
        private readonly IMapper dtoMapper;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
            dtoMapper = new Mapper(new MapperConfiguration(mapper =>
            {
                mapper.CreateMap<Category, CategoryViewDto>();
                mapper.CreateMap<Category, CategoryListDto>();
            }
            ));
        }

        [HttpGet]
        [Route("api/categories")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> List()
        {
            var categories = dtoMapper.Map<IList<Category>, IList<CategoryListDto>>(await categoryService.ListCategories());
            return Ok(categories);
        }

        [HttpGet]
        [Route("api/categories/{categoryId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Single([FromRoute] int categoryId)
        {
            var category = dtoMapper.Map<Category, CategoryViewDto>(await categoryService.SingleCategory(categoryId));
            return Ok(category);
        }

        [HttpPost]
        [Route("api/categories")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Create([FromBody] CategoryCreateDto categoryCreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await categoryService.CreateCategory(categoryCreateDto.Title, categoryCreateDto.ParentId);
            return Ok("Category has created");
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [Route("api/categories/{categoryId}")]
        public async Task<IActionResult> Delete(int categoryId)
        {
            await categoryService.DeleteCategory(categoryId);
            return Ok("Category deleted.");
        }

    }
}
