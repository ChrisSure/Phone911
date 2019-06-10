using Phone.Data.Entities.Catalog;
using System;
using System.ComponentModel.DataAnnotations;

namespace Phone.Data.DTOs.Catalog
{
    public class ProductViewDto : ProductListDto
    {

        /// <summary>
        /// Name of category which depends current product.
        /// </summary>
        public virtual Category Category { get; set; }

        /// <summary>
        /// Description of product.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Is aproval current product.
        /// </summary>
        public bool? IsAproval { get; set; }

        /// <summary>
        /// Time of creation.
        /// </summary>
        [Required]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Time of updating.
        /// </summary>
        [Required]
        public DateTime UpdatedAt { get; set; }
    }
}
