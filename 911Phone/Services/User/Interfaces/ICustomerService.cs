using Phone.Data.Entities.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phone.Services.User.Interfaces
{
    public interface ICustomerService
    {
        Task<IList<ApplicationUser>> ListCustomersAsync();
    }
}
