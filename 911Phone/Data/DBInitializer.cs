using Microsoft.AspNetCore.Identity;
using Phone.Data.Entities.Catalog;
using Phone.Data.Entities.Shop;
using Phone.Data.Entities.User;
using Phone.Helpers;
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
                await StoreProcFuncRepository.LoadAllToDb(context);
                await SetRolesAsync();
                await SeedDataAsync();
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

        #region SeedData seed
        private async Task SeedDataAsync()
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
                Profile profile = new Profile() { UserId = userSeller.Id, Name = "Robby", LastName = "Nets", Phone = "0988756123", Sex = true, Position = "Seller", Salary = 5000, Birthday = new DateTime(2000, 1, 18), Description = "Good worker", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now };
                context.Profiles.Add(profile);
            }
            // Create Seller2
            var userSeller2 = new ApplicationUser() { UserName = "Seller2", Email = "sell2@mail.com" };
            await userManager.CreateAsync(userSeller2, "Seller_911");
            if (await userManager.FindByIdAsync(userSeller2.Id) != null)
            {
                await userManager.AddToRoleAsync(userSeller2, RoleTypes.Seller);
                Profile profile = new Profile() { UserId = userSeller2.Id, Name = "Bryan", LastName = "Morris", Phone = "0639087667", Sex = true, Position = "Seller", Salary = 5000, Birthday = new DateTime(1998, 11, 18), Description = "Good worker", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now };
                context.Profiles.Add(profile);
            }
            // Create SuperSeller
            var userSuperSeller = new ApplicationUser() { UserName = "SuperSeller", Email = "supersell@mail.com" };
            await userManager.CreateAsync(userSuperSeller, "Seller_911");
            if (await userManager.FindByIdAsync(userSuperSeller.Id) != null)
            {
                await userManager.AddToRoleAsync(userSuperSeller, RoleTypes.SuperSeller);
                Profile profile = new Profile() { UserId = userSuperSeller.Id, Name = "Teddy", LastName = "Bear", Phone = "0988756129", Sex = true, Position = "Head of shop", Salary = 9000, Birthday = new DateTime(1994, 4, 12), Description = "Good worker head", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now };
                context.Profiles.Add(profile);
            }
            // Create Admin
            var userAdmin = new ApplicationUser() { UserName = "Admin", Email = "admin@mail.com" };
            await userManager.CreateAsync(userAdmin, "Admin_911");
            if (await userManager.FindByIdAsync(userAdmin.Id) != null)
            {
                await userManager.AddToRoleAsync(userAdmin, RoleTypes.Admin);
                Profile profile = new Profile() { UserId = userAdmin.Id, Name = "Sonya", LastName = "Blade", Phone = "0989829009", Sex = false, Birthday = new DateTime(1987, 4, 11), CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now };
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


            //Categories
            var category1 = new Category() { Title = "Smartfones", Left = 1, Right = 8, Level = 1, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now };
            context.Categories.Add(category1);
            var pidcategory1 = new Category() { Title = "Apple", Left = 2, Right = 3, Level = 2, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now };
            context.Categories.Add(pidcategory1);
            var pidcategory2 = new Category() { Title = "Samsung", Left = 4, Right = 5, Level = 2, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now };
            context.Categories.Add(pidcategory2);
            var pidcategory3 = new Category() { Title = "Xiaomi", Left = 6, Right = 7, Level = 2, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now };
            context.Categories.Add(pidcategory3);
            var category2 = new Category() { Title = "Tablet", Left = 9, Right = 14, Level = 1, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now };
            context.Categories.Add(category2);
            var pidcategory4 = new Category() { Title = "Apple", Left = 10, Right = 11, Level = 2, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now };
            context.Categories.Add(pidcategory4);
            var pidcategory5 = new Category() { Title = "Lenovo", Left = 12, Right = 13, Level = 2, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now };
            context.Categories.Add(pidcategory5);
            var category3 = new Category() { Title = "Laptop", Left = 15, Right = 16, Level = 1, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now };
            context.Categories.Add(category3);


            //Products
            var product1 = new Product() { Title = "Iphone 4", Price = 4000, Text = "Good smartfone", IsAproval = true, CategoryId = pidcategory1.Id, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now };
            context.Products.Add(product1);
            var product2 = new Product() { Title = "Iphone 6s", Price = 9000, IsAproval = false, CategoryId = pidcategory1.Id, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now };
            context.Products.Add(product2);
            var product3 = new Product() { Title = "Iphone X", Price = 20000, IsAproval = true, CategoryId = pidcategory1.Id, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now };
            context.Products.Add(product3);
            var product4 = new Product() { Title = "Samsung S8", Price = 12000, IsAproval = true, CategoryId = pidcategory2.Id, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now };
            context.Products.Add(product4);
            var product5 = new Product() { Title = "Samsung S10", Price = 18000, IsAproval = true, Text = "Flagman samsung company", CategoryId = pidcategory2.Id, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now };
            context.Products.Add(product5);
            var product6 = new Product() { Title = "Ipad 3", Price = 5000, IsAproval = true, Text = "Old tablet", CategoryId = pidcategory4.Id, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now };
            context.Products.Add(product6);
            var product7 = new Product() { Title = "Ipad air 2", Price = 12000, IsAproval = true, Text = "New tablet", CategoryId = pidcategory4.Id, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now };
            context.Products.Add(product7);
            var product8 = new Product() { Title = "Lenovo t 100", Price = 3000, IsAproval = true, CategoryId = pidcategory5.Id, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now };
            context.Products.Add(product8);
            var product9 = new Product() { Title = "HP sql 980", Price = 13000, IsAproval = true, CategoryId = category3.Id, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now };
            context.Products.Add(product9);


            //Shops
            var shop1 = new Shop() { Title = "Shop1", Description = "Central shop", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now };
            context.Shops.Add(shop1);
            var shop2 = new Shop() { Title = "Shop2", Description = "East shop", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now };
            context.Shops.Add(shop2);
            var shop3 = new Shop() { Title = "Shop3", Description = "West shop", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now };
            context.Shops.Add(shop3);
            //ShopCategory
            var sc1 = new ShopCategory() { ShopId = shop1.Id, CategoryId = category1.Id };
            shop1.ShopCategory.Add(sc1);
            var sc2 = new ShopCategory() { ShopId = shop1.Id, CategoryId = category2.Id };
            shop1.ShopCategory.Add(sc2);
            var sc3 = new ShopCategory() { ShopId = shop1.Id, CategoryId = category3.Id };
            shop1.ShopCategory.Add(sc3);
            var sc4 = new ShopCategory() { ShopId = shop2.Id, CategoryId = category3.Id };
            shop2.ShopCategory.Add(sc4);
            var sc5 = new ShopCategory() { ShopId = shop3.Id, CategoryId = category1.Id };
            shop3.ShopCategory.Add(sc5);
            var sc6 = new ShopCategory() { ShopId = shop3.Id, CategoryId = category2.Id };
            shop3.ShopCategory.Add(sc6);
            //ShopSeller
            var ss1 = new ShopSeller() { ShopId = shop1.Id, SellerId = userSuperSeller.Id };
            shop1.ShopSeller.Add(ss1);
            var ss2 = new ShopSeller() { ShopId = shop1.Id, SellerId = userSeller.Id };
            shop1.ShopSeller.Add(ss2);
            var ss3 = new ShopSeller() { ShopId = shop2.Id, SellerId = userSeller2.Id };
            shop2.ShopSeller.Add(ss3);
            var ss4 = new ShopSeller() { ShopId = shop3.Id, SellerId = userSeller.Id };
            shop3.ShopSeller.Add(ss4);


            //Orders
            var order1 = new Order() { TotalSum = 13000, TotalCount = 2, CustomerId = null, SellerId = userSeller.Id, ShopId = shop1.Id, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now };
            context.Orders.Add(order1);
            var order2 = new Order() { TotalSum = 12000, TotalCount = 1, CustomerId = userCustomer.Id, SellerId = userSeller.Id, ShopId = shop3.Id, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now };
            context.Orders.Add(order2);
            //Items
            var item1 = new ProductOrder() { OrderId = order1.Id, ProductId = product1.Id, Count = 2 };
            order1.ProductOrder.Add(item1);
            var item2 = new ProductOrder() { OrderId = order1.Id, ProductId = product2.Id, Count = 1 };
            order1.ProductOrder.Add(item2);
            var item3 = new ProductOrder() { OrderId = order2.Id, ProductId = product7.Id, Count = 1 };
            order2.ProductOrder.Add(item3);


            //Storage
            var storage1 = new Storage() { Count = 2, ShopId = shop1.Id, ProductId = product1.Id, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now };
            context.Storages.Add(storage1);
            var storage2 = new Storage() { Count = 1, ShopId = shop1.Id, ProductId = product2.Id, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now };
            context.Storages.Add(storage2);
            var storage3 = new Storage() { Count = 4, ShopId = shop1.Id, ProductId = product4.Id, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now };
            context.Storages.Add(storage3);

            var storage4 = new Storage() { Count = 3, ShopId = shop2.Id, ProductId = product1.Id, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now };
            context.Storages.Add(storage4);
            var storage5 = new Storage() { Count = 1, ShopId = shop2.Id, ProductId = product5.Id, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now };
            context.Storages.Add(storage5);

            var storage6 = new Storage() { Count = 1, ShopId = shop3.Id, ProductId = product1.Id, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now };
            context.Storages.Add(storage6);


            context.SaveChanges();
        }
        #endregion SeedData seed

    }
}
