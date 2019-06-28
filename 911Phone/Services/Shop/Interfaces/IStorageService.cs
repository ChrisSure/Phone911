using Phone.Data.Entities.Shop;
using System.Threading.Tasks;

namespace Phone.Services.Shop.Interfaces
{
    public interface IStorageService
    {
        Task Add(Storage storage);
        Task Delete(Storage storage);
    }
}
