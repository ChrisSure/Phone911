﻿using Microsoft.EntityFrameworkCore;
using Phone.Data;
using Phone.Data.Entities.Catalog;
using Phone.Exceptions;
using Phone.Repositories.Catalog.Interfaces;
using Phone.Repositories.Shop.Interfaces;
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
        /// Method return list categories by shop id
        /// <summary>
        /// <param name="shopId">int</param>
        /// <returns>IList<Category></Category></returns>
        public async Task<IList<Category>> ListCategoriesByShopIdAsync(int shopId)
        {
            var categories = new List<Category>();
            try
            {
                categories = await dbContext.Categories
                      .FromSql($"EXEC [Categories.ListShop] {shopId}").ToListAsync();
            }
            catch (SqlException ex)
            {
                Helpers.SqlExceptionTranslator.ReThrow(ex, "List Category For Shop");
            }
            return categories;
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
                throw new CurrentEntryNotFoundException("Current Category doesn't isset.");
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
        /// Method update category
        /// <summary>
        /// <param name="categoryId">int</param>
        /// <param name="title">string</param>
        /// <param name="parentId">int</param>
        /// <returns>void</returns>
        public async Task UpdateCategoryAsync(int categoryId, string title, int? parentId)
        {
            try
            {
                await dbContext.Database.ExecuteSqlCommandAsync(
                    $"EXEC [Categories.Update] {categoryId}, {title}, {parentId}");
            }
            catch (SqlException ex)
            {
                Helpers.SqlExceptionTranslator.ReThrow(ex, "Update Category");
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

        /// <summary>
        /// Method up category
        /// <summary>
        /// <param name="categoryId">int</param>
        /// <returns>void</returns>
        public async Task UpCategoryAsync(int categoryId)
        {
            try
            {
                await dbContext.Database.ExecuteSqlCommandAsync(
                    $"EXEC [Categories.SortUp] {categoryId}");
            }
            catch (SqlException ex)
            {
                Helpers.SqlExceptionTranslator.ReThrow(ex, "Sort Up Category");
            }
        }

        /// <summary>
        /// Method down category
        /// <summary>
        /// <param name="categoryId">int</param>
        /// <returns>void</returns>
        public async Task DownCategoryAsync(int categoryId)
        {
            try
            {
                await dbContext.Database.ExecuteSqlCommandAsync(
                    $"EXEC [Categories.SortDown] {categoryId}");
            }
            catch (SqlException ex)
            {
                Helpers.SqlExceptionTranslator.ReThrow(ex, "Sort Down Category");
            }
        }
    }
}
