using Phone.Data.Entities.Catalog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phone.Repositories.Catalog.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category> SingleCategoryAsync(int categoryId);
        Task<IList<Category>> ListCategories();
    }
}
