using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Phone.Data.Entities.Catalog
{
    public class Category
    {
        /// <summary>
        /// Identificator.
        /// </summary>
        [Key]
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Title.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        /// <summary>
        /// Left value.
        /// </summary>
        [Required]
        public short Left { get; set; }

        /// <summary>
        /// Right value.
        /// </summary>
        [Required]
        public short Right { get; set; }

        /// <summary>
        /// Level value.
        /// </summary>
        [Required]
        public byte Level { get; set; }

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
