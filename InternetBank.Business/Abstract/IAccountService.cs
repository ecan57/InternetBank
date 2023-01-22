using InternetBank.Entities.Concrete;
using System.Collections.Generic;

namespace InternetBank.Business.Abstract
{
    public interface IAccountService
    {
        Account Create(Account account, string password, string confirmPassword);
        Account Authenticate(string tcNo, string password);
        void Update(Account account, string password = null);
        void Delete(int id);
        IEnumerable<Account> GetAllAccounts();
        Account GetById(int id);
        Account GetByTCNo(string tcNo);
        Account GetByAccountNumber(string accountNumber);
    }
}
