using System.ComponentModel.DataAnnotations;

namespace Phone.Data.DTOs.Shop
{
    public class ShopListDto
    {
        /// <summary>
        /// Name for shop.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
    }
}
