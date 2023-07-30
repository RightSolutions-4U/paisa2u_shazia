using System;
using System.Collections.Generic;

namespace paisa2u.common.Models;

public partial class Category
{
    public int Catid { get; set; }

    public string? Catdesc { get; set; }

    public DateTime Endate { get; set; }

    public string Enuser { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
