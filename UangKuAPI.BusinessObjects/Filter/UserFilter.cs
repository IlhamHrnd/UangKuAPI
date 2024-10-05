namespace UangKuAPI.BusinessObjects.Filter
{
    public class UserFilter : Base.Base
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public UserFilter() : base()
        {
            Username = string.Empty;
            Password = string.Empty;
            Email = string.Empty;
        }
        public UserFilter(int pageNumber, int pageSize, string username, string password, string email) : base(pageNumber, pageSize)
        {
            Username = !string.IsNullOrEmpty(username) ? username : string.Empty;
            Password = !string.IsNullOrEmpty(password) ? password : string.Empty;
            Email = !string.IsNullOrEmpty(email) ? email : string.Empty;
        }
    }
}