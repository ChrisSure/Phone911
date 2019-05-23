using Microsoft.EntityFrameworkCore;
using Phone.Data;
using Phone.Data.Entities.Catalog;
using Phone.Exceptions;
using Phone.Repositories.Catalog.Interfaces;
using System.Collections.Generic;
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
        public async Task<IList<Category>> ListCategories()
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
    }
}
