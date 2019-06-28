using Phone.Data.DTOs.Shop;
using System.Threading.Tasks;

namespace PhoneUnitTests.Shop.Helpers
{
    class StorageTestHelper
    {
        /// <summary>
        /// Return list face data storage in like AddStorageDto 
        /// <summary>
        public static async Task<AddStorageDto> GetStorageCreateNormal()
        {
            return await Task.Run(() =>
                new AddStorageDto { Count = 2, ShopId = 1, ProductId = 1 }
            );
        }
    }
}
