using Phone.Data;
using Phone.Data.DTOs.Catalog;
using Phone.Data.Entities.Catalog;
using Phone.Repositories.Catalog.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phone.Repositories.Catalog
{
    public class OrderRepository : MainRepository, IOrderRepository
    {

        public OrderRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        /// <summary>
        /// Method get list of orders
        /// <summary>
        /// <returns>IList<Order></returns>
        public async Task<IList<Order>> ListOrdersAsync()
        {
            return await Task.Run(() => dbContext.Orders.ToList());
        }

        /// <summary>
        /// Method create order
        /// <summary>
        /// <param name="createOrderDto">CreateOrderDto</param>
        /// <returns>void<Product></returns>
        public async Task CreateOrderAsync(CreateOrderDto createOrderDto)
        {
            using (var transaction = dbContext.Database.BeginTransaction())
            {
                try
                {
                    Order order = new Order
                    {
                        TotalSum = createOrderDto.TotalSum,
                        TotalCount = createOrderDto.TotalCount,
                        CustomerId = createOrderDto.CustomerId,
                        SellerId = createOrderDto.SellerId,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    };
                    await dbContext.Orders.AddAsync(order);

                    foreach(var po in createOrderDto.productOrder)
                    {
                        ProductOrder productOrder = new ProductOrder { OrderId = order.Id, ProductId = po.ProductId };
                        order.ProductOrder.Add(productOrder);
                    }

                    await SaveAsync();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
            }
        }
    }
}
