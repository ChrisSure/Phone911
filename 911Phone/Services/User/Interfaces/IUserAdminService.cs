using Phone.Data.Entities.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phone.Services.User.Interfaces
{
    public interface IUserAdminService : IUserService
    {
        Task<IList<ApplicationUser>> ListAdminsAsync();
        Task<ApplicationUser> GetUserByIdAsync(string userId);
        Task<string> GetRoleByUserId(ApplicationUser userId);
        Task CreateUserAsync(ApplicationUser user, string role);
        Task<bool> CheckPassword(ApplicationUser user, string currentPassword);
        Task ChangePassword(ApplicationUser user, string currentpassword, string newpassword);
    }
}
