using Phone.Data.Entities.User;
using Phone.Repositories.User.Interfaces;
using Phone.Services.User.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phone.Services.User
{
    public class AdminService : IUserAdminService
    {
        private IUserAdminRepository userRepository;

        public AdminService(IUserAdminRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        /// <summary>
        /// Method return list user with role admin
        /// <summary>
        /// <returns>IList<ApplicationUser></returns>
        public async Task<IList<ApplicationUser>> ListAdminsAsync()
        {
            return await userRepository.ListAdminsAsync();
        }

        /// <summary>
        /// Method return single user with role admin
        /// <summary>
        /// <returns>IList<ApplicationUser></returns>
        public async Task<ApplicationUser> GetAdminAsync(string userId)
        {
            return await userRepository.GetAdminAsync(userId);
        }
    }
}
