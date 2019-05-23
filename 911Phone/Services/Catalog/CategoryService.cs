using Phone.Data.DTOs.Catalog;
using Phone.Data.Entities.Catalog;
using Phone.Repositories.Catalog.Interfaces;
using Phone.Services.Catalog.Interfaces;
using System.Collections.Generic;
using System.Linq;
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
        /// Method return list categories
        /// <summary>
        /// <returns>IList<Category></Category></returns>
        public async Task<IList<Category>> ListCategories()
        {
            return await categoryRepository.ListCategories();
        }

        /// <summary>
        /// Method return category by categoryId
        /// <summary>
        /// <param name="categoryId">int</param>
        /// <returns>Category</returns>
        public async Task<Category> SingleCategory(int categoryId)
        {
            return await categoryRepository.SingleCategoryAsync(categoryId);
        }
    }
}
