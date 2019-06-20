using Phone.Repositories.Shop.Interfaces;
using Phone.Services.Shop.Interfaces;

namespace Phone.Services.Shop
{
    public class StorageService : IStorageService
    {
        private IStorageRepository storageRepository;

        public StorageService(IStorageRepository storageRepository)
        {
            this.storageRepository = storageRepository;
        }
    }
}
