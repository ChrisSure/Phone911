using Phone.Data.Entities.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phone.Repositories.User.Interfaces
{
    public interface IAdminRepository
    {
        Task<IList<ApplicationUser>> ListAdminsAsync();
    }
}
