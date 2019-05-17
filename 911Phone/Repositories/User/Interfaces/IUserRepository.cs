﻿using Phone.Data.Entities.User;
using System.Threading.Tasks;

namespace Phone.Repositories.User.Interfaces
{
    public interface IUserRepository
    {
        Task<ApplicationUser> GetUserAsync(string userId);
        Task<ApplicationUser> GetUserByEmailAsync(string email);
        Task<string> GetRoleByUserId(ApplicationUser userId);
        Task CreateUserAsync(ApplicationUser user);
        Task AddUsersRoleAsync(ApplicationUser user, string role);
        Task<bool> CheckPassword(ApplicationUser user, string currentPassword);
        Task ChangePassword(ApplicationUser user, string currentpassword, string newpassword);
        Task ChangeEmail(ApplicationUser user);
        Task ChangeRole(ApplicationUser user, string role, string previousRole);
    }
}
