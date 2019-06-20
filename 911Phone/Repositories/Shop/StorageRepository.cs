using Phone.Data;
using Phone.Repositories.Shop.Interfaces;

namespace Phone.Repositories.Shop
{
    public class StorageRepository : MainRepository, IStorageRepository
    {
        public StorageRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
