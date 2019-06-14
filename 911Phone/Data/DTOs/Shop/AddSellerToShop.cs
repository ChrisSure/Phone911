using System.ComponentModel.DataAnnotations;

namespace Phone.Data.DTOs.Shop
{
    public class AddSellerToShop
    {
        /// <summary>
        /// Shop Id
        /// </summary>
        [Required]
        public int ShopId { get; set; }

        /// <summary>
        /// Seller Id
        /// </summary>
        [Required]
        [MaxLength(450)]
        public string SellerId { get; set; }
    }
}
