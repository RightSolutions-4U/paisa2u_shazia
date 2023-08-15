using System;
using System.Collections.Generic;

namespace paisa2u.common.Models;

public partial class Siteuser
{
    public int Userid { get; set; } 

    public string Username { get; set; } = null;
    public string Pwd { get; set; }//added by shazia on aug 11

    public string? Email { get; set; }

    public string Adminrole { get; set; } = null;

    public string? PasswordSalt { get; set; } =null;
    public string? PasswordHash { get; set; } = null;

    public string? JwtToken { get; set; } = null;
}
