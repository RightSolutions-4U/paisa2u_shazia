using paisa2u.common.Models;

namespace paisa2u.common.Resources
{
    public sealed record ProductResource
    (
        int productid,
        int catid,
        string productname,
        string productcode,
        string picture,
        double discountpercentage,
        double discountamount,
        string discountcode,
        string dicountcondition
    );
    
}
