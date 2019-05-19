using Phone.Data.Entities.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhoneUnitTests.User.Helpers
{
    class UserTestHelper
    {
        public readonly string EmailUser = "user1@mail.ua";

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

    }
}
