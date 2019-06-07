using Phone.Data.DTOs.Catalog;
using Phone.Data.Entities.Catalog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhoneUnitTests.Catalog.Helpers
{
    class CategoryTestHelper
    {
        /// <summary>
        /// Return face data category
        /// <summary>
        public static async Task<Category> GetCategory()
        {
            return await Task.Run(() =>
                new Category { Id = 1, Title = "Category_1" }
            );
        }

        /// <summary>
        /// Return face data list categories
        /// <summary>
        public static async Task<IList<Category>> GetCategories()
        {
            return await Task.Run(() =>
                new List<Category>
                {
                    new Category { Id=1, Title="Category_1" },
                    new Category { Id=2, Title="Category_2" },
                    new Category { Id=3, Title="Category_3" }
                }
            );
        }

        /// <summary>
        /// Return list face data category in like CategoryCreateDto 
        /// <summary>
        public static async Task<CategoryCreateDto> GetCategoryCreateNormal()
        {
            return await Task.Run(() =>
                new CategoryCreateDto { Title = "Category_1" }
            );
        }

        /// <summary>
        /// Return list face data category in like CategoryCreateDto 
        /// <summary>
        public static async Task<CategoryCreateDto> GetCategoryCreateUnNormal()
        {
            return await Task.Run(() =>
                new CategoryCreateDto { }
            );
        }
    }
}
