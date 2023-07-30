using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using paisa2u.common.Resources;
using paisa2u.common.Services;
using System.Text.Json;

namespace paisa2u.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
            
        }

        // GET: api/Categories
        [HttpGet("GetCategories")]
        public async Task<ActionResult<IEnumerable<CategoryResource>>> GetCategories(CancellationToken cancellationToken)
        {
            var a = await _categoryService.GetCategories(cancellationToken);
            var jsonString = JsonSerializer.Serialize(a);
            return Ok(jsonString);
        }
        // GET: api/Categories/5
        [HttpGet("GetCategory")]
        public async Task<ActionResult<ProductResource>> GetCategory(int catid, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _categoryService.GetCategory(catid, cancellationToken);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }

        // GET: api/GetProductsForAllCategories
        [HttpGet("GetProductsForAllCategories")]
        public async Task<ActionResult<IEnumerable<ProductResource>>> GetProductsForAllCategories(CancellationToken cancellationToken)
        {
            var a = await _categoryService.GetProductsForAllCategories(cancellationToken);
            var jsonString = JsonSerializer.Serialize(a);
            return Ok(jsonString);
        }

        // GET: api/Categories/5
        [HttpGet("GetProductsForSingleCategory")]
        public async Task<ActionResult<IEnumerable<CategoryResource>>> GetProductsForSingleCategory(int catid, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _categoryService.GetProductsForSingleCategory(catid, cancellationToken);
                var jsonString = JsonSerializer.Serialize(response);
                return Ok(jsonString);
                
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }
        // PUT: api/Category/5
        [HttpPut]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryResource resource, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _categoryService.UpdateCategory(resource.catid, resource, cancellationToken);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }



        // DELETE: api/Categories/5
        [HttpDelete]
        public async Task<IActionResult> DeleteCategory([FromBody] CategoryResource resource, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _categoryService.DeleteCategory(resource, cancellationToken);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }


    }
}
