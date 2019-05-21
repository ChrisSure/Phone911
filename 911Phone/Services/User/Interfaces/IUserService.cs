using Phone.Data.Entities.User;
using System.Threading.Tasks;

namespace Phone.Services.User.Interfaces
{
    public interface IUserService
    {
        Task<ApplicationUser> GetUserByIdAsync(string userId);
        Task<string> GetRoleByUserId(ApplicationUser userId);
        Task CreateUserAsync(ApplicationUser user, string role);
        Task<bool> CheckPassword(ApplicationUser user, string currentPassword);
        Task ChangePassword(ApplicationUser user, string currentpassword, string newpassword);
        Task ChangeEmail(string email, string userId);
        Task ChangeRole(string role, string userId);
        Task DeleteUser(string userId);
    }
}
