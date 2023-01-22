using System.ComponentModel.DataAnnotations;

namespace InternetBank.Core.Utities
{
    public class Authenticate
    {
        [Required]
        [Display(Name ="T.C. Kimlik Numarası ")]
        [RegularExpression(@"^[0-9]\d{11}$", ErrorMessage = "Lütfen 11 haneli T.C. Kimlik Numarasını giriniz.")]
        public string TCNo { get; set; }
        [Required]
        [Display(Name = "Şifre ")]
        [RegularExpression(@"^[0-9]\d{6}$", ErrorMessage = "Şifre 6 haneli sayıdan oluşmalıdır.")]
        public string Password { get; set; }
    }
}
