using System.ComponentModel.DataAnnotations;

namespace Phone.Data.DTOs.User
{
    public class UserAuthDto : UserBaseDto
    {
        /// <summary>
        /// Password user.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public virtual string Password { get; set; }
    }
}
