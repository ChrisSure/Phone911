using Phone.Data.Entities.User;
using Phone.Services.User.Interfaces;
using Phone.Repositories.User.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileNamespace = Phone.Data.Entities.User;
using Phone.Helpers.User;

namespace Phone.Services.User
{
    public class CustomerService : ICustomerService
    {
        private ICustomerRepository customerRepository;
        private IUserRepository userRepository;
        private IProfileRepository profileRepository;

        public CustomerService(ICustomerRepository customerRepository, IUserRepository userRepository, IProfileRepository profileRepository)
        {
            this.customerRepository = customerRepository;
            this.userRepository = userRepository;
            this.profileRepository = profileRepository;
        }

        /// <summary>
        /// Method return list user with role admin
        /// <summary>
        /// <returns>IList<ApplicationUser></returns>
        public async Task<IList<ApplicationUser>> ListCustomersAsync()
        {
            return await customerRepository.ListCustomersAsync();
        }

        /// <summary>
        /// Method create user and returned id
        /// <summary>
        /// <param name="profile">Profile</param>
        /// <param name="user">ApplicationUser</param>
        /// <returns>IList<ApplicationUser></returns>
        public async Task CreateCustomerAsync(ProfileNamespace.Profile profile, ApplicationUser user)
        {
            await userRepository.CreateUserAsync(user);
            var userObj = await userRepository.GetUserByEmailAsync(user.Email);
            await userRepository.AddUsersRoleAsync(userObj, RoleTypes.Customer);

            profile.UserId = userObj.Id;
            await profileRepository.CreateProfileAsync(profile);
        }
    }
}