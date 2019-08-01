using Phone.Data.Entities.User;
using Phone.Services.User.Interfaces;
using Phone.Repositories.User.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Phone.Data.DTOs.User;

namespace Phone.Services.User
{
    public class SellerService : ISellerService
    {
        private ISellerRepository sellerRepository;

        public SellerService(ISellerRepository sellerRepository)
        {
            this.sellerRepository = sellerRepository;
        }

        /// <summary>
        /// Method delegate to repository return list user with role admin
        /// <summary>
        /// <returns>IList<ApplicationUser></returns>
        public async Task<IList<ApplicationUser>> ListSellersAsync()
        {
            return await sellerRepository.ListSellersAsync();
        }

        /// <summary>
        /// Method delegate to repository return list seller by shop id
        /// <summary>
        /// <params name="shopId">int</params>
        /// <returns>IList<ApplicationUser></returns>
        public async Task<IList<SellerShopDto>> ListSellersByShopId(int shopId)
        {
            return await sellerRepository.ListSellersByShopIdAsync(shopId);
        }
    }
}
