using System;
using System.Collections.Generic;

namespace UangKuAPI.BusinessObjects.Models;

public partial class AppParameter
{
    public string ParameterId { get; set; } = null!;

    public string? ParameterName { get; set; }

    public string? ParameterValue { get; set; }

    public string Srcontrol { get; set; } = null!;

    public DateTime LastUpdateDateTime { get; set; }

    public string LastUpdateByUserId { get; set; } = null!;

    public bool IsUsedBySystem { get; set; }
}
