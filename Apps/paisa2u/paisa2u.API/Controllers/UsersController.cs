using paisa2u.common.Resources;
using paisa2u.common.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using paisa2u.common.Models;

namespace paisa2u.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IRegUserService _reguserService;
        private readonly IVendorService _vendorService;

        public UsersController(IRegUserService reguserService, IVendorService vendorService)
        {
            _reguserService = reguserService;
            _vendorService = vendorService;
        }
        //Added by Shazia Aug 4, 2023 for registration
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegUserRegisterResource resource, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _reguserService.Registration(resource, cancellationToken);
                //Add record to vendor table
                if(response.Vendortype == "V")
                {
                    var vendorresource = new VendorResource
                    (
                    0,
                    response.Regid, response.Endate, response.Enuser
                    );
                    var res = await _vendorService.AddVendor(vendorresource, cancellationToken);
                }
                

                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginResource resource, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _reguserService.Login(resource, cancellationToken);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }

        ////* GetRegUsers
        [HttpGet("GetRegUsers")]
        public async Task<List<RegUserResource>> GetRegUsers(CancellationToken cancellationToken)
        {
            return await _reguserService.GetRegUsers(cancellationToken);
            
        }

        [HttpGet("GetAllReferrals")]
        public async Task<List<RegUserResource>> GetAllReferrals(CancellationToken cancellationToken)
        {
            return await _reguserService.GetAllReferrals(cancellationToken);
        }

        [HttpGet("GetAllReferralsByRegid")]
        public async Task<List<RegUserResource>> GetAllReferralsByRegid(int Regid,CancellationToken cancellationToken)
        {
            return await _reguserService.GetAllReferralsByRegid(Regid,cancellationToken);
        }
        [HttpGet("GetRegUser")]
        public async Task<IActionResult> GetRegUser(int regid, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _reguserService.GetRegUser(regid, cancellationToken);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }

        [HttpPut("UpdateRegUser")]
        public async Task<IActionResult> UpdateRegUser([FromBody] RegUserResource resource, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _reguserService.UpdateRegUser(resource.Regid, resource, cancellationToken);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }
        [HttpDelete("DeleteRegUser")]
        public async Task<IActionResult> DeleteRegUser([FromBody] RegUserResource resource, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _reguserService.DeleteRegUser(resource, cancellationToken);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }

    }
}
