using System.ComponentModel.DataAnnotations;

namespace Phone.Data.DTOs.User
{
    public class UserPasswordChangeDto
    {
        /// <summary>
        /// Current password for user.
        /// </summary>
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        /// <summary>
        /// New password for user.
        /// </summary>
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        /// <summary>
        /// Confirm new password for user.
        /// </summary>
        [Compare("NewPassword", ErrorMessage = "Passwords don't match")]
        public string ConfirmNewPassword { get; set; }
    }
}
