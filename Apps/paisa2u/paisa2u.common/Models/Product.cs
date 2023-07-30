using System;
using System.Collections.Generic;

namespace paisa2u.common.Models;

public partial class Product
{
    public int Productid { get; set; }

    public int Catid { get; set; }

    public string Productname { get; set; } = null!;

    public string Productcode { get; set; } = null!;

    public string Picture { get; set; } = null!;

    public double Discountpercentage { get; set; }

    public double Discountamount { get; set; }

    public string Discountcode { get; set; } = null!;

    public string Dicountcondition { get; set; } = null!;

    public virtual Category Cat { get; set; } = null!;

    public virtual ICollection<Vendor> Vendors { get; set; } = new List<Vendor>();

    //Added by Mohtashim on 27-06-2023
    public ICollection<VendorProduct> VendorProducts { get; set; }
}
