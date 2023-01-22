using Bogus;
using InternetBank.Data.Abstract;
using InternetBank.Entities.Concrete;
using System.Collections.Generic;
using Type = InternetBank.Entities.Concrete.Type;

namespace InternetBank.Data.Concrete.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        public IEnumerable<Transaction> GetTransactions()
        {
            var transactionFaker = new Faker<Transaction>("tr")
                .RuleFor(t => t.Id, t => t.IndexVariable++)
                .RuleFor(t => t.TransactionUniqueReference, t => t.Random.Guid())
                .RuleFor(t => t.TransactionAmount, t => t.Finance.Amount())
                .RuleFor(t => t.TransactionStatus, t => t.Finance.Random.Enum<Status>())
                .RuleFor(t => t.IsSuccessful, t => t.Random.Bool())
                .RuleFor(t => t.TransactionAccount, t => t.Finance.Account())
                .RuleFor(t => t.TransactionTargetAccount, t => t.Finance.Account())
                .RuleFor(t => t.TransactionDescriptions, t => t.Lorem.Text())
                .RuleFor(t => t.TransactionType, t => t.Finance.Random.Enum<Type>())
                .RuleFor(t => t.TransactionDate, t => t.Date.Recent());
            return new Transaction[0];
        }
    }
}
