using System;
using System.ComponentModel.DataAnnotations;

namespace InternetBank.Entities.DTO
{
    public class GetAccountDTO
    {
        public int Id { get; set; }
        [Display(Name="Ad ")]
        public string Name { get; set; }
        [Display(Name = "Soyad ")]
        public string Surname { get; set; }
        [Display(Name = "Telefon Numarası ")]
        public string PhoneNumber { get; set; }
        [Display(Name = "E-Posta ")]
        public string Email { get; set; }
        [Display(Name = "Iban ")]
        public string Iban { get; set; }
        [Display(Name = "Cari Hesap Bakiyesi ")]
        public decimal CurrentAccountBalance { get; set; }
        [Display(Name = "Oluşturma Tarihi ")]
        public DateTime DateCreated { get; set; }
        [Display(Name = "Son Güncelleme Tarihi ")]
        public DateTime DateLastUpdated { get; set; }
    }
}
