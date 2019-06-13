using Phone.Data.Entities.User;
using System.ComponentModel.DataAnnotations;

namespace Phone.Data.Entities.Shop
{
    public class ShopSeller
    {
        /// <summary>
        /// Shop Id
        /// </summary>
        [Required]
        public int ShopId { get; set; }
        public Shop Shop { get; set; }

        /// <summary>
        /// Seller Id
        /// </summary>
        [Required]
        [MaxLength(450)]
        public string SellerId { get; set; }
        public ApplicationUser Seller { get; set; }
    }
}
