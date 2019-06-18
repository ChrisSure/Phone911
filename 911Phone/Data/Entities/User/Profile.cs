using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Phone.Data.Entities.User
{
    public class Profile
    {
        /// <summary>
        /// Identificator.
        /// </summary>
        [Key]
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Id User, who has this profile.
        /// </summary>
        [Required]
        [MaxLength(450)]
        public string UserId { get; set; }

        /// <summary>
        /// Relation with table user one to one.
        /// </summary>
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        /// <summary>
        /// Name for user.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// LastName for user.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        /// <summary>
        /// SurName for user.
        /// </summary>
        [MaxLength(100)]
        public string SurName { get; set; }

        /// <summary>
        /// Sex user (true - man, false - woomen).
        /// </summary>
        public bool? Sex { get; set; }

        /// <summary>
        /// Birthday user.
        /// </summary>
        [Column(TypeName="date")]
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// All about user (characteristik).
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Experience user (before works, experience and others).
        /// </summary>
        public string Experience { get; set; }

        /// <summary>
        /// Mobile phones user.
        /// </summary>
        [MaxLength(30)]
        [RegularExpression(@"^\d{10}$")]
        public string Phone { get; set; }

        /// <summary>
        /// Position user.
        /// </summary>
        [MaxLength(50)]
        public string Position { get; set; }

        /// <summary>
        /// Salary user.
        /// </summary>
        public int? Salary { get; set; }

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
