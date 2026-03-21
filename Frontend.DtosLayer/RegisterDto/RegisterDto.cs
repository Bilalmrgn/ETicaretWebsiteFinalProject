using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.DtosLayer.RegisterDto
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Ad alanı boş geçilemez")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Soyad alanı boş geçilemez")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Kullanıcı adı gereklidir")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email adresi zorunludur")]
        [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz")]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }
        public string City { get; set; }

        [Required(ErrorMessage = "Şifre zorunludur")]
        [MinLength(6, ErrorMessage = "Şifre en az 6 karakter olmalıdır")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Şifreler birbiriyle uyuşmuyor")]
        public string ConfirmPassword { get; set; }
    }
}
