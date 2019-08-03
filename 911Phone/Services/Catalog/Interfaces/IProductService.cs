using Phone.Data.DTOs.Catalog;
using Phone.Data.Entities.Catalog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phone.Services.Catalog.Interfaces
{
    public interface IProductService
    {
        Task<IList<Product>> ListProductsByCategoryIdAll(int categoryId);
        Task<IList<ProductListDto>> ListProductsByOrderIdAll(int orderId);
        Task<IList<Product>> ListByTitleMatch(string titleMatch, int shopId);
        Task<Product> SingleProduct(int productId);
        Task CreateProduct(Product product);
        Task UpdateProduct(int productId, Product product);
        Task DeleteProduct(int productId);
    }
}
