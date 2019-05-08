using Phone.Data.Entities.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phone.Services.User.Interfaces
{
    public interface IUserAdminService : IUserService
    {
        Task<IList<ApplicationUser>> ListAdminsAsync();
        Task<ApplicationUser> GetAdminAsync(string userId);
        Task<string> GetRoleByUserId(ApplicationUser userId);
        Task CreateUserAsync(ApplicationUser user);
    }
}
