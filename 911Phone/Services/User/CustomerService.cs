using Phone.Data.Entities.User;
using Phone.Services.User.Interfaces;
using Phone.Repositories.User.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phone.Services.User
{
    public class CustomerService : ICustomerService
    {
        private ICustomerRepository customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        /// <summary>
        /// Method return list user with role admin
        /// <summary>
        /// <returns>IList<ApplicationUser></returns>
        public async Task<IList<ApplicationUser>> ListCustomersAsync()
        {
            return await customerRepository.ListCustomersAsync();
        }
    }
}