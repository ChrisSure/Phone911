using Phone.Data.Entities.User;
using Phone.Services.User.Interfaces;
using Phone.Repositories.User.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        /// Method return list user with role admin
        /// <summary>
        /// <returns>IList<ApplicationUser></returns>
        public async Task<IList<ApplicationUser>> ListSellersAsync()
        {
            return await sellerRepository.ListSellersAsync();
        }
    }
}
