using System;
using System.ComponentModel.DataAnnotations;

namespace Phone.Data.DTOs.Shop
{
    public class ShopViewDto : ShopListDto
    {
        /// <summary>
        /// Description of shop.
        /// </summary>
        public string Description { get; set; }

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
