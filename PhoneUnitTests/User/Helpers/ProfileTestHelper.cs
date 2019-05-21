using Phone.Data.Entities.User;
using System.Threading.Tasks;

namespace PhoneUnitTests.User.Helpers
{
    class ProfileTestHelper
    {
        /// <summary>
        /// Return face data user
        /// <summary>
        public static async Task<Profile> GetProfile()
        {
            return await Task.Run(() =>
                new Profile { Id = 1, Name = "John" }
            );
        }
    }
}

