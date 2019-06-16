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
        /// Method delegate to service get order by id
        /// <summary>
        /// <param name="orderId">int</param>
        /// <returns>IList<Order></returns>
        public async Task<Order> SingleOrder(int orderId)
        {
            return await orderRepository.SingleOrderAsync(orderId);
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
        /// Method delegate to service get list of orders by seller id
        /// <summary>
        /// <param name="sellerId">string</param>
        /// <returns>IList<Order></returns>
        public async Task<IList<Order>> ListOrdersBySellerId(string sellerId)
        {
            return await orderRepository.ListOrdersBySellerIdAsync(sellerId);
        }

        /// <summary>
        /// Method delegate to service get list of orders by customer id
        /// <summary>
        /// <param name="customerId">string</param>
        /// <returns>IList<Order></returns>
        public async Task<IList<Order>> ListOrdersByCustomerId(string customerId)
        {
            return await orderRepository.ListOrdersByCustomerIdAsync(customerId);
        }

        /// <summary>
        /// Method delegate to service get list of orders by shop id
        /// <summary>
        /// <param name="shopId">int</param>
        /// <returns>IList<Order></returns>
        public async Task<IList<Order>> ListOrdersByShopId(int shopId)
        {
            return await orderRepository.ListOrdersByShopIdAsync(shopId);
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
