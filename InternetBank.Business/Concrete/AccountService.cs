using InternetBank.Business.Abstract;
using InternetBank.Data.Concrete.Context;
using InternetBank.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace InternetBank.Business.Concrete
{
    public class AccountService : IAccountService
    {
        private readonly InternetBankDbContext _dbContext;
        public AccountService(InternetBankDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Account Authenticate(string tcNo, string password)
        {
            if (string.IsNullOrEmpty(tcNo) || string.IsNullOrEmpty(password))
                return null;

            var account = _dbContext.Accounts.SingleOrDefault(x => x.TCNo == tcNo);
            if (account == null)
                return null;

            var accountPassword = _dbContext.Accounts.SingleOrDefault(x => x.Password == password);
            if (VerifyPassword(password) == null)
                return null;

            //kimlik doğrulama başarılı
            return account;
        }
        private static string VerifyPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                    builder.Append(bytes[i].ToString("x2"));
                return builder.ToString();
            }
        }
        public Account Create(Account account, string password, string confirmPassword)
        {
            //doğrulama
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentNullException("Şifre boş geçilemez.");

            //bu tcno ya ait bir kullanıcı var mı
            if (_dbContext.Accounts.Any(x => x.TCNo == account.TCNo)) throw new ApplicationException("Bu T.C. Kimlik Numarasına ait bir hesap zaten var.");

            //şifreler eşleşiyor mu?
            if (!password.Equals(confirmPassword)) throw new ApplicationException("Şifreler eşleşmiyor.");

            CreatePassword(password);
            account.Password = password;

            _dbContext.Accounts.Add(account);
            _dbContext.SaveChanges();

            return account;
        }
        private static void CreatePassword(string password)
        {
            //şifre kontrolü
            if (string.IsNullOrEmpty(password)) throw new ArgumentNullException("Şifre");
            using (var hmac = new HMACSHA512())
            {
                byte[] bytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public void Delete(int id)
        {
            var account = _dbContext.Accounts.Find(id);
            if (account != null)
            {
                _dbContext.Accounts.Remove(account);
                _dbContext.SaveChanges();
            }
        }

        public IEnumerable<Account> GetAllAccounts()
        {
            return _dbContext.Accounts.ToList();
        }

        public Account GetById(int id)
        {
            var account = _dbContext.Accounts.Where(x => x.Id == id).FirstOrDefault();
            return account;
        }

        public void Update(Account account, string password = null)
        {
            // kullanıcı bul
            var accountUpdated = _dbContext.Accounts.Find(account.Id);
            if (accountUpdated == null) throw new ApplicationException("Hesap bulunamadı.");

            //eşleşme yapma
            if (!string.IsNullOrWhiteSpace(account.TCNo) && account.TCNo != accountUpdated.TCNo)
            {
                //tcno ile eşleşmediği için hata alma
                if (_dbContext.Accounts.Any(x => x.TCNo == account.TCNo)) throw new ApplicationException(account.TCNo + " kullanılmaktadır.");
                accountUpdated.TCNo = account.TCNo;
            }

            if (!string.IsNullOrWhiteSpace(password))
            {
                CreatePassword(password);
                accountUpdated.Password = password;
            }

            _dbContext.Accounts.Update(accountUpdated);
            _dbContext.SaveChanges();
        }

        public Account GetByTCNo(string tcNo)
        {
            var account = _dbContext.Accounts.Where(x => x.TCNo == tcNo).SingleOrDefault();
            if (account == null)
                return null;
            return account;
        }

        public Account GetByAccountNumber(string accountNumber)
        {
            var account = _dbContext.Accounts.Where(x => x.AccountNumber == accountNumber).SingleOrDefault();
            if (account == null)
                return null;
            return account;
        }
    }
}
