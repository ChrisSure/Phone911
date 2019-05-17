using Phone.Data.Entities.User;
using System.Threading.Tasks;

namespace Phone.Services.User.Interfaces
{
    public interface IProfileService
    {
        Task<Profile> GetProfileById(int profileId);
        Task<Profile> GetProfileByUserId(string userId);
        Task CreateProfileAsync(Profile profile);
        Task UpdateProfileAsync(Profile profile, int profileId);
    }
}
