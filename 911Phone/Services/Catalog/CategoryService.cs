using Phone.Data.Entities.Catalog;
using Phone.Repositories.Catalog.Interfaces;
using Phone.Services.Catalog.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Phone.Services.Catalog
{
    public class CategoryService : ICategoryService
    {
        private ICategoryRepository categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        /// <summary>
        /// Method delegate to repository return list categories
        /// <summary>
        /// <returns>IList<Category></Category></returns>
        public async Task<IList<Category>> ListCategories()
        {
            return await categoryRepository.ListCategoriesAsync();
        }

        /// <summary>
        /// Method delegate to repository return list categories by shop id
        /// <summary>
        /// <param name="shopId">int</param>
        /// <returns>IList<Category></Category></returns>
        public async Task<IList<Category>> ListCategoriesByShopId(int shopId)
        {
            return await categoryRepository.ListCategoriesByShopIdAsync(shopId);
        }

        /// <summary>
        /// Method delegate to repository return category by categoryId
        /// <summary>
        /// <param name="categoryId">int</param>
        /// <returns>Category</returns>
        public async Task<Category> SingleCategory(int categoryId)
        {
            return await categoryRepository.SingleCategoryAsync(categoryId);
        }

        /// <summary>
        /// Method delegate to repository create category
        /// <summary>
        /// <param name="title">string</param>
        /// <param name="parentId">int</param>
        /// <returns>void</returns>
        public async Task CreateCategory(string title, int? parentId)
        {
            await categoryRepository.CreateCategoryAsync(title, parentId);
        }

        /// <summary>
        /// Method delegate to repository update category
        /// <summary>
        /// <param name="categoryId">int</param>
        /// <param name="title">string</param>
        /// <param name="parentId">int</param>
        /// <returns>void</returns>
        public async Task UpdateCategory(int categoryId, string title, int? parentId)
        {
            await categoryRepository.UpdateCategoryAsync(categoryId, title, parentId);
        }

        /// <summary>
        /// Method delegate to repository delete category
        /// <summary>
        /// <param name="categoryId">int</param>
        /// <returns>void</returns>
        public async Task DeleteCategory(int categoryId)
        {
            await categoryRepository.DeleteCategoryAsync(categoryId);
        }

        /// <summary>
        /// Method delegate to repository up category
        /// <summary>
        /// <param name="categoryId">int</param>
        /// <returns>void</returns>
        public async Task UpCategory(int categoryId)
        {
            await categoryRepository.UpCategoryAsync(categoryId);
        }

        /// <summary>
        /// Method delegate to repository down category
        /// <summary>
        /// <param name="categoryId">int</param>
        /// <returns>void</returns>
        public async Task DownCategory(int categoryId)
        {
            await categoryRepository.DownCategoryAsync(categoryId);
        }
    }
}
