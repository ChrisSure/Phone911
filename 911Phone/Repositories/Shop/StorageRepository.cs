using Microsoft.EntityFrameworkCore;
using Phone.Data;
using Phone.Data.Entities.Shop;
using Phone.Exceptions;
using Phone.Repositories.Catalog.Interfaces;
using Phone.Repositories.Shop.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Phone.Repositories.Shop
{
    public class StorageRepository : MainRepository, IStorageRepository
    {
        IShopRepository shopRepository;
        IProductRepository productRepository;

        public StorageRepository(ApplicationDbContext dbContext, IShopRepository shopRepository, IProductRepository productRepository) : base(dbContext)
        {
            this.shopRepository = shopRepository;
            this.productRepository = productRepository;
        }

        /// <summary>
        /// Method add product to shop
        /// <summary>
        /// <param name="storage">Storage</param>
        /// <returns>void</returns>
        public async Task AddAsync(Storage storage)
        {
            await shopRepository.SingleShopAsync(storage.ShopId);
            await productRepository.SingleLiteProductAsync(storage.ProductId);

            var storageCheck = await Task.Run(() => dbContext.Storages.AsNoTracking().Where(s => s.ProductId == storage.ProductId).Where(s => s.ShopId == storage.ShopId).FirstOrDefault());
            if (storageCheck == null)
            {
                await dbContext.Storages.AddAsync(storage);
                await SaveAsync();
            } else
            {
                storage.Id = storageCheck.Id;
                storage.Count += storageCheck.Count;
                storage.CreatedAt = storageCheck.CreatedAt;
                await Task.Run(() => dbContext.Storages.Update(storage));
                await SaveAsync();
            }
        }

        /// <summary>
        /// Method delete product from shop
        /// <summary>
        /// <param name="storage">Storage</param>
        /// <returns>void</returns>
        public async Task DeleteAsync(Storage storage)
        {
            await shopRepository.SingleShopAsync(storage.ShopId);
            await productRepository.SingleLiteProductAsync(storage.ProductId);

            var storageCheck = await Task.Run(() => dbContext.Storages.AsNoTracking().Where(s => s.ProductId == storage.ProductId).Where(s => s.ShopId == storage.ShopId).FirstOrDefault());
            if (storageCheck == null)
            {
                throw new CurrentEntryNotFoundException("Current Storage doesn't isset.");
            }

            if ((storageCheck.Count - storage.Count) <= 0)
            {
                await Task.Run(() => dbContext.Storages.Remove(storageCheck));
                await SaveAsync();
            } else
            {
                storage.Id = storageCheck.Id;
                storage.Count = (short)(storageCheck.Count - storage.Count);
                storage.CreatedAt = storageCheck.CreatedAt;
                await Task.Run(() => dbContext.Storages.Update(storage));
                await SaveAsync();
            }
        }
    }
}
