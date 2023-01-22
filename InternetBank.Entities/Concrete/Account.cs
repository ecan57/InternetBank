using System;

namespace InternetBank.Entities.Concrete
{
    public class Account
    {
        public int Id { get; set; }
        public string TCNo { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Iban { get; set; }
        public string AccountNumber { get; set; }
        public decimal CurrentAccountBalance { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateLastUpdated { get; set; }

        public Account()
        {
            Random random = new Random();
            Iban = $"TR570000000000{Convert.ToString((long)Math.Floor(random.NextDouble() * 99_0000_0000_00L + 11_0000_0000_00L))}";
            AccountNumber = Iban.Substring(15, 11);
        }
    }
}
