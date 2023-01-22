using InternetBank.Business.Abstract;
using InternetBank.Core.Utities;
using InternetBank.Data.Concrete.Context;
using InternetBank.Entities.Concrete;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Type = InternetBank.Entities.Concrete.Type;

namespace InternetBank.Business.Concrete
{
    public class TransactionService : ITransactionService
    {
        private InternetBankDbContext _dbContext;
        private ILogger<TransactionService> _logger;
        private IAccountService _accountService;

        Transaction transaction = new Transaction();
        Response response = new Response();

        Account account; //gönderen account
        Account targetAccount; //gönderilen account

        public TransactionService(InternetBankDbContext dbContext, ILogger<TransactionService> logger, IAccountService accountService)
        {
            _dbContext = dbContext;
            _logger = logger;
            _accountService = accountService;
        }
        public Response CreateNewTransaction(Transaction transaction)
        {
            try
            {
                _dbContext.Transactions.Add(transaction);
                _dbContext.SaveChanges();
                response.ResponseCode = "00";
                response.ResponseMessage = "İşlem oluşturma başarılı.";
            }
            catch (Exception ex)
            {
                _logger.LogError($"Bir hata oluştu => {ex.Message}");
            }
            return response;
        }

        public Response Deposit(string tcNo, string accountNumber, decimal amount, string password)
        {
            var authenticateAccount = _accountService.Authenticate(tcNo, password);
            if (authenticateAccount == null)
            {
                throw new ApplicationException("Geçersiz kimlik doğrulama ayrıntıları ");
            }
            //kullanıcı kimliği doğrulandıktan sonra işlem sağlanır
            try
            {
                account = _accountService.GetByAccountNumber(accountNumber);

                account.CurrentAccountBalance += amount; //seçilen hesabta deposit işlemi için girilen amount kadar bakiye artışı sağlanır

                if (_dbContext.Entry(account).State == Microsoft.EntityFrameworkCore.EntityState.Modified)
                {
                    //duruma göre sonuç getirir
                    transaction.TransactionStatus = Status.Success;
                    response.ResponseCode = "00";
                    response.ResponseMessage = "İşlem başarılı.";
                }
                else
                {
                    transaction.TransactionStatus = Status.Failed;
                    response.ResponseCode = "00";
                    response.ResponseMessage = "İşlem başarısız.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Bir hata oluştu => Mesaj: {ex.Message}");
            }

            transaction.TransactionDate = DateTime.Now;
            transaction.TransactionType = Type.Deposit;
            transaction.TransactionAmount = amount;
            transaction.TransactionAccount = accountNumber;
            transaction.TransactionDescriptions = $"Kaynak hesapta {JsonConvert.SerializeObject(transaction.TransactionAccount)} yeni işlem. {transaction.TransactionDate} İŞLEMTİPİ =>  {transaction.TransactionType} İŞLEMDURUMU => {transaction.TransactionStatus}";

            _dbContext.Transactions.Add(transaction);
            _dbContext.SaveChanges();

            return response;
        }

        public Response FundsTransfer(string tcNo, string fromAccount, string toAccount, decimal amount, string password)
        {
            var authenticateAccount = _accountService.Authenticate(tcNo, password);
            if (authenticateAccount == null)
            {
                throw new ApplicationException("Geçersiz kimlik bilgileri.");
            }

            try
            {
                account = _accountService.GetByAccountNumber(fromAccount);
                targetAccount = _accountService.GetByAccountNumber(toAccount);

                account.CurrentAccountBalance -= amount; //amount hesaptan düşer
                targetAccount.CurrentAccountBalance += amount; //amount targetaccount a eklenir

                if ((_dbContext.Entry(account).State == Microsoft.EntityFrameworkCore.EntityState.Modified) && (_dbContext.Entry(targetAccount).State == Microsoft.EntityFrameworkCore.EntityState.Modified))
                {
                    transaction.TransactionStatus = Status.Success;
                    response.ResponseCode = "00";
                    response.ResponseMessage = "İşlem başarılı.";
                }
                else
                {
                    transaction.TransactionStatus = Status.Failed;
                    response.ResponseCode = "00";
                    response.ResponseMessage = "İşlem başarısız.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Bir hata oluştu => {ex.Message}");
            }

            transaction.TransactionDate = DateTime.Now;
            transaction.TransactionType = Type.Transfer;
            transaction.TransactionAmount = amount;
            transaction.TransactionAccount = fromAccount;
            transaction.TransactionTargetAccount = toAccount;
            transaction.TransactionDescriptions = $"Kaynak hesaptan yeni işlem {JsonConvert.SerializeObject(transaction.TransactionAccount)} hedef => {JsonConvert.SerializeObject(transaction.TransactionTargetAccount)} hesaba {transaction.TransactionDate} İŞLEMTİPİ =>  {transaction.TransactionType} İŞLEMDURUMU => {transaction.TransactionStatus}";

            _dbContext.Transactions.Add(transaction);
            _dbContext.SaveChanges();

            return response;
        }

        public Response Withdraw(string tcNo, string accountNumber, decimal amount, string password)
        {
            var authenticateAccount = _accountService.Authenticate(tcNo, password);
            if (authenticateAccount == null)
            {
                throw new ApplicationException("Geçersiz kimlik doğrulama ayrıntıları ");
            }

            try
            {
                account = _accountService.GetByAccountNumber(accountNumber);

                account.CurrentAccountBalance -= amount;

                if (_dbContext.Entry(account).State == Microsoft.EntityFrameworkCore.EntityState.Modified)
                {
                    transaction.TransactionStatus = Status.Success;
                    response.ResponseCode = "00";
                    response.ResponseMessage = "İşlem başarılı.";
                }
                else
                {
                    transaction.TransactionStatus = Status.Failed;
                    response.ResponseCode = "00";
                    response.ResponseMessage = "İşlem başarısız.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Bir hata oluştu => {ex.Message}");
            }

            transaction.TransactionDate = DateTime.Now;
            transaction.TransactionType = Type.Withdraw;
            transaction.TransactionAmount = amount;
            transaction.TransactionAccount = accountNumber;
            transaction.TransactionDescriptions = $"Kaynak hesapta {JsonConvert.SerializeObject(transaction.TransactionAccount)} yeni işlem. {transaction.TransactionDate} İŞLEMTİPİ =>  {transaction.TransactionType} İŞLEMDURUMU => {transaction.TransactionStatus}";

            _dbContext.Transactions.Add(transaction);
            _dbContext.SaveChanges();

            return response;
        }

        public IEnumerable<Transaction> GetAllTransactions()
        {
            return _dbContext.Transactions.ToList();
        }
    }
}
