using System;
using System.Collections.Generic;

namespace UangKuAPI.BusinessObjects.Models;

public partial class UserPicture
{
    public string PictureId { get; set; } = null!;

    public byte[] Picture { get; set; } = null!;

    public string? PictureName { get; set; }

    public string? PictureFormat { get; set; }

    public string PersonId { get; set; } = null!;

    public int IsDeleted { get; set; }

    public string CreatedByUserId { get; set; } = null!;

    public DateTime CreatedDateTime { get; set; }

    public DateTime LastUpdateDateTime { get; set; }

    public string LastUpdateByUserId { get; set; } = null!;
}
