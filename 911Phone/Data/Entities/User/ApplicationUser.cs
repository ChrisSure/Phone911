using Microsoft.AspNetCore.Identity;
using Phone.Data.Entities.Shop;
using ProfileNamespace = Phone.Data.Entities.User;

namespace Phone.Data.Entities.User
{
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Blocking user.
        /// </summary>
        public virtual bool? IsBlocked { get; set; }

        /// <summary>
        /// Relation with table profile one to one.
        /// </summary>
        public ProfileNamespace.Profile Profile { get; set; }

        /// <summary>
        /// List of sellers.
        /// </summary>
        public ShopSeller Seller { get; set; }

    }
}
