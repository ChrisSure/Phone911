using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Phone.Data.DTOs.Shop;
using Phone.Data.Entities.Shop;
using Phone.Services.Shop.Interfaces;
using System.Threading.Tasks;

namespace Phone.Controllers.Shop
{
    public class StorageController : MainController
    {
        private IStorageService storageService;
        private readonly IMapper dtoMapper;

        public StorageController(IStorageService storageService)
        {
            this.storageService = storageService;
            dtoMapper = new Mapper(new MapperConfiguration(mapper =>
            {
                mapper.CreateMap<Storage, AddStorageDto>().ReverseMap();
            }
            ));
        }

        [HttpPost]
        [Route("api/storage")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Add([FromBody] AddStorageDto addStorageDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var itemModel = dtoMapper.Map<AddStorageDto, Storage>(addStorageDto);
            await storageService.Add(itemModel);
            return Ok("Product added.");
        }

        [HttpPut]
        [Route("api/storage")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Delete([FromBody] AddStorageDto addStorageDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var itemModel = dtoMapper.Map<AddStorageDto, Storage>(addStorageDto);
            await storageService.Delete(itemModel);
            return Ok("Product deleted.");
        }
    }
}
