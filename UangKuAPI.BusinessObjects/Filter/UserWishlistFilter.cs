namespace UangKuAPI.BusinessObjects.Filter
{
    public class UserWishlistFilter : Base.Base
    {
        public string? TransType { get; set; }
        public string? PersonID { get; set; }
        public bool? IsComplete { get; set; }
        public string? WishlistID { get; set; }
        public UserWishlistFilter() : base()
        {
            TransType = string.Empty;
            PersonID = string.Empty;
            IsComplete = null;
            WishlistID = string.Empty;
        }
        public UserWishlistFilter(int pageNumber, int pageSize, string? transType, string? personID, bool? isComplete, string? wishlistID) : base(pageNumber, pageSize)
        {
            TransType = !string.IsNullOrEmpty(transType) ? transType : string.Empty;
            PersonID = !string.IsNullOrEmpty(personID) ? personID : string.Empty;
            IsComplete = isComplete.HasValue ? isComplete.Value : null;
            WishlistID = !string.IsNullOrEmpty(wishlistID) ? wishlistID : string.Empty;
        }
    }
}