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
using Phone.Exceptions.User;
using System.Data;
using System.Data.SqlClient;
using Phone.Helpers;

namespace Phone.Repositories.User
{
    public class UserRepository : IUserRepository
    {
        private UserManager<ApplicationUser> userManager;
        private ApplicationDbContext dbContext;

        public UserRepository(UserManager<ApplicationUser> user, ApplicationDbContext dbContext)
        {
            this.userManager = user;
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Method return single user with role admin
        /// <summary>
        /// <returns>IList<ApplicationUser></returns>
        public async Task<ApplicationUser> GetUserAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new CurrentEntryNotFoundException();
            }
            return user;
        }

        /// <summary>
        /// Method return single user by his email
        /// <summary>
        /// <returns>IList<ApplicationUser></returns>
        public async Task<ApplicationUser> GetUserByEmailAsync(string email)
        {
            var user = await dbContext.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new CurrentEntryNotFoundException();
            }
            return user;
        }

        /// <summary>
        /// Method remove user role
        /// <summary>
        /// <param name="user">ApplicationUser</param>
        /// <param name="role">string</param>
        /// <returns>void</returns>
        public async Task RemoveRoleUser(ApplicationUser user, string role)
        {
            EnsureIdentitySuccess(await userManager.RemoveFromRoleAsync(user, role));
        }

        /// <summary>
        /// Method remove user
        /// <summary>
        /// <param name="user">ApplicationUser</param>
        /// <returns>void</returns>
        public async Task DeleteUser(ApplicationUser user)
        {
            EnsureIdentitySuccess(await userManager.DeleteAsync(user));
        }


        /// <summary>
        /// Method checking user password
        /// <summary>
        /// <param name="user">ApplicationUser</param>
        /// <param name="currentPassword">string</param>
        /// <returns>bool</returns>
        public async Task<bool> CheckPassword(ApplicationUser user, string currentPassword)
        {
            return await userManager.CheckPasswordAsync(user, currentPassword);
        }

        /// <summary>
        /// Method change user password
        /// <summary>
        /// <param name="user">ApplicationUser</param>
        /// <param name="currentPassword">string</param>
        /// <param name="newpassword">string</param>
        /// <returns>void</returns>
        public async Task ChangePassword(ApplicationUser user, string currentpassword, string newpassword)
        {
            EnsureIdentitySuccess(await userManager.ChangePasswordAsync(user, currentpassword, newpassword));
        }

        /// <summary>
        /// Method change user email
        /// <summary>
        /// <param name="user">ApplicationUser</param>
        /// <returns>void</returns>
        public async Task ChangeEmail(ApplicationUser user)
        {
            await userManager.UpdateAsync(user);
            await SaveAsync();
        }

        /// <summary>
        /// Method change user role
        /// <summary>
        /// <param name="user">ApplicationUser</param>
        /// <param name="role">string</param>
        /// <param name="previousRole">string</param>
        /// <returns>void</returns>
        public async Task ChangeRole(ApplicationUser user, string role, string previousRole)
        {
            EnsureIdentitySuccess(await userManager.RemoveFromRoleAsync(user, previousRole));
            EnsureIdentitySuccess(await userManager.AddToRoleAsync(user, role));
        }

        /// <summary>
        /// Method return single user with role admin
        /// <summary>
        /// <returns>IList<ApplicationUser></returns>
        public async Task<string> GetRoleByUser(ApplicationUser user)
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

        /// <summary>
        /// Method update-create profile or throw exception
        /// <summary>
        /// <returns>void</returns>
        public virtual async Task SaveAsync()
        {
            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException dbuException)
            {
                if (dbuException.InnerException != null)
                {
                    var sqlException = dbuException.InnerException as SqlException;
                    Helpers.SqlExceptionTranslator.ReThrow(sqlException, "");
                } else
                {
                    throw new DbUpdateException(dbuException.Message, dbuException);
                }
            }
        }
        #endregion
    }
}
