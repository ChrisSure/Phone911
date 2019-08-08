using Phone.Data;
using Phone.Data.Entities.Catalog;
using Phone.Repositories.Catalog.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Phone.Exceptions;
using Microsoft.EntityFrameworkCore;
using Phone.Data.DTOs.Catalog;
using Phone.Data.Entities.Shop;

namespace Phone.Repositories.Catalog
{
    public class ProductRepository : MainRepository, IProductRepository
    {

        private ICategoryRepository categoryRepository;

        public ProductRepository(ApplicationDbContext dbContext, ICategoryRepository categoryRepository) : base(dbContext)
        {
            this.categoryRepository = categoryRepository;
        }

        /// <summary>
        /// Method return list products by categoryId
        /// <summary>
        /// <param name="categoryId">int</param>
        /// <returns>IList<Product></returns>
        public async Task<IList<Product>> ListProductsByCategoryIdAllAsync(int categoryId)
        {
            return await Task.Run(() => dbContext.Products.Where(p => p.CategoryId == categoryId).Select(p => new Product
            {
                Title = p.Title,
                Image = p.Image,
                Price = p.Price,
                Text = p.Text,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt,
                IsAproval = p.IsAproval,
                Category = p.Category,
                Storages = p.Storages
            }).ToList());
        }

        /// <summary>
        /// Method return list products by orderId
        /// <summary>
        /// <param name="orderId">int</param>
        /// <returns>IList<Product></returns>
        public async Task<IList<ProductListDto>> ListProductsByOrderIdAllAsync(int orderId)
        {
            return await dbContext.Products.
                Include(p => p.ProductOrder).ThenInclude(po => po.Order).Where(i => i.ProductOrder.Order.Id == orderId)
                .Select(p => new ProductListDto
                {
                    Title = p.Title,
                    Price = p.Price,
                    Image = p.Image,
                    Count = (short)p.ProductOrder.Count
                }).ToListAsync();
        }

        /// <summary>
        /// Method return list products by title match
        /// <summary>
        /// <param name="titleMatch">string</param>
        /// <param name="shopId">int</param>
        /// <returns>IList<Product></returns>
        public async Task<IList<Product>> ListByTitleMatchAsync(string titleMatch, int shopId)
        {
            var categoryList = await categoryRepository.ListCategoriesByShopIdAsync(shopId);
            var categoryIdArray = await GetCategoryIdArray(categoryList);

            return await Task.Run(() => dbContext.Products.Where(x => x.Title.Contains(titleMatch)).Where(x => categoryIdArray.Contains(x.CategoryId)).Select(p => new Product
            {
                Id = p.Id,
                Title = p.Title,
                Image = p.Image,
                Price = p.Price,
                Storages = p.Storages.Where(s => s.ShopId == shopId).Select(s => new Storage { Count = s.Count}).ToList()
            }).ToList());
        }

        /// <summary>
        /// Method return list products by category id
        /// <summary>
        /// <param name="categoryId">int</param>
        /// <param name="shopId">int</param>
        /// <returns>IList<Product></returns>
        public async Task<IList<Product>> ListByCategoryShopIdAsync(int categoryId, int shopId)
        {
            return await Task.Run(() => dbContext.Products.Where(x => x.CategoryId == categoryId).Select(p => new Product
            {
                Id = p.Id,
                Title = p.Title,
                Image = p.Image,
                Price = p.Price,
                Storages = p.Storages.Where(s => s.ShopId == shopId).Select(s => new Storage { Count = s.Count }).ToList()
            }).ToList());
        }

        /// <summary>
        /// Method return list products by category id and title match
        /// <summary>
        /// <param name="categoryId">int</param>
        /// <param name="titleMatch">string</param>
        /// <param name="shopId">int</param>
        /// <returns>IList<Product></returns>
        public async Task<IList<Product>> ListByCategoryAndTitleMatchShopIdAsync(int categoryId, string titleMatch, int shopId)
        {
            var categoryList = await categoryRepository.ListCategoriesByShopIdAsync(shopId);
            var categoryIdArray = await GetCategoryIdArray(categoryList);

            return await Task.Run(() => dbContext.Products.Where(x => x.CategoryId == categoryId).Where(x => x.Title.Contains(titleMatch))
            .Where(x => categoryIdArray.Contains(x.CategoryId)).Select(p => new Product
            {
                Id = p.Id,
                Title = p.Title,
                Image = p.Image,
                Price = p.Price,
                Storages = p.Storages.Where(s => s.ShopId == shopId).Select(s => new Storage { Count = s.Count }).ToList()
            }).ToList());
        }


        /// <summary>
        /// Method return one product
        /// <summary>
        /// <param name="productId">int</param>
        /// <returns>Product</returns>
        public async Task<Product> SingleProductAsync(int productId)
        {
            var product = await Task.Run(() => dbContext.Products.Where(p => p.Id == productId).Select(p => new Product
            {
                Title = p.Title,
                Image = p.Image,
                Price = p.Price,
                Text = p.Text,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt,
                IsAproval = p.IsAproval,
                Category = p.Category,
                Storages = p.Storages
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


        private async Task<IList<int>> GetCategoryIdArray(IList<Category> listCategory)
        {
            List<int> listArrayId = new List<int>();
            await Task.Run(() => {
                foreach(var category in listCategory)
                {
                    listArrayId.Add(category.Id);
                }
            });
            return listArrayId;
        }

    }
}
