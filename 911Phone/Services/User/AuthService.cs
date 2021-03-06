﻿using Phone.Data.Entities.User;
using Phone.Repositories.User.Interfaces;
using Phone.Services.User.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Phone.Services.User
{
    public class AuthService : IUserAuthService
    {
        private IUserAuthRepository userRepository;

        public AuthService(IUserAuthRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        /// <summary>
        /// Method return single user with role admin
        /// <summary>
        /// <param name="userId">string</param>
        /// <returns>ApplicationUser</returns>
        public async Task<ApplicationUser> GetUserByIdAsync(string userId)
        {
            return await userRepository.GetUserAsync(userId);
        }

        /// <summary>
        /// Method delegate to repository get user roles
        /// <summary>
        /// <param name="user">ApplicationUser</param>
        /// <returns>IList<string></returns>
        public async Task<IList<string>> GetUserRolesAsync(ApplicationUser user)
        {
            return await userRepository.GetUserRolesAsync(user);
        }

        /// <summary>
        /// Method delegate to repository find user by email
        /// <summary>
        /// <param name="email">string</param>
        /// <returns>ApplicationUser || null</returns>
        public async Task<ApplicationUser> FindUserByEmailAsync(string email)
        {
            return await userRepository.FindUserByEmailAsync(email);
        }

        /// <summary>
        /// Method delegate to repository check for user password
        /// <summary>
        /// <param name="user">ApplicationUser</param>
        /// <param name="password">string</param>
        /// <returns>boolean</returns>
        public async Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
        {
            return await userRepository.CheckPasswordAsync(user, password);
        }

        /// <summary>
        /// Method delegate to repository check if user is customer
        /// <summary>
        /// <param name="user">ApplicationUser</param>
        /// <returns>bool</returns>
        public async Task<bool> IsCustomer(ApplicationUser user)
        {
            return await userRepository.IsCustomerAsync(user);
        }
    }
}
