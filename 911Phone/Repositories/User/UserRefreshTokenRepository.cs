using Microsoft.EntityFrameworkCore;
using Phone.Data;
using Phone.Data.Entities.User;
using Phone.Exceptions;
using Phone.Repositories.User.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phone.Repositories.User
{
    public class UserRefreshTokenRepository : IUserRefreshTokenRepository
    {
        private readonly ApplicationDbContext db;

        public UserRefreshTokenRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        /// <summary>
        /// Method for create refresh token
        /// <summary>
        /// <param name="model">UserRefreshToken</param>
        /// <returns>void</returns>
        public async Task CreateAsync(UserRefreshToken model)
        {
            await db.UserRefreshTokens.AddAsync(model);
            await SaveAsync();
        }

        /// <summary>
        /// Method for remove refresh token
        /// <summary>
        /// <param name="key">int</param>
        /// <returns>void</returns>
        public async Task DeleteAsync(int key)
        {
            var model = await GetAsync(key);

            if (model != null)
            {
                db.UserRefreshTokens.Remove(model);
                await SaveAsync();
            }
            else
            {
                throw new CurrentEntryNotFoundException();
            }
        }

        /// <summary>
        /// Method return refresh token by key
        /// <summary>
        /// <param name="key">int</param>
        /// <returns>UserRefreshToken</returns>
        public async Task<UserRefreshToken> GetAsync(int key)
        {
            var token = await db.UserRefreshTokens.FindAsync(key);
            if (token == null)
            {
                throw new CurrentEntryNotFoundException();
            }
            return token;
        }

        /// <summary>
        /// Method if exists user refresh token
        /// <summary>
        /// <param name="userId">string</param>
        /// <returns>UserRefreshToken || null</returns>
        public async Task<UserRefreshToken> GetByUserIdAsync(string userId)
        {
            return await db.UserRefreshTokens.FirstOrDefaultAsync(token => token.UserId == userId);
        }

        public async Task<IEnumerable<UserRefreshToken>> GetListAsync()
        {
            return await db.UserRefreshTokens.ToListAsync();
        }

        /// <summary>
        /// Method for save refresh token
        /// <summary>
        /// <returns>void</returns>
        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }

        /// <summary>
        /// Method for update refresh token
        /// <summary>
        /// <param name="model">UserRefreshToken</param>
        /// <returns>void</returns>
        public async Task UpdateAsync(UserRefreshToken model)
        {
            db.UserRefreshTokens.Update(model);
            await SaveAsync();
        }
    }
}
