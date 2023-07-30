using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using paisa2u.common.Models;
using paisa2u.common.Resources;
using paisa2u.common.Services;
using System.Threading;

namespace paisa2u.API.Controllers
{
    public class SubscriptionpercsController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionpercsController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        // GET: Subscriptionpercs/Get/5
        [HttpGet("GetSubsPercent")]
        public async Task<ActionResult<SubscriptionResource>> GetSubsPercent(int id, CancellationToken cancellationToken)
        {

            try
            {
                var response = await _subscriptionService.GetSubsPercent(id, cancellationToken);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }

        }
        // Post: api/Subscriptionpercs
        [HttpPost("AddSubsPercent")]
        public async Task<ActionResult<SubscriptionResource>> AddSubsPercent([FromBody] SubscriptionResource resource, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _subscriptionService.AddSubsPercent(resource, cancellationToken);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }

        // PUT: Subscriptionpercs/Edit/5
        [HttpPut("UpdateSubsPerc")]
        public async Task<ActionResult<SubscriptionResource>> UpdateSubsPerc([FromBody] SubscriptionResource resource, CancellationToken cancellationToken)
        {
            if (resource == null)
            {
                return NotFound();
            }
            try
            {
                var response = await _subscriptionService.UpdateSubsPercent(resource.Recid, resource, cancellationToken);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }

        }


        // GET: Subscriptionpercs/Delete/5
        [HttpDelete("Delete")]
        public async Task<ActionResult<SubscriptionResource>> Delete([FromBody] SubscriptionResource resource, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _subscriptionService.DeleteSubsPercent(resource, cancellationToken);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }


        }



    }
}