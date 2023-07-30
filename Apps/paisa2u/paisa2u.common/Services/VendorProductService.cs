using Microsoft.EntityFrameworkCore;
using paisa2u.common.Resources;
using paisa2u.common.Models;
using System.Configuration;

namespace paisa2u.common.Services
{
    public class VendorProductService : IVendorProductService
    {
        private readonly PaisaDbContext _context;

        public VendorProductService(PaisaDbContext context)
        {
            _context = context;
        }
        public async Task<VendorProductResource> AddVendorProduct(VendorProductResource resource, CancellationToken cancellationToken)
        {
            var vendorproduct = new VendorProduct
            {
                Productid = resource.productid,
                Vendorid = resource.vendorid
            };
            await _context.VendorProducts.AddAsync(vendorproduct, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return new VendorProductResource(
                vendorproduct.Vendorid,
                vendorproduct.Productid
                );
        }

        public async Task<VendorProductResource> DeleteVendorProduct(VendorProductResource resource, CancellationToken cancellationToken)
        {
            var vendorproduct = await _context.VendorProducts
                .FirstOrDefaultAsync(x => x.Vendorid == resource.vendorid && x.Productid== resource.productid, cancellationToken);
            if (vendorproduct.Vendorid != resource.vendorid && vendorproduct.Productid != resource.productid)
            {
                throw new Exception("Vendor product to delete does not exist");
            }
            var vendorproducttodelete = new VendorProductResource(
                    vendorproduct.Vendorid,
                    vendorproduct.Productid
                );
            _context.Entry(vendorproduct).State = EntityState.Deleted;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new Exception("Vendor product can not be deleted!");

            }
            return vendorproducttodelete;
        }
    }
}
