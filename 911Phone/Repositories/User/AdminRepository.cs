using Microsoft.AspNetCore.Identity;
using Phone.Data.Entities.User;
using Phone.Helpers.User;
using Phone.Repositories.User.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phone.Repositories.User
{
    public class AdminRepository : IUserAdminRepository
    {
        private UserManager<ApplicationUser> userManager;

        public AdminRepository(UserManager<ApplicationUser> user)
        {
            this.userManager = user;
        }

        /// <summary>
        /// Method return list user with role admin
        /// <summary>
        /// <returns>IList<ApplicationUser></returns>
        public async Task<IList<ApplicationUser>> ListAdminsAsync()
        {
            return await userManager.GetUsersInRoleAsync(RoleTypes.SuperAdmin);
        }
    }
}
