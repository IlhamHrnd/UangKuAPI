namespace UangKuAPI.BusinessObjects.Filter
{
    public class UserWishlistFilter : Base
    {
        public string WishlistID { get; set; }
        public string PersonID { get; set; }
        public bool IsComplete { get; set; }
        public UserWishlistFilter() : base()
        {
            WishlistID = string.Empty;
            PersonID = string.Empty;
            IsComplete = false;
        }
        public UserWishlistFilter(int pageNumber, int pageSize, string? wishlistID, string? personID, bool isComplete) : base(pageNumber, pageSize)
        {
            WishlistID = !string.IsNullOrEmpty(wishlistID) ? wishlistID : string.Empty;
            PersonID = !string.IsNullOrEmpty(personID) ? personID : string.Empty;
            IsComplete = isComplete;
        }
    }
}
