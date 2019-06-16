using Phone.Data.Entities.Shop;
using Phone.Repositories.Shop.Interfaces;
using Phone.Services.Shop.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShopEntity = Phone.Data.Entities.Shop.Shop;


namespace Phone.Services.Shop
{
    public class ShopService : IShopService
    {
        private IShopRepository shopRepository;

        public ShopService(IShopRepository shopRepository)
        {
            this.shopRepository = shopRepository;
        }

        /// <summary>
        /// Method delegate to service get shop by id
        /// <summary>
        /// <param name="shopId">int</param>
        /// <returns>IList<Shop></returns>
        public async Task<ShopEntity> SingleShop(int shopId)
        {
            return await shopRepository.SingleShopAsync(shopId);
        }

        /// <summary>
        /// Method delegate to service get list of shops
        /// <summary>
        /// <returns>IList<Shop></returns>
        public async Task<IList<ShopEntity>> ListShops()
        {
            return await shopRepository.ListShopsAsync();
        }

        /// <summary>
        /// Method delegate to service get list of shops by seller id
        /// <summary>
        /// <param name="sellerId">string</param>
        /// <returns>IList<Shop></returns>
        public async Task<IList<ShopEntity>> ListShopsBySellerId(string sellerId)
        {
            return await shopRepository.ListShopsBySellerIdAsync(sellerId);
        }

        /// <summary>
        /// Method delegate to service create shop
        /// <summary>
        /// <param name="shop">Shop</param>
        /// <returns>void</returns>
        public async Task CreateShop(ShopEntity shop)
        {
            shop.CreatedAt = DateTime.Now;
            shop.UpdatedAt = DateTime.Now;
            await shopRepository.CreateShopAsync(shop);
        }

        /// <summary>
        /// Method delegate to service update shop
        /// <summary>
        /// <param name="shopId">int</param>
        /// <param name="shop">Shop</param>
        /// <returns>void</returns>
        public async Task UpdateShop(int shopId, ShopEntity shop)
        {
            await shopRepository.SingleShopNoTrackAsync(shopId);
            shop.Id = shopId;
            shop.UpdatedAt = DateTime.Now;
            await shopRepository.UpdateShopAsync(shop);
        }

        /// <summary>
        /// Method delegate to service add category to shop
        /// <summary>
        /// <param name="shopCategory">ShopCategory</param>
        /// <returns>void</returns>
        public async Task AddCategoryToShop(ShopCategory shopCategory)
        {
            await shopRepository.AddCategoryToShopAsync(shopCategory);
        }

        /// <summary>
        /// Method delegate to service add seller to shop
        /// <summary>
        /// <param name="shopSeller">ShopSeller</param>
        /// <returns>void</returns>
        public async Task AddSellerToShop(ShopSeller shopSeller)
        {
            await shopRepository.AddSellerToShopAsync(shopSeller);
        }

        /// <summary>
        /// Method delegate to service remove seller from shop
        /// <summary>
        /// <param name="shopSeller">ShopSeller</param>
        /// <returns>void</returns>
        public async Task RemoveSellerFromShop(ShopSeller shopSeller)
        {
            await shopRepository.RemoveSellerFromShopAsync(shopSeller);
        }
    }
}
