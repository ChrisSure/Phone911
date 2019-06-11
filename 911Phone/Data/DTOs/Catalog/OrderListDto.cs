using System;
using System.ComponentModel.DataAnnotations;

namespace Phone.Data.DTOs.Catalog
{
    public class OrderListDto
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
        /// Time of creation.
        /// </summary>
        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
