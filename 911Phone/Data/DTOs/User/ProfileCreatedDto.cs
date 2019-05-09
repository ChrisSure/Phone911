using System.ComponentModel.DataAnnotations;

namespace Phone.Data.DTOs.User
{
    public class ProfileCreatedDto : ProfileInfoDto
    {
        /// <summary>
        /// Id User, who has this profile.
        /// </summary>
        [Required]
        [MaxLength(450)]
        public string UserId { get; set; }
    }
}
