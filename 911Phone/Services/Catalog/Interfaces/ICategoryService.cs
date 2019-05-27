using Phone.Data.Entities.Catalog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phone.Services.Catalog.Interfaces
{
    public interface ICategoryService
    {
        Task<Category> SingleCategory(int categoryId);
        Task<IList<Category>> ListCategories();
        Task CreateCategory(string title, int? parentId);
    }
}
