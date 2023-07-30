using System;
using System.Collections.Generic;

namespace paisa2u.common.Models;

public partial class City
{
    public int Cityid { get; set; }

    public int? Countryid { get; set; }

    public string Cityname { get; set; } = null!;

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual Country? Country { get; set; }
}
