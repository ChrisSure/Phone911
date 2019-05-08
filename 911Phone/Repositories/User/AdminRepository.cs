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
            return await userManager.FindByIdAsync(userId);
        }

        /// <summary>
        /// Method return single user with role admin
        /// <summary>
        /// <returns>IList<ApplicationUser></returns>
        public async Task<string> GetRoleByUserId(ApplicationUser user)
        {
            return (await userManager.GetRolesAsync(user)).FirstOrDefault();
        }

        /// <summary>
        /// Method create user and returned id
        /// <summary>
        /// <param name="user">ApplicationUser</param>
        /// <returns>void</returns>
        public async Task CreateUserAsync(ApplicationUser user)
        {
            await userManager.CreateAsync(user);
        }
    }
}
