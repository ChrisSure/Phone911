using System.Collections.Generic;
using System.Threading.Tasks;
using ShopEntity = Phone.Data.Entities.Shop.Shop;

namespace Phone.Services.Shop.Interfaces
{
    public interface IShopService
    {
        Task<ShopEntity> SingleShop(int shopId);
        Task<IList<ShopEntity>> ListShops();
        Task<IList<ShopEntity>> ListShopsBySellerId(string sellerId);
        Task CreateShop(ShopEntity shop);
        Task UpdateShop(int shopId, ShopEntity shop);
    }
}
