using Phone.Data.Entities.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phone.Repositories.User.Interfaces
{
    public interface IUserAdminRepository : IUserRepository
    {
        Task<IList<ApplicationUser>> ListAdminsAsync();
        Task<ApplicationUser> GetAdminAsync(string userId);
    }
}
