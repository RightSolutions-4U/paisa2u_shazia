using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using paisa2u.common.Models;
using paisa2u.common.Resources;
using paisa2u.common.Services;
using System.Collections.Generic;
using System.Text.Json;

namespace paisa2u.API.Controllers
{
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class VendorsController : ControllerBase
    {
        private readonly IVendorService _vendorService;
        public VendorsController(IVendorService vendorService)
        {
            _vendorService = vendorService;
        }
        //Added by Shazia on JUl 23- jul 31, 2023 all functions
        [HttpGet("GetSingleProductByVendor")]
        public async Task<IEnumerable<ProductByVendor>> GetSingleProductByVendor(CancellationToken cancellationToken)
        {
            return await _vendorService.GetSingleProductByVendor(cancellationToken);
            
        }
        [HttpGet("GetAllProductsOfAllVendor")]
        public async Task<IEnumerable<ProductByVendor>> GetAllProductsOfAllVendor(CancellationToken cancellationToken)
        {
            return await _vendorService.GetAllProductsOfAllVendor(cancellationToken);

        }
        

        [HttpGet("GetAllProductsByVendor")]
        public async Task<List<ProductByVendor>> GetAllProductsByVendor(string vendorid,CancellationToken cancellationToken)
        {
            return await _vendorService.GetAllProductsByVendor(vendorid, cancellationToken);

        }

        [HttpGet("GetAllProductsByCat")]
        public async Task<List<ProductByVendor>> GetAllProductsByCat(int catid, CancellationToken cancellationToken)
        {
            return await _vendorService.GetAllProductsByCat(catid, cancellationToken);

        }

        [HttpGet("GetAllProductsByCatAmount")]
        public async Task<List<ProductByVendor>> GetAllProductsByCatAmount(int catid, int fromamount, int toamount, CancellationToken cancellationToken)
        {
            return await _vendorService.GetAllProductsByCatAmount(catid, fromamount, toamount, cancellationToken);

        }
        [HttpGet("GetVendor")]
        public async Task<ActionResult<IEnumerable<VendorList>>> GetVendor()
        {
            var a = await _vendorService.GetVendors();
            var jsonString = JsonSerializer.Serialize(a);
            return Ok(jsonString);
        }

        //by mohtashim on paging
        [HttpGet("GetSingleProductByVendorP")]
        public async Task<ActionResult<ProductByVendorResponse>> GetSingleProductByVendorP(int page)
        {
            return await _vendorService.GetSingleProductByVendorP(page);

        }
        //paging on Aug 21,2023 by Shazia
        [HttpGet("GetAllProductsOfAllVendorP")]
        public async Task<ActionResult<ProductByVendorResponse>> GetAllProductsOfAllVendorP(int page)
        {
            return await _vendorService.GetAllProductsOfAllVendorP(page);

        }
        [HttpGet("GetAllProductsByVendorP")]
        public async Task<ActionResult<ProductByVendorResponse>> GetAllProductsByVendorP(string vendorid, int page)
        {
            return await _vendorService.GetAllProductsByVendorP(vendorid, page);

        }
        [HttpGet("GetAllProductsByCatP")]
        public async Task<ActionResult<ProductByVendorResponse>> GetAllProductsByCatP(int catid , int page)
        {
            return await _vendorService.GetAllProductsByCatP(catid, page);

        }
        [HttpGet("GetVendorP")]
        public async Task<ActionResult<IEnumerable<VendorList>>> GetVendorsP(int page)
        {
            var a = await _vendorService.GetVendorsP(page);
            var jsonString = JsonSerializer.Serialize(a);
            return Ok(jsonString);
        }
    }
}
