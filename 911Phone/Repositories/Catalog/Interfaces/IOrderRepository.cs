using Phone.Data.DTOs.Catalog;
using Phone.Data.Entities.Catalog;
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
    }
}
