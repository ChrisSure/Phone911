using Phone.Data.Entities.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phone.Repositories.User.Interfaces
{
    public interface IUserRefreshTokenRepository
    {
        Task CreateAsync(UserRefreshToken model);
        Task DeleteAsync(int key);
        Task<UserRefreshToken> GetAsync(int key);
        Task<UserRefreshToken> GetByUserIdAsync(string userId);
        Task<IEnumerable<UserRefreshToken>> GetListAsync();
        Task SaveAsync();
        Task UpdateAsync(UserRefreshToken model);
    }
}
