using System;
using System.ComponentModel.DataAnnotations;

namespace Phone.Data.DTOs.User
{
    public class AuthTokensDto : ICloneable
    {
        [Required]
        public string AccessToken { get; set; }

        public DateTime ExpireOn { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public override bool Equals(object obj)
        {
            var dto = obj as AuthTokensDto;
            return dto != null &&
                   AccessToken == dto.AccessToken &&
                   ExpireOn.ToLongDateString() == dto.ExpireOn.ToLongDateString();
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(AccessToken, ExpireOn);
        }
    }
}
