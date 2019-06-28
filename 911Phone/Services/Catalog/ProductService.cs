using Phone.Data.Entities.Catalog;
using Phone.Repositories.Catalog.Interfaces;
using Phone.Services.Catalog.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phone.Services.Catalog
{
    public class ProductService : IProductService
    {
        private IProductRepository productRepository;

        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        /// <summary>
        /// Method delegate to service return list products by categoryId
        /// <summary>
        /// <param name="categoryId">int</param>
        /// <returns>IList<Product></returns>
        public async Task<IList<Product>> ListProductsByCategoryIdAll(int categoryId)
        {
            return await productRepository.ListProductsByCategoryIdAllAsync(categoryId);
        }

        /// <summary>
        /// Method delegate to service return one product
        /// <summary>
        /// <param name="productId">int</param>
        /// <returns>Product</returns>
        public async Task<Product> SingleProduct(int productId)
        {
            return await productRepository.SingleProductAsync(productId);
        }

        /// <summary>
        /// Method delegate to service create product
        /// <summary>
        /// <param name="product">Product</param>
        /// <returns>void</returns>
        public async Task CreateProduct(Product product)
        {
            product.CreatedAt = DateTime.Now;
            product.UpdatedAt = DateTime.Now;
            await productRepository.CreateProductAsync(product);
        }

        /// <summary>
        /// Method delegate to service update product
        /// <summary>
        /// <param name="productId">int</param>
        /// <param name="product">Product</param>
        /// <returns>void</returns>
        public async Task UpdateProduct(int productId, Product product)
        {
            var currentProduct = await productRepository.SingleLiteProductAsync(productId);
            product.Id = productId;
            product.CreatedAt = currentProduct.CreatedAt;
            product.UpdatedAt = DateTime.Now;
            await productRepository.UpdateProductAsync(product);
        }

        /// <summary>
        /// Method delegate to service delete product
        /// <summary>
        /// <param name="productId">int</param>
        /// <returns>void</returns>
        public async Task DeleteProduct(int productId)
        {
            Product product = await productRepository.SingleLiteProductAsync(productId);
            await productRepository.DeleteProductAsync(product);
        }
    }
}
