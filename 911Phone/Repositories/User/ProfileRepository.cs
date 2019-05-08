using Phone.Data;
using System.Linq;
using Phone.Data.Entities.User;
using Phone.Repositories.User.Interfaces;
using System.Threading.Tasks;
using Phone.Exceptions;

namespace Phone.Repositories.User
{
    public class ProfileRepository : IProfileRepository
    {
        private ApplicationDbContext db;

        public ProfileRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        /// <summary>
        /// Method return profile by userId
        /// <summary>
        /// <returns>IList<ApplicationUser></returns>
        public async Task<Profile> GetProfileByUserId(string userId)
        {
            var profile = await Task.Run(() => db.Profiles.Where(p => p.UserId == userId).FirstOrDefault());
            if (profile == null)
            {
                throw new CurrentEntryNotFoundException();
            }
            return profile;
        }
    }
}
