using Phone.Data.Entities.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phone.Services.User.Interfaces
{
    public interface IUserService
    {

        Task<IList<string>> GetUserRolesAsync(ApplicationUser user);

    }
}
