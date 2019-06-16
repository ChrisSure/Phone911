using System.ComponentModel.DataAnnotations;

namespace Phone.Data.DTOs.Shop
{
    public class AddCategoryToShopDto
    {
        /// <summary>
        /// Shop Id
        /// </summary>
        [Required]
        public int ShopId { get; set; }

        /// <summary>
        /// Category Id
        /// </summary>
        [Required]
        public int CategoryId { get; set; }
    }
}
