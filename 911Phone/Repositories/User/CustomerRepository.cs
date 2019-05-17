using Microsoft.AspNetCore.Identity;
using Phone.Data.Entities.User;
using Phone.Helpers.User;
using Phone.Repositories.User.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phone.Repositories.User
{
    public class CustomerRepository : ICustomerRepository
    {
        private UserManager<ApplicationUser> userManager;

        public CustomerRepository(UserManager<ApplicationUser> user)
        {
            this.userManager = user;
        }

        /// <summary>
        /// Method return list user with role customer
        /// <summary>
        /// <returns>IList<ApplicationUser></returns>
        public async Task<IList<ApplicationUser>> ListCustomersAsync()
        {
            return await userManager.GetUsersInRoleAsync(RoleTypes.Customer);
        }
    }
}
