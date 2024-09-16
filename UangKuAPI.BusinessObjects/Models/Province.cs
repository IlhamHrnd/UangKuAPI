using System;
using System.Collections.Generic;

namespace UangKuAPI.BusinessObjects.Models;

public partial class Province
{
    public int ProvId { get; set; }

    public string? ProvName { get; set; }

    public int? LocationId { get; set; }

    public int? Status { get; set; }
}
