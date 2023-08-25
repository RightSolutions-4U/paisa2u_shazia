using System;
using System.Collections.Generic;

namespace paisa2u.common.Models;

public partial class Users
{
    public int RegId { get; set; }

    public string Firstname { get; set; } = null!;

    public string? Middlename { get; set; }

    public string Lastname { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Pwd { get; set; } = null!;

    public int Referredby { get; set; } 

    public string Regtype { get; set; } = null!;

    public string? Vendortype { get; set; }

    public string Phonenumber { get; set; } = null!;

    public DateTime Endate { get; set; }

    public string Enuser { get; set; } = null!;

    public string Substype { get; set; } = null!;

    public bool Regstatus { get; set; }

    public bool Autorenewal { get; set; }

    public string? Qrpicture { get; set; }
    public string? PasswordSalt { get; set; } 
    public string? PasswordHash { get; set; }
    public string? vendorfilename { get; set; }
    

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    /*public virtual ICollection<Subscriptionperc> Subscriptionpercs { get; set; } = new List<Subscriptionperc>();

    public virtual ICollection<Vendor> Vendors { get; set; } = new List<Vendor>();*/
}
