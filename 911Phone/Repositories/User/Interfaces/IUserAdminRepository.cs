using Phone.Data.Entities.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phone.Repositories.User.Interfaces
{
    public interface IUserAdminRepository : IUserRepository
    {
        Task<IList<ApplicationUser>> ListAdminsAsync();
        Task<ApplicationUser> GetUserAsync(string userId);
        Task<ApplicationUser> GetUserByEmailAsync(string email);
        Task<string> GetRoleByUserId(ApplicationUser userId);
        Task CreateUserAsync(ApplicationUser user);
        Task AddUsersRoleAsync(ApplicationUser user, string role);
    }
}
