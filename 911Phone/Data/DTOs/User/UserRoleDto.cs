using System.ComponentModel.DataAnnotations;


namespace Phone.Data.DTOs.User
{
    public class UserRoleDto
    {
        /// <summary>
        /// User Role
        /// </summary>
        [Required]
        public virtual string Role { get; set; }
    }
}
