using System.ComponentModel.DataAnnotations;

namespace InternetBank.Entities.DTO
{
    public class UpdateAccountDTO
    {
        [Display(Name = "Ad ")]
        public string Name { get; set; }
        [Display(Name = "Soyad ")]
        public string Surname { get; set; }
        [Display(Name = "Telefon Numarası ")]
        [RegularExpression(@"^(5(\d{2})-(\d{3})-(\d{2})-(\d{2}))$")]
        public string PhoneNumber { get; set; }
        [Display(Name = "E-Posta ")]
        public string Email { get; set; }
        [Display(Name = "Şifre ")]
        [RegularExpression(@"^[0-9]{6}$/", ErrorMessage = "Şifre 6 haneli sayıdan oluşmalıdır.")]
        public string Password { get; set; }

    }
}
