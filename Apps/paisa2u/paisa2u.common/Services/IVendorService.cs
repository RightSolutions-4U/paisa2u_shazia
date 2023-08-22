using Microsoft.AspNetCore.Mvc;
using paisa2u.common.Models;
using paisa2u.common.Resources;

namespace paisa2u.common.Services
{
    public interface IVendorService
    {
        Task<VendorResource> AddVendor(VendorResource resource, CancellationToken cancellationToken);
        Task<VendorResource> RemoveVendor(VendorResource resource, CancellationToken cancellationToken);
        Task<List<VendorList>> GetVendors();

        Task<List<ProductByVendor>> GetSingleProductByVendor(CancellationToken cancellationToken);
        Task<IEnumerable<ProductByVendor>> GetAllProductsOfAllVendor(CancellationToken cancellationToken);
       Task<List<ProductByVendor>> GetAllProductsByVendor(string vendorid,CancellationToken cancellationToken);
        Task<List<ProductByVendor>> GetAllProductsByCat(int catid, CancellationToken cancellationToken);

        Task<List<ProductByVendor>> GetAllProductsByCatAmount(int catid,int fromamount, int toamount, CancellationToken cancellationToken);
        //with paging on Aug 21, 2023 by Shazia
        Task<ActionResult<ProductByVendorResponse>> GetSingleProductByVendorP(int page);
        Task<ActionResult<ProductByVendorResponse>> GetAllProductsOfAllVendorP(int page);
        Task<ActionResult<ProductByVendorResponse>> GetAllProductsByVendorP(string vendorid, int page);
        Task<ActionResult<ProductByVendorResponse>> GetAllProductsByCatP(int catid, int page);
        Task<ActionResult<VendorListResponse>> GetVendorsP(int page);

    }
}
