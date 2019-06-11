using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Phone.Data.DTOs.Catalog;
using Phone.Data.Entities.Catalog;
using Phone.Services.Catalog.Interfaces;
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
            }
            ));
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
