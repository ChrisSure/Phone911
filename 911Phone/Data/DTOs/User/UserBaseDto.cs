using System.ComponentModel.DataAnnotations;


namespace Phone.Data.DTOs.User
{
    public class UserBaseDto
    {
        /// <summary>
        /// Email user.
        /// </summary>
        [Required]
        [EmailAddress]
        public virtual string Email { get; set; }
    }
}
