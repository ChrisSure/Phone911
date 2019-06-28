using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Phone.Data.Entities.Catalog;
using Phone.Data.Entities.Shop;
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
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Shop> Shops { get; set; }
        public virtual DbSet<Storage> Storages { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
                .HasIndex(b => b.Email)
                .IsUnique();

            modelBuilder.Entity<Product>()
                .HasIndex(b => b.Title);

            modelBuilder.Entity<ProductOrder>()
                .HasKey(t => new { t.OrderId, t.ProductId });

            modelBuilder.Entity<ShopCategory>()
                .HasKey(t => new { t.ShopId, t.CategoryId });
            modelBuilder.Entity<ShopSeller>()
                .HasKey(t => new { t.ShopId, t.SellerId });
        }
    }
}
