using Phone.Data.Entities.User;
using Phone.Repositories.User.Interfaces;
using Phone.Services.User.Interfaces;
using System.Threading.Tasks;

namespace Phone.Services.User
{
    public class ProfileService : IProfileService
    {
        private IProfileRepository profileRepository;

        public ProfileService(IProfileRepository profileRepository)
        {
            this.profileRepository = profileRepository;
        }

        /// <summary>
        /// Method return profile by userId
        /// <summary>
        /// <param name="userId">string</param>
        /// <returns>IList<ApplicationUser></returns>
        public async Task<Profile> GetProfileByUserId(string userId)
        {
            return await profileRepository.GetProfileByUserId(userId);
        }

        /// <summary>
        /// Method create profile
        /// <summary>
        /// <param name="profile">Profile</param>
        /// <returns>void</returns>
        public async Task CreateProfileAsync(Profile profile)
        {
            await profileRepository.CreateProfileAsync(profile);
        }
    }
}
