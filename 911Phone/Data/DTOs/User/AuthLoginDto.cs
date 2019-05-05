using System.ComponentModel.DataAnnotations;

namespace Phone.Data.DTOs.User
{
    public class AuthLoginDto
    {
        /// <summary>
        /// Email user.
        /// </summary>
        [EmailAddress]
        public virtual string Email { get; set; }

        /// <summary>
        /// Password user.
        /// </summary>
        [DataType(DataType.Password)]
        public virtual string Password { get; set; }

    }
}
