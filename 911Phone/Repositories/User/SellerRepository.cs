using Microsoft.AspNetCore.Identity;
using Phone.Data.Entities.User;
using Phone.Helpers.User;
using Phone.Repositories.User.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phone.Repositories.User
{
    public class SellerRepository : ISellerRepository
    {
        private UserManager<ApplicationUser> userManager;

        public SellerRepository(UserManager<ApplicationUser> user)
        {
            this.userManager = user;
        }

        /// <summary>
        /// Method return list user with role seller and super-seller
        /// <summary>
        /// <returns>IList<ApplicationUser></returns>
        public async Task<IList<ApplicationUser>> ListSellersAsync()
        {
            List<ApplicationUser> listSellers = new List<ApplicationUser>();
            listSellers.AddRange(await userManager.GetUsersInRoleAsync(RoleTypes.SuperSeller));
            listSellers.AddRange(await userManager.GetUsersInRoleAsync(RoleTypes.Seller));
            return listSellers;
        }
    }
}
