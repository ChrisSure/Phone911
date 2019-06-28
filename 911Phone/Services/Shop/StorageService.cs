using Phone.Data.Entities.Shop;
using Phone.Repositories.Shop.Interfaces;
using Phone.Services.Shop.Interfaces;
using System;
using System.Threading.Tasks;

namespace Phone.Services.Shop
{
    public class StorageService :  IStorageService
    {
        private IStorageRepository storageRepository;

        public StorageService(IStorageRepository storageRepository)
        {
            this.storageRepository = storageRepository;
        }

        /// <summary>
        /// Method delegate to service add product to shop
        /// <summary>
        /// <param name="storage">Storage</param>
        /// <returns>void</returns>
        public async Task Add(Storage storage)
        {
            storage.CreatedAt = DateTime.Now;
            storage.UpdatedAt = DateTime.Now;
            await storageRepository.AddAsync(storage);
        }

        /// <summary>
        /// Method delegate to service delete product from shop
        /// <summary>
        /// <param name="storage">Storage</param>
        /// <returns>void</returns>
        public async Task Delete(Storage storage)
        {
            storage.UpdatedAt = DateTime.Now;
            await storageRepository.DeleteAsync(storage);
        }
    }
}
