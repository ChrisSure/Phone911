﻿using Phone.Data.Entities.User;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Phone.Repositories.User.Interfaces
{
    public interface IUserRepository
    {

        Task<IList<string>> GetUserRolesAsync(ApplicationUser user);

        Task<object> FindUserByEmailAsync(string email);

        Task<bool> CheckPasswordAsync(ApplicationUser user, string password);

    }
}
