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


        #region UserController.Delete
        /// <summary>
        /// Test for checking delete user
        /// <summary>
        [Fact]
        public async void DeleteOkUserAsync()
        {
            // Arrange
            UserController controller = new UserController(mockUserService.Object, mockProfileService.Object);

            // Act
            var result = await controller.Delete(It.IsAny<string>());

            // Assert
            Assert.IsType<OkObjectResult>(result);
            mockUserService.Verify(
                mock => mock.DeleteUser(It.IsAny<string>()));
        }

        /// <summary>
        /// Test for checking error delete user
        /// <summary>
        [Fact]
        public async void IsBadRequestObjectResultDeleteUserAsync()
        {
            // Arrange
            mockUserService.Setup(service => service.DeleteUser(It.IsAny<string>())).Throws(new CurrentEntryNotFoundException("Specified User not found"));
            UserController controller = new UserController(mockUserService.Object, mockProfileService.Object);

            var ex = await Assert.ThrowsAsync<CurrentEntryNotFoundException>(() => controller.Delete(It.IsAny<string>()));
            Assert.Equal("Specified User not found", ex.Message);
        }
        #endregion UserController.Delete


        #region UserController.Update
        ///// <summary>
        ///// Test for checking update user password 
        ///// <summary>
        //[Fact]
        //public async void UpdatePasswordUserAsync()
        //{
        //    // Arrange
        //    Mock<ApplicationUser> mockUser = new Mock<ApplicationUser>();
        //    Mock<UserPasswordChangeDto> mockUserPasswordDto = new Mock<UserPasswordChangeDto>();
        //    mockUserService.Setup(service => service.CheckPassword(mockUser.Object, It.IsAny<string>())).ReturnsAsync(true);
        //    UserController controller = new UserController(mockUserService.Object, mockProfileService.Object);

        //    // Act
        //    var result = await controller.ChangePassword(mockUserPasswordDto.Object, It.IsAny<string>());

        //    // Assert
        //    Assert.IsType<OkObjectResult>(result);
        //    mockUserService.Verify(
        //        mock => mock.ChangePassword(mockUser.Object, It.IsAny<string>(), It.IsAny<string>()), Times.Never());
        //}

        /// <summary>
        /// Test for checking error update user password 
        /// <summary>
        [Fact]
        public async void UpdateBadRequestPasswordUserAsync()
        {
            // Arrange
            Mock<ApplicationUser> mockUser = new Mock<ApplicationUser>();
            Mock<UserPasswordChangeDto> mockUserPasswordDto = new Mock<UserPasswordChangeDto>();
            mockUserService.Setup(service => service.CheckPassword(mockUser.Object, It.IsAny<string>())).ReturnsAsync(false);
            UserController controller = new UserController(mockUserService.Object, mockProfileService.Object);

            // Act
            var result = await controller.ChangePassword(mockUserPasswordDto.Object, It.IsAny<string>());

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            mockUserService.Verify(
                mock => mock.ChangePassword(mockUser.Object, It.IsAny<string>(), It.IsAny<string>()), Times.Never());
        }

        /// <summary>
        /// Test for checking update user email 
        /// <summary>
        [Fact]
        public async void UpdateEmailUserAsync()
        {
            // Arrange
            Mock<UserBaseDto> mockUserBaseDto = new Mock<UserBaseDto>();
            UserController controller = new UserController(mockUserService.Object, mockProfileService.Object);

            // Act
            var result = await controller.ChangeEmail(mockUserBaseDto.Object, It.IsAny<string>());

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        /// <summary>
        /// Test for checking error update user email 
        /// <summary>
        [Fact]
        public async void UpdateEmailFailedUserAsync()
        {
            // Arrange
            Mock<UserBaseDto> mockUserBaseDto = new Mock<UserBaseDto>();
            mockUserService.Setup(service => service.ChangeEmail(It.IsAny<string>(), It.IsAny<string>())).Throws(new CurrentEntryNotFoundException("Specified User not found"));
            UserController controller = new UserController(mockUserService.Object, mockProfileService.Object);

            var ex = await Assert.ThrowsAsync<CurrentEntryNotFoundException>(() => controller.ChangeEmail(mockUserBaseDto.Object, It.IsAny<string>()));
            Assert.Equal("Specified User not found", ex.Message);
        }

        /// <summary>
        /// Test for checking update user role 
        /// <summary>
        [Fact]
        public async void UpdateRoleUserAsync()
        {
            // Arrange
            Mock<UserRoleDto> mockUserRoleDto = new Mock<UserRoleDto>();
            UserController controller = new UserController(mockUserService.Object, mockProfileService.Object);

            // Act
            var result = await controller.ChangeRole(mockUserRoleDto.Object, It.IsAny<string>());

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        /// <summary>
        /// Test for checking error update user role 
        /// <summary>
        [Fact]
        public async void UpdateRoleFailedUserAsync()
        {
            // Arrange
            Mock<UserRoleDto> mockUserRoleDto = new Mock<UserRoleDto>();
            mockUserService.Setup(service => service.ChangeRole(It.IsAny<string>(), It.IsAny<string>())).Throws(new CurrentEntryNotFoundException("Specified User not found"));
            UserController controller = new UserController(mockUserService.Object, mockProfileService.Object);

            var ex = await Assert.ThrowsAsync<CurrentEntryNotFoundException>(() => controller.ChangeRole(mockUserRoleDto.Object, It.IsAny<string>()));
            Assert.Equal("Specified User not found", ex.Message);
        }

        #endregion UserController.Update


        #region UserController.Profile
        /// <summary>
        /// Test for checking create profile user
        /// <summary>
        [Fact]
        public async Task CreateUserProfileTestAsync()
        {
            // Arrange
            Mock<Profile> mockProfile = new Mock<Profile>();
            Mock<ProfileCreatedDto> mockProfileCreatedDto = new Mock<ProfileCreatedDto>();
            Mock<UserController> mockControler = new Mock<UserController>(mockUserService.Object, mockProfileService.Object) { CallBase = true };
            mockControler.SetupGet(mock => mock.BaseApiUrl).Returns(It.IsAny<string>());

            // Act
            var result = await mockControler.Object.CreateProfile(mockProfileCreatedDto.Object);
            var okResult = Assert.IsType<CreatedResult>(result);

            // Assert
            Assert.IsType<CreatedResult>(result);
            mockProfileService.Verify(
                mock => mock.CreateProfileAsync(mockProfile.Object), Times.Never());
        }

        /// <summary>
        /// Test for checking update profile user password 
        /// <summary>
        [Fact]
        public async void UpdateProfileUserAsync()
        {
            // Arrange
            Mock<Profile> mockProfile = new Mock<Profile>();
            Mock<ProfileCreatedDto> mockProfileCreatedDto = new Mock<ProfileCreatedDto>();
            UserController controller = new UserController(mockUserService.Object, mockProfileService.Object);

            // Act
            var result = await controller.UpdateProfile(mockProfileCreatedDto.Object, It.IsAny<int>());

            // Assert
            Assert.IsType<OkObjectResult>(result);
            mockProfileService.Verify(
                mock => mock.UpdateProfileAsync(mockProfile.Object, It.IsAny<int>()), Times.Never());
        }

        ///// <summary>
        ///// Test for checking error update profile user email 
        ///// <summary>
        //[Fact]
        //public async void UpdateErrorProfileUserAsync()
        //{
        //    // Arrange
        //    Mock<Profile> mockProfile = new Mock<Profile>();
        //    Mock<ProfileCreatedDto> mockProfileCreatedDto = new Mock<ProfileCreatedDto>();
        //    mockProfileService.Setup(service => service.UpdateProfileAsync(mockProfile.Object, It.IsAny<int>())).Throws(new CurrentEntryNotFoundException("Specified Profile not found"));
        //    UserController controller = new UserController(mockUserService.Object, mockProfileService.Object);

        //    var ex = await Assert.ThrowsAsync<CurrentEntryNotFoundException>(() => controller.UpdateProfile(mockProfileCreatedDto.Object, It.IsAny<int>()));
        //    Assert.Equal("Specified Profile not found", ex.Message);
        //}
        #endregion UserController.Profile


    }
}
