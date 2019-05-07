using Microsoft.AspNetCore.Identity;
using Phone.Data.Entities.User;
using Phone.Helpers.User;
using Phone.Repositories.User.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
            List<ApplicationUser> listAdmins = new List<ApplicationUser>();
            listAdmins.AddRange(await userManager.GetUsersInRoleAsync(RoleTypes.SuperAdmin));
            listAdmins.AddRange(await userManager.GetUsersInRoleAsync(RoleTypes.Admin));
            return listAdmins;
        }

        /// <summary>
        /// Method return single user with role admin
        /// <summary>
        /// <returns>IList<ApplicationUser></returns>
        public async Task<ApplicationUser> GetAdminAsync(string userId)
        {
            return await userManager.Users.Where(u => u.Id == userId).Include(u => u.Profile).FirstOrDefaultAsync();
        }
    }
}
