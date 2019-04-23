using Microsoft.AspNetCore.Identity;


namespace Phone.Data.Entities
{
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Blocking user.
        /// </summary>
        public virtual bool? IsBlocked { get; set; }
    }
}
