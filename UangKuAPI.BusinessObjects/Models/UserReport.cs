using System;
using System.Collections.Generic;

namespace UangKuAPI.BusinessObjects.Models;

public partial class UserReport
{
    public string ReportNo { get; set; } = null!;

    public DateTime? DateErrorOccured { get; set; }

    public string SrerrorLocation { get; set; } = null!;

    public string SrerrorPossibility { get; set; } = null!;

    public string? ErrorCronologic { get; set; }

    public byte[]? Picture { get; set; }

    public int? IsApprove { get; set; }

    public string SrreportStatus { get; set; } = null!;

    public DateTime? ApprovedDateTime { get; set; }

    public string? ApprovedByUserId { get; set; }

    public DateTime? VoidDateTime { get; set; }

    public string? VoidByUserId { get; set; }

    public DateTime CreatedDateTime { get; set; }

    public string CreatedByUserId { get; set; } = null!;

    public DateTime LastUpdateDateTime { get; set; }

    public string LastUpdateByUserId { get; set; } = null!;

    public string PersonId { get; set; } = null!;
}
