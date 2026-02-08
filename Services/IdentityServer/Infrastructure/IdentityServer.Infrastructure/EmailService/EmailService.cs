using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;



namespace IdentityServer.Infrastructure.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendResetPasswordEmail(string resetEmailLink, string toEmail)
        {
            var host = _configuration["EmailSettings:Host"];
            var port = int.Parse(_configuration["EmailSettings:Port"]);
            var email = _configuration["EmailSettings:Email"];
            var password = _configuration["EmailSettings:Password"];

            //smtp servisi oluşturmamız gerekiyor
            var smtpClient = new SmtpClient(host,port);
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(email, password);
            smtpClient.EnableSsl = true;

            //şimdi mesajımızı gönderebiliriz
            var mailMessage = new MailMessage
            {
                From = new MailAddress(email, "Benim Uygulamam"),
                Subject = "Şifre Sıfırlama",
                Body = $@"
                <h4>Şifrenizi yenilemek için aşağıdaki linke tıklayın</h4>
                <p><a href='{resetEmailLink}'>Şifreyi Yenile</a></p>",
                IsBodyHtml = true
            };

            mailMessage.To.Add(toEmail);

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
