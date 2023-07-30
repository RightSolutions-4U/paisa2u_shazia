using Microsoft.AspNetCore.Mvc;
using paisa2u.common.Resources;
using paisa2u.common.Services;
using System.Text.Json;

namespace paisa2u.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IVendorProductService _vendorproductService;
        private readonly IProductService _productService;

        public ProductsController(IVendorProductService vendorProductService, IProductService productService )
        {
            _vendorproductService = vendorProductService;
            _productService = productService;
        }

        // GET: api/Products
        [HttpGet("GetProducts")]
        public async Task<ActionResult<IEnumerable<ProductResource>>> GetProducts(CancellationToken cancellationToken)
        {
            var a = await _productService.GetProducts(cancellationToken);
            var jsonString = JsonSerializer.Serialize(a);
            return Ok(jsonString);
        }

        // POST: api/Products
        [HttpPost]
        public async Task<ActionResult<ProductResource>> PostProduct(int vendorid, [FromBody]   ProductResource resource, CancellationToken cancellationToken)
        {

            var response = await _productService.AddProduct(resource, cancellationToken);
            //Add record to vendorproduct table
            if (response.productid != null)
            {
                var vendorproduct = new VendorProductResource
                (
                    response.productid,
                    vendorid
                );
                var res = await _vendorproductService.AddVendorProduct(vendorproduct, cancellationToken);
            };
            return Ok(response);

        }

        // GET: api/Products/5
        [HttpGet("GetProduct")]
        public async Task<ActionResult<ProductResource>> GetProduct(int id, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _productService.GetProduct(id, cancellationToken);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }

        // PUT: api/Products/5
        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductResource resource, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _productService.UpdateProduct(resource.productid, resource, cancellationToken);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }



        // DELETE: api/Products/5
        [HttpDelete]
        public async Task<IActionResult> DeleteProduct([FromBody] ProductResource resource, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _productService.DeleteProduct(resource, cancellationToken);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }

        
    }
}
