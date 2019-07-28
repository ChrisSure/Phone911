using System.ComponentModel.DataAnnotations;

namespace Phone.Data.DTOs.Shop
{
    public class ShopListDto
    {
        /// <summary>
        /// Identificator.
        /// </summary>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Name for shop.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        /// <summary>
        /// Description of shop.
        /// </summary>
        public string Description { get; set; }
    }
}
