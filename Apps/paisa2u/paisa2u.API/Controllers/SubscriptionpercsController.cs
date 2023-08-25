using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using paisa2u.common.Models;
using paisa2u.common.Resources;
using paisa2u.common.Services;
using System.Threading;

namespace paisa2u.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionpercsController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionpercsController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        
        [HttpGet("GetSubsPercentByRegId")]
        public async Task<ActionResult<SubscriptionResource>> GetSubsPercentByRegId(int RegId, CancellationToken cancellationToken)
        {

            try
            {
                var response = await _subscriptionService.GetSubsPercentByRegId(RegId, cancellationToken);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }

        }
        
        [HttpPost("GetSubsPercentAll")]
        public async Task<ActionResult<RegUserResource>> GetSubsPercentAll(CancellationToken cancellationToken)
        {

            try
            {
                var response = await _subscriptionService.GetSubsPercentAll(cancellationToken);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }

        }
        [HttpPost("GetSubsPercentAllSubs")]
        public async Task<ActionResult<SubscriptionResource>> GetSubsPercentAllSubs(CancellationToken cancellationToken)
        {

            try
            {
                var response = await _subscriptionService.GetSubsPercentAllSubs(cancellationToken);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }

        }
        [HttpPost("AddSubsPercent")]
        public async Task<ActionResult<SubscriptionResource>> AddSubsPercent(IFormCollection collection, CancellationToken cancellationToken)
        {
            try
            {
                Subscriptionperc subscriptionperc = new Subscriptionperc();
                subscriptionperc.Endate = DateTime.Now;
                subscriptionperc.Enuser = "Shazia";/*Request.Cookies["username"];*/ // set in login of the form
                var Appowner = collection["Appowner"];
                var Vendor = collection["Vendor"];
                var Customer = collection["Customer"];
                var Subvendor = collection["Subvendor"];
                subscriptionperc.Appowner = float.Parse(Appowner);
                subscriptionperc.Vendor = float.Parse(Vendor);
                subscriptionperc.Customer = float.Parse(Customer);
                subscriptionperc.Subvendor = float.Parse(Subvendor);
                

                var subscription = new SubscriptionResource
                (
                subscriptionperc.RecId,
                int.Parse(collection["RegId"]),
                float.Parse(collection["Appowner"]),
                float.Parse(collection["Vendor"]),
                float.Parse(collection["Subvendor"]),
                float.Parse(collection["Customer"]),
                 DateTime.Now,
                subscriptionperc.Enuser

                );

                var response = await _subscriptionService.AddSubsPercent(subscription, cancellationToken);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }

        // PUT: Subscriptionpercs/Edit/5
        [HttpPost("UpdateSubsPerc")]
        public async Task<ActionResult<SubscriptionResource>> UpdateSubsPerc(IFormCollection collection, CancellationToken cancellationToken)
        {
            try
            {
                Subscriptionperc subscriptionperc = new Subscriptionperc();

                subscriptionperc.Endate = DateTime.Parse(collection["Endate"]);
                subscriptionperc.RegId = int.Parse(collection["RegId"]);
                subscriptionperc.RecId = int.Parse(collection["RecId"]);
                subscriptionperc.Enuser = collection["Enuser"];
                var Appowner = collection["Appowner"];
                var Vendor = collection["Vendor"];
                var Customer = collection["Customer"];
                var Subvendor = collection["Subvendor"];
                subscriptionperc.Appowner = float.Parse(Appowner);
                subscriptionperc.Vendor = float.Parse(Vendor);
                subscriptionperc.Customer = float.Parse(Customer);
                subscriptionperc.Subvendor = float.Parse(Subvendor);


                var subscription = new SubscriptionResource
                (
                int.Parse(collection["RecId"]),
                int.Parse(collection["RegId"]),
                float.Parse(collection["Appowner"]),
                float.Parse(collection["Vendor"]),
                float.Parse(collection["Customer"]),
                float.Parse(collection["Subvendor"]),
                subscriptionperc.Endate,
                collection["Enuser"]

                );

                var response = await _subscriptionService.UpdateSubsPercent(subscription.Recid, subscription, cancellationToken);
                return Ok(response);
                
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }

        }


        // GET: Subscriptionpercs/Delete/5
        [HttpGet("DeleteSubsPerc")]
        public async Task<ActionResult<SubscriptionResource>> DeleteSubsPerc(int Recid, CancellationToken cancellationToken)
        {
             try
                
                {
             
                var response = await _subscriptionService.DeleteSubsPercent(Recid, cancellationToken);
                    return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }


        }



    }
}