namespace UangKuAPI.Filter
{
    public class UserWishlistFilter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string WishlistID { get; set; }
        public string PersonID { get; set; }
        public UserWishlistFilter()
        {
            PageNumber = 1;
            PageSize = 10;
            WishlistID = string.Empty;
            PersonID = string.Empty;
        }
        public UserWishlistFilter(int pageNumber, int pageSize, string? wishlistID, string? personID)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize < 1 ? 1 : pageSize;
            WishlistID = !string.IsNullOrEmpty(wishlistID) ? wishlistID : string.Empty;
            PersonID = !string.IsNullOrEmpty(personID) ? personID : string.Empty;
        }
    }
}
