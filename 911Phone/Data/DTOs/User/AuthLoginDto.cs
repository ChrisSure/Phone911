using System.ComponentModel.DataAnnotations;

namespace Phone.Data.DTOs.User
{
    public class AuthLoginDto
    {

        [EmailAddress]
        public virtual string Email { get; set; }

        [DataType(DataType.Password)]
        public virtual string Password { get; set; }

    }
}
