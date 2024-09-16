using System;
using System.Collections.Generic;

namespace UangKuAPI.BusinessObjects.Models;

public partial class UserWishlist
{
    public string WishlistId { get; set; } = null!;

    public string PersonId { get; set; } = null!;

    public string SrproductCategory { get; set; } = null!;

    public string? ProductName { get; set; }

    public int? ProductQuantity { get; set; }

    public decimal? ProductPrice { get; set; }

    public string? ProductLink { get; set; }

    public string CreatedByUserId { get; set; } = null!;

    public DateTime CreatedDateTime { get; set; }

    public string LastUpdateByUserId { get; set; } = null!;

    public DateTime LastUpdateDateTime { get; set; }

    public DateOnly? WishlistDate { get; set; }

    public byte[]? ProductPicture { get; set; }

    public int IsComplete { get; set; }
}
