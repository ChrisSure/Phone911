using Phone.Data.Entities.Catalog;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Phone.Data.DTOs.Catalog
{
    public class CreateOrderDto
    {
        /// <summary>
        /// Total sum of order.
        /// </summary>
        [Required]
        public int TotalSum { get; set; }

        /// <summary>
        /// Total count item of order.
        /// </summary>
        [Required]
        public int TotalCount { get; set; }

        /// <summary>
        /// Customer Id, who ordered products
        /// </summary>
        [MaxLength(450)]
        public string CustomerId { get; set; }

        /// <summary>
        /// Seller Id, who sold products
        /// </summary>
        [Required]
        [MaxLength(450)]
        public string SellerId { get; set; }

        public IList<ProductOrderDto> productOrder { get; set; }


    }
}
