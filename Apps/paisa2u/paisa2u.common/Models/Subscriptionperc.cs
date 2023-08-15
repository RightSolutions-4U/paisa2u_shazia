using System;
using System.Collections.Generic;

namespace paisa2u.common.Models;

public partial class Subscriptionperc
{
    public int RecId { get; set; }

    public int RegId { get; set; }

    public double? Appowner { get; set; }

    public double? Vendor { get; set; }

    public double? Subvendor { get; set; }

    public double? Customer { get; set; }

    public DateTime Endate { get; set; }

    public string Enuser { get; set; } = null!;

    public virtual Users Reg { get; set; } = null!;
}
