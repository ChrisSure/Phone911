using Phone.Data.DTOs.Catalog;
using Phone.Data.Entities.Catalog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhoneUnitTests.Catalog.Helpers
{
    class ProductTestHelper
    {
        /// <summary>
        /// Return face data product
        /// <summary>
        public static async Task<Product> GetProduct()
        {
            return await Task.Run(() =>
                new Product { Id = 1, Title = "Product_1" }
            );
        }

        /// <summary>
        /// Return face data list products
        /// <summary>
        public static async Task<IList<Product>> GetProducts()
        {
            return await Task.Run(() =>
                new List<Product>
                {
                    new Product { Id=1, Title="Product_1", CategoryId = 1 },
                    new Product { Id=2, Title="Product_2", CategoryId = 1 },
                    new Product { Id=3, Title="Product_3", CategoryId = 2 }
                }
            );
        }

        /// <summary>
        /// Return list face data product in like ProductCreateDto 
        /// <summary>
        public static async Task<ProductCreateDto> GetProductCreateNormal()
        {
            return await Task.Run(() =>
                new ProductCreateDto { Title = "Product_1", CategoryId = 1, Image = "Image", IsAproval = true, Price = 9000, Text = "" }
            );
        }

        /// <summary>
        /// Return list face data product in like ProductCreateDto 
        /// <summary>
        public static async Task<ProductCreateDto> GetProductCreateUnNormal()
        {
            return await Task.Run(() =>
                new ProductCreateDto { }
            );
        }
    }
}
