using Microsoft.EntityFrameworkCore;
using Phone.Data;
using Phone.Data.Entities.Catalog;
using Phone.Exceptions;
using Phone.Repositories.Catalog.Interfaces;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;


namespace Phone.Repositories.Catalog
{
    public class CategoryRepository : ICategoryRepository
    {
        private ApplicationDbContext dbContext;

        public CategoryRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Method return list categories
        /// <summary>
        /// <returns>IList<Category></Category></returns>
        public async Task<IList<Category>> ListCategoriesAsync()
        {
            return await dbContext.Categories.OrderBy(c => c.Left).ToListAsync();
        }

        /// <summary>
        /// Method return category by categoryId
        /// <summary>
        /// <param name="categoryId">int</param>
        /// <returns>Category</returns>
        public async Task<Category> SingleCategoryAsync(int categoryId)
        {
            var category = await dbContext.Categories.FindAsync(categoryId);
            if (category == null)
            {
                throw new CurrentEntryNotFoundException();
            }
            return category;
        }

        /// <summary>
        /// Method create category
        /// <summary>
        /// <param name="title">string</param>
        /// <param name="parentId">int</param>
        /// <returns>void</returns>
        public async Task CreateCategoryAsync(string title, int? parentId)
        {
            try
            {
                await dbContext.Database.ExecuteSqlCommandAsync(
                    $"EXEC [Categories.Create] {title}, {parentId}");
            } catch (SqlException ex)
            {
                Helpers.SqlExceptionTranslator.ReThrow(ex, "Create Category");
            }
        }

        /// <summary>
        /// Method delete category
        /// <summary>
        /// <param name="categoryId">int</param>
        /// <returns>void</returns>
        public async Task DeleteCategoryAsync(int categoryId)
        {
            try
            {
                await dbContext.Database.ExecuteSqlCommandAsync(
                    $"EXEC [Categories.Delete] {categoryId}");
            }
            catch (SqlException ex)
            {
                Helpers.SqlExceptionTranslator.ReThrow(ex, "Delete Category");
            }
        }
    }
}
