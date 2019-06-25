using System.ComponentModel.DataAnnotations;

namespace Phone.Data.DTOs.Shop
{
    public class AddStorageDto
    {
        /// <summary>
        /// Count product.
        /// </summary>
        [Required]
        public short Count { get; set; }

        /// <summary>
        /// Id shop, by who depends on this product count.
        /// </summary>
        [Required]
        public int ShopId { get; set; }

        /// <summary>
        /// Id product, by who depends on this product count.
        /// </summary>
        [Required]
        public int ProductId { get; set; }
    }
}
