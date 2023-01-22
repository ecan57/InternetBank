using Bogus;
using InternetBank.Data.Abstract;
using InternetBank.Entities.Concrete;
using System.Collections.Generic;

namespace InternetBank.Data.Concrete.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        public IEnumerable<Account> GetAccounts()
        {
            var accountId = 0;
            var accountFaker = new Faker<Account>("tr")
                .RuleFor(a => a.Id, a => accountId++)
                .RuleFor(u => u.TCNo, u => u.Random.Long(00000000000, 99999999999).ToString())
                .RuleFor(u => u.Name, u => u.Person.FirstName)
                .RuleFor(u => u.Surname, u => u.Person.LastName)
                .RuleFor(u => u.PhoneNumber, u => u.Phone.PhoneNumber())
                .RuleFor(u => u.Email, u => u.Person.Email)
                .RuleFor(u => u.Password, u => u.Internet.Password(6, false, @"^[0-9]$"))
                .RuleFor(a => a.Iban, a => a.Finance.Iban())
                .RuleFor(a => a.AccountNumber, a => a.Finance.Account())
                .RuleFor(a => a.CurrentAccountBalance, a => a.Random.Decimal())
                .RuleFor(a => a.DateCreated, a => a.Date.Recent())
                .RuleFor(a => a.DateLastUpdated, a => a.Date.Recent());
            return accountFaker.Generate(10);
        }
    }
}
