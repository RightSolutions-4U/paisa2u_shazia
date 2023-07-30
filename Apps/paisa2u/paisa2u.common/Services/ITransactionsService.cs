//Added by Mohtashim 03-07-2023
using paisa2u.common.Resources;

namespace paisa2u.common.Services
{
    public interface ITransactionsService
    {
            Task<TransactionsResource> AddTransaction(TransactionsResource resource, CancellationToken cancellationToken);
            Task<TransactionsResource> GetTransaction(int Tranid, CancellationToken cancellationToken);
            Task<TransactionsResource> GetTransactions(CancellationToken cancellationToken);

            Task<TransactionsResource> GetTransactionsWithRegId(int RegId,CancellationToken cancellationToken);
            Task<TransactionsResource> UpdateTransaction(int Tranid, TransactionsResource resource, CancellationToken cancellationToken);
            Task<TransactionsResource> DeleteTransaction(TransactionsResource resource, CancellationToken cancellationToken);
   
    }
}
