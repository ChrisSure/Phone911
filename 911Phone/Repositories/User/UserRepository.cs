using Microsoft.AspNetCore.Identity;
using Phone.Data.Entities.User;
using Phone.Repositories.User.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phone.Repositories.User
{
    public class UserRepository : IUserRepository
    {

        private UserManager<ApplicationUser> userManager;

        public UserRepository(UserManager<ApplicationUser> user)
        {
            this.userManager = user;
        }

        public async Task<IList<string>> GetUserRolesAsync(ApplicationUser user)
        {
            return await userManager.GetRolesAsync(user);
        }

    }
}
