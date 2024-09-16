using System;
using System.Collections.Generic;

namespace UangKuAPI.BusinessObjects.Models;

public partial class PostalCode
{
    public uint PostalId { get; set; }

    public int? SubdisId { get; set; }

    public int? DisId { get; set; }

    public int? CityId { get; set; }

    public int? ProvId { get; set; }

    public int? PostalCode1 { get; set; }
}
