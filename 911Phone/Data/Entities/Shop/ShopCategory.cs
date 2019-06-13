using Phone.Data.Entities.Catalog;
using System.ComponentModel.DataAnnotations;

namespace Phone.Data.Entities.Shop
{
    public class ShopCategory
    {
        /// <summary>
        /// Shop Id
        /// </summary>
        [Required]
        public int ShopId { get; set; }
        public Shop Shop { get; set; }

        /// <summary>
        /// Category Id
        /// </summary>
        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
