using System.ComponentModel.DataAnnotations;

namespace Phone.Data.DTOs.Catalog
{
    public class CategoryListDto : CategoryBaseDto
    {
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
    }
}
