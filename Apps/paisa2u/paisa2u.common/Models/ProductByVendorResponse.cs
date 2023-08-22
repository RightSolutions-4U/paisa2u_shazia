namespace paisa2u.common.Models
{
  
        public class ProductByVendorResponse
        {
            public List<ProductByVendor> Products { get; set; } = new List<ProductByVendor>();

            public int pages { get; set; }

            public int CurrentPage { get; set; }

        }

    }
