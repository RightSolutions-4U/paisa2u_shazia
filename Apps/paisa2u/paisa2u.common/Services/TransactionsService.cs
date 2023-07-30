using paisa2u.common.Models;
using paisa2u.common.Resources;
using System.Configuration;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace paisa2u.common.Services
{
    public class TransactionsService : ITransactionsService
    {
        private readonly PaisaDbContext _context;

        public TransactionsService(PaisaDbContext context)
        {
            _context = context;
            
        }
        public async Task<TransactionsResource> AddTransaction(TransactionsResource resource, CancellationToken cancellationToken)
        {
            var transaction = new Transactins
            {
                RegId = resource.regId,
                Amount = resource.amount,
                Endate = resource.endate,
                Enuser = resource.enuser
            };
            
            await _context.Transactins.AddAsync(transaction, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return new TransactionsResource(
                transaction.Tranid,
                transaction.RegId,
                transaction.Amount,
                transaction.Endate,
                transaction.Enuser
                );
        }

        public Task<TransactionsResource> DeleteTransaction(TransactionsResource resource, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<TransactionsResource> GetTransaction(int Tranid, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<TransactionsResource> GetTransactions(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<TransactionsResource> GetTransactionsWithRegId(int RegId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<TransactionsResource> UpdateTransaction(int Tranid, TransactionsResource resource, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
