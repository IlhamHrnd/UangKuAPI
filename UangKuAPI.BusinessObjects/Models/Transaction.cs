using System;
using System.Collections.Generic;

namespace UangKuAPI.BusinessObjects.Models;

public partial class Transaction
{
    public string TransNo { get; set; } = null!;

    public string PersonId { get; set; } = null!;

    public string Srtransaction { get; set; } = null!;

    public string SrtransItem { get; set; } = null!;

    public decimal? Amount { get; set; }

    public string? Description { get; set; }

    public byte[]? Photo { get; set; }

    public string? TransType { get; set; }

    public DateOnly? TransDate { get; set; }

    public DateTime CreatedDateTime { get; set; }

    public string CreatedByUserId { get; set; } = null!;

    public DateTime LastUpdateDateTime { get; set; }

    public string LastUpdateByUserId { get; set; } = null!;
}
