using Microsoft.Extensions.Configuration;
using MiniETicaretAPI.Application.Abstactions.Services;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace MiniETicaretAPI.Infrastructure.Services
{
    public class MailService : IMailService
    {
        private readonly IConfiguration _configuration;
        private string _mailUserName;
        private string _mailPassword;
        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
            _mailUserName = Environment.GetEnvironmentVariable(_configuration["Mail:UserName"]) ?? "";
            _mailPassword = Environment.GetEnvironmentVariable(_configuration["Mail:Password"]) ?? "";
        }

        public async Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
            await SendMailAsync(new[] { to }, subject, body, isBodyHtml);
        }

        public async Task SendMailAsync(string[] tos, string subject, string body, bool isBodyHtml = true)
        {
            MailMessage mail = new();
            mail.IsBodyHtml = isBodyHtml;
            foreach (var item in tos)
            {
                mail.To.Add(item);
            }
            mail.Subject = subject;
            mail.Body = body;
            mail.From = new MailAddress(_mailUserName, "Mini E-Ticaret", Encoding.UTF8);

            SmtpClient smtp = new();
            smtp.Credentials = new NetworkCredential(_mailUserName, _mailPassword);
            smtp.Port = Convert.ToInt32(_configuration["Mail:Port"]);
            smtp.Host = _configuration["Mail:Server"];
            await smtp.SendMailAsync(mail);
        }

        public async Task SendPasswordResetMailAsync(string to, string userId, string resetToken)
        {
            StringBuilder stringBuilder = new();
            stringBuilder.AppendLine("Merhaba <br> Eğer yeni şifre talebinde bulunduysanız aşağıda ki linkten şifrenizi yenileyebilirsiniz. <br><strong><a target=\"_blank\" href=\"");
            stringBuilder.AppendLine(_configuration["AngularClient:Url"]);
            stringBuilder.AppendLine("/update-password/");
            stringBuilder.AppendLine(userId);
            stringBuilder.AppendLine("/");
            stringBuilder.AppendLine(resetToken);
            stringBuilder.AppendLine("\">Şifre Yenileme Linki</a></strong>");
            stringBuilder.AppendLine("<br><br> Eğer yeni şifre talebinde bulunmadıysanız bu maili dikkate almayınız.");
            
            await SendMailAsync(to, "Şifre Yenileme", stringBuilder.ToString());            
        }
    }
}
