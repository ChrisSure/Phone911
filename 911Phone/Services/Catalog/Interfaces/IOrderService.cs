using Phone.Data.DTOs.Catalog;
using Phone.Data.Entities.Catalog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phone.Services.Catalog.Interfaces
{
    public interface IOrderService
    {
        Task CreateOrder(CreateOrderDto createOrderDto);
        Task<IList<Order>> ListOrders();
    }
}
