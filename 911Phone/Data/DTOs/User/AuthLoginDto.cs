using System.ComponentModel.DataAnnotations;

namespace Phone.Data.DTOs.User
{
    public class AuthLoginDto
    {
        /// <summary>
        /// Email user.
        /// </summary>
        [Required]
        [EmailAddress]
        public virtual string Email { get; set; }

        /// <summary>
        /// Password user.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public virtual string Password { get; set; }

    }
}
