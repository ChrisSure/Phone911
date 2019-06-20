using Phone.Data.Entities.Shop;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Phone.Data.DTOs.Catalog
{
    public class ProductListDto
    {
        /// <summary>
        /// Name for product.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        /// <summary>
        /// Price of product.
        /// </summary>
        public int Price { get; set; }

        /// <summary>
        /// Price of product.
        /// </summary>
        [MaxLength(255)]
        public string Image { get; set; }

        /// <summary>
        /// List of storage by product id.
        /// </summary>
        public IList<Storage> Storages { get; set; } = new List<Storage>();
    }
}
