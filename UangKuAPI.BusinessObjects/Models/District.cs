using System;
using System.Collections.Generic;

namespace UangKuAPI.BusinessObjects.Models;

public partial class District
{
    public int DisId { get; set; }

    public string? DisName { get; set; }

    public int? CityId { get; set; }
}
