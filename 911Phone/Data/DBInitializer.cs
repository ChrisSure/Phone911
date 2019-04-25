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

        private async Task SeedUserAsync()
        {
            // Create Customer
            var userCustomer = new ApplicationUser() { UserName = "Customer", Email = "cust@mail.com" };
            await userManager.CreateAsync(userCustomer, "Customer_911");

            if (await userManager.FindByIdAsync(userCustomer.Id) != null) {
                await userManager.AddToRoleAsync(userCustomer, RoleTypes.Customer);
                Profile profile = new Profile() { UserId = userCustomer.Id, Name = "Mark", LastName = "Libert", Phone = "0980876538" };
                context.Profiles.Add(profile);
                context.SaveChanges();
            }
        }


    }
}
