using Phone.Data.DTOs.Catalog;
using Phone.Data.Entities.Catalog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phone.Repositories.Catalog.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order> SingleOrderAsync(int orderId);
        Task CreateOrderAsync(CreateOrderDto createOrderDto);
        Task<IList<Order>> ListOrdersAsync();
        Task<IList<Order>> ListOrdersBySellerIdAsync(string sellerId);
        Task<IList<Order>> ListOrdersByCustomerIdAsync(string customerId);
        Task<IList<Order>> ListOrdersByShopIdAsync(int shopId);
        Task<IList<Order>> ListOrdersBySellerIdDetailAsync(string sellerId, DateTime start, DateTime finish);
    }
}
