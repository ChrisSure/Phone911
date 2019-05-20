using Phone.Data.DTOs.User;
using Phone.Data.Entities.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhoneUnitTests.User.Helpers
{
    class UserTestHelper
    {
        /// <summary>
        /// Return face email user
        /// <summary>
        public readonly string EmailUser = "user1@mail.ua";

        /// <summary>
        /// Return face role user
        /// <summary>
        public readonly string RoleUser = "Seller";

        /// <summary>
        /// Return list face data users
        /// <summary>
        public async Task<IList<ApplicationUser>> GetUsers()
        {
            return await Task.Run(() =>
                new List<ApplicationUser>
                {
                    new ApplicationUser { Id="131d1b9d-6e7f-449a-9f7e-2b9769fd5001", Email="user1@mail.ua"},
                    new ApplicationUser { Id="131d1b9d-6e7f-449a-9f7e-2b9769fd5002", Email="user2@mail.ua"},
                    new ApplicationUser { Id="131d1b9d-6e7f-449a-9f7e-2b9769fd5003", Email="user3@mail.ua"},
                    new ApplicationUser { Id="131d1b9d-6e7f-449a-9f7e-2b9769fd5004", Email="user4@mail.ua"}
                }
            );
        }

        /// <summary>
        /// Return list face data users in like UserCreateDto 
        /// <summary>
        public async Task<UserCreateDto> GetUserCreateNormal()
        {
            return await Task.Run(() =>
                new UserCreateDto { Email = "user1@mail.ua", UserName = "User", Password = "Messi_911", ConfirmPassword = "Messi_911", Role = "Seller" }
            );
        }

        /// <summary>
        /// Return list face data users in like UserCreateDto 
        /// <summary>
        public async Task<UserCreateDto> GetUserCreateUnNormal()
        {
            return await Task.Run(() =>
                new UserCreateDto { Email = "user1@mail.ua", UserName = "User", Password = "Messi_911", ConfirmPassword = "Msessi_911", Role = "Seller" }
            );
        }

        /// <summary>
        /// Return face data user
        /// <summary>
        public async Task<ApplicationUser> GetUser()
        {
            return await Task.Run(() =>
                new ApplicationUser { Id = "131d1b9d-6e7f-449a-9f7e-2b9769fd5001", Email = "user1@mail.ua" }
            );
        }

        public async Task<string> GetRole()
        {
            return await Task.Run(() =>
                RoleUser
            );
        }
    }
}
