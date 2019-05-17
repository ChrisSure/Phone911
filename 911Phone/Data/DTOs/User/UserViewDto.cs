namespace Phone.Data.DTOs.User
{
    public class UserViewDto : UserBaseDto
    {
        /// <summary>
        /// Blocking user.
        /// </summary>
        public virtual bool? IsBlocked { get; set; }
    }
}
