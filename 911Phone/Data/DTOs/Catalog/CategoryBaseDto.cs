using System.ComponentModel.DataAnnotations;

namespace Phone.Data.DTOs.Catalog
{
    public class CategoryBaseDto
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
    }
}
