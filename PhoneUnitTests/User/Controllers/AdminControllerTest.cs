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
    public class AdminControllerTest
    {
        private readonly Mock<IAdminService> mockAdminService;
        private readonly Mock<UserViewDto> mockUserViewDto;
        private readonly UserTestHelper userHelper;

        public AdminControllerTest()
        {
            mockAdminService = new Mock<IAdminService>();
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
            mockAdminService.Setup(service => service.ListAdminsAsync()).ReturnsAsync(await userHelper.GetUsers());
            AdminController controller = new AdminController(mockAdminService.Object);

            // Act
            var result = await controller.ListAdmins();
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<List<UserViewDto>>(okResult.Value);

            // Assert
            Assert.Equal((await userHelper.GetUsers()).FirstOrDefault(f => f.Email == userHelper.EmailUser).Email, model.FirstOrDefault(f => f.Email == userHelper.EmailUser).Email);
        }

    }
}
