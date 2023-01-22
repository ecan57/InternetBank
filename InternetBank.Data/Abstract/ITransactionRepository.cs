using InternetBank.Entities.Concrete;
using System.Collections.Generic;

namespace InternetBank.Data.Abstract
{
    public interface ITransactionRepository
    {
        IEnumerable<Transaction> GetTransactions();
    }
}
