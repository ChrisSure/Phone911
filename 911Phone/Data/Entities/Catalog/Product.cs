using Phone.Data.Entities.Shop;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Phone.Data.Entities.Catalog
{
    public class Product
    {
        /// <summary>
        /// Identificator.
        /// </summary>
        [Key]
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Id Category, by who depends on this product.
        /// </summary>
        [Required]
        public int CategoryId { get; set; }

        /// <summary>
        /// Relation with table category many to one.
        /// </summary>
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        /// <summary>
        /// Name for product.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        /// <summary>
        /// Description of product.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Price of product.
        /// </summary>
        public int Price { get; set; }

        /// <summary>
        /// Price of product.
        /// </summary>
        [MaxLength(255)]
        public string Image { get; set; }

        /// <summary>
        /// Is aproval current product.
        /// </summary>
        public bool? IsAproval { get; set; }

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
        /// List of storage by product id.
        /// </summary>
        public IList<Storage> Storages { get; set; } = new List<Storage>();
    }
}
