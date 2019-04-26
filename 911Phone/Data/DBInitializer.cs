using Microsoft.AspNetCore.Identity;
using Phone.Data.Entities.User;
using Phone.Helpers.User;
using System;
using System.Threading.Tasks;

namespace Phone.Data
{
    public class DBInitializer
    {
        readonly ApplicationDbContext context;
        readonly UserManager<ApplicationUser> userManager;
        readonly RoleManager<IdentityRole> roleManager;

        public DBInitializer(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }


        public async Task RunAsync()
        {
            if (context.Database.EnsureCreated())
            {
                await SetRolesAsync();
                await SeedUserAsync();
            }
        }

        #region Set roles
        private async Task SetRolesAsync()
        {
            if (!await roleManager.RoleExistsAsync(RoleTypes.Customer))
                await roleManager.CreateAsync(new IdentityRole(RoleTypes.Customer));
            if (!await roleManager.RoleExistsAsync(RoleTypes.Seller))
                await roleManager.CreateAsync(new IdentityRole(RoleTypes.Seller));
            if (!await roleManager.RoleExistsAsync(RoleTypes.SuperSeller))
                await roleManager.CreateAsync(new IdentityRole(RoleTypes.SuperSeller));
            if (!await roleManager.RoleExistsAsync(RoleTypes.Admin))
                await roleManager.CreateAsync(new IdentityRole(RoleTypes.Admin));
            if (!await roleManager.RoleExistsAsync(RoleTypes.SuperAdmin))
                await roleManager.CreateAsync(new IdentityRole(RoleTypes.SuperAdmin));
            context.SaveChanges();
        }
        #endregion Set roles

        #region Users seed
        private async Task SeedUserAsync()
        {
            // Create Customer
            var userCustomer = new ApplicationUser() { UserName = "Customer", Email = "cust@mail.com" };
            await userManager.CreateAsync(userCustomer, "Customer_911");
            if (await userManager.FindByIdAsync(userCustomer.Id) != null) {
                await userManager.AddToRoleAsync(userCustomer, RoleTypes.Customer);
                Profile profile = new Profile() { UserId = userCustomer.Id, Name = "Mark", LastName = "Libert", Phone = "0980876538", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now };
                context.Profiles.Add(profile);
            }

            // Create Customer2
            var userCustomer2 = new ApplicationUser() { UserName = "Customer2", Email = "cust2@mail.com" };
            await userManager.CreateAsync(userCustomer2, "Customer_911");
            if (await userManager.FindByIdAsync(userCustomer2.Id) != null)
            {
                await userManager.AddToRoleAsync(userCustomer2, RoleTypes.Customer);
                Profile profile = new Profile() { UserId = userCustomer2.Id, Name = "Liza", LastName = "Bentley", Phone = "0958973654", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now };
                context.Profiles.Add(profile);
            }

            // Create Seller
            var userSeller = new ApplicationUser() { UserName = "Seller", Email = "sell@mail.com" };
            await userManager.CreateAsync(userSeller, "Seller_911");
            if (await userManager.FindByIdAsync(userSeller.Id) != null)
            {
                await userManager.AddToRoleAsync(userSeller, RoleTypes.Seller);
                Profile profile = new Profile() { UserId = userSeller.Id, Name = "Robby", LastName = "Nets", Phone = "0988756123", Sex = true, Position = "Seller", Salary = 5000, Age = 22, Description = "Good worker", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now };
                context.Profiles.Add(profile);
            }

            // Create Seller2
            var userSeller2 = new ApplicationUser() { UserName = "Seller2", Email = "sell2@mail.com" };
            await userManager.CreateAsync(userSeller2, "Seller_911");
            if (await userManager.FindByIdAsync(userSeller2.Id) != null)
            {
                await userManager.AddToRoleAsync(userSeller2, RoleTypes.Seller);
                Profile profile = new Profile() { UserId = userSeller2.Id, Name = "Bryan", LastName = "Morris", Phone = "0639087667", Sex = true, Position = "Seller", Salary = 5000, Age = 22, Description = "Good worker", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now };
                context.Profiles.Add(profile);
            }

            // Create SuperSeller
            var userSuperSeller = new ApplicationUser() { UserName = "SuperSeller", Email = "supersell@mail.com" };
            await userManager.CreateAsync(userSuperSeller, "Seller_911");
            if (await userManager.FindByIdAsync(userSuperSeller.Id) != null)
            {
                await userManager.AddToRoleAsync(userSuperSeller, RoleTypes.SuperSeller);
                Profile profile = new Profile() { UserId = userSuperSeller.Id, Name = "Teddy", LastName = "Bear", Phone = "0988756129", Sex = true, Position = "Head of shop", Salary = 9000, Age = 24, Description = "Good worker head", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now };
                context.Profiles.Add(profile);
            }

            // Create Admin
            var userAdmin = new ApplicationUser() { UserName = "Admin", Email = "admin@mail.com" };
            await userManager.CreateAsync(userAdmin, "Admin_911");
            if (await userManager.FindByIdAsync(userAdmin.Id) != null)
            {
                await userManager.AddToRoleAsync(userAdmin, RoleTypes.Admin);
                Profile profile = new Profile() { UserId = userAdmin.Id, Name = "Sonya", LastName = "Blade", Phone = "0989829009", Sex = false, Age = 24, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now };
                context.Profiles.Add(profile);
            }

            // Create SuperAdmin
            var userSuper = new ApplicationUser() { UserName = "SuperAdmin", Email = "super@mail.com" };
            await userManager.CreateAsync(userSuper, "Super_911");
            if (await userManager.FindByIdAsync(userSuper.Id) != null)
            {
                await userManager.AddToRoleAsync(userSuper, RoleTypes.SuperAdmin);
                Profile profile = new Profile() { UserId = userSuper.Id, Name = "Taras", LastName = "Kuk", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now };
                context.Profiles.Add(profile);
            }

            context.SaveChanges();
        }
        #endregion Users seed

    }
}
