using System.ComponentModel.DataAnnotations;

namespace Phone.Data.DTOs.Catalog
{
    public class CategoryCreateDto
    {
        /// <summary>
        /// Parent Id.
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// Title.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
    }
}
