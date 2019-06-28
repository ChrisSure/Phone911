using Phone.Data.Entities.Catalog;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Phone.Data.Entities.Shop
{
    public class Storage
    {
        /// <summary>
        /// Identificator.
        /// </summary>
        [Key]
        [Required]
        public int Id { get; set; }

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
        /// Relation with table shop many to one.
        /// </summary>
        [ForeignKey("ShopId")]
        public virtual Shop Shop { get; set; }

        /// <summary>
        /// Id product, by who depends on this product count.
        /// </summary>
        [Required]
        public int ProductId { get; set; }

        /// <summary>
        /// Relation with table shop many to one.
        /// </summary>
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

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
