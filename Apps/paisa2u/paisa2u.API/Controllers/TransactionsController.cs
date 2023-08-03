using Microsoft.AspNetCore.Mvc;
using paisa2u.common.Resources;
using paisa2u.common.Services;
using System.Text.Json;


namespace paisa2u.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionsService _transactionsService;
       
        
        public TransactionsController(ITransactionsService transactionsService)
        {
            _transactionsService = transactionsService;

        }
        // POST: api/Transactions
        [HttpPost("AddTransaction")]
        public async Task<ActionResult<TransactionsResource>> AddTransaction([FromBody] TransactionsResource resource,  CancellationToken cancellationToken)
        {
            try
            {
                var response = await _transactionsService.AddTransaction(resource, cancellationToken);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
        //Added by Shazia on Aug 1, 2023
        // GET: api/Transactions
        [HttpGet("GetTransactionsWithRegId")]
        public async Task<ActionResult<IEnumerable<TransactionsResource>>> GetTransactionsWithRegId(int RegId, CancellationToken cancellationToken)
            
        {
            var a = await _transactionsService.GetTransactionsWithRegId(RegId, cancellationToken);
            var jsonString = JsonSerializer.Serialize(a);
            return Ok(jsonString);

        }
        //Added by Shazia on Aug 1, 2023
        // GET: api/Transactions
        [HttpGet("GetTransactions")]
        public async Task<ActionResult<IEnumerable<TransactionsResource>>> GetTransactions(CancellationToken cancellationToken)

        {
            var a = await _transactionsService.GetTransactions(cancellationToken);
            var jsonString = JsonSerializer.Serialize(a);
            return Ok(jsonString);

        }
        //Added by Shazia on Aug 1, 2023
        // GET: api/Transactions
        [HttpGet("GetTransaction")]
        public async Task<ActionResult<IEnumerable<TransactionsResource>>> GetTransaction(int Tranid, CancellationToken cancellationToken)

        {
            var a = await _transactionsService.GetTransaction(Tranid, cancellationToken);
            var jsonString = JsonSerializer.Serialize(a);
            return Ok(jsonString);
           

        }
        //Added by Shazia on Aug 2, 2023
        // GET: api/Transactions
        [HttpGet("GetWallet")]
        public ActionResult GetWallet(int RegId, CancellationToken cancellationToken)

        {
            var a = _transactionsService.GetWallet(RegId, cancellationToken);
            var jsonString = JsonSerializer.Serialize(a);
            return Ok(jsonString);
            
        }
    }
}
