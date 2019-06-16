using Phone.Data.Entities.Catalog;
using Phone.Data.Entities.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ShopName = Phone.Data.Entities.Shop;


namespace Phone.Data.DTOs.Catalog
{
    public class OrderViewDto
    {
        /// <summary>
        /// Identificator.
        /// </summary>
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
        /// Relation with table users many to one.
        /// </summary
        public virtual ApplicationUser Customer { get; set; }

        /// <summary>
        /// Relation with table users many to one.
        /// </summary>
        public virtual ApplicationUser Seller { get; set; }

        /// <summary>
        /// Relation with table shop many to one.
        /// </summary>
        public virtual ShopName.Shop Shop { get; set; }

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

    }
}
