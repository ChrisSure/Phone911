using Phone.Data.DTOs.Shop;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShopEntity = Phone.Data.Entities.Shop.Shop;

namespace PhoneUnitTests.Shop.Helpers
{
    class ShopTestHelper
    {
        /// <summary>
        /// Return face data shop
        /// <summary>
        public static async Task<ShopEntity> GetShop()
        {
            return await Task.Run(() =>
                new ShopEntity { Id = 1, Title = "Shop_1" }
            );
        }

        /// <summary>
        /// Return face data list shop
        /// <summary>
        public static async Task<IList<ShopEntity>> GetShops()
        {
            return await Task.Run(() =>
                new List<ShopEntity>
                {
                    new ShopEntity { Id=1, Title="Shop_1" },
                    new ShopEntity { Id=2, Title="Shop_2" },
                    new ShopEntity { Id=3, Title="Shop_3" }
                }
            );
        }

        /// <summary>
        /// Return list face data shop in like ShopCreateDto 
        /// <summary>
        public static async Task<ShopCreateDto> GetShopCreateNormal()
        {
            return await Task.Run(() =>
                new ShopCreateDto { Title = "Product_1" }
            );
        }

        /// <summary>
        /// Return list face data shop in like ShopCreateDto 
        /// <summary>
        public static async Task<ShopCreateDto> GetShopCreateUnNormal()
        {
            return await Task.Run(() =>
                new ShopCreateDto { }
            );
        }
    }
}
