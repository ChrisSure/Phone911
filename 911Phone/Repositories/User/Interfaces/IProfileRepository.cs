using Phone.Data.Entities.User;
using System.Threading.Tasks;

namespace Phone.Repositories.User.Interfaces
{
    public interface IProfileRepository
    {
        Task<Profile> GetProfileById(int profileId);
        Task<Profile> GetProfileByUserId(string userId);
        Task CreateProfileAsync(Profile profile);
        Task UpdateProfileAsync(Profile profile);
    }
}
