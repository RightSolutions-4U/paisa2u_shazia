using System;
using System.Collections.Generic;

namespace paisa2u.common.Models;

public partial class Vendor
{
    public int Vendorid { get; set; }

    public int RegId { get; set; }

    public DateTime Endate { get; set; }

    public string Enuser { get; set; } = null!;
    public string pictureurl { get; set; } 

    public virtual Users Reg { get; set; } = null!;
 
    //added by shazia on aug 21
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    
    //Added by Mohtashim on 27-06-2023
    public ICollection<VendorProduct> VendorProducts { get; set; }
}
