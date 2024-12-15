using System;
using System.Collections.Generic;

namespace UangKuAPI.BusinessObjects.Models;

public partial class UserDocument
{
    public string DocumentId { get; set; } = null!;

    public string PersonId { get; set; } = null!;

    public string? FileName { get; set; }

    public string? FileExtention { get; set; }

    public string? Note { get; set; }

    public DateTime DocumentDate { get; set; }

    public string? FilePath { get; set; }

    public ulong IsDeleted { get; set; }

    public DateTime LastUpdateDateTime { get; set; }

    public string LastUpdateByUserId { get; set; } = null!;

    public DateTime CreatedDateTime { get; set; }

    public string CreatedByUserId { get; set; } = null!;
}

public class UserDocumentUpload : UserDocument
{
    public byte[]? DocumentData { get; set; }
}
