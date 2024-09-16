using System;
using System.Collections.Generic;

namespace UangKuAPI.BusinessObjects.Models;

public partial class AppStandardReference
{
    public string StandardReferenceId { get; set; } = null!;

    public string? StandardReferenceName { get; set; }

    public int? ItemLength { get; set; }

    public bool IsUsedBySystem { get; set; }

    public bool IsActive { get; set; }

    public string? Note { get; set; }

    public DateTime LastUpdateDateTime { get; set; }

    public string LastUpdateByUserId { get; set; } = null!;
}
