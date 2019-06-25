using Phone.Data.Entities.Shop;
using System.Threading.Tasks;

namespace Phone.Repositories.Shop.Interfaces
{
    public interface IStorageRepository
    {
        Task AddAsync(Storage storage);
        Task DeleteAsync(Storage storage);
    }
}
