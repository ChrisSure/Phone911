using Phone.Data.DTOs.User;
using Phone.Data.Entities.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phone.Repositories.User.Interfaces
{
    public interface ISellerRepository
    {
        Task<IList<ApplicationUser>> ListSellersAsync();
        Task<IList<SellerShopDto>> ListSellersByShopIdAsync(int shopId);
    }
}
