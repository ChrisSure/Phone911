using AutoMapper;
using Phone.Services.Shop.Interfaces;

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
            }
            ));
        }
    }
}
