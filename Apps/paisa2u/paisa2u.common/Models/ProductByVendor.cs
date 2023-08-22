namespace paisa2u.common.Models
{
    public class ProductByVendor
    {
        public long NID { get; set; }
        public int RegId { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Lastname { get; set; }
        public int Vendorid { get; set; }
        public int Productid { get; set; }
        public string Productname { get; set; }
        public string Productcode { get; set; }
        public string Picture { get; set; }
        public double Discountpercentage { get; set; }
        public double Discountamount { get; set; }
        public string Discountcode { get; set; }
        public string Dicountcondition { get; set; }
        public int Catid { get; set; }
        public string Catdesc { get; set; }
        public string pictureurl { get; set; }

    }
}
