namespace Phone.Data.DTOs.User
{
    public class UserViewDto : UserBaseDto
    {
        /// <summary>
        /// UserName user.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Blocking user.
        /// </summary>
        public virtual bool? IsBlocked { get; set; }
    }
}
