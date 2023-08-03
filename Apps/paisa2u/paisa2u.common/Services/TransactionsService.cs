using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using paisa2u.common.Models;
using paisa2u.common.Resources;
using System.Configuration;
using System.Threading;
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
            var transaction = new transactions
            {
                RegId = resource.regId,
                Amount = resource.amount,
                Endate = resource.endate,
                Enuser = resource.enuser,
                Drcrid = resource.drcrid
            };
            
            await _context.Transactins.AddAsync(transaction, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return new TransactionsResource(
                transaction.Tranid,
                transaction.RegId,
                (float)transaction.Amount,
                transaction.Endate,
                transaction.Enuser,
                transaction.Drcrid
                );
        }

        public async Task<IEnumerable<TransactionsResource>> DeleteTransaction(TransactionsResource resource, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TransactionsResource>> GetTransaction(int Tranid, CancellationToken cancellationToken)
        {
            List<TransactionsResource> transList = new List<TransactionsResource>();
            var trans = await _context.Transactins.Where(x => x.Tranid == Tranid)
                .ToListAsync(cancellationToken);
            foreach (transactions transaction in trans)
            {
                transList.Add
                (
                new TransactionsResource(
                    transaction.Tranid,
                    transaction.RegId,
                    (float)transaction.Amount,
                    transaction.Endate,
                    transaction.Enuser,
                    transaction.Drcrid
                   )
                );
            }
            return transList;
        }

        public double GetWallet(int RegId, CancellationToken cancellationToken)
        {
            double trans =  _context.Transactins.Where(x => x.RegId == RegId).Sum(bal => bal.Amount);
            return trans;
        }

        public async Task<IEnumerable<TransactionsResource>> GetTransactions(CancellationToken cancellationToken)
        {
            List<TransactionsResource> transList = new List<TransactionsResource>();
            var trans = await _context.Transactins.ToListAsync(cancellationToken);
            foreach (transactions transaction in trans)
            {
                transList.Add
                (
                new TransactionsResource(
                    transaction.Tranid,
                    transaction.RegId,
                    (float)transaction.Amount,
                    transaction.Endate,
                    transaction.Enuser,
                    transaction.Drcrid
                   )
                );
            }
            return transList;
        }
        //GetTransactionsWithRegId
        public async Task<IEnumerable<TransactionsResource>> GetTransactionsWithRegId(int RegId, CancellationToken cancellationToken)
        {
            List<TransactionsResource> transList = new List<TransactionsResource>();
            var trans = await _context.Transactins.Where(x => x.RegId == RegId)
                .ToListAsync(cancellationToken);
            foreach (transactions transaction in trans)
            {
                transList.Add
                (
                new TransactionsResource(
                    transaction.Tranid,
                    transaction.RegId,
                    (float)transaction.Amount,
                    transaction.Endate,
                    transaction.Enuser,
                    transaction.Drcrid
                   )
                );
            }
            return transList;
        }

        public Task<TransactionsResource> UpdateTransaction(int Tranid, TransactionsResource resource, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<TransactionsResource>> ITransactionsService.UpdateTransaction(int Tranid, TransactionsResource resource, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
