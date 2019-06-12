using Microsoft.AspNetCore.Mvc;
using Moq;
using Phone.Controllers.Catalog;
using Phone.Data.DTOs.Catalog;
using Phone.Exceptions;
using Phone.Services.Catalog.Interfaces;
using PhoneUnitTests.Catalog.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace PhoneUnitTests.Catalog.Controllers
{
    public class OrderControllerTest
    {
        private readonly Mock<IOrderService> mockOrderService;

        public OrderControllerTest()
        {
            mockOrderService = new Mock<IOrderService>();
        }

        #region OrderController.Single
        /// <summary>
        /// Test for checking return one Order
        /// <summary>
        [Fact]
        public async Task GetOrderTestAsync()
        {
            // Arrange
            mockOrderService.Setup(service => service.SingleOrder(It.IsAny<int>())).ReturnsAsync(await OrderTestHelper.GetOrder());
            OrderController controller = new OrderController(mockOrderService.Object);

            // Act
            var result = await controller.Single(It.IsAny<int>());
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<OrderViewDto>(okResult.Value);

            // Assert
            Assert.Equal((await OrderTestHelper.GetOrder()).TotalSum, model.TotalSum);
        }

        /// <summary>
        /// Test for checking exception for return one Order
        /// <summary>
        [Fact]
        public async void GetOrderByIdFailedAsync()
        {
            // Arrange
            mockOrderService.Setup(service => service.SingleOrder(It.IsAny<int>())).Throws(new CurrentEntryNotFoundException("Current Order doesn't isset."));
            OrderController controller = new OrderController(mockOrderService.Object);

            var ex = await Assert.ThrowsAsync<CurrentEntryNotFoundException>(() => controller.Single(It.IsAny<int>()));
            Assert.Equal("Current Order doesn't isset.", ex.Message);
        }
        #endregion OrderController.Single


        #region OrderController.List
        /// <summary>
        /// Test for checking return list orders
        /// <summary>
        [Fact]
        public async Task GetListOrdersTestAsync()
        {
            // Arrange
            mockOrderService.Setup(service => service.ListOrders()).ReturnsAsync(await OrderTestHelper.GetOrders());
            OrderController controller = new OrderController(mockOrderService.Object);

            // Act
            var result = await controller.List();
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<List<OrderListDto>>(okResult.Value);

            // Assert
            Assert.Equal((await OrderTestHelper.GetOrders()).Where(o => o.TotalSum == 4000).FirstOrDefault().TotalCount, model.Where(o => o.TotalSum == 4000).FirstOrDefault().TotalCount);
            Assert.Equal((await OrderTestHelper.GetOrders()).Count, model.Count);
        }
        #endregion OrderController.List


        #region OrderController.Create
        /// <summary>
        /// Test for checking create order
        /// <summary>
        [Fact]
        public async Task CreateOrderTestAsync()
        {
            // Arrange
            Mock<CreateOrderDto> mockOrderDto = new Mock<CreateOrderDto>();
            OrderController controller = new OrderController(mockOrderService.Object);

            // Act
            var result = await controller.Create(await OrderTestHelper.GetOrderCreateNormal());
            var okResult = Assert.IsType<OkObjectResult>(result);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            mockOrderService.Verify(
                mock => mock.CreateOrder(mockOrderDto.Object), Times.Never());
        }
        #endregion OrderController.Create
    }
}
