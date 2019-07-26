using Phone.Data.DTOs.Catalog;
using Phone.Data.Entities.Catalog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phone.Repositories.Catalog.Interfaces
{
    public interface IProductRepository
    {
        Task<IList<Product>> ListProductsByCategoryIdAllAsync(int categoryId);
        Task<IList<ProductListDto>> ListProductsByOrderIdAllAsync(int orderId);
        Task<Product> SingleProductAsync(int productId);
        Task<Product> SingleLiteProductAsync(int productId);
        Task CreateProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(Product product);
    }
}
