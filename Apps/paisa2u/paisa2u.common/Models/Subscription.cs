using System;
using System.Collections.Generic;

namespace paisa2u.common.Models;

public partial class Subscription
{
    public int Subsid { get; set; }

    public string Subtype { get; set; } = null!;

    public int? Subfee { get; set; }

    public float? Appowner { get; set; }

    public float? Vendor { get; set; }

    public float? Subvendor { get; set; }

    public float? Customer { get; set; }

    public DateTime? Endate { get; set; }

    public string? Enuser { get; set; }
}
