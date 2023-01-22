using System;
using System.ComponentModel.DataAnnotations;
using Type = InternetBank.Entities.Concrete.Type;

namespace InternetBank.Entities.DTO
{
    public class TransactionRequestDTO
    {
        [Display(Name = "İşlem Miktarı ")]
        public decimal TransactionAmount { get; set; }
        [Display(Name = "İşlem Hesabı ")]
        public string TransactionAccount { get; set; }
        [Display(Name = "İşlem Hedef Hesabı ")]
        public string TransactionTargetAccount { get; set; }
        [Display(Name = "İşlem Tipi ")]
        public Type TransactionType { get; set; }
        [Display(Name = "İşlem Tarihi ")]
        public DateTime TransactionDate { get; set; }
    }
}
