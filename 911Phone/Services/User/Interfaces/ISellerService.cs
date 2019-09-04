using Phone.Data.DTOs.User;
using Phone.Data.Entities.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phone.Services.User.Interfaces
{
    public interface ISellerService
    {
        Task<IList<ApplicationUser>> ListSellersAsync();
        Task<IList<SellerShopDto>> ListSellersByShopId(int shopId);
    }
}
