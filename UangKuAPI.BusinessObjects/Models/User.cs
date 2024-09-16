using System;
using System.Collections.Generic;

namespace UangKuAPI.BusinessObjects.Models;

public partial class User
{
    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Srsex { get; set; } = null!;

    public string Sraccess { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Srstatus { get; set; } = null!;

    public DateTime ActiveDate { get; set; }

    public DateTime LastLogin { get; set; }

    public DateTime LastUpdateDateTime { get; set; }

    public string LastUpdateByUser { get; set; } = null!;

    public string PersonId { get; set; } = null!;
}
