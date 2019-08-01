using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Phone.Data;
using Phone.Data.DTOs.User;
using Phone.Data.Entities.User;
using Phone.Helpers.User;
using Phone.Repositories.User.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phone.Repositories.User
{
    public class SellerRepository : ISellerRepository
    {
        private UserManager<ApplicationUser> userManager;
        ApplicationDbContext dbContext;

        public SellerRepository(UserManager<ApplicationUser> user, ApplicationDbContext dbContext)
        {
            this.userManager = user;
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Method return list user with role seller and super-seller
        /// <summary>
        /// <returns>IList<ApplicationUser></returns>
        public async Task<IList<ApplicationUser>> ListSellersAsync()
        {
            List<ApplicationUser> listSellers = new List<ApplicationUser>();
            listSellers.AddRange(await userManager.GetUsersInRoleAsync(RoleTypes.SuperSeller));
            listSellers.AddRange(await userManager.GetUsersInRoleAsync(RoleTypes.Seller));
            return listSellers;
        }

        /// <summary>
        /// Method return list seller by shop id
        /// <summary>
        /// <params name="shopId">int</params>
        /// <returns>IList<ApplicationUser></returns>
        public async Task<IList<SellerShopDto>> ListSellersByShopIdAsync(int shopId)
        {
            return await dbContext.Users.Include(u => u.Profile).Include(u => u.Seller).ThenInclude(s => s.Shop)
                .Where(s => s.Seller.Shop.Id == shopId)
                .Select(s => new SellerShopDto
                {
                    Email = s.Email,
                    Name = s.Profile.Name,
                    LastName = s.Profile.LastName,
                    SurName = s.Profile.SurName,
                    Sex = s.Profile.Sex,
                    Birthday = s.Profile.Birthday,
                    Description = s.Profile.Description,
                    Experience = s.Profile.Experience,
                    Phone = s.Profile.Phone,
                    Position = s.Profile.Position,
                    Salary = s.Profile.Salary
                }).ToListAsync();
        }

    }
}
