using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Phone.Data.Entities.Shop
{
    public class Shop
    {
        /// <summary>
        /// Identificator.
        /// </summary>
        [Key]
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

        /// <summary>
        /// List of categories who depends on current shop.
        /// </summary>
        public List<ShopCategory> ShopCategory { get; set; }

        /// <summary>
        /// List of sellers who are working in current shop.
        /// </summary>
        public List<ShopSeller> ShopSeller { get; set; }

        public Shop()
        {
            ShopCategory = new List<ShopCategory>();
            ShopSeller = new List<ShopSeller>();
        }
    }
}
