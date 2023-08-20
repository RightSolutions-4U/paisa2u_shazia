using paisa2u.common.Resources;
using Microsoft.EntityFrameworkCore;
using paisa2u.common.Models;
using System.Text;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Numerics;
using System.Drawing;
using System.Linq;

namespace paisa2u.common.Services
{
    public class VendorService : IVendorService
    {
        private readonly PaisaDbContext _context;

        public VendorService(PaisaDbContext context)
        {
            _context = context;
        }
        public async Task<VendorResource> AddVendor(VendorResource resource, CancellationToken cancellationToken)
        {
            var vendor = new Vendor
            {
                RegId = resource.Regid,
                Endate = resource.Endate,
                Enuser = resource.Enuser,
                pictureurl = resource.pictureurl
            };
            await _context.Vendors.AddAsync(vendor, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return resource;
        }
        public async Task<List<ProductByVendor>> GetSingleProductByVendor(CancellationToken cancellationToken)
        {
            var SingleProductByVendor1 = await _context
                            .GetProductByVendor
                            .ToListAsync();

            return SingleProductByVendor1;
            
        }
        public async Task<IEnumerable<ProductByVendor>> GetAllProductsOfAllVendor(CancellationToken cancellationToken)
        {
            var AllProducts = await _context
                            .GetAllProductsByVendor
                            .ToListAsync();

            return AllProducts;

        }
      
        public async Task<List<ProductByVendor>> GetAllProductsByCat(int catid, CancellationToken cancellationToken)
        {
            var SingleProductByVendor1 = await _context
                            .GetAllProductsByVendor
                            .Where(t => t.Catid == catid)
                            .ToListAsync();

            return SingleProductByVendor1;

        }

        public async Task<List<ProductByVendor>> GetAllProductsByCatAmount(int catid,int fromamount, int toamount, CancellationToken cancellationToken)
        {
            var SingleProductByVendor1 = await _context
                            .GetAllProductsByVendor
                            .Where(t => t.Catid == catid && (t.Discountamount>=fromamount && t.Discountamount <= toamount))
                            .ToListAsync();

            return SingleProductByVendor1;

        }
        public Task<VendorResource> RemoveVendor(VendorResource resource, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<List<VendorList>> GetVendors()
        {
            var vendors = await _context
                        .VendorList
                        .ToListAsync();
            return vendors;
        }

        public async Task<List<ProductByVendor>> GetAllProductsByVendor(string vendorid, CancellationToken cancellationToken)
        {
            var AllProductByVendor = await _context
                            .GetAllProductsByVendor
                            .Where(t => t.Vendorid.ToString() == vendorid)
                            .ToListAsync();
            return AllProductByVendor;
        }
    }
}
