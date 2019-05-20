using Microsoft.AspNetCore.Mvc;
using Moq;
using Phone.Controllers.User;
using Phone.Data.DTOs.User;
using Phone.Data.Entities.User;
using Phone.Exceptions;
using Phone.Services.User.Interfaces;
using PhoneUnitTests.User.Helpers;
using System.Threading.Tasks;
using Xunit;

namespace PhoneUnitTests.User.Controllers
{
    public class UserControllerTest
    {
        private readonly Mock<IUserService> mockUserService;
        private readonly Mock<IProfileService> mockProfileService;
        private readonly UserTestHelper userHelper;

        public UserControllerTest()
        {
            mockUserService = new Mock<IUserService>();
            mockProfileService = new Mock<IProfileService>();
            userHelper = new UserTestHelper();
        }

        #region UserController.Single
        /// <summary>
        /// Test for checking return one user
        /// <summary>
        [Fact]
        public async Task GetUserTestAsync()
        {
            // Arrange
            mockUserService.Setup(service => service.GetUserByIdAsync(It.IsAny<string>())).ReturnsAsync(await userHelper.GetUser());
            mockUserService.Setup(service => service.GetRoleByUserId(It.IsAny<ApplicationUser>())).ReturnsAsync(await userHelper.GetRole());
            mockProfileService.Setup(service => service.GetProfileByUserId(It.IsAny<string>())).Returns(ProfileTestHelper.GetProfile());
            UserController controller = new UserController(mockUserService.Object, mockProfileService.Object);

            // Act
            var result = await controller.Single(It.IsAny<string>());
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<UserSingleDto>(okResult.Value);

            // Assert
            Assert.Equal((await userHelper.GetUser()).Email, model.UserInfo.Email);
            Assert.Equal((await ProfileTestHelper.GetProfile()).Name, model.ProfileInfo.Name);
            Assert.Equal((await userHelper.GetRole()), model.RoleInfo);
        }

        /// <summary>
        /// Test for checking exception for return one user
        /// <summary>
        [Fact]
        public async void GetUserByIdFailedAsync()
        {
            // Arrange
            mockUserService.Setup(service => service.GetUserByIdAsync(It.IsAny<string>())).Throws(new CurrentEntryNotFoundException("Specified User not found"));
            UserController controller = new UserController(mockUserService.Object, mockProfileService.Object);

            var ex = await Assert.ThrowsAsync<CurrentEntryNotFoundException>(() => controller.Single(It.IsAny<string>()));
            Assert.Equal("Specified User not found", ex.Message);
        }
        #endregion UserController.Single


        #region UserController.Create
        /// <summary>
        /// Test for checking create user
        /// <summary>
        [Fact]
        public async Task CreateUserTestAsync()
        {
            // Arrange
            Mock<ApplicationUser> mockUser = new Mock<ApplicationUser>();
            Mock<UserController> mockControler = new Mock<UserController>(mockUserService.Object, mockProfileService.Object) { CallBase = true };
            mockControler.SetupGet(mock => mock.BaseApiUrl).Returns(It.IsAny<string>());
            
            // Act
            var result = await mockControler.Object.Create(await userHelper.GetUserCreateNormal());
            var okResult = Assert.IsType<CreatedResult>(result);

            // Assert
            Assert.IsType<CreatedResult>(result);
            mockUserService.Verify(
                mock => mock.CreateUserAsync(mockUser.Object, It.IsAny<string>()), Times.Never());
        }

        /// <summary>
        /// Test for checking failed create user (error validation)
        /// <summary>
        [Fact]
        public async void IsBadRequestObjectResultCreateUserAsync()
        {
            // Arrange
            Mock<ApplicationUser> mockUser = new Mock<ApplicationUser>();
            UserController controller = new UserController(mockUserService.Object, mockProfileService.Object);

            // Act
            controller.ModelState.AddModelError("ConfirmPassword", "Passwords don't match");
            var result = await controller.Create(await userHelper.GetUserCreateUnNormal());
           
            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
        #endregion UserController.Create



    }
}
