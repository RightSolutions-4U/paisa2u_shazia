using System;
using System.Collections.Generic;

namespace paisa2u.common.Models;

public partial class Subscription
{
    public int Subsid { get; set; }

    public string Subtype { get; set; } = null!;

    public int? Subfee { get; set; } 

    public double? Appowner { get; set; }

    public double? Vendor { get; set; }

    public double? Subvendor { get; set; }

    public double? Customer { get; set; }

    public DateTime? Endate { get; set; }

    public string? Enuser { get; set; }
}
