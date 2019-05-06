using Phone.Data.Entities.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phone.Repositories.User.Interfaces
{
    public interface IUserAuthRepository : IUserRepository
    {
        Task<IList<string>> GetUserRolesAsync(ApplicationUser user);
        Task<ApplicationUser> FindUserByEmailAsync(string email);
        Task<bool> CheckPasswordAsync(ApplicationUser user, string password);
    }
}
