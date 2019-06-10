using System.ComponentModel.DataAnnotations;

namespace Phone.Data.DTOs.Catalog
{
    public class ProductCreateDto
    {
        /// <summary>
        /// Id Category, by who depends on this product.
        /// </summary>
        [Required]
        public int CategoryId { get; set; }

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
        /// Description of product.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Is aproval current product.
        /// </summary>
        public bool? IsAproval { get; set; }

    }
}
