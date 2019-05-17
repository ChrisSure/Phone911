using Phone.Data;
using System.Linq;
using Phone.Data.Entities.User;
using Phone.Repositories.User.Interfaces;
using System.Threading.Tasks;
using Phone.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Phone.Repositories.User
{
    public class ProfileRepository : IProfileRepository
    {
        private ApplicationDbContext dbContext;

        public ProfileRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Method return profile by id
        /// <summary>
        /// <param name="profileId">int</param>
        /// <returns>Profile</returns>
        public async Task<Profile> GetProfileById(int profileId)
        {
            var profile = await dbContext.Profiles.AsNoTracking().Where(p => p.Id == profileId).FirstOrDefaultAsync();
            if (profile == null)
            {
                throw new CurrentEntryNotFoundException();
            }
            return profile;
        }

        /// <summary>
        /// Method return profile by userId
        /// <summary>
        /// <returns>IList<ApplicationUser></returns>
        public async Task<Profile> GetProfileByUserId(string userId)
        {
            var profile = await Task.Run(() => dbContext.Profiles.Where(p => p.UserId == userId).FirstOrDefault());
            if (profile == null)
            {
                throw new CurrentEntryNotFoundException();
            }
            return profile;
        }

        /// <summary>
        /// Method create profile
        /// <summary>
        /// <param name="profile">Profile</param>
        /// <returns>void</returns>
        public async Task CreateProfileAsync(Profile profile)
        {
            await dbContext.Profiles.AddAsync(profile);
            await SaveAsync();
        }

        /// <summary>
        /// Method update profile
        /// <summary>
        /// <param name="profile">Profile</param>
        /// <returns>void</returns>
        public async Task UpdateProfileAsync(Profile profile)
        {
            await Task.Run(() => dbContext.Profiles.Update(profile));
            await SaveAsync();
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
                throw new DbUpdateException(dbuException.Message, dbuException);
            }
        }

    }
}
