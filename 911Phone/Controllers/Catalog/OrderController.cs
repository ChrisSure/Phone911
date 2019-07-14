using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Phone.Data.DTOs.Catalog;
using Phone.Data.Entities.Catalog;
using Phone.Services.Catalog.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phone.Controllers.Catalog
{
    public class OrderController : MainController
    {
        private IOrderService orderService;
        private readonly IMapper dtoMapper;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
            dtoMapper = new Mapper(new MapperConfiguration(mapper =>
            {
                mapper.CreateMap<Order, OrderListDto>();
                mapper.CreateMap<Order, OrderViewDto>().ReverseMap();
            }
            ));
        }

        [HttpGet]
        [Route("api/orders/{orderId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Single([FromRoute] int orderId)
        {
            var order = dtoMapper.Map<Order, OrderViewDto>(await orderService.SingleOrder(orderId));
            return Ok(order);
        }

        [HttpGet]
        [Route("api/orders")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> List()
        {
            var orders = dtoMapper.Map<IList<Order>, IList<OrderListDto>>(await orderService.ListOrders());
            return Ok(orders);
        }

        [HttpGet]
        [Route("api/orders/{sellerId}/seller")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> ListBySellerId([FromRoute] string sellerId)
        {
            var orders = dtoMapper.Map<IList<Order>, IList<OrderListDto>>(await orderService.ListOrdersBySellerId(sellerId));
            return Ok(orders);
        }

        [HttpGet]
        [Route("api/orders/{sellerId}/{start}/{finish}/seller-detail")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> ListBySellerIdDetail([FromRoute] string sellerId, [FromRoute] DateTime start, [FromRoute] DateTime finish)
        {
            var orders = dtoMapper.Map<IList<Order>, IList<OrderListDto>>(await orderService.ListOrdersBySellerIdDetail(sellerId, start, finish));
            return Ok(orders);
        }

        [HttpGet]
        [Route("api/orders/{customerId}/customer")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> ListByCustomerId([FromRoute] string customerId)
        {
            var orders = dtoMapper.Map<IList<Order>, IList<OrderListDto>>(await orderService.ListOrdersByCustomerId(customerId));
            return Ok(orders);
        }

        [HttpGet]
        [Route("api/orders/{shopId}/shop")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> ListByShopId([FromRoute] int shopId)
        {
            var orders = dtoMapper.Map<IList<Order>, IList<OrderListDto>>(await orderService.ListOrdersByShopId(shopId));
            return Ok(orders);
        }

        [HttpPost]
        [Route("api/orders")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Create([FromBody] CreateOrderDto createOrderDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await orderService.CreateOrder(createOrderDto);
            return Ok("Order has created");
        }

    }
}
