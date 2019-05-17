using Phone.Data.Entities.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phone.Repositories.User.Interfaces
{
    public interface ICustomerRepository
    {
        Task<IList<ApplicationUser>> ListCustomersAsync();
    }
}
