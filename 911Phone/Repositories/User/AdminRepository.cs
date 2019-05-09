using Microsoft.AspNetCore.Identity;
using Phone.Data.Entities.User;
using Phone.Helpers.User;
using Phone.Repositories.User.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Phone.Data;
using Phone.Exceptions;
using System;
using Phone.Exceptions.User;

namespace Phone.Repositories.User
{
    public class AdminRepository : IUserAdminRepository
    {
        private UserManager<ApplicationUser> userManager;
        private ApplicationDbContext context;

        public AdminRepository(UserManager<ApplicationUser> user, ApplicationDbContext context)
        {
            this.userManager = user;
            this.context = context;
        }

        /// <summary>
        /// Method return list user with role admin
        /// <summary>
        /// <returns>IList<ApplicationUser></returns>
        public async Task<IList<ApplicationUser>> ListAdminsAsync()
        {
            List<ApplicationUser> listAdmins = new List<ApplicationUser>();
            listAdmins.AddRange(await userManager.GetUsersInRoleAsync(RoleTypes.SuperAdmin));
            listAdmins.AddRange(await userManager.GetUsersInRoleAsync(RoleTypes.Admin));
            return listAdmins;
        }

        /// <summary>
        /// Method return single user with role admin
        /// <summary>
        /// <returns>IList<ApplicationUser></returns>
        public async Task<ApplicationUser> GetUserAsync(string userId)
        {
            return await userManager.FindByIdAsync(userId);
        }

        /// <summary>
        /// Method return single user by his email
        /// <summary>
        /// <returns>IList<ApplicationUser></returns>
        public async Task<ApplicationUser> GetUserByEmailAsync(string email)
        {
            var user = await context.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new CurrentEntryNotFoundException();
            }
            return user;
        }

        /// <summary>
        /// Method return single user with role admin
        /// <summary>
        /// <returns>IList<ApplicationUser></returns>
        public async Task<string> GetRoleByUserId(ApplicationUser user)
        {
            return (await userManager.GetRolesAsync(user)).FirstOrDefault();
        }

        /// <summary>
        /// Method create user and returned id
        /// <summary>
        /// <param name="user">ApplicationUser</param>
        /// <returns>void</returns>
        public async Task CreateUserAsync(ApplicationUser user)
        {
            EnsureIdentitySuccess(await userManager.CreateAsync(user));
        }

        /// <summary>
        /// Method create user and returned id
        /// <summary>
        /// <param name="user">ApplicationUser</param>
        /// <param name="role">string</param>
        /// <returns>void</returns>
        public async Task AddUsersRoleAsync(ApplicationUser user, string role)
        {
            await userManager.AddToRoleAsync(user, role);
        }


        #region Utilities
        private void EnsureIdentitySuccess(IdentityResult identityResult)
        {
            if (identityResult.Succeeded)
                return;

            List<string> exceptions = new List<string>();

            foreach (IdentityError item in identityResult.Errors)
            {
                exceptions.Add(item.Code);
            }

            throw new UserException("Identity issue(s): " + string.Join(", ", exceptions));
        }
        #endregion
    }
}
