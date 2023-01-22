using InternetBank.Entities.Concrete;
using System.Collections.Generic;

namespace InternetBank.Data.Abstract
{
    public interface IAccountRepository
    {
        IEnumerable<Account> GetAccounts();
    }
}
