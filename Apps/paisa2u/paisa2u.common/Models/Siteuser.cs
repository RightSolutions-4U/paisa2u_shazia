using System;
using System.Collections.Generic;

namespace paisa2u.common.Models;

public partial class Siteuser
{
    public int Userid { get; set; }

    public string? Username { get; set; }

    public string? Email { get; set; }

    public string? Adminrole { get; set; }

    public string? PasswordSalt { get; set; }
    public string? PasswordHash { get; set; }

    public string? JwtToken { get; set; }
}
