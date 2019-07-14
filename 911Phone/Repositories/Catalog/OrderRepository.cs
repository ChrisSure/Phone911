using Phone.Data;
using Phone.Data.DTOs.Catalog;
using Phone.Data.Entities.Catalog;
using Phone.Repositories.Catalog.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Phone.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Phone.Repositories.Catalog
{
    public class OrderRepository : MainRepository, IOrderRepository
    {

        public OrderRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        /// <summary>
        /// Method get order by id
        /// <summary>
        /// <param name="orderId">int</param>
        /// <returns>IList<Order></returns>
        public async Task<Order> SingleOrderAsync(int orderId)
        {
            var order = await Task.Run(() => dbContext.Orders.Where(o => o.Id == orderId).Select(o => new Order
            {
                Id = o.Id,
                TotalSum = o.TotalSum,
                TotalCount = o.TotalCount,
                Customer = o.Customer,
                Seller = o.Seller,
                Shop = o.Shop,
                CreatedAt = o.CreatedAt,
                UpdatedAt = o.UpdatedAt,
                ProductOrder = o.ProductOrder
            }).FirstOrDefault());
            if (order == null)
            {
                throw new CurrentEntryNotFoundException("Current Order doesn't isset.");
            }
            return order;
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
        /// Method get list of orders by seller id
        /// <summary>
        /// <param name="sellerId">string</param>
        /// <returns>IList<Order></returns>
        public async Task<IList<Order>> ListOrdersBySellerIdAsync(string sellerId)
        {
            return await Task.Run(() => dbContext.Orders.Where(o => o.SellerId == sellerId).ToList());
        }

        /// <summary>
        /// Method delegate to service get list of orders by seller id detail
        /// <summary>
        /// <param name="sellerId">string</param>
        /// <param name="start">DateTime</param>
        /// <param name="finish">DateTime</param>
        /// <returns>IList<Order></returns>
        public async Task<IList<Order>> ListOrdersBySellerIdDetailAsync(string sellerId, DateTime start, DateTime finish)
        {
            return await Task.Run(() => dbContext.Orders.Where(o => o.SellerId == sellerId).Where(o => o.CreatedAt >= start).Where(o => o.CreatedAt <= finish).ToList());
        }

        /// <summary>
        /// Method get list of orders by customer id
        /// <summary>
        /// <param name="customerId">string</param>
        /// <returns>IList<Order></returns>
        public async Task<IList<Order>> ListOrdersByCustomerIdAsync(string customerId)
        {
            return await Task.Run(() => dbContext.Orders.Where(o => o.CustomerId == customerId).ToList());
        }

        /// <summary>
        /// Method get list of orders by shop id
        /// <summary>
        /// <param name="shopId">int</param>
        /// <returns>IList<Order></returns>
        public async Task<IList<Order>> ListOrdersByShopIdAsync(int shopId)
        {
            return await Task.Run(() => dbContext.Orders.Where(o => o.ShopId == shopId).ToList());
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
                        ShopId = createOrderDto.ShopId,
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
