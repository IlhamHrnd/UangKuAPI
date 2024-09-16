using System;
using System.Collections.Generic;

namespace UangKuAPI.BusinessObjects.Models;

public partial class Subdistrict
{
    public int SubdisId { get; set; }

    public string? SubdisName { get; set; }

    public int? DisId { get; set; }
}
