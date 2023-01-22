using InternetBank.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using Type = InternetBank.Entities.Concrete.Type;

namespace InternetBank.Data.Concrete.Mapping
{
    public class TransactionMap : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transactions");

            builder.HasKey(key => key.Id);
            builder.Property(key => key.Id).ValueGeneratedOnAdd();

            builder.Property(p => p.TransactionUniqueReference).IsUnicode().HasConversion(t => t.ToString().Replace("-", "").Substring(1, 9), t => Guid.NewGuid()).IsRequired();
            //sorun: veritabanında guid üretimi olmuyor. Hepsi 0'lardan oluşuyor.

            builder.Property(p => p.TransactionAmount).HasColumnType("decimal").IsRequired();

            builder.Property(p => p.TransactionStatus).HasConversion(s => s.ToString(), s => (Status)Enum.Parse(typeof(Status), s)).IsRequired();

            //builder.Property(p => p.IsSuccessful).HasConversion(s => s.ToString(), s => (, s)).IsRequired();
            builder.Property(p => p.IsSuccessful).IsRequired();

            builder.Property(p => p.TransactionAccount).IsRequired();

            builder.Property(p => p.TransactionTargetAccount).IsRequired(false);

            builder.Property(p => p.TransactionDescriptions).IsRequired(false);

            builder.Property(p => p.TransactionType).HasConversion(t => t.ToString(), t => (Type)Enum.Parse(typeof(Type), t)).IsRequired();

            builder.Property(p => p.TransactionDate).HasColumnType("datetime2").IsRequired();
        }
    }
}
