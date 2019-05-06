using Microsoft.AspNetCore.Identity;
using Phone.Data.Entities.User;
using Phone.Exceptions;
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

        /// <summary>
        /// Method get user roles
        /// <summary>
        /// <param name="user">ApplicationUser</param>
        /// <returns>IList<string></returns>
        public async Task<IList<string>> GetUserRolesAsync(ApplicationUser user)
        {
            return await userManager.GetRolesAsync(user);
        }

        /// <summary>
        /// Method find user by email
        /// <summary>
        /// <param name="email">string</param>
        /// <returns>ApplicationUser || null</returns>
        public async Task<ApplicationUser> FindUserByEmailAsync(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new CurrentEntryNotFoundException();
            }
            return user;
        }

        /// <summary>
        /// Method check for user password
        /// <summary>
        /// <param name="user">ApplicationUser</param>
        /// <param name="password">string</param>
        /// <returns>boolean</returns>
        public async Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
        {
            return await userManager.CheckPasswordAsync(user, password);
        }

    }
}
