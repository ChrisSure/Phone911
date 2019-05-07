using System.ComponentModel.DataAnnotations;

namespace Phone.Data.DTOs.User
{
    public class UserProfileDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public bool? IsBlocked { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [MaxLength(100)]
        public string SurName { get; set; }


        public bool? Sex { get; set; }

        public byte? Age { get; set; }

        public string Description { get; set; }

        public string Experience { get; set; }

        [MaxLength(30)]
        [RegularExpression(@"^\d{10}$")]
        public string Phone { get; set; }

        [MaxLength(50)]
        public string Position { get; set; }

        public int? Salary { get; set; }
    }
}
