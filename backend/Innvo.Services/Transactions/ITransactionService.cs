using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Innvo.Models.Transaction;

namespace Innvo.Services.Transaction
{
    public interface ITransactionService
    {
        public Task<List<TransactionListItem>> GetAll();
        public Task<TransactionDetail>? GetOne(int id);

    }
}