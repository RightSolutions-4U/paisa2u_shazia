using paisa2u.common.Resources;
using Microsoft.EntityFrameworkCore;
using paisa2u.common.Models;
using Microsoft.AspNetCore.Mvc;

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

        public async Task<List<ProductByVendor>> GetAllProductsByCatAmount(int catid, int fromamount, int toamount, CancellationToken cancellationToken)
        {
            var SingleProductByVendor1 = await _context
                            .GetAllProductsByVendor
                            .Where(t => t.Catid == catid && (t.Discountamount >= fromamount && t.Discountamount <= toamount))
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

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductByVendorResponse>> GetSingleProductByVendorP(int page)
        {
            var pageResults = 3f;
            var pageCount = Math.Ceiling(_context.GetProductByVendorP.Count() / pageResults);
            var SingleProductByVendor1 = await _context
                            .GetProductByVendorP
                            .Skip((page - 1) * (int)pageResults)
                            .Take((int)pageResults)
                            .ToListAsync();
            var response = new ProductByVendorResponse
            {
                Products = SingleProductByVendor1,
                CurrentPage = page,
                pages = (int)pageCount
            };
            return response;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductByVendorResponse>> GetAllProductsByVendorP(string vendorid, int page)
        {
            var pageResults = 3f;
            var pageCount = Math.Ceiling(_context.GetAllProductsByVendorP.Count() / pageResults);
            var AllProductByVendor = await _context
                            .GetAllProductsByVendorP
                            .Where(t => t.Vendorid.ToString() == vendorid)
                            .Skip((page - 1) * (int)pageResults)
                            .Take((int)pageResults)
                            .ToListAsync();
            var response = new ProductByVendorResponse
            {
                Products = AllProductByVendor,
                CurrentPage = page,
                pages = (int)pageCount
            };
            return response;
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductByVendorResponse>> GetAllProductsOfAllVendorP(int page)
        {
            var pageResults = 3f;
            var pageCount = Math.Ceiling(_context.GetAllProductsByVendorP.Count() / pageResults);
            var AllProductByVendor = await _context
                            .GetAllProductsByVendorP
                            .Skip((page - 1) * (int)pageResults)
                            .Take((int)pageResults)
                            .ToListAsync();
            var response = new ProductByVendorResponse
            {
                Products = AllProductByVendor,
                CurrentPage = page,
                pages = (int)pageCount
            };
            return response;
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductByVendorResponse>> GetAllProductsByCatP(int catid, int page)
        {
            var pageResults = 3f;
            var pageCount = Math.Ceiling(_context.GetAllProductsByCatP.Count() / pageResults);
            var AllProductByVendor = await _context
                            .GetAllProductsByVendorP
                            .Where(t => t.Catid == catid)
                            .Skip((page - 1) * (int)pageResults)
                            .Take((int)pageResults)
                            .ToListAsync();
            var response = new ProductByVendorResponse
            {
                Products = AllProductByVendor,
                CurrentPage = page,
                pages = (int)pageCount
            };
            return response;
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VendorListResponse>> GetVendorsP(int page)
        {
            var pageResults = 3f;
            var pageCount = Math.Ceiling(_context.GetVendorsP.Count() / pageResults);
            var AllVendors = await _context
                            .GetVendorsP
                            .Skip((page - 1) * (int)pageResults)
                            .Take((int)pageResults)
                            .ToListAsync();
            var response = new VendorListResponse
            {
                vendors = AllVendors,
                CurrentPage = page,
                pages = (int)pageCount
            };
            return response;
        }

    }
}


