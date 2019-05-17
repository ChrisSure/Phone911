using Phone.Data.Entities.User;
using Phone.Services.User.Interfaces;
using Phone.Repositories.User.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phone.Services.User
{
    public class AdminService : IAdminService
    {
        private IAdminRepository adminRepository;

        public AdminService(IAdminRepository adminRepository)
        {
            this.adminRepository = adminRepository;
        }

        /// <summary>
        /// Method return list user with role admin
        /// <summary>
        /// <returns>IList<ApplicationUser></returns>
        public async Task<IList<ApplicationUser>> ListAdminsAsync()
        {
            return await adminRepository.ListAdminsAsync();
        }
    }
}
