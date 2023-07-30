using System;
using System.Collections.Generic;

namespace paisa2u.common.Models;

public partial class Address
{
    public int Addid { get; set; }

    public int RegId { get; set; }

    public int? Countryid { get; set; }

    public int? Cityid { get; set; }

    public string Streetaddress { get; set; } = null!;

    public string? Postal { get; set; }

    public string District { get; set; } = null!;

    public string Townstehsil { get; set; } = null!;

    public string Area { get; set; } = null!;

    public string? Longitude { get; set; }

    public string? Latitude { get; set; }

    public virtual City? City { get; set; }

    public virtual Country? Country { get; set; }

    public virtual Users Reg { get; set; } = null!;
}
