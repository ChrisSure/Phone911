using Microsoft.AspNetCore.Identity;

namespace Phone.Data.Entities.User
{
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Blocking user.
        /// </summary>
        public virtual bool? IsBlocked { get; set; }

        /// <summary>
        /// Info Profile user.
        /// </summary>
        public Profile Profile { get; set; }
    }
}
