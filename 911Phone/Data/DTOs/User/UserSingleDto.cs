namespace Phone.Data.DTOs.User
{
    public class UserSingleDto
    {
        public UserViewDto UserInfo { get; set; }
        
        public ProfileInfoDto ProfileInfo { get; set; }

        public string RoleInfo { get; set; }
    }
}
