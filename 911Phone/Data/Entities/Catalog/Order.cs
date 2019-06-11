using Phone.Data.Entities.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Phone.Data.Entities.Catalog
{
    public class Order
    {
        /// <summary>
        /// Identificator.
        /// </summary>
        [Key]
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Total sum of order.
        /// </summary>
        [Required]
        public int TotalSum { get; set; }

        /// <summary>
        /// Total count item of order.
        /// </summary>
        [Required]
        public int TotalCount { get; set; }

        /// <summary>
        /// Customer Id, who ordered products
        /// </summary>
        [MaxLength(450)]
        public string CustomerId { get; set; }
        /// <summary>
        /// Relation with table users many to one.
        /// </summary>
        [ForeignKey("CustomerId")]
        public virtual ApplicationUser Customer { get; set; }

        /// <summary>
        /// Seller Id, who sold products
        /// </summary>
        [Required]
        [MaxLength(450)]
        public string SellerId { get; set; }
        /// <summary>
        /// Relation with table users many to one.
        /// </summary>
        [ForeignKey("SellerId")]
        public virtual ApplicationUser Seller { get; set; }

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
        /// List of products.
        /// </summary>
        public List<ProductOrder> ProductOrder { get; set; }

        public Order()
        {
            ProductOrder = new List<ProductOrder>();
        }


    }
}
