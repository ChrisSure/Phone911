using Xunit;
using Moq;
using Phone.Services.User.Interfaces;
using Phone.Controllers.User;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Phone.Data.DTOs.User;
using Phone.Exceptions;
using System.Security.Claims;
using System;
using Phone.Data.Entities.User;


namespace PhoneUnitTests.User.Controllers
{
    public class AuthControllerTest
    {
        private readonly Mock<IUserService> mockUserService;
        private readonly Mock<IJwtService> mockJwtService;
        private readonly Mock<ApplicationUser> mockUser;
        private readonly Mock<AuthLoginDto> mockAuthLoginDto;

        public AuthControllerTest()
        {
            mockUserService = new Mock<IUserService>();
            mockJwtService = new Mock<IJwtService>();
            mockUser = new Mock<ApplicationUser>();
            mockAuthLoginDto = new Mock<AuthLoginDto>();
        }

        /// <summary>
        /// Test for checking uncorrectly data user during authentication
        /// <summary>
        [Fact]
        public async Task UnCorrectlyLoginOrPasswordTestAsync()
        {
            // Arrange
            mockUserService.Setup(service => service.FindUserByEmailAsync(It.IsAny<string>())).Throws(new CurrentEntryNotFoundException());
            AuthController controller = new AuthController(mockJwtService.Object, mockUserService.Object);

            //Act
            var result = await controller.LoginAsync(mockAuthLoginDto.Object);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// <summary>
        /// Test for checking correctly data user during authentication
        /// <summary>
        [Theory]
        [InlineData("token1", "token1")]
        [InlineData("token2", "token2")]
        [InlineData("token1", "token2")]
        [InlineData("token2", "token1")]
        [InlineData("somedata", "somedata")]
        public async Task CorrectlyLoginTestAsync(string accessToken, string refreshToken)
        {
            // Arrange
            var userClaims = It.IsAny<Claim[]>();
            var expectedAccessToken = accessToken;
            var expectedRefreshToken = refreshToken;
            var expectedTime = DateTime.Now.AddMinutes(120);
            var expectedTokens = new AuthTokensDto
            {
                AccessToken = expectedAccessToken,
                RefreshToken = expectedRefreshToken,
                ExpireOn = expectedTime
            };
            mockUserService.Setup(userService => userService.FindUserByEmailAsync(It.IsAny<string>())).ReturnsAsync(mockUser.Object);
            mockUserService.Setup(userService => userService.CheckPasswordAsync(mockUser.Object, It.IsAny<string>())).ReturnsAsync(true);
            mockJwtService.Setup(jwtService => jwtService.GetClaimsAsync(mockUser.Object)).ReturnsAsync(userClaims);
            mockJwtService.Setup(jwtService => jwtService.GenerateJwtAccessToken(userClaims)).Returns(expectedAccessToken);
            mockJwtService.Setup(jwtService => jwtService.GenerateJwtRefreshToken()).Returns(expectedRefreshToken);
            mockJwtService.Setup(jwtService => jwtService.LoginByRefreshTokenAsync(Guid.NewGuid().ToString(), expectedRefreshToken));
            mockJwtService.Setup(jwtService => jwtService.ExpirationTime).Returns(expectedTime);

            //Act
            AuthController authController = new AuthController(mockJwtService.Object, mockUserService.Object);
            var result = await authController.LoginAsync(mockAuthLoginDto.Object);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualTokens = Assert.IsAssignableFrom<AuthTokensDto>(okResult.Value);
            Assert.Equal(expectedTokens, actualTokens);
        }





    }
}
