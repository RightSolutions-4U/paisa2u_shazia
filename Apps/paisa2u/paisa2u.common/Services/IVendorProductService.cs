using paisa2u.common.Resources;

namespace paisa2u.common.Services
{
    public interface IVendorProductService
    {
        Task<VendorProductResource> AddVendorProduct(VendorProductResource resource, CancellationToken cancellationToken);
        Task<VendorProductResource> DeleteVendorProduct(VendorProductResource resource, CancellationToken cancellationToken);
    }
}
