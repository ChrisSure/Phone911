using Microsoft.Extensions.Configuration;
using Moq;
using Phone.Data.Entities.User;
using Phone.Repositories.User.Interfaces;
using Phone.Services.User;
using Phone.Services.User.Interfaces;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Xunit;


namespace PhoneUnitTests.User.Services
{
    public class JwtServiceTest
    {
        private readonly Mock<IUserRefreshTokenRepository> mockRefreshRepository;
        private readonly Mock<IUserService> mockUserService;
        private readonly Mock<IConfiguration> mockConfiguration;
        private readonly Mock<ClaimsPrincipal> mockClaimsPrincipal;
        private readonly Mock<UserRefreshToken> mockUserRefreshToken;
        private readonly Mock<ApplicationUser> mockUser;

        public JwtServiceTest()
        {
            mockRefreshRepository = new Mock<IUserRefreshTokenRepository>();
            mockUserService = new Mock<IUserService>();
            mockConfiguration = new Mock<IConfiguration>();
            mockClaimsPrincipal = new Mock<ClaimsPrincipal>();
            mockUserRefreshToken = new Mock<UserRefreshToken>();
            mockUser = new Mock<ApplicationUser>();
        }

        /// <summary>
        /// Test for checking generation refresh token
        /// <summary>
        [Fact]
        public void GenerateJwtRefreshTokenReturnsStringLength44()
        {
            JwtService jwtService = new JwtService(mockRefreshRepository.Object, mockUserService.Object, mockConfiguration.Object);
            var refreshToken = jwtService.GenerateJwtRefreshToken();

            Assert.Equal(44, refreshToken.Length);
        }

        /// <summary>
        /// Test for checking create refresh token
        /// <summary>
        [Fact]
        public async void LoginByRefreshTokenMustUpdateRefreshTokenWhenItExisitsAsync()
        {
            mockRefreshRepository.Setup(refreshRepository => refreshRepository.GetByUserIdAsync(It.IsAny<string>())).ReturnsAsync(mockUserRefreshToken.Object);

            JwtService jwtService = new JwtService(mockRefreshRepository.Object, mockUserService.Object, mockConfiguration.Object);
            await jwtService.LoginByRefreshTokenAsync("id", "token");

            mockRefreshRepository.Verify(refreshRepository => refreshRepository.UpdateAsync(mockUserRefreshToken.Object), Times.Once);
            mockRefreshRepository.Verify(refreshRepository => refreshRepository.CreateAsync(It.IsAny<UserRefreshToken>()), Times.Never);
        }

        /// <summary>
        /// Test for checking update refresh token
        /// <summary>
        [Fact]
        public async void LoginByRefreshTokenMustCreateRefreshTokenWhenUserNotHaveYetAsync()
        {
            mockRefreshRepository.Setup(refreshRepository => refreshRepository.GetByUserIdAsync(It.IsAny<string>())).ReturnsAsync((UserRefreshToken)null);

            JwtService jwtService = new JwtService(mockRefreshRepository.Object, mockUserService.Object, mockConfiguration.Object);
            await jwtService.LoginByRefreshTokenAsync("id", "token");

            mockRefreshRepository.Verify(refreshRepository => refreshRepository.UpdateAsync(It.IsAny<UserRefreshToken>()), Times.Never);
            mockRefreshRepository.Verify(refreshRepository => refreshRepository.CreateAsync(It.IsAny<UserRefreshToken>()), Times.Once);
        }

        /// <summary>
        /// Test for checking exists username in claims
        /// <summary>
        [Fact]
        public async void GetClaimsMustWriteUserNameInClaims()
        {
            var roles = new List<string> { "somerole" };
            mockUserService.Setup(userService => userService.GetUserRolesAsync(mockUser.Object)).ReturnsAsync(roles);
            mockUser.SetupGet(user => user.UserName).Returns("userName");
            mockUser.SetupGet(user => user.Email).Returns("email");
            mockUser.SetupGet(user => user.Id).Returns("id");

            JwtService jwtService = new JwtService(mockRefreshRepository.Object, mockUserService.Object, mockConfiguration.Object);
            var actualClaims = await jwtService.GetClaimsAsync(mockUser.Object);
            var existsUserName = new List<Claim>(actualClaims)
                .Exists(claim => claim.Type == JwtRegisteredClaimNames.Sub && claim.Value == "userName");

            mockUserService.Verify();
            mockUser.Verify();
            Assert.True(existsUserName);
        }

        /// <summary>
        /// Test for checking exists email in claims
        /// <summary>
        [Fact]
        public async void GetClaimsMustWriteEmailInClaims()
        {
            var roles = new List<string> { "somerole" };
            mockUserService.Setup(userService => userService.GetUserRolesAsync(mockUser.Object)).ReturnsAsync(roles);
            mockUser.SetupGet(user => user.UserName).Returns("userName");
            mockUser.SetupGet(user => user.Email).Returns("email");
            mockUser.SetupGet(user => user.Id).Returns("id");

            JwtService jwtService = new JwtService(mockRefreshRepository.Object, mockUserService.Object, mockConfiguration.Object);
            var actualClaims = await jwtService.GetClaimsAsync(mockUser.Object);
            var expectedClaim = new Claim(JwtRegisteredClaimNames.Email, "email");
            var existsEmail = new List<Claim>(actualClaims)
                .Exists(claim => claim.Type == JwtRegisteredClaimNames.Email && claim.Value == "email");

            mockUserService.Verify();
            mockUser.Verify();
            Assert.True(existsEmail);
        }

        /// <summary>
        /// Test for checking exists id in claims
        /// <summary>
        [Fact]
        public async void GetClaimsMustWriteIdInClaims()
        {
            var roles = new List<string> { "somerole" };
            mockUserService.Setup(userService => userService.GetUserRolesAsync(mockUser.Object)).ReturnsAsync(roles);
            mockUser.SetupGet(user => user.UserName).Returns("userName");
            mockUser.SetupGet(user => user.Email).Returns("email");
            mockUser.SetupGet(user => user.Id).Returns("id");

            JwtService jwtService = new JwtService(mockRefreshRepository.Object, mockUserService.Object, mockConfiguration.Object);
            var actualClaims = await jwtService.GetClaimsAsync(mockUser.Object);
            var expectedClaim = new Claim("uid", "id");
            var existsId = new List<Claim>(actualClaims)
                .Exists(claim => claim.Type == "uid" && claim.Value == "id");

            mockUserService.Verify();
            mockUser.Verify();
            Assert.True(existsId);
        }

        /// <summary>
        /// Test for checking exists somerole in claims
        /// <summary>
        [Fact]
        public async void GetClaimsMustWriteRoleInClaims()
        {
            var roles = new List<string> { "somerole" };
            mockUserService.Setup(userService => userService.GetUserRolesAsync(mockUser.Object)).ReturnsAsync(roles);
            mockUser.SetupGet(user => user.UserName).Returns("userName");
            mockUser.SetupGet(user => user.Email).Returns("email");
            mockUser.SetupGet(user => user.Id).Returns("id");

            JwtService jwtService = new JwtService(mockRefreshRepository.Object, mockUserService.Object, mockConfiguration.Object);
            var actualClaims = await jwtService.GetClaimsAsync(mockUser.Object);
            var existsRole = new List<Claim>(actualClaims)
                .Exists(claim => claim.Type == ClaimTypes.Role && claim.Value == "somerole");

            mockUserService.Verify();
            mockUser.Verify();
            Assert.True(existsRole);
        }

        /// <summary>
        /// Test for checking exists jti key in claims
        /// <summary>
        [Fact]
        public async void GetClaimsMustGenerateJtiInClaims()
        {
            var roles = new List<string> { "somerole" };
            mockUserService.Setup(userService => userService.GetUserRolesAsync(mockUser.Object)).ReturnsAsync(roles);
            mockUser.SetupGet(user => user.UserName).Returns("userName");
            mockUser.SetupGet(user => user.Email).Returns("email");
            mockUser.SetupGet(user => user.Id).Returns("id");

            JwtService jwtService = new JwtService(mockRefreshRepository.Object, mockUserService.Object, mockConfiguration.Object);
            var actualClaims = await jwtService.GetClaimsAsync(mockUser.Object);
            var existsJti = new List<Claim>(actualClaims)
                .Exists(claim => claim.Type == JwtRegisteredClaimNames.Jti);

            mockUserService.Verify();
            mockUser.Verify();
            Assert.True(existsJti);
        }
    }
}
