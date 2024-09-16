using System;
using System.Collections.Generic;

namespace UangKuAPI.BusinessObjects.Models;

public partial class AppStandardReferenceItem
{
    public string StandardReferenceId { get; set; } = null!;

    public string ItemId { get; set; } = null!;

    public string? ItemName { get; set; }

    public string? Note { get; set; }

    public int IsUsedBySystem { get; set; }

    public int IsActive { get; set; }

    public DateTime LastUpdateDateTime { get; set; }

    public string LastUpdateByUserId { get; set; } = null!;

    public byte[]? ItemIcon { get; set; }
}
