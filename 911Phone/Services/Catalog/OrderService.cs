using Phone.Data.DTOs.Catalog;
using Phone.Data.Entities.Catalog;
using Phone.Repositories.Catalog.Interfaces;
using Phone.Services.Catalog.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phone.Services.Catalog
{
    public class OrderService : IOrderService
    {
        private IOrderRepository orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        /// <summary>
        /// Method delegate to service get list of orders
        /// <summary>
        /// <returns>IList<Order></returns>
        public async Task<IList<Order>> ListOrders()
        {
            return await orderRepository.ListOrdersAsync();
        }

        /// <summary>
        /// Method delegate to service create order
        /// <summary>
        /// <param name="createOrderDto">CreateOrderDto</param>
        /// <returns>void</returns>
        public async Task CreateOrder(CreateOrderDto createOrderDto)
        {
            await orderRepository.CreateOrderAsync(createOrderDto);
        }
    }
}
