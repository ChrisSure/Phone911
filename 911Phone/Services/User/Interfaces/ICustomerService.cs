using Phone.Data.Entities.User;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileNamespace = Phone.Data.Entities.User;

namespace Phone.Services.User.Interfaces
{
    public interface ICustomerService
    {
        Task<IList<ApplicationUser>> ListCustomersAsync();
        Task CreateCustomerAsync(ProfileNamespace.Profile profile, ApplicationUser user);
    }
}
