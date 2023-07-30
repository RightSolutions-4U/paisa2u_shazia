using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using paisa2u.common.Models;
using paisa2u.common.Resources;
using paisa2u.common.Services;
using System.Threading;

namespace paisa2u.API.Controllers
{
    

    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly IAddressService _addressService;

        

        public AddressesController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        // GET: api/Addresses
        [HttpGet("GetAddresses")]
        public async Task<IEnumerable<AddressResource>> GetAddresses(CancellationToken cancellationToken)
        {
            return await _addressService.GetAddresses(cancellationToken);
        }

        // GET: api/Addresses/5
        [HttpGet("GetAddress")]
        public async Task<ActionResult<AddressResource>> GetAddress(int id, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _addressService.GetAddress(id, cancellationToken);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }

        // PUT: api/Addresses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAddress([FromBody] AddressResource resource, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _addressService.UpdateAddress(resource.addid, resource, cancellationToken);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }

        // POST: api/Addresses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Address>> PostAddress([FromBody] AddressResource resource, CancellationToken cancellationToken)
        {
            var response = await _addressService.AddAddress(resource, cancellationToken);
            return Ok(response);
        }

        // DELETE: api/Addresses/5
        [HttpDelete]
        public async Task<IActionResult> DeleteAddress([FromBody] AddressResource resource, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _addressService.DeleteAddress(resource, cancellationToken);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }

        
    }
}
