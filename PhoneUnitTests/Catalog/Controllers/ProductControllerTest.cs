using Microsoft.AspNetCore.Mvc;
using Moq;
using Phone.Controllers.Catalog;
using Phone.Data.DTOs.Catalog;
using Phone.Data.Entities.Catalog;
using Phone.Exceptions;
using Phone.Services.Catalog.Interfaces;
using PhoneUnitTests.Catalog.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace PhoneUnitTests.Catalog.Controllers
{
    public class ProductControllerTest
    {
        private readonly Mock<IProductService> mockProductService;

        public ProductControllerTest()
        {
            mockProductService = new Mock<IProductService>();
        }

        #region ProductController.Single
        /// <summary>
        /// Test for checking return one Product
        /// <summary>
        [Fact]
        public async Task GetProductTestAsync()
        {
            // Arrange
            mockProductService.Setup(service => service.SingleProduct(It.IsAny<int>())).ReturnsAsync(await ProductTestHelper.GetProduct());
            ProductController controller = new ProductController(mockProductService.Object);

            // Act
            var result = await controller.SingleAll(It.IsAny<int>());
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<ProductViewDto>(okResult.Value);

            // Assert
            Assert.Equal((await ProductTestHelper.GetProduct()).Title, model.Title);
        }

        /// <summary>
        /// Test for checking exception for return one Product
        /// <summary>
        [Fact]
        public async void GetCategoryByIdFailedAsync()
        {
            // Arrange
            mockProductService.Setup(service => service.SingleProduct(It.IsAny<int>())).Throws(new CurrentEntryNotFoundException("Current Product doesn't isset."));
            ProductController controller = new ProductController(mockProductService.Object);

            var ex = await Assert.ThrowsAsync<CurrentEntryNotFoundException>(() => controller.SingleAll(It.IsAny<int>()));
            Assert.Equal("Current Product doesn't isset.", ex.Message);
        }
        #endregion ProductController.Single


        #region ProductController.List
        /// <summary>
        /// Test for checking return list categories
        /// <summary>
        [Fact]
        public async Task GetListProductsByCategoryIdTestAsync()
        {
            // Arrange
            mockProductService.Setup(service => service.ListProductsByCategoryIdAll(It.IsAny<int>())).ReturnsAsync(await ProductTestHelper.GetProducts());
            ProductController controller = new ProductController(mockProductService.Object);

            // Act
            var result = await controller.ListByCategoryId(It.IsAny<int>());
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<List<ProductListDto>>(okResult.Value);

            // Assert
            Assert.Equal((await ProductTestHelper.GetProducts()).Where(c => c.Title == "Product_1").FirstOrDefault().Title, model.Where(c => c.Title == "Product_1").FirstOrDefault().Title);
            Assert.Equal((await ProductTestHelper.GetProducts()).Count, model.Count);
        }
        #endregion ProductController.List


        #region ProductController.Create
        /// <summary>
        /// Test for checking create product
        /// <summary>
        [Fact]
        public async Task CreateProductTestAsync()
        {
            // Arrange
            Mock<Product> mockProduct = new Mock<Product>();
            ProductController controller = new ProductController(mockProductService.Object);

            // Act
            var result = await controller.Create(await ProductTestHelper.GetProductCreateNormal());
            var okResult = Assert.IsType<OkObjectResult>(result);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            mockProductService.Verify(
                mock => mock.CreateProduct(mockProduct.Object), Times.Never());
        }

        /// <summary>
        /// Test for checking failed create product (error validation)
        /// <summary>
        [Fact]
        public async void IsBadRequestObjectResultCreateProductAsync()
        {
            // Arrange
            ProductController controller = new ProductController(mockProductService.Object);

            // Act
            controller.ModelState.AddModelError("Title", "Title is require value");
            var result = await controller.Create(await ProductTestHelper.GetProductCreateUnNormal());

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
        #endregion ProductController.Create


        #region ProductController.Update
        /// <summary>
        /// Test for checking update product
        /// <summary>
        [Fact]
        public async void UpdateOkProductAsync()
        {
            // Arrange
            Mock<Product> mockProduct = new Mock<Product>();
            ProductController controller = new ProductController(mockProductService.Object);

            // Act
            var result = await controller.Update(await ProductTestHelper.GetProductCreateNormal(), It.IsAny<int>());
            var okResult = Assert.IsType<OkObjectResult>(result);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            mockProductService.Verify(
                mock => mock.UpdateProduct(It.IsAny<int>(), mockProduct.Object), Times.Never());
        }
        #endregion ProductController.Update


        #region ProductController.Delete
        /// <summary>
        /// Test for checking delete product
        /// <summary>
        [Fact]
        public async void DeleteOkProductAsync()
        {
            // Arrange
            ProductController controller = new ProductController(mockProductService.Object);

            // Act
            var result = await controller.Delete(It.IsAny<int>());

            // Assert
            Assert.IsType<OkObjectResult>(result);
            mockProductService.Verify(
                mock => mock.DeleteProduct(It.IsAny<int>()));
        }

        /// <summary>
        /// Test for checking error delete product
        /// <summary>
        [Fact]
        public async void IsBadRequestObjectResultDeleteProductAsync()
        {
            // Arrange
            mockProductService.Setup(service => service.DeleteProduct(It.IsAny<int>())).Throws(new CurrentEntryNotFoundException("Current Product doesn't isset."));
            ProductController controller = new ProductController(mockProductService.Object);

            var ex = await Assert.ThrowsAsync<CurrentEntryNotFoundException>(() => controller.Delete(It.IsAny<int>()));
            Assert.Equal("Current Product doesn't isset.", ex.Message);
        }
        #endregion ProductController.Delete


    }
}
