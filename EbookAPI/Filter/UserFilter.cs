namespace EbookAPI.Filter
{
    public class UserFilter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public UserFilter()
        {
            PageNumber = 1;
            PageSize = 50;
        }
        public UserFilter(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize > 50 ? 50 : pageSize;
        }
    }

    public class UserNameFilter
    {
        public string? Username { get; set; }
        public string? Password { get; set; }

        public UserNameFilter()
        {
            Username = string.Empty;
            Password = string.Empty;
        }

        public UserNameFilter(string? username, string? password)
        {
            Username = string.IsNullOrEmpty(username) ? string.Empty : username;
            Password = string.IsNullOrEmpty(password) ? string.Empty : password;
        }
    }


}
