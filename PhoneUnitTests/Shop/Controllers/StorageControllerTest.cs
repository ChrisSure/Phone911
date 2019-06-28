using Microsoft.AspNetCore.Mvc;
using Moq;
using Phone.Controllers.Shop;
using Phone.Data.Entities.Shop;
using Phone.Services.Shop.Interfaces;
using PhoneUnitTests.Shop.Helpers;
using System.Threading.Tasks;
using Xunit;

namespace PhoneUnitTests.Shop.Controllers
{
    public class StorageControllerTest
    {
        private readonly Mock<IStorageService> mockStorageService;

        public StorageControllerTest()
        {
            mockStorageService = new Mock<IStorageService>();
        }

        /// <summary>
        /// Test for checking add storage products to shop
        /// <summary>
        [Fact]
        public async Task AddStorageTestAsync()
        {
            // Arrange
            Mock<Storage> mockStorage = new Mock<Storage>();
            StorageController controller = new StorageController(mockStorageService.Object);

            // Act
            var result = await controller.Add(await StorageTestHelper.GetStorageCreateNormal());
            var okResult = Assert.IsType<OkObjectResult>(result);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            mockStorageService.Verify(
                mock => mock.Add(mockStorage.Object), Times.Never());
        }

        /// <summary>
        /// Test for checking delete storage products to shop
        /// <summary>
        [Fact]
        public async Task DeleteStorageTestAsync()
        {
            // Arrange
            Mock<Storage> mockStorage = new Mock<Storage>();
            StorageController controller = new StorageController(mockStorageService.Object);

            // Act
            var result = await controller.Delete(await StorageTestHelper.GetStorageCreateNormal());
            var okResult = Assert.IsType<OkObjectResult>(result);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            mockStorageService.Verify(
                mock => mock.Delete(mockStorage.Object), Times.Never());
        }

    }
}
