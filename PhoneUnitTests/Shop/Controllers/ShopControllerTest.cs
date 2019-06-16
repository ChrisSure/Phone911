using Microsoft.AspNetCore.Mvc;
using Moq;
using Phone.Controllers.Shop;
using Phone.Data.DTOs.Shop;
using Phone.Data.Entities.Shop;
using Phone.Exceptions;
using Phone.Services.Shop.Interfaces;
using PhoneUnitTests.Shop.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using ShopEntity = Phone.Data.Entities.Shop.Shop;

namespace PhoneUnitTests.Shop.Controllers
{
    public class ShopControllerTest
    {
        private readonly Mock<IShopService> mockShopService;

        public ShopControllerTest()
        {
            mockShopService = new Mock<IShopService>();
        }

        #region ShopController.Single
        /// <summary>
        /// Test for checking return one Shop
        /// <summary>
        [Fact]
        public async Task GetShopTestAsync()
        {
            // Arrange
            mockShopService.Setup(service => service.SingleShop(It.IsAny<int>())).ReturnsAsync(await ShopTestHelper.GetShop());
            ShopController controller = new ShopController(mockShopService.Object);

            // Act
            var result = await controller.Single(It.IsAny<int>());
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<ShopViewDto>(okResult.Value);

            // Assert
            Assert.Equal((await ShopTestHelper.GetShop()).Title, model.Title);
        }

        /// <summary>
        /// Test for checking exception for return one Product
        /// <summary>
        [Fact]
        public async void GetShopByIdFailedAsync()
        {
            // Arrange
            mockShopService.Setup(service => service.SingleShop(It.IsAny<int>())).Throws(new CurrentEntryNotFoundException("Current Shop doesn't isset."));
            ShopController controller = new ShopController(mockShopService.Object);

            var ex = await Assert.ThrowsAsync<CurrentEntryNotFoundException>(() => controller.Single(It.IsAny<int>()));
            Assert.Equal("Current Shop doesn't isset.", ex.Message);
        }
        #endregion ShopController.Single


        #region ShopController.List
        /// <summary>
        /// Test for checking return list shops
        /// <summary>
        [Fact]
        public async Task GetListShopTestAsync()
        {
            // Arrange
            mockShopService.Setup(service => service.ListShops()).ReturnsAsync(await ShopTestHelper.GetShops());
            ShopController controller = new ShopController(mockShopService.Object);

            // Act
            var result = await controller.List();
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<List<ShopListDto>>(okResult.Value);

            // Assert
            Assert.Equal((await ShopTestHelper.GetShops()).Where(c => c.Title == "Shop_1").FirstOrDefault().Title, model.Where(c => c.Title == "Shop_1").FirstOrDefault().Title);
            Assert.Equal((await ShopTestHelper.GetShops()).Count, model.Count);
        }

        /// <summary>
        /// Test for checking return list shops by seller id
        /// <summary>
        [Fact]
        public async Task GetListShopBySellerIdTestAsync()
        {
            // Arrange
            mockShopService.Setup(service => service.ListShopsBySellerId(It.IsAny<string>())).ReturnsAsync(await ShopTestHelper.GetShops());
            ShopController controller = new ShopController(mockShopService.Object);

            // Act
            var result = await controller.ListBySellerId(It.IsAny<string>());
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<List<ShopListDto>>(okResult.Value);

            // Assert
            Assert.Equal((await ShopTestHelper.GetShops()).Where(c => c.Title == "Shop_1").FirstOrDefault().Title, model.Where(c => c.Title == "Shop_1").FirstOrDefault().Title);
            Assert.Equal((await ShopTestHelper.GetShops()).Count, model.Count);
        }
        #endregion ShopController.List


        #region ShopController.Create
        /// <summary>
        /// Test for checking create shop
        /// <summary>
        [Fact]
        public async Task CreateShopTestAsync()
        {
            // Arrange
            Mock<ShopEntity> mockShop = new Mock<ShopEntity>();
            ShopController controller = new ShopController(mockShopService.Object);

            // Act
            var result = await controller.Create(await ShopTestHelper.GetShopCreateNormal());
            var okResult = Assert.IsType<OkObjectResult>(result);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            mockShopService.Verify(
                mock => mock.CreateShop(mockShop.Object), Times.Never());
        }

        /// <summary>
        /// Test for checking failed create shop (error validation)
        /// <summary>
        [Fact]
        public async void IsBadRequestObjectResultCreateShopAsync()
        {
            // Arrange
            ShopController controller = new ShopController(mockShopService.Object);

            // Act
            controller.ModelState.AddModelError("Title", "Title is require value");
            var result = await controller.Create(await ShopTestHelper.GetShopCreateUnNormal());

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
        #endregion ShopController.Create


        #region ShopController.Update
        /// <summary>
        /// Test for checking update shop
        /// <summary>
        [Fact]
        public async void UpdateOkShopAsync()
        {
            // Arrange
            Mock<ShopEntity> mockShop = new Mock<ShopEntity>();
            ShopController controller = new ShopController(mockShopService.Object);

            // Act
            var result = await controller.Update(await ShopTestHelper.GetShopCreateNormal(), It.IsAny<int>());
            var okResult = Assert.IsType<OkObjectResult>(result);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            mockShopService.Verify(
                mock => mock.UpdateShop(It.IsAny<int>(), mockShop.Object), Times.Never());
        }
        #endregion ShopController.Update


        #region ShopController.AddCategoryAndSellerToShop
        /// <summary>
        /// Test for checking add category to shop
        /// <summary>
        [Fact]
        public async Task AddCategoryToShopTestAsync()
        {
            // Arrange
            Mock<ShopCategory> mockShopCategory = new Mock<ShopCategory>();
            Mock<AddCategoryToShopDto> mockAddCategoryToShopDto = new Mock<AddCategoryToShopDto>();
            ShopController controller = new ShopController(mockShopService.Object);

            // Act
            var result = await controller.AddCategoryToShop(mockAddCategoryToShopDto.Object);
            var okResult = Assert.IsType<OkObjectResult>(result);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            mockShopService.Verify(
                mock => mock.AddCategoryToShop(mockShopCategory.Object), Times.Never());
        }

        /// <summary>
        /// Test for checking add seller to shop
        /// <summary>
        [Fact]
        public async Task AddSellerToShopTestAsync()
        {
            // Arrange
            Mock<ShopSeller> mockShopSeller = new Mock<ShopSeller>();
            Mock<AddSellerToShopDto> mockAddSellerToShopDto = new Mock<AddSellerToShopDto>();
            ShopController controller = new ShopController(mockShopService.Object);

            // Act
            var result = await controller.AddSellerToShop(mockAddSellerToShopDto.Object);
            var okResult = Assert.IsType<OkObjectResult>(result);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            mockShopService.Verify(
                mock => mock.AddSellerToShop(mockShopSeller.Object), Times.Never());
        }
        #endregion ShopController.AddCategoryAndSellerToShop


        #region ShopController.RemoveSellerFromShop
        /// <summary>
        /// Test for checking remove seller from shop
        /// <summary>
        [Fact]
        public async void RemoveSellerFromShopAsync()
        {
            // Arrange
            Mock<ShopSeller> mockSeller = new Mock<ShopSeller>();
            Mock<AddSellerToShopDto> mockAddSellerToShopDto = new Mock<AddSellerToShopDto>();
            ShopController controller = new ShopController(mockShopService.Object);

            // Act
            var result = await controller.RemoveSellerFromShop(mockAddSellerToShopDto.Object);
            var okResult = Assert.IsType<OkObjectResult>(result);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            mockShopService.Verify(
                mock => mock.RemoveSellerFromShop(mockSeller.Object), Times.Never());
        }
        #endregion ShopController.RemoveSellerFromShop


    }
}
