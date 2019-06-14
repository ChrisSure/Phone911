using Microsoft.EntityFrameworkCore;
using Phone.Data;
using Phone.Data.Entities.Shop;
using Phone.Exceptions;
using Phone.Exceptions.Shop;
using Phone.Repositories.Catalog.Interfaces;
using Phone.Repositories.Shop.Interfaces;
using Phone.Repositories.User.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopEntity = Phone.Data.Entities.Shop.Shop;


namespace Phone.Repositories.Shop
{
    public class ShopRepository : MainRepository, IShopRepository
    {
        private ICategoryRepository categoryRepository;
        private IUserRepository userRepository;

        public ShopRepository(ApplicationDbContext dbContext, ICategoryRepository categoryRepository, IUserRepository userRepository) : base(dbContext)
        {
            this.categoryRepository = categoryRepository;
            this.userRepository = userRepository;
        }

        /// <summary>
        /// Method get shop by id
        /// <summary>
        /// <param name="shopId">int</param>
        /// <returns>IList<Shop></returns>
        public async Task<ShopEntity> SingleShopAsync(int shopId)
        {
            var shop = await dbContext.Shops.Where(s => s.Id == shopId).FirstOrDefaultAsync();
            if (shop == null)
            {
                throw new CurrentEntryNotFoundException("Current Shop doesn't isset.");
            }
            return shop;
        }

        /// <summary>
        /// Method get shop by id
        /// <summary>
        /// <param name="shopId">int</param>
        /// <returns>IList<Shop></returns>
        public async Task<ShopEntity> SingleShopNoTrackAsync(int shopId)
        {
            var shop = await dbContext.Shops.AsNoTracking().Where(s => s.Id == shopId).FirstOrDefaultAsync();
            if (shop == null)
            {
                throw new CurrentEntryNotFoundException("Current Shop doesn't isset.");
            }
            return shop;
        }

        /// <summary>
        /// Method get list of shops
        /// <summary>
        /// <returns>IList<Shop></returns>
        public async Task<IList<ShopEntity>> ListShopsAsync()
        {
            return await Task.Run(() => dbContext.Shops.ToList());
        }

        /// <summary>
        /// Method get list of shops by seller id
        /// <summary>
        /// <param name="sellerId">string</param>
        /// <returns>IList<Shop></returns>
        public async Task<IList<ShopEntity>> ListShopsBySellerIdAsync(string sellerId)
        {
            return await Task.Run(() => dbContext.Shops.Where(s => s.ShopSeller.Any(u => u.SellerId == sellerId)).ToList());
        }

        /// <summary>
        /// Method create shop
        /// <summary>
        /// <param name="shop">Shop</param>
        /// <returns>void</returns>
        public async Task CreateShopAsync(ShopEntity shop)
        {
            await dbContext.Shops.AddAsync(shop);
            await SaveAsync();
        }

        /// <summary>
        /// Method update shop
        /// <summary>
        /// <param name="shop">Shop</param>
        /// <returns>void</returns>
        public async Task UpdateShopAsync(ShopEntity shop)
        {
            await Task.Run(() => dbContext.Shops.Update(shop));
            await SaveAsync();
        }

        /// <summary>
        /// Method add category to shop
        /// <summary>
        /// <param name="shopCategory">ShopCategory</param>
        /// <returns>void</returns>
        public async Task AddCategoryToShopAsync(ShopCategory shopCategory)
        {
            var shop = await SingleShopAsync(shopCategory.ShopId);

            var category = await categoryRepository.SingleCategoryAsync(shopCategory.CategoryId);
            if (category.Level != 1)
            {
                throw new LevelShopCategoryException("Category' level must be 1.");
            }

            var check = await dbContext.Shops.Where(s => s.ShopCategory.Any(u => u.ShopId == shop.Id)).Where(s => s.ShopCategory.Any(u => u.CategoryId == category.Id)).ToListAsync();
            if (check.Count > 0)
            {
                throw new UniqShopException("Current category has already been in current shop.");
            }

            var sc = new ShopCategory() { ShopId = shop.Id, CategoryId = category.Id };
            await Task.Run(() => shop.ShopCategory.Add(sc));
            await SaveAsync();
        }

        /// <summary>
        /// Method add seller to shop
        /// <summary>
        /// <param name="shopSeller">ShopSeller</param>
        /// <returns>void</returns>
        public async Task AddSellerToShopAsync(ShopSeller shopSeller)
        {
            var shop = await SingleShopAsync(shopSeller.ShopId);
            var seller = await userRepository.GetUserAsync(shopSeller.SellerId);

            var check = await dbContext.Shops.Where(s => s.ShopSeller.Any(u => u.ShopId == shop.Id)).Where(s => s.ShopSeller.Any(u => u.SellerId == seller.Id)).ToListAsync();
            if (check.Count > 0)
            {
                throw new UniqShopException("Current seller has already been in current shop.");
            }

            var sse = new ShopSeller() { ShopId = shop.Id, SellerId = seller.Id };
            await Task.Run(() => shop.ShopSeller.Add(sse));
            await SaveAsync();
        }
    }
}
