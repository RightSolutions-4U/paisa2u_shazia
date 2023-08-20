using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using paisa2u.common.Resources;
using paisa2u.common.Services;

namespace paisa2u.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionSetupController : ControllerBase
    {
        private readonly ISubscriptionSetupService _subscriptionSetupService;

        public SubscriptionSetupController(ISubscriptionSetupService subscriptionSetupService)
        {
            _subscriptionSetupService = subscriptionSetupService;
        }

        [HttpGet("GetSubscriptionBySubType")]

        public async Task<ActionResult<SubscriptionSetupResource>> GetSubscriptionBySubType(string Subtype, CancellationToken cancellationToken)
        {

            try
            {
                var response = await _subscriptionSetupService.GetSubscriptionBySubType(Subtype, cancellationToken);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }

        }
    }
}