using Phone.Data.DTOs.Catalog;
using Phone.Data.Entities.Catalog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phone.Services.Catalog.Interfaces
{
    public interface IOrderService
    {
        Task<Order> SingleOrder(int orderId);
        Task CreateOrder(CreateOrderDto createOrderDto);
        Task<IList<Order>> ListOrders();
        Task<IList<Order>> ListOrdersBySellerId(string sellerId);
        Task<IList<Order>> ListOrdersByCustomerId(string customerId);
        Task<IList<Order>> ListOrdersByShopId(int shopId);
        Task<IList<Order>> ListOrdersBySellerIdDetail(string sellerId, DateTime start, DateTime finish);
    }
}
