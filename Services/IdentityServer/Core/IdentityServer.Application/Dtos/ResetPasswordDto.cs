using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Application.Dtos
{
    public class ResetPasswordDto
    {
        public string Email { get; set; }
        public string Token { get; set; }

        [Required(ErrorMessage = "Yeni şifre zorunludur.")]
        [MinLength(6, ErrorMessage = "Şifre en az 6 karakter olmalıdır.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Şifre tekrarı zorunludur.")]
        [Compare("Password", ErrorMessage = "Şifreler birbiriyle eşleşmiyor.")]
        public string ConfirmPassword { get; set; }
    }
}