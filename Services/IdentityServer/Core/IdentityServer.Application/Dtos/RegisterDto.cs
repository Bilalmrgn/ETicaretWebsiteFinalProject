using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Application.Dtos
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Ad alanı zorunludur.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Soyad alanı zorunludur.")]
        public string? Surname { get; set; }

        [Required(ErrorMessage = "Kullanıcı adı zorunludur.")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "E-posta alanı zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Şifre alanı zorunludur.")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        public string? PhoneNumber { get; set; }
        [Required(ErrorMessage = "Şehir alanı zorunludur.")]
        public string? City { get; set; }

        [Required(ErrorMessage = "Şifre tekrarı zorunludur.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Şifreler uyuşmuyor.")]
        public string? ConfirmPassword { get; set; }
    }
}
