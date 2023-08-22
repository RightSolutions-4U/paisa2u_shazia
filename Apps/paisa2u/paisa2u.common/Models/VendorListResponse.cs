namespace paisa2u.common.Models
{
    public class VendorListResponse
    {
        public List <VendorList> vendors { get; set; } = new List <VendorList> ();
        public int pages { get; set; }

        public int CurrentPage { get; set; }
    }
}
