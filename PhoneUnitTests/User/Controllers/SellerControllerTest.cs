using Microsoft.AspNetCore.Mvc;
using Moq;
using Phone.Controllers.User;
using Phone.Data.DTOs.User;
using Phone.Services.User.Interfaces;
using PhoneUnitTests.User.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace PhoneUnitTests.User.Controllers
{
    public class SellerControllerTest
    {
        private readonly Mock<ISellerService> mockSellerService;
        private readonly Mock<UserViewDto> mockUserViewDto;
        private readonly UserTestHelper userHelper;

        public SellerControllerTest()
        {
            mockSellerService = new Mock<ISellerService>();
            mockUserViewDto = new Mock<UserViewDto>();
            userHelper = new UserTestHelper();
        }

        /// <summary>
        /// Test for checking return list users with role - admin
        /// <summary>
        [Fact]
        public async Task GetListAdminsTestAsync()
        {
            // Arrange
            mockSellerService.Setup(service => service.ListSellersAsync()).ReturnsAsync(await userHelper.GetUsers());
            SellerController controller = new SellerController(mockSellerService.Object);

            // Act
            var result = await controller.ListSellers();
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<List<UserViewDto>>(okResult.Value);

            // Assert
            Assert.Equal((await userHelper.GetUsers()).FirstOrDefault(f => f.Email == userHelper.EmailUser).Email, model.FirstOrDefault(f => f.Email == userHelper.EmailUser).Email);
        }
    }
}
