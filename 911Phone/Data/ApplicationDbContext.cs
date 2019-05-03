using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Phone.Data.Entities.User;

namespace Phone.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Profile> Profiles { get; set; }
        public virtual DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
    }
}
