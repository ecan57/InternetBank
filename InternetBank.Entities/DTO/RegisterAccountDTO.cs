using System;
using System.ComponentModel.DataAnnotations;

namespace InternetBank.Entities.DTO
{
    public class RegisterAccountDTO
    {
        public int Id { get; set; }


        [Required, Display(Name = "T.C. Kimlik Numarası ")]
        [RegularExpression(@"^[0-9]{11}$")]
        public string TCNo { get; set; }


        [Required, Display(Name = "Ad ")]
        public string Name { get; set; }


        [Required, Display(Name = "Soyad ")]
        public string Surname { get; set; }

        [Display(Name = "Telefon Numarası ")]
        public string PhoneNumber { get; set; }


        [Required, MinLength(1), DataType(DataType.EmailAddress), EmailAddress, MaxLength(50), Display(Name = "E-Posta ")]
        public string Email { get; set; }


        [Required, Display(Name = "Şifre ")]
        [RegularExpression(@"^[0-9]{6}$", ErrorMessage = "Şifre 6 haneli sayıdan oluşmalıdır.")]
        public string Password { get; set; }


        [Required, Display(Name = "Şifre ")]
        [Compare("Password", ErrorMessage = "Şifreler eşleşmiyor.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Oluşturma Tarihi ")]
        public DateTime DateCreated { get; set; }
        [Display(Name = "Son Güncelleme Tarihi ")]
        public DateTime DateLastUpdated { get; set; }


        //public static bool EmailControl(string email)
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
