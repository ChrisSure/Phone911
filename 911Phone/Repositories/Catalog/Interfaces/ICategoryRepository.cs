using Phone.Data.Entities.Catalog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phone.Repositories.Catalog.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category> SingleCategoryAsync(int categoryId);
        Task<IList<Category>> ListCategoriesAsync();
        Task<IList<Category>> ListCategoriesByShopIdAsync(int shopId);
        Task CreateCategoryAsync(string title, int? parentId);
        Task UpdateCategoryAsync(int categoryId, string title, int? parentId);
        Task DeleteCategoryAsync(int categoryId);
        Task UpCategoryAsync(int categoryId);
        Task DownCategoryAsync(int categoryId);
    }
}
