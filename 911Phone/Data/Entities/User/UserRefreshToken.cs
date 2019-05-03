using System;
using System.ComponentModel.DataAnnotations;

namespace Phone.Data.Entities.User
{
    public class UserRefreshToken
    {
        /// <summary>
        /// Identificator.
        /// </summary>
        [Key]
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Id User.
        /// </summary>
        [Required]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        /// <summary>
        /// Token for user 44 characters.
        /// </summary>
        [Required]
        [MaxLength(44)]
        public string RefreshToken { get; set; }

        /// <summary>
        /// Datetime by end live refresh token.
        /// </summary>
        [Required]
        public DateTime ExpireOn { get; set; }
    }
}
