using System.ComponentModel.DataAnnotations;

namespace UangKuAPI.BusinessObjects.Model
{
    public class UserWishlist
    {
        [Key]
        [Required(ErrorMessage = "WishlistID Is Required")]
        public string? WishlistID { get; set; }
        public string? PersonID { get; set; }
        public string? SRProductCategory { get; set; }
        public string? ProductName { get; set; }
        public int? ProductQuantity { get; set; }
        public decimal ProductPrice { get; set; }
        public string? ProductLink { get; set; }
        public string? CreatedByUserID { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public string? LastUpdateByUserID { get; set; }
        public DateTime? LastUpdateDateTime { get; set; }
        public DateTime? WishlistDate { get; set; }
        public byte[]? ProductPicture { get; set; }
        public bool? IsComplete { get; set; }
    }

    public class UserWishlist2
    {
        [Key]
        [Required(ErrorMessage = "WishlistID Is Required")]
        public string? WishlistID { get; set; }
        public string? PersonID { get; set; }
        public string? SRProductCategory { get; set; }
        public string? ProductName { get; set; }
        public int? ProductQuantity { get; set; }
        public decimal ProductPrice { get; set; }
        public string? ProductLink { get; set; }
        public string? CreatedByUserID { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public string? LastUpdateByUserID { get; set; }
        public DateTime? LastUpdateDateTime { get; set; }
        public DateTime? WishlistDate { get; set; }
        public string? ProductPicture { get; set; }
        public bool? IsComplete { get; set; }
        public long PictureSize { get; set; }
    }

    public class UserWishlistPerCategory
    {
        public int? CountProductCategory { get; set; }
        public string? ItemName { get; set; }
        public byte[]? ItemIcon { get; set; }
    }
}
