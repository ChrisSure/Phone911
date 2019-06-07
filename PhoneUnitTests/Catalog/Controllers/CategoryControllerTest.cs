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
    public class CategoryControllerTest
    {
        private readonly Mock<ICategoryService> mockCategoryService;

        public CategoryControllerTest()
        {
            mockCategoryService = new Mock<ICategoryService>();
        }

        #region CategoryController.Single
        /// <summary>
        /// Test for checking return one category
        /// <summary>
        [Fact]
        public async Task GetCategoryTestAsync()
        {
            // Arrange
            mockCategoryService.Setup(service => service.SingleCategory(It.IsAny<int>())).ReturnsAsync(await CategoryTestHelper.GetCategory());
            CategoryController controller = new CategoryController(mockCategoryService.Object);

            // Act
            var result = await controller.Single(It.IsAny<int>());
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<CategoryViewDto>(okResult.Value);

            // Assert
            Assert.Equal((await CategoryTestHelper.GetCategory()).Title, model.Title);
        }

        /// <summary>
        /// Test for checking exception for return one category
        /// <summary>
        [Fact]
        public async void GetCategoryByIdFailedAsync()
        {
            // Arrange
            mockCategoryService.Setup(service => service.SingleCategory(It.IsAny<int>())).Throws(new CurrentEntryNotFoundException("Specified Category not found"));
            CategoryController controller = new CategoryController(mockCategoryService.Object);

            var ex = await Assert.ThrowsAsync<CurrentEntryNotFoundException>(() => controller.Single(It.IsAny<int>()));
            Assert.Equal("Specified Category not found", ex.Message);
        }
        #endregion CategoryController.Single


        #region CategoryController.List
        /// <summary>
        /// Test for checking return list categories
        /// <summary>
        [Fact]
        public async Task GetListCategoriesTestAsync()
        {
            // Arrange
            mockCategoryService.Setup(service => service.ListCategories()).ReturnsAsync(await CategoryTestHelper.GetCategories());
            CategoryController controller = new CategoryController(mockCategoryService.Object);

            // Act
            var result = await controller.List();
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<List<CategoryListDto>>(okResult.Value);

            // Assert
            Assert.Equal((await CategoryTestHelper.GetCategories()).Where(c => c.Id == 1).FirstOrDefault().Title, model.Where(c => c.Id == 1).FirstOrDefault().Title);
        }
        #endregion CategoryController.List


        #region CategoryController.Create
        /// <summary>
        /// Test for checking create category
        /// <summary>
        [Fact]
        public async Task CreateCategoryTestAsync()
        {
            // Arrange
            CategoryController controller = new CategoryController(mockCategoryService.Object);

            // Act
            var result = await controller.Create(await CategoryTestHelper.GetCategoryCreateNormal());
            var okResult = Assert.IsType<OkObjectResult>(result);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            mockCategoryService.Verify(
                mock => mock.CreateCategory(It.IsAny<string>(), It.IsAny<int?>()));
        }

        /// <summary>
        /// Test for checking failed create category (error validation)
        /// <summary>
        [Fact]
        public async void IsBadRequestObjectResultCreateCategoryAsync()
        {
            // Arrange
            CategoryController controller = new CategoryController(mockCategoryService.Object);

            // Act
            controller.ModelState.AddModelError("Title", "Title is require value");
            var result = await controller.Create(await CategoryTestHelper.GetCategoryCreateUnNormal());

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
        #endregion CategoryController.Create


        #region CategoryController.Update
        /// <summary>
        /// Test for checking update category
        /// <summary>
        [Fact]
        public async void UpdateOkCategoryAsync()
        {
            // Arrange
            CategoryController controller = new CategoryController(mockCategoryService.Object);

            // Act
            var result = await controller.Update(await CategoryTestHelper.GetCategoryCreateNormal(), It.IsAny<int>());
            var okResult = Assert.IsType<OkObjectResult>(result);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            mockCategoryService.Verify(
                mock => mock.UpdateCategory(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int?>()));
        }
        #endregion CategoryController.Update


        #region CategoryController.Delete
        /// <summary>
        /// Test for checking delete category
        /// <summary>
        [Fact]
        public async void DeleteOkCategoryAsync()
        {
            // Arrange
            CategoryController controller = new CategoryController(mockCategoryService.Object);

            // Act
            var result = await controller.Delete(It.IsAny<int>());

            // Assert
            Assert.IsType<OkObjectResult>(result);
            mockCategoryService.Verify(
                mock => mock.DeleteCategory(It.IsAny<int>()));
        }

        /// <summary>
        /// Test for checking error delete category
        /// <summary>
        [Fact]
        public async void IsBadRequestObjectResultDeleteCategoryAsync()
        {
            // Arrange
            mockCategoryService.Setup(service => service.DeleteCategory(It.IsAny<int>())).Throws(new CurrentEntryNotFoundException("Specified Category not found"));
            CategoryController controller = new CategoryController(mockCategoryService.Object);

            var ex = await Assert.ThrowsAsync<CurrentEntryNotFoundException>(() => controller.Delete(It.IsAny<int>()));
            Assert.Equal("Specified Category not found", ex.Message);
        }
        #endregion CategoryController.Delete


        #region CategoryController.Sorting
        /// <summary>
        /// Test for checking sort up category
        /// <summary>
        [Fact]
        public async void SortUpOkCategoryAsync()
        {
            // Arrange
            CategoryController controller = new CategoryController(mockCategoryService.Object);

            // Act
            var result = await controller.Up(It.IsAny<int>());
            var okResult = Assert.IsType<OkObjectResult>(result);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            mockCategoryService.Verify(
                mock => mock.UpCategory(It.IsAny<int>()));
        }

        /// <summary>
        /// Test for checking sort down category
        /// <summary>
        [Fact]
        public async void SortDownOkCategoryAsync()
        {
            // Arrange
            CategoryController controller = new CategoryController(mockCategoryService.Object);

            // Act
            var result = await controller.Down(It.IsAny<int>());
            var okResult = Assert.IsType<OkObjectResult>(result);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            mockCategoryService.Verify(
                mock => mock.DownCategory(It.IsAny<int>()));
        }
        #endregion CategoryController.Sorting

    }
}
