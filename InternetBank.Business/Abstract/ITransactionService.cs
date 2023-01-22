using InternetBank.Core.Utities;
using InternetBank.Entities.Concrete;
using System.Collections.Generic;

namespace InternetBank.Business.Abstract
{
    public interface ITransactionService
    {
        Response CreateNewTransaction(Transaction transaction);
        Response Deposit(string tcNo, string accountNumber, decimal amount, string password);
        Response Withdraw(string tcNo, string accountNumber, decimal amount, string password);
        Response FundsTransfer(string tcNo, string fromAccount, string toAccount, decimal amount, string password);
        IEnumerable<Transaction> GetAllTransactions();
    }
}
