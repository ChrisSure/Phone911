using Phone.Data.DTOs.Catalog;
using Phone.Data.Entities.Catalog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhoneUnitTests.Catalog.Helpers
{
    class OrderTestHelper
    {
        /// <summary>
        /// Return face data order
        /// <summary>
        public static async Task<Order> GetOrder()
        {
            return await Task.Run(() =>
                new Order { Id = 1, TotalSum = 9000 }
            );
        }

        /// <summary>
        /// Return face data list order
        /// <summary>
        public static async Task<IList<Order>> GetOrders()
        {
            return await Task.Run(() =>
                new List<Order>
                {
                    new Order { Id=1, TotalSum = 3000, TotalCount = 2 },
                    new Order { Id=2, TotalSum = 5000, TotalCount = 1 },
                    new Order { Id=3, TotalSum = 4000, TotalCount = 3 }
                }
            );
        }

        /// <summary>
        /// Return list face data order in like CreateOrderDto 
        /// <summary>
        public static async Task<CreateOrderDto> GetOrderCreateNormal()
        {
            return await Task.Run(() =>
                new CreateOrderDto { TotalSum = 3000, TotalCount = 2, CustomerId = null, SellerId = "699c510a-8031-48e1-a1b4-b2fcdfd01012", productOrder = new List<ProductOrderDto>() }
            );
        }
    }
}
