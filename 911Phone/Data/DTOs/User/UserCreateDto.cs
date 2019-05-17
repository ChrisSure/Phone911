using System.ComponentModel.DataAnnotations;

namespace Phone.Data.DTOs.User
{
    public class UserCreateDto : UserBaseDto
    {
        /// <summary>
        /// User name.
        /// </summary>
        [Required]
        public virtual string UserName { get; set; }

        /// <summary>
        /// Password user.
        /// </summary>
        [DataType(DataType.Password)]
        public virtual string Password { get; set; }

        /// <summary>
        /// Compare Password user.
        /// </summary>
        [Compare("Password", ErrorMessage = "Passwords don't match")]
        public virtual string ConfirmPassword { get; set; }

        /// <summary>
        /// User Role
        /// </summary>
        public virtual string Role { get; set; }
    }
}
