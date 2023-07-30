using System;
using System.Collections.Generic;

namespace paisa2u.common.Models;

public partial class Subscriptionperc
{
    public int RecId { get; set; }

    public int RegId { get; set; }

    public float? Appowner { get; set; }

    public float? Vendor { get; set; }

    public float? Subvendor { get; set; }

    public float? Customer { get; set; }

    public DateTime Endate { get; set; }

    public string Enuser { get; set; } = null!;

    public virtual Users Reg { get; set; } = null!;
}
