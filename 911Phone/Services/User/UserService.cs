using Phone.Data.Entities.User;
using Phone.Repositories.User.Interfaces;
using Phone.Services.User.Interfaces;
using System.Threading.Tasks;

namespace Phone.Services.User
{
    public class UserService : IUserService
    {
        private IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        /// <summary>
        /// Method return single user with role admin
        /// <summary>
        /// <param name="userId">string</param>
        /// <returns>ApplicationUser</returns>
        public async Task<ApplicationUser> GetUserByIdAsync(string userId)
        {
            return await userRepository.GetUserAsync(userId); 
        }

        /// <summary>
        /// Method remove user
        /// <summary>
        /// <param name="userId">string</param>
        /// <returns>void</returns>
        public async Task DeleteUser(string userId)
        {
            ApplicationUser user = await userRepository.GetUserAsync(userId);
            string role = await userRepository.GetRoleByUser(user);
            await userRepository.RemoveRoleUser(user, role);
            await userRepository.DeleteUser(user);
        }

        /// <summary>
        /// Method checking user password
        /// <summary>
        /// <param name="user">ApplicationUser</param>
        /// <param name="currentPassword">string</param>
        /// <returns>bool</returns>
        public async Task<bool> CheckPassword(ApplicationUser user, string currentPassword)
        {
            return await userRepository.CheckPassword(user, currentPassword);
        }

        /// <summary>
        /// Method change user password
        /// <summary>
        /// <param name="user">ApplicationUser</param>
        /// <param name="currentPassword">string</param>
        /// <param name="newpassword">string</param>
        /// <returns>void</returns>
        public async Task ChangePassword(ApplicationUser user, string currentpassword, string newpassword)
        {
            await userRepository.ChangePassword(user, currentpassword, newpassword);
        }

        /// <summary>
        /// Method change user email
        /// <summary>
        /// <param name="email">string</param>
        /// <param name="userId">string</param>
        /// <returns>void</returns>
        public async Task ChangeEmail(string email, string userId)
        {
            var user = await GetUserByIdAsync(userId);
            user.Email = email;
            await userRepository.ChangeEmail(user);
        }

        /// <summary>
        /// Method change user role
        /// <summary>
        /// <param name="role">string</param>
        /// <param name="userId">string</param>
        /// <returns>void</returns>
        public async Task ChangeRole(string role, string userId)
        {
            var user = await GetUserByIdAsync(userId);
            var previousRole = await GetRoleByUserId(user);
            await userRepository.ChangeRole(user, role, previousRole);
        }

        /// <summary>
        /// Method return role  by user
        /// <summary>
        /// <param name="user">ApplicationUser</param>
        /// <returns>string</returns>
        public async Task<string> GetRoleByUserId(ApplicationUser user)
        {
            return await userRepository.GetRoleByUser(user);
        }

        /// <summary>
        /// Method create user and returned id
        /// <summary>
        /// <param name="user">ApplicationUser</param>
        /// <returns>IList<ApplicationUser></returns>
        public async Task CreateUserAsync(ApplicationUser user, string role)
        {
            await userRepository.CreateUserAsync(user);
            var userObj = await userRepository.GetUserByEmailAsync(user.Email);
            await userRepository.AddUsersRoleAsync(userObj, role);
        }

    }
}
