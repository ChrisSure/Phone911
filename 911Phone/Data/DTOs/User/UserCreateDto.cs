using System.ComponentModel.DataAnnotations;

namespace Phone.Data.DTOs.User
{
    public class UserCreateDto : UserBaseDto
    {
        /// <summary>
        /// UserName user.
        /// </summary>
        public string UserName { get; set; }

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

    }
}
