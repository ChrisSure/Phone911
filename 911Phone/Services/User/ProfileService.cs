using Phone.Data.Entities.User;
using Phone.Repositories.User.Interfaces;
using Phone.Services.User.Interfaces;
using System;
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
        /// Method return profile by id
        /// <summary>
        /// <param name="profileId">int</param>
        /// <returns>Profile</returns>
        public async Task<Profile> GetProfileById(int profileId)
        {
            return await profileRepository.GetProfileById(profileId);
        }

        /// <summary>
        /// Method return profile by userId
        /// <summary>
        /// <param name="userId">string</param>
        /// <returns>Profile</returns>
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

        /// <summary>
        /// Method update profile
        /// <summary>
        /// <param name="profile">Profile</param>
        /// <param name="profileId">int</param>
        /// <returns>void</returns>
        public async Task UpdateProfileAsync(Profile profile, int profileId)
        {
            var currentProfile = await profileRepository.GetProfileById(profileId);
            profile.UpdatedAt = DateTime.Now;
            profile.CreatedAt = currentProfile.CreatedAt;
            profile.Id = profileId;
            await profileRepository.UpdateProfileAsync(profile);
        }
    }
}
