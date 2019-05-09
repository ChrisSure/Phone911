using Phone.Data.Entities.User;
using System.Threading.Tasks;

namespace Phone.Services.User.Interfaces
{
    public interface IProfileService
    {
        Task<Profile> GetProfileByUserId(string userId);
        Task CreateProfileAsync(Profile profile);
    }
}
