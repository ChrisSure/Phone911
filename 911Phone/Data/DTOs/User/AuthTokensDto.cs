using System;
using System.ComponentModel.DataAnnotations;

namespace Phone.Data.DTOs.User
{
    public class AuthTokensDto : ICloneable
    {
        /// <summary>
        /// Access token user.
        /// </summary>
        [Required]
        public string AccessToken { get; set; }

        /// <summary>
        /// Refresh token user.
        /// </summary>
        [Required]
        public string RefreshToken { get; set; }

        /// <summary>
        /// Datetime, which set user login time.
        /// </summary>
        public DateTime ExpireOn { get; set; }


        /// <summary>
        /// Methods for cloning
        /// </summary>
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
        /// <summary>
        /// Methods for cloning
        /// </summary>
    }
}
