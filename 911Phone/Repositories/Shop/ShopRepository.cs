using Microsoft.EntityFrameworkCore;
using Phone.Data;
using Phone.Exceptions;
using Phone.Repositories.Shop.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopEntity = Phone.Data.Entities.Shop.Shop;


namespace Phone.Repositories.Shop
{
    public class ShopRepository : MainRepository, IShopRepository
    {
        public ShopRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        /// <summary>
        /// Method get shop by id
        /// <summary>
        /// <param name="shopId">int</param>
        /// <returns>IList<Shop></returns>
        public async Task<ShopEntity> SingleShopAsync(int shopId)
        {
            var shop = await dbContext.Shops.AsNoTracking().Where(s => s.Id == shopId).FirstOrDefaultAsync();
            if (shop == null)
            {
                throw new CurrentEntryNotFoundException("Current Shop doesn't isset.");
            }
            return shop;
        }

        /// <summary>
        /// Method get list of shops
        /// <summary>
        /// <returns>IList<Shop></returns>
        public async Task<IList<ShopEntity>> ListShopsAsync()
        {
            return await Task.Run(() => dbContext.Shops.ToList());
        }

        /// <summary>
        /// Method get list of shops by seller id
        /// <summary>
        /// <param name="sellerId">string</param>
        /// <returns>IList<Shop></returns>
        public async Task<IList<ShopEntity>> ListShopsBySellerIdAsync(string sellerId)
        {
            return await Task.Run(() => dbContext.Shops.Where(s => s.ShopSeller.Any(u => u.SellerId == sellerId)).ToList());
        }

        /// <summary>
        /// Method create shop
        /// <summary>
        /// <param name="shop">Shop</param>
        /// <returns>void</returns>
        public async Task CreateShopAsync(ShopEntity shop)
        {
            await dbContext.Shops.AddAsync(shop);
            await SaveAsync();
        }

        /// <summary>
        /// Method update shop
        /// <summary>
        /// <param name="shop">Shop</param>
        /// <returns>void</returns>
        public async Task UpdateShopAsync(ShopEntity shop)
        {
            await Task.Run(() => dbContext.Shops.Update(shop));
            await SaveAsync();
        }
    }
}
