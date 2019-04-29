using Microsoft.AspNetCore.Identity;
using Phone.Data.Entities.User;
using Phone.Services.User.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phone.Services.User
{
    public class UserService : IUserService
    {
        private UserManager<ApplicationUser> userManager;

        public UserService(UserManager<ApplicationUser> user)
        {
            userManager = user;
        }

        public async Task<IList<string>> GetUserRolesAsync(ApplicationUser user)
        {
            return await userManager.GetRolesAsync(user);
        }

    }
}
