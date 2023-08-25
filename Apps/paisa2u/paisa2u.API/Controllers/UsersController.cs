using paisa2u.common.Resources;
using paisa2u.common.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using paisa2u.common.Models;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

namespace paisa2u.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IRegUserService _reguserService;
        private readonly IVendorService _vendorService;
        private readonly ISubscriptionSetupService _subscriptionSetupService;
        private readonly ITransactionsService _transactionsService;

        public UsersController(IRegUserService reguserService, IVendorService vendorService, 
                ISubscriptionSetupService subscriptionSetupService, ITransactionsService transactionService)
        {
            _reguserService = reguserService;
            _vendorService = vendorService;
            _subscriptionSetupService = subscriptionSetupService;
            _transactionsService = transactionService;
        }
        //Added by Shazia Aug 4, 2023 for registration
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegUserRegisterResource resource, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _reguserService.Registration(resource, cancellationToken);
                //Add record to vendor table
                if(response.Regtype == "V")
                {
                    var vendorresource = new VendorResource
                    (
                    0,
                    response.Regid, response.Endate, response.Enuser, response.vendorfilename
                    );
                    var res = await _vendorService.AddVendor(vendorresource, cancellationToken);
                }
                if (response.Regtype != "O") //for all other types deduct fees on Aug 18
                {
                    var result = await _subscriptionSetupService.GetSubscriptionBySubType(response.Substype, cancellationToken);
                    if (result != null)
                    {
                        var transactionresource = new TransactionsResource
                        (0, response.Regid, result.Subfee, response.Endate, response.Enuser,"D");
                        var resulttrans = await _transactionsService.AddTransaction(transactionresource, cancellationToken);
                        if (resulttrans == null)
                        {
                            return BadRequest(new { ErrorMessage = resulttrans });
                        }
                    }
                    else { return BadRequest(new { ErrorMessage = "Subscription fee is null" }); }
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
        [HttpPost("Login_check_email")]
        public async Task<IActionResult> Login_check_email([FromBody] UserLoginResource resource, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _reguserService.Login_check_email(resource, cancellationToken);
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
        [HttpGet("GetRegType")]
        public async Task<RegUserResource> GetRegType(int Regid, CancellationToken cancellationToken)
        {
            return await _reguserService.GetRegType(Regid, cancellationToken);
        }
        [HttpGet("GetRegUser")]
        public async Task<ActionResult<RegUserResource>> GetRegUser(int regid, CancellationToken cancellationToken)
        {
            try
            {
                return await _reguserService.GetRegUser(regid, cancellationToken);
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("UpdateRegUser")]
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
