﻿using Phone.Data.Entities.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phone.Services.User.Interfaces
{
    public interface IUserAuthService
    {
        Task<ApplicationUser> GetUserByIdAsync(string userId);
        Task<IList<string>> GetUserRolesAsync(ApplicationUser user);
        Task<ApplicationUser> FindUserByEmailAsync(string email);
        Task<bool> CheckPasswordAsync(ApplicationUser user, string password);
        Task<bool> IsCustomer(ApplicationUser user);
    }
}
