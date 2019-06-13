using System.Collections.Generic;
using System.Threading.Tasks;
using ShopEntity = Phone.Data.Entities.Shop.Shop;

namespace Phone.Repositories.Shop.Interfaces
{
    public interface IShopRepository
    {
        Task<ShopEntity> SingleShopAsync(int shopId);
        Task<IList<ShopEntity>> ListShopsAsync();
        Task<IList<ShopEntity>> ListShopsBySellerIdAsync(string sellerId);
        Task CreateShopAsync(ShopEntity shop);
        Task UpdateShopAsync(ShopEntity shop);
    }
}
