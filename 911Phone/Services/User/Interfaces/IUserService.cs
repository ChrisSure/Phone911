using Phone.Data.Entities.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phone.Services.User.Interfaces
{
    public interface IUserService
    {

        Task<IList<string>> GetUserRolesAsync(ApplicationUser user);
        Task<object> FindUserByEmailAsync(string email);
        Task<bool> CheckPasswordAsync(ApplicationUser user, string password);

    }
}
