using InternetBank.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InternetBank.Data.Concrete.Mapping
{
    public class AccountMap : IEntityTypeConfiguration<Account>
    {
        //Random random = new Random();
        //Account account = new Account();
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Accounts");

            builder.HasKey(key => key.Id);
            builder.Property(key => key.Id).ValueGeneratedOnAdd();

            builder.HasIndex(i => i.TCNo).IsUnique();
            //builder.Property(p => p.TCNo).HasColumnType(@"(^[0-9]\d{11}$)").HasMaxLength(11).IsRequired();
            builder.Property(p => p.TCNo).HasMaxLength(11).IsRequired();

            builder.Property(p => p.Name).HasMaxLength(20).IsRequired();

            builder.Property(p => p.Surname).HasMaxLength(30).IsRequired();

            //builder.Property(p => p.PhoneNumber).HasColumnType(@"(^(5[0-9]\d{2})-([0-9]\d{3})-([0-9]\d{2})-([0-9]\d{2})$)").HasMaxLength(10);
            builder.Property(p => p.PhoneNumber).HasMaxLength(10);

            //builder.Property(p => p.Email).HasColumnType(@"(^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$)").IsRequired(false);
            builder.Property(p => p.Email).HasMaxLength(100).IsRequired(false);

            //builder.Property(p => p.Password).HasColumnType(@"(^[0-9]\d{6}$)").HasMaxLength(6).IsRequired();
            builder.Property(p => p.Password).HasMaxLength(6).IsRequired();

            builder.HasIndex(p => p.Iban).IsUnique(true);
            //builder.Property(p => p.Iban).HasColumnType($"TR57 0000 0000 00{Convert.ToString((long)Math.Floor(random.NextDouble() * 99_0000_0000_00L + 11_0000_0000_00L))}");
            builder.Property(p => p.Iban).HasMaxLength(30);

            //builder.Property(p => p.AccountNumber).HasColumnType($"{account.Iban.Substring(15, 11)}");
            builder.Property(p => p.AccountNumber).HasMaxLength(11);

            builder.Property(p => p.CurrentAccountBalance).HasColumnType("decimal");

            builder.Property(p => p.DateCreated).HasColumnType("datetime2").IsRequired();

            builder.Property(p => p.DateLastUpdated).HasColumnType("datetime2").IsRequired();
        }
        //public bool EmailControl(string email)
        //{
        //    try
        //    {
        //        var mail = new System.Net.Mail.MailAddress(email);
        //        return mail.Address == email;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}
    }
}
