﻿using Phone.Data;
using Phone.Data.Entities.Catalog;
using Phone.Repositories.Catalog.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Phone.Exceptions;


namespace Phone.Repositories.Catalog
{
    public class ProductRepository : MainRepository, IProductRepository
    {
        public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        /// <summary>
        /// Method return list products by categoryId
        /// <summary>
        /// <param name="categoryId">int</param>
        /// <returns>IList<Product></returns>
        public async Task<IList<Product>> ListProductsByCategoryIdAllAsync(int categoryId)
        {
            return await Task.Run(() => dbContext.Products.Where(p => p.CategoryId == categoryId).ToList());
        }

        /// <summary>
        /// Method return one product
        /// <summary>
        /// <param name="productId">int</param>
        /// <returns>Product</returns>
        public async Task<Product> SingleProductAsync(int productId)
        {
            var product = await Task.Run(() => dbContext.Products.Where(p => p.Id == productId).Select(p => new Product {
                Title = p.Title, Image = p.Image, Price = p.Price, Text = p.Text, CreatedAt = p.CreatedAt, UpdatedAt = p.UpdatedAt, IsAproval = p.IsAproval, Category = p.Category
            }).FirstOrDefault());
            if (product == null)
            {
                throw new CurrentEntryNotFoundException("Current Product doesn't isset.");
            }
            return product;
        }

        /// <summary>
        /// Method return one product lite
        /// <summary>
        /// <param name="productId">int</param>
        /// <returns>Product</returns>
        public async Task<Product> SingleLiteProductAsync(int productId)
        {
            var product = await Task.Run(() => dbContext.Products.FindAsync(productId));
            if (product == null)
            {
                throw new CurrentEntryNotFoundException("Current Product doesn't isset.");
            }
            return product;
        }

        /// <summary>
        /// Method create product
        /// <summary>
        /// <param name="product">Product</param>
        /// <returns>void</returns>
        public async Task CreateProductAsync(Product product)
        {
            await dbContext.Products.AddAsync(product);
            await SaveAsync();
        }

        /// <summary>
        /// Method update product
        /// <summary>
        /// <param name="product">Product</param>
        /// <returns>void</returns>
        public async Task UpdateProductAsync(Product product)
        {
            await Task.Run(() => dbContext.Products.Update(product));
            await SaveAsync();
        }

        /// <summary>
        /// Method delete product
        /// <summary>
        /// <param name="product">Product</param>
        /// <returns>void</returns>
        public async Task DeleteProductAsync(Product product)
        {
            await Task.Run(() => dbContext.Products.Remove(product));
            await SaveAsync();
        }

    }
}