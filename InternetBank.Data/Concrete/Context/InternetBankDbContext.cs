using InternetBank.Data.Concrete.Mapping;
using InternetBank.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace InternetBank.Data.Concrete.Context
{
    public class InternetBankDbContext : DbContext
    {
        public InternetBankDbContext(DbContextOptions<InternetBankDbContext> options) : base(options)
        {

        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        //public DbSet<GetAccountDTO> GetAccountsDTO { get; set; }
        //public DbSet<RegisterAccountDTO> RegisterAccountsDTO { get; set; }
        //public DbSet<UpdateAccountDTO> UpdateAccountsDTO { get; set; }
        //public DbSet<TransactionRequestDTO> TransactionsRequestDTO { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=ECAN; Database=InternetBankDB; User Id=sa; Password=57;");

            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AccountMap());
            modelBuilder.ApplyConfiguration(new TransactionMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
