using System;

namespace InternetBank.Entities.Concrete
{
    public class Transaction
    {
        public int Id { get; set; }
        public Guid TransactionUniqueReference { get; set; }
        public decimal TransactionAmount { get; set; }
        public Status TransactionStatus { get; set; }
        public bool IsSuccessful 
        { 
            get => TransactionStatus.Equals(Status.Success); 
            set { } 
        }
        public string TransactionAccount { get; set; }
        public string TransactionTargetAccount { get; set; }
        public string TransactionDescriptions { get; set; }
        public Type TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }

    }
    public enum Status
    {
        Success,
        Failed,
        Error
    }

    public enum Type
    {
        Deposit,
        Withdraw,
        Transfer
    }
}
