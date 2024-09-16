using System;
using System.Collections.Generic;

namespace UangKuAPI.BusinessObjects.Models;

public partial class AppProgram
{
    public string ProgramId { get; set; } = null!;

    public string? ProgramName { get; set; }

    public string? Note { get; set; }

    public ulong? IsProgram { get; set; }

    public ulong? IsProgramAddAble { get; set; }

    public ulong? IsProgramEditAble { get; set; }

    public ulong? IsProgramDeleteAble { get; set; }

    public ulong? IsProgramViewAble { get; set; }

    public ulong? IsProgramApprovalAble { get; set; }

    public ulong? IsProgramUnApprovalAble { get; set; }

    public ulong? IsProgramVoidAble { get; set; }

    public ulong? IsProgramUnVoidAble { get; set; }

    public ulong? IsProgramPrintAble { get; set; }

    public ulong? IsVisible { get; set; }

    public DateTime LastUpdateDateTime { get; set; }

    public string LastUpdateByUserId { get; set; } = null!;

    public ulong IsUsedBySystem { get; set; }
}
