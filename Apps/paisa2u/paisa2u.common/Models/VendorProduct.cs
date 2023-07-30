//Added by Mohtashim on 27-06-2023

namespace paisa2u.common.Models
{
    public class VendorProduct
    {
        public int Vendorid { get; set; }
        public Vendor Vendor { get; set; }
        public int Productid { get; set; }
        public Product Product { get; set; }
    }
}
