//Added by Mohtashim 03-07-2023
using paisa2u.common.Resources;

namespace paisa2u.common.Services
{
    public interface ITransactionsService
    {
            Task<TransactionsResource> AddTransaction(TransactionsResource resource, CancellationToken cancellationToken);
            Task<IEnumerable<TransactionsResource>> GetTransaction(int Tranid, CancellationToken cancellationToken);
            Task<IEnumerable<TransactionsResource>> GetTransactions(CancellationToken cancellationToken);

            Task<IEnumerable<TransactionsResource>> GetTransactionsWithRegId(int RegId,CancellationToken cancellationToken);
            Task<IEnumerable<TransactionsResource>> UpdateTransaction(int Tranid, TransactionsResource resource, CancellationToken cancellationToken);
            Task<IEnumerable<TransactionsResource>> DeleteTransaction(TransactionsResource resource, CancellationToken cancellationToken);
            double GetWallet(int regId, CancellationToken cancellationToken);
    }
}
