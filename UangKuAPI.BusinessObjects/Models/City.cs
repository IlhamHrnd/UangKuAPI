using System;
using System.Collections.Generic;

namespace UangKuAPI.BusinessObjects.Models;

public partial class City
{
    public int CityId { get; set; }

    public string? CityName { get; set; }

    public int? ProvId { get; set; }
}
